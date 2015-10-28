using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using System.Math;

namespace ShielTunnelHealthEvaluation.CORE.FuzzyAHP
{
    public static  class Starndarization
    {
        public static double ValueStardarization(double originValue,IndexStardrizationType indexStdType,double xn,double xm,double A,double B)
        {
            double value;
            double temp=(originValue-xn)/(xm-xn);
            switch(indexStdType)
            {
                case IndexStardrizationType.Pessimistic:
                        value = value = A * temp * Math.Pow(Math.E, B * (temp - 1));
                        break;
                case IndexStardrizationType.Optimistic:
                        value = value = A * (1-temp) * Math.Pow(Math.E, B * (temp - 1));
                        break;
                default:
                        value = A * temp;
                        break;
            }
            return value;
        }
    }
}
