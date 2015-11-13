using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Xml.Serialization;
using System.Data;
using System.Windows;

namespace ShieldTunnelHealthEvaluation.CORE.FuzzyAHP
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
            if(JudgementMatrix==null)
            {
                return result;
            }
            if (matrixDimension == 1 )
            {
                if(maxEigenValue==1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            double CI = (maxEigenValue - matrixDimension) / (matrixDimension - 1);
            double RI = RIs[matrixDimension];
           // double CR = CI / RI;
            if (CI<= 0.1*RI)
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
            if(WeightVector==null)
            {
                MessageBox.Show("判断矩阵错误，无法计算权重！");
            }
        }
    }
  

}
