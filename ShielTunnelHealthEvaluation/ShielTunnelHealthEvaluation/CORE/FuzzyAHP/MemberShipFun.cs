using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;

namespace ShielTunnelHealthEvaluation.CORE.FuzzyAHP
{
    public class MemberShipFun
    {
        public double[] ValueDivision;
        public static readonly int HealthLevelCount = 5;
        public MemberShipFun()
        {
            ValueDivision = new double[] {0,0,6.25,18.75,31.25,43.75,56.25,68.75,81.25,93.75,100,100};
        }
        private void TriMembership()
        {

        }
        public DenseVector TrapezoiMebership(double x)
        {
            DenseVector HealthLevelMS = new DenseVector(HealthLevelCount);
            for (int i = 0; i < HealthLevelCount; i++)
            {
                HealthLevelMS[i] = TrapezoiMebershipFun(x, ValueDivision[i * 2], ValueDivision[2 * i + 1], ValueDivision[2 * i + 2], ValueDivision[2 * i + 3]);
            }
            return HealthLevelMS;
        }
        private double TrapezoiMebershipFun(double x,double a,double b,double c,double d)
        {
            double result;
         
            if(a<=x&&x<b)
            {
                result = (x - a) / (b - a);
            }
            else if(c<x&&x<=d)
            {
                result = (d - x) /(d - c);
            }
            else if(b<=x&&x<=c)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }
            return result;
        }
    }
}
