using IS3.Core;
using IS3.Monitoring;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.DataBaseManager
{
    /// <summary>
    /// 由project获得更方便评估的数据结构GroupedMonitorDataByTime
    /// </summary>
    public class MonitorDataUtil
    {
        public GroupedMonitorDataByTime groupedDatas = new GroupedMonitorDataByTime();
        public MonitorDataUtil(Project proj)
        {
            Project _prj = proj;
            Domain _monitorDomain;
            _monitorDomain = _prj.getDomain(DomainType.Monitoring);
            var _test = _monitorDomain.getObjects("MonPoint");
            List<DGObject> _allMonPoints = _test.merge();
            foreach (var point in _allMonPoints)
            {
                List<DGObject> aPointView = new List<DGObject>() { point };
                try
                {
                    MonPoint monP = (MonPoint)point;
                    string monTarget = monP.monTarget;
                    var componetNames = monP.componentNames;
                    List<DataView> dataViews = point.tableViews(aPointView);
                    var rows = dataViews[1].Table.AsEnumerable().ToList();
                    string index=IndexMonPUtil.MonP2Index(Tuple.Create<string, string>(componetNames, monTarget));
                    if(groupedDatas.MonitorDataTable.Keys.Contains(index)) //已有此指标
                    {
                        var newtable=groupedDatas.MonitorDataTable[index];
                        rows.Union<DataRow>(newtable);
                    }
                    else
                    {
                        groupedDatas.MonitorDataTable.Add(index, rows);
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }
    }
}
