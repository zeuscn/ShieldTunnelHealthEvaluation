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
    public class MonitorDataUtil
    {
        public GroupedMonitorDataByTime groupedDatas = new GroupedMonitorDataByTime();
        public MonitorDataUtil(Project proj)
        {
            //try
            //{
            Project _prj = proj;
            Domain _monitorDomain;              // the geology domain of the project

            _monitorDomain = _prj.getDomain(DomainType.Monitoring);
            var _test = _monitorDomain.getObjects("MonPoint");
            List<DGObject> _testPoints = _test.merge();
            int i = 0;
            foreach (var point in _testPoints)
            {
                i++;
                List<DGObject> _testObjectTablewiew = new List<DGObject>() { point };
                try
                {
                    MonPoint monP = (MonPoint)point;
                    string monTarget = monP.monTarget;
                    var componetNames = monP.componentNames;
                    List<DataView> dataViews = point.tableViews(_testObjectTablewiew);
                    var rows = dataViews[1].Table.AsEnumerable().ToList();
                    //DataTable dt = dataViews[1].Table;
                    //var _newestData = (from d in dt.AsEnumerable()
                    //                   group d by d.Field<DateTime>("time").ToString("yyyy--MM--dd") into g
                    //                   orderby g.Key descending
                    //                   select g).ToList();
                    string index=IndexMonPUtil.MonP2Index(Tuple.Create<string, string>(componetNames, monTarget));
                    if(componetNames=="STRS")
                    {

                    }
                    if(i>222)
                    {

                    }
                    if(groupedDatas.MonitorDataTable.Keys.Contains(index))
                    {
                        var newtable=groupedDatas.MonitorDataTable[index];
                        rows.Union<DataRow>(newtable);
                    }
                    else
                    {
                        groupedDatas.MonitorDataTable.Add(index, rows);
                    }
                    //GroupedMonitorDataByTime groupedData = new GroupedMonitorDataByTime()
                    //{
                    //    componentNames = componetNames,
                    //    monTarget = monTarget,
                    //    indexName=index,
                    //    MonitorDataTable = _newestData
                    //};
                    //groupedDatas.Add(groupedData);
                    //var _newestWorstDataGroup = _newestData[0];
                    //List<DataRow> _newestWorstData=_newestWorstDataGroup.ToList();
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            //var test=groupedDatas.SelectNewestDateBefore(groupedDatas.MonitorDataTable.ElementAt(0).Value, DateTime.MaxValue);
            //groupedDatas.SelectMaxValue(test);
            //DGObjectsCollection _testGroup = _monitorDomain.getObjects("MonGroup");
            //List<DGObject> _testObjs = _testGroup.merge();
            //foreach (DGObject _testobj in _testObjs)
            //{
            //    List<DGObject> _testObjstableView = new List<DGObject>();
            //    _testObjstableView.Add(_testobj);
            //    try
            //    {
            //        List<DataView> dataViews = _testobj.tableViews(_testObjstableView);
            //    }
            //    catch (Exception e)
            //    {
            //        continue;
            //    }
            //}
        }
    }
}
