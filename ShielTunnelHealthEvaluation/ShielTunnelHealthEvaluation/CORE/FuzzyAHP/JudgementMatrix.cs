using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;

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
        private void CalculateEigenVector()
        {
            var evd = JudgementMatrix.Evd();
            var eigenVectors = evd.EigenVectors;
            var eigenValues = evd.EigenValues;
            int maxIndex = eigenValues.AbsoluteMaximumIndex();
            maxEigenValue = eigenValues.AbsoluteMaximum().Real;
            var weight = eigenVectors.Column(maxIndex);
            weight = weight / (weight.Sum());
            WeightVector = (DenseVector)weight;
        }
    }
    [Serializable]
    public class JudgementMatrixInfos
    {
        public string ExpertName { get; set; }
        public DateTime Time { get; set; }
        public List<JudgementMatrixInfo> JudgeMatrixSet { get; set; }
        public JudgementMatrixInfos()
        {
        }
        public JudgementMatrixInfos(AHPIndexHierarchy ahpIndexHierarchy)
        {

        }
        public void InitialData(AHPIndexHierarchy ahpIndexHierarchy)
        {
            JudgementMatrixInfo JudgeMatrixInfo = new JudgementMatrixInfo();
            JudgeMatrixInfo.IndexsSequence = ahpIndexHierarchy.ChildrenNames;
            JudgeMatrixSet.Add(JudgeMatrixInfo);
            if(ahpIndexHierarchy.Children!=null&&ahpIndexHierarchy.Children.Count>0)
            {
                foreach(AHPIndexHierarchy item in ahpIndexHierarchy.Children)
                {
                    InitialData(item);
                }
            }
        }
    }
}
