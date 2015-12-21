using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;

namespace ShieldTunnelHealthEvaluation.CORE
{
    public class SingleIndexCriteria
    {
        public string ProjectName { get; set; }
        public string  IndexName { get; set; }
        public int LevelType { get; set; }
        public DenseVector CriteriaValues { get; set; }
        public bool IsAbsolute { get; set; }
    }
}
