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
using System.Diagnostics;

namespace ShieldTunnelHealthEvaluation.UI
{
    public class MainWindowViewModel
    {
        private Project _prj;                       // the project
        private IMainFrame _mainFrame;              // the main frame
        private List<AHPIndexHierarchy> myAhpIndexsHierarchys = new List<AHPIndexHierarchy>();
        public MonitorDataUtil _monDataUtil;
        public List<AHPIndexHierarchy> MyAHPIndexHierarchys { get { return myAhpIndexsHierarchys; } set { myAhpIndexsHierarchys = value; } }
        public List<DateTime> AllDataUpdateDate { get; set; }
        public MainWindowViewModel()
        {
            _mainFrame = Globals.mainframe;
            _prj = Globals.project;
            Debug.Assert(_mainFrame != null || _prj != null);
            _monDataUtil=new MonitorDataUtil(_prj);
            AllDataUpdateDate = _monDataUtil.groupedDatas.AllTime();
            myAhpIndexsHierarchys.Add(XMLIO.ReadIndexHierarchyXml());//从xml读取指标体系
        }
    }
}
