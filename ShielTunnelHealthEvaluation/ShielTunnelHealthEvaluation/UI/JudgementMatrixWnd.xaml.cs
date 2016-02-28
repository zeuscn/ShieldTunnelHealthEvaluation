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
    public partial class JudgementMatrixWnd : Window
    {
        AllExpertJudgementMatrixs _judgementMatrixInfosSet;
        JudgementMatrixsSetting judgemetnMatrixInfos;
        Dictionary<string, JudgementMatrixInfo> judgeMatrixDic;
        int matrixNo;
        int matrixTotalNo;
        public List<string> MatrixGrade { get; set; }
        public List<string> Sequences{get;set;}
        DenseMatrix weighMatrix;
        bool isFirstAfterContextChanged = true;
        public JudgementMatrixWnd(AHPIndexHierarchy ahpIndexHierarchy)
        {
            MatrixGrade = new List<string> { "1/2", "1", "2"};
            InitializeComponent();
            _judgementMatrixInfosSet = new AllExpertJudgementMatrixs();
            judgemetnMatrixInfos = new JudgementMatrixsSetting(ahpIndexHierarchy);
            judgeMatrixDic = judgemetnMatrixInfos.JudgeMatrixDic;
            matrixNo = 0;
            matrixTotalNo = judgeMatrixDic.Count;
            JudgementMatrixInfo judgemtInfo = judgeMatrixDic.ElementAt(matrixNo).Value;
            RefreshData(judgemtInfo.IndexsSequence, judgemtInfo.JudgementMatrix);
            dgWeight.CellEditEnding += dgWeight_CellEditEnding;
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
                    if(i==j)
                    {
                        dr[j] = 1;
                        continue;
                    }
                    if (dm[i, j]==0)
                    {
                        dr[j] = 1;
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
                    if (double.TryParse(cellContent,out cellNumber))
                    {
                        weighMatrix[i, j] = cellNumber;
                    }
                    else
                    {
                        MessageBox.Show("判断矩阵需要输入数字!");
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
            JudgementMatrixInfo judgemtInfo = judgeMatrixDic.ElementAt(matrixNo).Value;
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
            //不为一个元素,切实最后一个
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
            //Style styleCenter2 = new Style(typeof(ComboBox));
            //Setter setHorizonalCenter2 = new Setter(ComboBox., HorizontalAlignment.Center);
            //Setter setVerticalCenter2 = new Setter(ComboBox.VerticalAlignmentProperty, VerticalAlignment.Center);
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
    }
}
