using MathNet.Numerics.LinearAlgebra.Double;
using ShieldTunnelHealthEvaluation.CORE;
using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using unvell.ReoGrid;

namespace ShieldTunnelHealthEvaluation.UI
{
    /// <summary>
    /// JudgementMatrixUC.xaml 的交互逻辑
    /// </summary>
    public partial class LookUpJudgementMatrixWnd : Window
    {
        AllExpertJudgementMatrixs _judgementMatrixInfosSet;//所有有关信息
        JudgementMatrixsGroup judgemetnMatrixInfos;
        Dictionary<string, SingleBasicJudgementMatrixInfo> judgeMatrixDic;
        int matrixNo;
        int matrixTotalNo;
        public List<string> MatrixGrade { get; set; }
        public List<string> Sequences{get;set;}
        DenseMatrix weighMatrix;
        bool isFirstAfterContextChanged = true;
        public  Dictionary<string,string> ExpertDateDic { get; set; }
        public LookUpJudgementMatrixWnd(AHPIndexHierarchy ahpIndexHierarchy)
        {
            MatrixGrade = new List<string> { "1/2", "1", "2"};
            InitializeComponent();
            _judgementMatrixInfosSet = BinaryIO.ReadMatrixInfosSet();//读取已有的，这个地方需要细化
            if(_judgementMatrixInfosSet==null)
            {
                _judgementMatrixInfosSet = new AllExpertJudgementMatrixs();
            }
            this.DataContext = _judgementMatrixInfosSet.JudgementMatrixInfosList;
            judgeMatrixDic = _judgementMatrixInfosSet.JudgementMatrixInfosList[0].JudgeMatrixDic;
            matrixNo = 0;
            matrixTotalNo = judgeMatrixDic.Count;
            SingleBasicJudgementMatrixInfo judgemtInfo = judgeMatrixDic.ElementAt(matrixNo).Value;
            RefreshData(judgemtInfo.IndexsSequence, judgemtInfo.JudgementMatrix);
            dgWeight.CellEditEnding += dgWeight_CellEditEnding;
        }
        private void InitialExpertInfos()
        {
            foreach(var matrixGroup in this._judgementMatrixInfosSet.JudgementMatrixInfosList)
            {
                //暂时不考虑专家重名，重名则自己写上单位信息
               this.ExpertDateDic.Add( matrixGroup.ExpertName,matrixGroup.Time);
            }
        }
        public void RefreshData(List<string> sequences, DenseMatrix weighMatrix)
        {
            Debug.Assert(sequences != null && sequences.Count > 0);
            dgWeight.ItemsSource = null;
            this.Sequences = sequences;
            if (weighMatrix != null)
            {
                this.weighMatrix = weighMatrix;
            }
            else
            {
                this.weighMatrix = new DenseMatrix(sequences.Count);
            }
            initialData();
        }
        private void initialData()
        {
            DataTable dependedDT=CreateDT(Sequences,weighMatrix);
            this.dgWeight.ItemsSource = dependedDT.DefaultView;
            isFirstAfterContextChanged = true;
        }
        private DataTable CreateDT(List<string> headers,DenseMatrix dm)
        {
            var resultDT = new DataTable();
            for(int i=0;i<headers.Count;i++)
            {
                resultDT.Columns.Add(new DataColumn(headers[i]));
            }
            for(int i=0;i<dm.RowCount;i++)
            {
                DataRow dr = resultDT.NewRow();
                for(int j=0;j<dm.ColumnCount;j++)
                {
                    if(i==j)//强制为1
                    {
                        dr[j] = 1;
                        continue;
                    }
                    else if (dm[i, j]==0)
                    {
                        dr[j] = 1;
                    }
                    else
                    {
                        string tempGrade;
                        if(ConvertDouble2MatrixGrade(dm[i,j],out tempGrade))
                        {
                            dr[j] = tempGrade;
                        }
                    }
                }
                resultDT.Rows.Add(dr);
            }
            return resultDT;
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            weighMatrix = new DenseMatrix(Sequences.Count);
            for (int i = 0; i < Sequences.Count; i++)
            {
                for (int j = 0; j < Sequences.Count; j++)
                {
                    var currentRow=dgWeight.Items[i] as DataRowView;
                    var cellContent = currentRow[j].ToString();
                    if (i == j)
                    {
                        weighMatrix[i, j] = 1;
                    }
                    double cellNumber;
                    if (ConvertMatrixGrade2Double(cellContent, out cellNumber))
                    {
                        weighMatrix[i, j] = cellNumber;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            judgeMatrixDic.ElementAt(matrixNo).Value.JudgementMatrix = weighMatrix;
            judgeMatrixDic.ElementAt(matrixNo).Value.CalculateEigenVector();
            if (!judgeMatrixDic.ElementAt(matrixNo).Value.CheckConsistency() || !judgeMatrixDic.ElementAt(matrixNo).Value.IsJudgementMatrix())
            {
                MessageBox.Show("未通过一致性检验！");
                return;
            }
            matrixNo++;
            SingleBasicJudgementMatrixInfo judgemtInfo = judgeMatrixDic.ElementAt(matrixNo).Value;
            NextMatrix:
            if (matrixNo < matrixTotalNo)
            {
                if (judgemtInfo.IndexsSequence.Count== 1) //一个元素的，跳过，并赋值
                {
                    judgeMatrixDic.ElementAt(matrixNo).Value.JudgementMatrix = new DenseMatrix(1,1,new double[]{1});
                    judgeMatrixDic.ElementAt(matrixNo).Value.CalculateEigenVector();
                    matrixNo++;//接下来继续
                    goto NextMatrix;
                }
            }
            //不是仅一个元素,且是最后一个
            if (matrixNo == matrixTotalNo - 1)//如果是最后一个
            {
                this.btnOK.Content = "完成";
                judgemtInfo = judgeMatrixDic.ElementAt(matrixNo).Value;
                RefreshData(judgemtInfo.IndexsSequence, judgemtInfo.JudgementMatrix);
                return;
            }
            //没了
            if (matrixNo >= matrixTotalNo)//如果没了
            {
                _judgementMatrixInfosSet.JudgementMatrixInfosList.Add(judgemetnMatrixInfos);
                BinaryIO.OutputMatrixInfosSet(_judgementMatrixInfosSet);
                this.Close();
                return;
            }
            //还没结束，也不是最后一个，也不是一个元素
            judgemtInfo = judgeMatrixDic.ElementAt(matrixNo).Value;
            RefreshData(judgemtInfo.IndexsSequence, judgemtInfo.JudgementMatrix);
        }

        private void dgWeight_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = this.Sequences[e.Row.GetIndex()];
            e.Row.HorizontalContentAlignment = HorizontalAlignment.Center;
            e.Row.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void dgWeight_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridComboBoxColumn comboBoxColumn = new DataGridComboBoxColumn();
            comboBoxColumn.Header = e.PropertyName;

            Binding binding = new Binding();
            // 需要绑定的列
            binding.Path = new PropertyPath(e.PropertyName);
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            comboBoxColumn.SelectedValueBinding = binding;
            Binding itemsSourceBinding = new Binding();
            itemsSourceBinding.Source = this;
            itemsSourceBinding.Path = new PropertyPath("MatrixGrade");
            BindingOperations.SetBinding(comboBoxColumn, DataGridComboBoxColumn.ItemsSourceProperty, itemsSourceBinding);
            //居中样式
            Style styleCenter = new Style(typeof(ComboBox));
            Setter setHorizonalCenter = new Setter(ComboBox.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
            Setter setVerticalCenter = new Setter(ComboBox.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            styleCenter.Setters.Add(setHorizonalCenter);
            styleCenter.Setters.Add(setVerticalCenter);
            comboBoxColumn.ElementStyle = styleCenter;
            comboBoxColumn.EditingElementStyle = styleCenter;
            e.Column = comboBoxColumn;

           
        }
        private void dgWeight_CellEditEnding(object sender,DataGridCellEditEndingEventArgs e)
        {
            var originRowHeader = e.Row.Header.ToString();
            var originColHeader = e.Column.Header.ToString();
            int originRowIndex = this.Sequences.IndexOf(originRowHeader);
            int originColIndex = this.Sequences.IndexOf(originColHeader);
            var originCell = this.dgWeight.GetCell(originRowIndex, originColIndex);
            var originComBox = (ComboBox)originCell.Content;
            var originSeletectedIndex = originComBox.SelectedIndex;
            var toModifyCell=this.dgWeight.GetCell(originColIndex, originRowIndex);
            var toModifyCmBox=(ComboBox) toModifyCell.Content;
            toModifyCmBox.SelectedIndex = this.MatrixGrade.Count-1-originSeletectedIndex;
        }
        private void SetDiagonalUnediteable()
        {
            for(int i=0;i<this.Sequences.Count;i++)
            {
                var diagonalCell=this.dgWeight.GetCell(i, i);
                diagonalCell.IsEnabled = false;
            }
        }

        private void dgWeight_LayoutUpdated(object sender, EventArgs e)
        {
            if(isFirstAfterContextChanged)
            {
                SetDiagonalUnediteable();
                isFirstAfterContextChanged = false;
            }
        }

        private bool ConvertMatrixGrade2Double(string grade,out double result)
        {
            if(grade.Contains('/'))
            {
                string[] splitedStrings = grade.Split(new char[] { '/' });
                double first, second;
                double.TryParse(splitedStrings[0],out first);
                double.TryParse(splitedStrings[1], out second);
                result=first/second;
                return true;
            }
            else
            {
                if(double.TryParse(grade,out result))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("判断矩阵需要输入数字");
                    return false;
                }
            }
        }
        private bool ConvertDouble2MatrixGrade(double matrixNumber,out string grade)
        {
            const double toleratedDeviation=0.001;
            foreach(var gradeItem in MatrixGrade)
            {
                double gradeNumber;
                if(ConvertMatrixGrade2Double(gradeItem,out gradeNumber))
                {
                    if(Math.Abs(gradeNumber-matrixNumber)<toleratedDeviation)
                    {
                        grade = gradeItem;
                        return true;
                    }
                }
            }
            grade = "error";
            return false;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
