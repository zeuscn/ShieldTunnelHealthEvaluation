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
        public List<string> Sequences{get;set;}
        DenseMatrix weighMatrix;
        public JudgementMatrixWnd(AHPIndexHierarchy ahpIndexHierarchy)
        {
            InitializeComponent();
            _judgementMatrixInfosSet = new AllExpertJudgementMatrixs();
            judgemetnMatrixInfos = new JudgementMatrixsSetting(ahpIndexHierarchy);
            judgeMatrixDic = judgemetnMatrixInfos.JudgeMatrixDic;
            matrixNo = 0;
            matrixTotalNo = judgeMatrixDic.Count;
            JudgementMatrixInfo judgemtInfo = judgeMatrixDic.ElementAt(matrixNo).Value;
            RefreshData(judgemtInfo.IndexsSequence, judgemtInfo.JudgementMatrix);
            
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
            //this.dgWeight.DataContext = dependedDT;
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
                    }
                    dr[j] = dm[i, j];
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
                    var cellContent = currentRow[i].ToString();
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
            if (matrixNo < matrixTotalNo)
            {
                JudgementMatrixInfo judgemtInfo = judgeMatrixDic.ElementAt(matrixNo).Value;
                if (judgemtInfo.IndexsSequence.Count== 1) //一个元素的
                {
                    judgeMatrixDic.ElementAt(matrixNo).Value.JudgementMatrix = new DenseMatrix(1,1,new double[]{1});
                    judgeMatrixDic.ElementAt(matrixNo).Value.CalculateEigenVector();
                    matrixNo++;
                }
                RefreshData(judgemtInfo.IndexsSequence, judgemtInfo.JudgementMatrix);
            }
            if (matrixNo == matrixTotalNo - 1)
            {
                this.btnOK.Content = "完成";
            }
            if (matrixNo >= matrixTotalNo)
            {
                _judgementMatrixInfosSet.JudgementMatrixInfosList.Add(judgemetnMatrixInfos);
                BinaryIO.OutputMatrixInfosSet(_judgementMatrixInfosSet);
                this.Close();
                return;
            }
        }

        private void dgWeight_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = this.Sequences[e.Row.GetIndex()];
        }

        private void dgWeight_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridComboBoxColumn comboBoxColumn = new DataGridComboBoxColumn();
            comboBoxColumn.Header = e.PropertyName;
            // ComboBox 选择后的值
            comboBoxColumn.SelectedValuePath = e.PropertyName;
            // ComboBox 前台显示的值
            comboBoxColumn.DisplayMemberPath = e.PropertyName;

            Binding binding = new Binding();
            // 需要绑定的列
            binding.Path = new PropertyPath(e.PropertyName);
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            comboBoxColumn.SelectedValueBinding = binding;

            Binding itemsSourceBinding = new Binding();
            itemsSourceBinding.Source = new List<string>{"1/2","1","2","0"};
            BindingOperations.SetBinding(comboBoxColumn, DataGridComboBoxColumn.ItemsSourceProperty, itemsSourceBinding);

            e.Column = comboBoxColumn;
        }
    }
}
