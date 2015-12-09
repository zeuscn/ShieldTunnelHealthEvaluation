using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShieldTunnelHealthEvaluation.DataBaseManager
{
    class GroupedMonitorDataByTime
    {
        //public string componentNames;
        //public string monTarget;
        //public string indexName;
        //public List<IGrouping<string, DataRow>> MonitorDataTable;
        public Dictionary<string, List<DataRow>> MonitorDataTable = new Dictionary<string, List<DataRow>>();
        public List<DataRow> SelectNewestDateBefore(List<DataRow> dataRows, DateTime targetTime)
        {
            var q = (from d in dataRows
                     where d.Field<DateTime>("time") < targetTime
                     group d by d.Field<DateTime>("time").ToString("yyyy--MM--dd") into g
                     orderby g.Key descending
                     select g).ToList();
            var _newestData = q[0].ToList();
            return _newestData;
        }
        public double SelectMaxValue(List<DataRow> datarows)
        {
            var maxRow = (double)datarows.Max(r => r.Field<decimal>("value"));
            return maxRow;
        }
    }
}
