using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using IS3.Core;
using System.Windows;
using System.Data;
using ShieldTunnelHealthEvaluation.DataBaseManager;

namespace ShieldTunnelHealthEvaluation.UI
{
    public class MainWindowViewModel
    {
        Project _prj;                       // the project
        Domain _monitorDomain;              // the geology domain of the project
        IMainFrame _mainFrame;              // the main frame
        IView _inputView;                   // the input view (boreholes, projeciton line)
        DGObjectsCollection _test;        // all the boreholes
        bool _initFailed;
        public List<AHPIndexHierarchy> MyAHPIndexHierarachys { get; set; }
        public MainWindowViewModel()
        {
            //try
            //{

            _mainFrame = Globals.mainframe;
            _prj = Globals.project;

            if (_mainFrame == null || _prj == null) { _initFailed = true; }
            MonitorDataUtil _monDataUtil=new MonitorDataUtil(_prj);


            //MonitorDataUtil 
            //_monitorDomain = _prj.getDomain(DomainType.Monitoring);
            //if (_monitorDomain == null) { _initFailed = true; }
            //_test = _monitorDomain.getObjects("MonPoint");
            //List<DGObject> _testPoints = _test.merge();
            //foreach (var point in _testPoints)
            //{
            //    List<DGObject> _testObjectTablewiew = new List<DGObject>() { point };
            //    try
            //    {
            //        List<DataView> dataViews = point.tableViews(_testObjectTablewiew);
            //        DataTable dt= dataViews[1].Table;
            //        var _newestData = (from d in dt.AsEnumerable()
            //                           group d by d.Field<DateTime>("time").ToString("yyyy--MM--dd") into g
            //                           orderby g.Key descending 
            //                           select g).ToList();
            //        var _newestWorstDataGroup = _newestData[0];
            //        List<DataRow> _newestWorstData=_newestWorstDataGroup.ToList();
            //    }
            //    catch (Exception e)
            //    {
            //        continue;
            //    }
            //}
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
            //MyAHPIndexHierarachys = new List<AHPIndexHierarchy>();
            //MyAHPIndexHierarachys.Add(XMLIO.ReadIndexHierarchyXml());

            //JudgementMatrixInfosSet testSet= BinaryIO.ReadMatrixInfosSet();
            //Calculation cal = new Calculation(MyAHPIndexHierarachys[0]);
            //}
            //catch(Exception e)
            //{
            //    MessageBox.Show(e.ToString());
            //}
        }
    }
}
