using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.CORE
{
    public static class EvaluatedGradeInfo
    {
        public static readonly List<string> EvaluatedFiveGrades = new List<string>{"好", "较好", "较差", "差", "危险"};
        private static readonly List<double> IndexGradeCriteria = new List<double> { 81.25, 56.25, 31.25, 6.25 ,0};
        public static string Memebership2Grade(DenseVector memebershipVector)
        {
            int maxIndex=memebershipVector.MaximumIndex();
            if(maxIndex>EvaluatedFiveGrades.Count-1||maxIndex<0)
            {
                throw (new Exception("隶属度向量数量大于评估指标分级数"));
            }
            return EvaluatedFiveGrades[maxIndex];
        }
        public static string Indexvalue2Grade(double indexValue)
        {
            for(int i=0;i<IndexGradeCriteria.Count;i++)
            {
                if(indexValue>=IndexGradeCriteria[i])
                {
                    return EvaluatedFiveGrades[i];
                }
            }
            return EvaluatedFiveGrades[EvaluatedFiveGrades.Count - 1];
        }
    }
}
