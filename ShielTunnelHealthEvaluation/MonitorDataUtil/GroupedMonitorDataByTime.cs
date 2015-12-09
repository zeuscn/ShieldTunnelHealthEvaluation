using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MonitorDataUtil
{
    /// <summary>
    /// 
    /// </summary>
   public  class GroupedMonitorDataByTime
    {
        public string componentNames;
        public string monTarget;
       public  List<IGrouping<string,DataRow>> MonitorDataTable;
       #region query
        public string SelectNewestDateBefore(string evaluateTime)
        {
            string newestDate = string.Empty;

            return newestDate; 
        }
       #endregion
    }
}
