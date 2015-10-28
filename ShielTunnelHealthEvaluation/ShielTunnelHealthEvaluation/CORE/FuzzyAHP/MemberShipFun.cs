using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;

namespace ShielTunnelHealthEvaluation.CORE.FuzzyAHP
{
    public class MemberShipFun
    {
        double[] HealthLevelMS;
        double[] ValueDivision;
        public MemberShipFun()
        {
            HealthLevelMS = new double[4];
            ValueDivision = new double[] { 100, 100, 93.75, 81.25, 68.75, 56.25, 43.75, 31.25, 18.75, 6.25, 0, 0 };
        }
        private void TriMembership()
        {

        }
        private void TrapezoiMebership(double x)
        {
            for(int i=0;i<5;i++)
            {
                HealthLevelMS[i] = TrapezoiMebershipFun(x, ValueDivision[i * 2], ValueDivision[2 * i + 1], ValueDivision[2 * i + 2], ValueDivision[2 * i + 3]);
            }
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
                result = (d - x) * (d - a);
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
