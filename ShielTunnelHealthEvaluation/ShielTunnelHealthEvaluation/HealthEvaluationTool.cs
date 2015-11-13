using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS3.Core;

namespace ShieldTunnelHealthEvaluation
{
    class HealthEvaluationTool:Tools
    {
        public override string name() { return "iS3.SimpleGeologyTools"; }
        public override string provider() { return "Tongji iS3 team"; }
        public override string version() { return "1.0"; }

        List<ToolTreeItem> items;
        public override IEnumerable<ToolTreeItem> treeItems()
        {
            return items;
        }

        MainWindow _mainWnd;
        public void HealthEvalutation()
        {
            if (_mainWnd != null)
            {
                _mainWnd.Show();
                return;
            }

            _mainWnd = new MainWindow();
            _mainWnd.Closed += (o, args) =>
                {
                    _mainWnd = null;
                };
            _mainWnd.Show();
        }

        public HealthEvaluationTool()
        {
            items = new List<ToolTreeItem>();

            ToolTreeItem item = new ToolTreeItem("Monitor|Basic", "HealthEvaluation", HealthEvalutation);
            items.Add(item);
        }
    }
}
