using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ShielTunnelHealthEvaluation.CORE.FuzzyAHP
{
    class FuzzyOperator
    {
        public double WeightedAverage(double[]a,double[] r)
        {
            double result=0;
            if(a.Length!=r.Length)
            {
                MessageBox.Show("The two vectors don't have the same dimension!");
            }
            for(int i=0;i<a.Length;i++)
            {
                result += a[i] * r[i];
            }
            return result;
        }
        public double MainFactor(double[]a,double[] r)
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
        public double AverageFactor(double[]a,double[] r)
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
