using MathNet.Numerics.LinearAlgebra.Double;
using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.CORE
{
     static class Criteria
    {
         private const Dictionary<string, double[]> FiveLevelCriteria = new Dictionary<string, double[]>
         {
             {"Settlement",new double[10]{0,1,2,3,4,5,6,7,8,9}},
             {"Convergence",new double[10]{0,1,2,3,4,5,6,7,8,9}},
             {"SoilPressure",new double[10]{0,1,2,3,4,5,6,7,8,9}},
             {"ConcreteStress",new double[10]{0,1,2,3,4,5,6,7,8,9}},
             {"SteelStress",new double[10]{0,1,2,3,4,5,6,7,8,9}},
             {"SteelCorrosion",new double[10]{0,1,2,3,4,5,6,7,8,9}}
         };
         private  const double[] GradeCriteria = new double[] {100,87.5,75,62.5,50,37.5,25,12.5,0,0 };
         public static DenseVector CalculateFuzzyVector(string index,double originValue)
         {
             MemberShipFun membershipFun = new MemberShipFun();
             membershipFun.ValueDivision = FiveLevelCriteria[index];
             var gradeFuzzyVector= membershipFun.TrapezoiMebership(originValue);
             return gradeFuzzyVector;
         }
         public static double CalculateStandardGrade(string index,double originValue)
         {
             var indexCriteria = FiveLevelCriteria[index];

         }
    }
}
