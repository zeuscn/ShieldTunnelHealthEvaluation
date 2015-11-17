using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.CORE
{
    /// <summary>
    /// the value of index at different time
    /// </summary>
    public class DifferentDateIndexData
    {
        public DateTime UpdateDate { get; set; }
        public Dictionary<int,double> IndexValue { get; set; }
        //public MonitorItemType GroupOrPoint;
    }
    enum MonitorItemType
    {
        MonitorPoint,
        MonitorGroup
    }
}
