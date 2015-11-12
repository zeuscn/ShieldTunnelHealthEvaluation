using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ShielTunnelHealthEvaluation.CORE.FuzzyAHP
{
    public static class FuzzyOperator
    {
        public static DenseVector WeightedAverage(DenseVector weightedVector,DenseMatrix fuzzyMatrix)
        {
            DenseVector result;
            result = (weightedVector * fuzzyMatrix);
            return result;
        }
        public static double MainFactor(double[]a,double[] r)//todo：需要修改
        {
            double[] temp=new double[a.Length];
            double result=0;
            if(a.Length!=r.Length)
            {
                MessageBox.Show("The two vectors don't have the same dimension!");
            }
            for(int i=0;i<a.Length;i++)
            {
                temp[i] = Math.Min(a[i], r[i]);
            }
            result = temp.Max();
            return result;
        }
        public static  double AverageFactor(double[]a,double[] r)//todo:需要修改
        {
            double[] temp = new double[a.Length];
            double result=0;
            if(a.Length!=r.Length)
            {
                MessageBox.Show("The two vectors don't have the same dimension!");
            }
            for(int i=0;i<a.Length;i++)
            {
                temp[i] = Math.Pow(a[i],r[i]);
            }
            result = temp.Min();
            return result;
        }
    }
}
