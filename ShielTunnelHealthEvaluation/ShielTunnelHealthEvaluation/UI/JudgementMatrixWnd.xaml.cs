using MathNet.Numerics.LinearAlgebra.Double;
using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using System;
using System.Collections.Generic;
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
        JudgementMatrixInfosSet _judgementMatrixInfosSet;
        JudgementMatrixInfos judgemetnMatrixInfos;
        Dictionary<string, JudgementMatrixInfo> judgeMatrixDic;
        int matrixNo;
        int matrixTotalNo;
        List<string> sequences;
        DenseMatrix weighMatrix;
        public JudgementMatrixWnd(AHPIndexHierarchy ahpIndexHierarchy)
        {
            _judgementMatrixInfosSet = new JudgementMatrixInfosSet();
            judgemetnMatrixInfos = new JudgementMatrixInfos(ahpIndexHierarchy);
            judgeMatrixDic = judgemetnMatrixInfos.JudgeMatrixDic;
            InitializeComponent();
            matrixNo = 0;
            matrixTotalNo = judgeMatrixDic.Count;
            JudgementMatrixInfo judgemtInfo = judgeMatrixDic.ElementAt(matrixNo).Value;
            RefreshData(judgemtInfo.IndexsSequence, judgemtInfo.JudgementMatrix);
        }
        public void RefreshData(List<string> sequences, DenseMatrix weighMatrix)
        {
            Debug.Assert(sequences != null && sequences.Count > 0);
            this.sequences = sequences;
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
            Worksheet mySheet = this.reoWeigh.CurrentWorksheet;
            mySheet.SetRows(sequences.Count);
            mySheet.SetCols(sequences.Count);
            for (int i = 0; i < sequences.Count; i++)
            {
                mySheet.RowHeaders[i].Text = sequences[i];
                mySheet.ColumnHeaders[i].Text = sequences[i];
                if (weighMatrix != null)
                {
                    for (int j = 0; j < sequences.Count; j++)
                    {
                        mySheet[i, j] = weighMatrix[i, j];
                    }
                }
            }

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            weighMatrix = new DenseMatrix(sequences.Count);
            Worksheet mySheet = this.reoWeigh.CurrentWorksheet;
            for (int i = 0; i < sequences.Count; i++)
            {
                for (int j = 0; j < sequences.Count; j++)
                {
                    weighMatrix[i, j] = double.Parse(mySheet[i, j].ToString());
                }
            }
            judgeMatrixDic.ElementAt(matrixNo).Value.JudgementMatrix = weighMatrix;
            judgeMatrixDic.ElementAt(matrixNo).Value.CalculateEigenVector();
            if (!judgeMatrixDic.ElementAt(matrixNo).Value.CheckConsistency())
            {
                return ;
            }
            matrixNo++;
            if (matrixNo < matrixTotalNo)
            {
                JudgementMatrixInfo judgemtInfo = judgeMatrixDic.ElementAt(matrixNo).Value;
                RefreshData(judgemtInfo.IndexsSequence, judgemtInfo.JudgementMatrix);
            }
            if(matrixNo==matrixTotalNo-1)
            {
                this.btnOK.Content = "完成";
            }
            if (matrixNo == matrixTotalNo )
            {
                _judgementMatrixInfosSet.JudgementMatrixInfosList.Add(judgemetnMatrixInfos);
                BinaryIO.OutputMatrixInfosSet(_judgementMatrixInfosSet);
                this.Close();
                return;
            }
        }
    }
}
