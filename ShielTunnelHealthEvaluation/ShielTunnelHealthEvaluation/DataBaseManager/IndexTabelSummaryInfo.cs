using ShieldTunnelHealthEvaluation.CORE;
using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.DataBaseManager
{
    class IndexTabelSummaryInfo
    {
        public TunnelHealthIndex MyTunnelHealthIndex{get;set;}
        public MonitorItemType MyMonitorItemType { get; set; }
        public List<string> GroupTableNames { get; set; }
        public List<string> PointTableNames { get; set; }
        public List<string> DataTableNames { get; set; }
        public Dictionary<DateTime, double> TimeWorstValueDic { get; set; }
        public IndexTabelSummaryInfo()
        {
            GroupTableNames = new List<string>();
            PointTableNames = new List<string>();
            DataTableNames = new List<string>();
            TimeWorstValueDic = new Dictionary<DateTime, double>();
        }
    }
}
