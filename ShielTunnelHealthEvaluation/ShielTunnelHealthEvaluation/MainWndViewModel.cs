using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS3.Core;

namespace ShielTunnelHealthEvaluation
{
    
    class MainWndViewModel
    {
        DGObjectsCollection _monitorGroup;
        public MainWndViewModel()
        {
            //inputProData();
        }
        /// <summary>
        /// acquire data from iS3
        /// </summary>
        public void inputProData()
        {
            IMainFrame _mainFrame = Globals.mainframe;
            Project _project = Globals.project;
            Domain _monitorDom = _project.getDomain(DomainType.Monitoring);
            _monitorGroup=_monitorDom.getObjects("MonitorGroup");
        }
    }
}
