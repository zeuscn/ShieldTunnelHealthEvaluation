using MathNet.Numerics.LinearAlgebra.Double;
using ShielTunnelHealthEvaluation.CORE.FuzzyAHP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using unvell.ReoGrid;
using mathNet = MathNet.Numerics;

namespace ShielTunnelHealthEvaluation.UI
{
    /// <summary>
    /// Weight.xaml 的交互逻辑
    /// </summary>
    public partial class WeightWnd : Window
    {
        JudgementMatrixInfos judgemetnMatrixInfos;
        Dictionary<string, JudgementMatrixInfo> judgeMatrixDic;
        int matrixNo;
        int matrixTotalNo;
        public WeightWnd(AHPIndexHierarchy ahpIndexHierarchy)
        {
            judgemetnMatrixInfos = new JudgementMatrixInfos(ahpIndexHierarchy);
            judgeMatrixDic = judgemetnMatrixInfos.JudgeMatrixDic;
            InitializeComponent();
            matrixNo = 0;
            matrixTotalNo = judgeMatrixDic.Count;
            JudgementMatrixInfo judgemtInfo=judgeMatrixDic.ElementAt(matrixNo).Value;
            this.judgeMatrixUC.RefreshData(judgemtInfo.IndexsSequence, judgemtInfo.JudgementMatrix);
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            matrixNo++;
            if(matrixNo<matrixTotalNo)
            {
                JudgementMatrixInfo judgemtInfo=judgeMatrixDic.ElementAt(matrixNo).Value;
                this.judgeMatrixUC.RefreshData(judgemtInfo.IndexsSequence, judgemtInfo.JudgementMatrix);
            }
        }
    }
}
