using MathNet.Numerics.LinearAlgebra.Double;
using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.CORE
{
     public class Criteria
    {
         private readonly Dictionary<string, double[]> FiveLevelCriteria;
         private readonly double[] GradeCriteria;
         private readonly DenseVector GradeVector;
         public Criteria()
         {
             FiveLevelCriteria = new Dictionary<string, double[]>//todo:如何确定临界值
         {
             {"Settlement",new double[12]{double.MinValue,1,2,3,4,5,6,7,8,9,10,double.MaxValue}},
             {"Convergence",new double[12]{double.MinValue,2,4,6,8,10,12,14,16,18,20,double.MaxValue}},
             {"SoilPressure",new double[12]{double.MinValue,1,2,3,4,5,6,7,8,9,10,double.MaxValue}},
             {"ConcreteStress",new double[12]{double.MinValue,1,2,3,4,5,6,7,8,9,10,double.MaxValue}},
             {"SteelStress",new double[12]{double.MinValue,1,2,3,4,5,6,7,8,9,10,double.MaxValue}},
             {"SteelCorrosion",new double[12]{double.MinValue,1,2,3,4,5,6,7,8,9,10,double.MaxValue}}
         };
             GradeCriteria = new double[] { 100, 87.5, 75, 62.5, 50, 37.5, 25, 12.5, 0, 0 };
             GradeVector = new DenseVector(new double[5]{100, 75, 50, 25, 0 }) ;
         }
         public  DenseVector CalculateFuzzyVector(string index,double originValue)
         {
             MemberShipFun membershipFun = new MemberShipFun();
             membershipFun.ValueDivision = FiveLevelCriteria[index];
             var gradeFuzzyVector= membershipFun.TrapezoiMebership(originValue);
             return gradeFuzzyVector;
         }
         public  double CalculateStandardGrade(string index,double originValue)
         {
             //var indexCriteria = FiveLevelCriteria[index];
             var fuzzyVector = CalculateFuzzyVector(index, originValue);
             return fuzzyVector.DotProduct(GradeVector);//todo:计算方法错误，怎么得到百分制？如何标准化？
         }
    }
}
