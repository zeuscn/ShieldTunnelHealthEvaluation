using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.CORE
{
    public static class DenseMatrixExtensiveMethod
    {
        public static DenseMatrix DataTable2Matrix(this DenseMatrix dm,DataTable dt)
        {
            var createdDm = new DenseMatrix(dt.Rows.Count,dt.Columns.Count);
            for(int j=0;j<dt.Rows.Count;j++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    createdDm[j,i] = (double)dt.Rows[j][i];
                }
            }
            return createdDm;
        }
    }
}
