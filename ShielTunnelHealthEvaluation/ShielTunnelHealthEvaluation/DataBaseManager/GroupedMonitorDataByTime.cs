using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace ShieldTunnelHealthEvaluation.DataBaseManager
{
    public class GroupedMonitorDataByTime
    {
        private string timeField = "time";
        private string valueField = "value";
        private string readingField = "reading";
        private string timeFormat = "yyyy--MM--dd";
        public Dictionary<string, List<DataRow>> MonitorDataTable = new Dictionary<string, List<DataRow>>();
        public List<DataRow> SelectNewestDateBefore(List<DataRow> dataRows, DateTime targetTime)
        {
            var q = (from d in dataRows
                     where d.Field<DateTime>(timeField) < targetTime
                     group d by d.Field<DateTime>(timeField).ToShortDateString() into g
                     orderby g.Key descending
                     select g).ToList();
            var _newestData = q[0].ToList();
            return _newestData;
        }
        public double SelectMaxValue(List<DataRow> datarows)
        {
            if(datarows.Count<1)
            {
                return 10000;
            }
            var maxRow = (double?)datarows.Max(r => r.Field<decimal?>(readingField));
            Debug.Assert(maxRow != null);
            return (double)maxRow;
        }
        public List<DateTime> SelectIndexMonTime(List<DataRow> datarows)
        {
            var allTime = (from v in datarows
                           group v by v.Field<DateTime>(timeField).ToShortDateString() into g
                           orderby g.Key descending
                           select Convert.ToDateTime(g.Key)).ToList();
            return allTime;
        }
        public List<DateTime> AllTime()
        {
            List<DateTime> allTime = new List<DateTime>();
            foreach (var v in MonitorDataTable.Values)
            {
                allTime.Union(SelectIndexMonTime(v));
            }
            return allTime.Distinct().ToList();
        }
    }
}
