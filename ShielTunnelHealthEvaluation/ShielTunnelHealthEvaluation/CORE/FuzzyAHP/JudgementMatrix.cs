using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Xml.Serialization;
using System.Data;

namespace ShielTunnelHealthEvaluation.CORE.FuzzyAHP
{
    [Serializable]
    public class JudgementMatrixInfo
    {
        public List<string> IndexsSequence { get; set; }
        public DenseMatrix JudgementMatrix { get; set; }
        private double[] RIs { get; set; }
        private double maxEigenValue;
        private int matrixDimension { get { return JudgementMatrix.ColumnCount; } }
        public DenseVector WeightVector
        {
            get;
            set;
        }
        public JudgementMatrixInfo()
        {
            RIs = new double[11] { 0, 0, 0.58, 0.9, 1.12, 1.24, 1.32, 1.41, 1.45, 1.49, 1.51 };
        }
        public bool CheckConsistency()
        {
            bool result = false;
            double CI = (maxEigenValue - matrixDimension) / (matrixDimension - 1);
            double RI = RIs[matrixDimension];
            double CR = CI / RI;
            if (CR < 0.1)
            {
                result = true;
            }
            return result;
        }
        public void CalculateEigenVector()
        {
            var evd = JudgementMatrix.Evd();
            var eigenVectors = evd.EigenVectors;
            var eigenValues = evd.EigenValues;
            DenseVector weight = new DenseVector(eigenValues.Count);
            for (int i = 0; i < eigenValues.Count;i++ )
            {
                weight[i] = eigenValues[i].Real;
            }
                weight = weight / (weight.Sum());
            WeightVector = weight;
            maxEigenValue = WeightVector.AbsoluteMaximum();
        }
    }
    [Serializable]
    public class JudgementMatrixInfos
    {
        public string ExpertName { get; set; }
        public DateTime Time { get; set; }
       [XmlIgnore]
        public Dictionary<string,JudgementMatrixInfo> JudgeMatrixDic { get; set; }
        public JudgementMatrixInfos()
        {
        }
        public JudgementMatrixInfos(AHPIndexHierarchy ahpIndexHierarchy)
        {
            JudgeMatrixDic = new Dictionary<string, JudgementMatrixInfo>();
            InitialData(ahpIndexHierarchy);
        }
        private void InitialData(AHPIndexHierarchy ahpIndexHierarchy)
        {
            if(ahpIndexHierarchy.Children==null||ahpIndexHierarchy.Children.Count<1)
            {
                return;
            }
            JudgementMatrixInfo judgeMatrixInfo = new JudgementMatrixInfo();
            judgeMatrixInfo.IndexsSequence = ahpIndexHierarchy.ChildrenNames;
            JudgeMatrixDic.Add(ahpIndexHierarchy.Name,judgeMatrixInfo);
            if(ahpIndexHierarchy.Children!=null&&ahpIndexHierarchy.Children.Count>0)
            {
                foreach(AHPIndexHierarchy item in ahpIndexHierarchy.Children)
                {
                    InitialData(item);
                }
            }
        }
    }
    [Serializable]
    public class JudgementMatrixInfosSet
    {
        public  List<JudgementMatrixInfos> JudgementMatrixInfosList { get; set; }
        public JudgementMatrixInfosSet()
        {
            this.JudgementMatrixInfosList = new List<JudgementMatrixInfos>();
        }
    }
}
