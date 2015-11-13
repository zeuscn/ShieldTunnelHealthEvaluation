using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using IS3.Core;
using System.Windows;

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

                //_mainFrame = Globals.mainframe;
                //_prj = Globals.project;

                //if (_mainFrame == null || _prj == null) { _initFailed = true;  }

                //_monitorDomain = _prj.getDomain(DomainType.Monitoring);
                //if (_monitorDomain == null) { _initFailed = true;  }
                //_test = _monitorDomain.getObjects("MonPoint");


                MyAHPIndexHierarachys = new List<AHPIndexHierarchy>();
                MyAHPIndexHierarachys.Add(XMLIO.ReadIndexHierarchyXml());

                //JudgementMatrixInfosSet testSet= BinaryIO.ReadMatrixInfosSet();
                Calculation cal = new Calculation(MyAHPIndexHierarachys[0]);
            //}
            //catch(Exception e)
            //{
            //    MessageBox.Show(e.ToString());
            //}
        }
    }
}
