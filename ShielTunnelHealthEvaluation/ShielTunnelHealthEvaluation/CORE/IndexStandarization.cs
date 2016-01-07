using MathNet.Numerics.LinearAlgebra.Double;
using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using ShieldTunnelHealthEvaluation.DataBaseManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShieldTunnelHealthEvaluation.CORE
{
     public class IndexStandarization
    {
         //private readonly Dictionary<string, double[]> FiveLevelCriteria;
         private readonly double[] GradeCriteria;
         private readonly DenseVector GradeVector;
         public List<SingleIndexCriteria> criterias;
         public string projectname="test2";
         public IndexStandarization()
         {
             TableCriteriaUtil tableCriteriaUtil = new TableCriteriaUtil(new DbConnection());
             criterias=tableCriteriaUtil.Read(projectname);
         //    FiveLevelCriteria = new Dictionary<string, double[]>//todo:如何确定临界值
         //{
         //    {"Settlement",new double[12]{double.MinValue,1,2,3,4,5,6,7,8,9,10,double.MaxValue}},
         //    {"Convergence",new double[12]{double.MinValue,2,4,6,8,10,12,14,16,18,20,double.MaxValue}},
         //    {"SoilPressure",new double[12]{double.MinValue,1,2,3,4,5,6,7,8,9,10,double.MaxValue}},
         //    {"ConcreteStress",new double[12]{double.MinValue,1,2,3,4,5,6,7,8,9,10,double.MaxValue}},
         //    {"SteelStress",new double[12]{double.MinValue,1,2,3,4,5,6,7,8,9,10,double.MaxValue}},
         //    {"SteelCorrosion",new double[12]{double.MinValue,1,2,3,4,5,6,7,8,9,10,double.MaxValue}}
         //};
             //GradeCriteria = new double[] { 100, 87.5, 75, 62.5, 50, 37.5, 25, 12.5, 0, 0 };
             //GradeVector = new DenseVector(new double[5]{100, 75, 50, 25, 0 }) ;
         }
         //public  DenseVector CalculateFuzzyVector(string index,double originValue)
         //{
             //MemberShipFun membershipFun = new MemberShipFun();
             //membershipFun.ValueDivision = FiveLevelCriteria[index];
             //var gradeFuzzyVector= membershipFun.TrapezoiMebership(originValue);
             //return gradeFuzzyVector;
         //}
         public double CalculateStandardGrade(string index, IndexOptimizationType optimType,double originValue)
         {
             var indexCriteria = criterias.Find(c => c.IndexName == index);
             if(indexCriteria==null)
             {
                 MessageBox.Show("there is no criteria for this index");
             }
             return ValueStandarization(indexCriteria.CriteriaValues, originValue, optimType);
         }
         private double ValueStandarization(List<CriteriaDividing> criteriaValues, double indexValue, IndexOptimizationType optimType)
        {
             
            double xmax=criteriaValues[criteriaValues.Count-1].DividingValue;//todo:需要细化
            double xmin=criteriaValues[0].DividingValue;
             if(optimType==null)
             {
                 MessageBox.Show("optimType is null!");
             }
             if(optimType==IndexOptimizationType.Negative)
             {
                 if (indexValue > xmax)
                 {
                     return 0;
                 }
                 if (indexValue < xmin)
                 {
                     MessageBox.Show("the range of criteria is wrong!");
                 }
                 double result = 100 - (indexValue - xmin) / (xmax - xmin) * 100;
                 return result;
             }
             else if(optimType==IndexOptimizationType.Positive)
             {
                 if (indexValue < xmin)
                 {
                     return 0;
                 }
                 if (indexValue >xmax)
                 {
                     MessageBox.Show("the range of criteria is wrong!");
                 }
                 double result = (indexValue - xmin) / (xmax - xmin) * 100;
                 return result;
             }
             else if (optimType == IndexOptimizationType.Middle)
             {
                 double middle = criteriaValues[1].DividingValue;
                 if (indexValue > xmax)
                 {
                     return 0;
                 }
                 if (indexValue < xmin)
                 {
                     MessageBox.Show("the range of criteria is wrong!");
                 }
                 double result = 100 - (indexValue - xmin) / (xmax - xmin) * 100;
                 return result;
             }
             else
             {
                 MessageBox.Show("optimType is Undefined!");
                 return 0;
             }
            
        }
    }
}
