using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using IS3.Core;
using IS3.Monitoring;

namespace MonitorDataUtil
{
    public class MonitorDataUtil
    {
        
        List<GroupedMonitorDataByTime> groupedDatas = new List<GroupedMonitorDataByTime>();
        public MonitorDataUtil(Project proj)
        {
            //try
            //{
            Project  _prj = proj;
            Domain _monitorDomain;              // the geology domain of the project

            _monitorDomain = _prj.getDomain(DomainType.Monitoring);
            var _test = _monitorDomain.getObjects("MonPoint");
            List<DGObject> _testPoints = _test.merge();
            foreach (var point in _testPoints)
            {
                List<DGObject> _testObjectTablewiew = new List<DGObject>() { point };
                try
                {
                    MonPoint monP = (MonPoint)point;
                    string monTarget = monP.monTarget;
                    var componetNames = monP.componentNames;
                    List<DataView> dataViews = point.tableViews(_testObjectTablewiew);
                    DataTable dt= dataViews[1].Table;
                    var _newestData = (from d in dt.AsEnumerable()
                                       group d by d.Field<DateTime>("time").ToString("yyyy--MM--dd") into g
                                       orderby g.Key descending 
                                       select g).ToList();
                    GroupedMonitorDataByTime groupedData = new GroupedMonitorDataByTime()
                    {
                        componentNames=componetNames,
                        monTarget=monTarget,
                        MonitorDataTable = _newestData
                    };
                    //var _newestWorstDataGroup = _newestData[0];
                    //List<DataRow> _newestWorstData=_newestWorstDataGroup.ToList();
                }
                catch (Exception e)
                {
                    continue;
                }
            }
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
