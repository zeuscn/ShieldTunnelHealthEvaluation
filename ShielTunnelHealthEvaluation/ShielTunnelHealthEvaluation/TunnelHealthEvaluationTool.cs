using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS3.Core;

namespace ShielTunnelHealthEvaluation
{
    class TunnelHealthEvaluationTool:Tools
    {
        public override string name() { return "iS3.TunnelHealthEvaluationTools"; }
        public override string provider() { return "Tongji iS3 team"; }
        public override string version() { return "1.0"; }

        List<ToolTreeItem> items;
        public override IEnumerable<ToolTreeItem> treeItems()
        {
            return items;
        }
        MainWindow mainwnd;
        public TunnelHealthEvaluationTool()
        {
            items = new List<ToolTreeItem>();
            ToolTreeItem item = new ToolTreeItem("Monitor|Basic", "TunnelHealthEvaluation", EvaluateHealth);
            items.Add(item);
        }
        public void EvaluateHealth()
        {
            if(mainwnd!=null)
            {
                mainwnd.Show();
                return;
            }
            mainwnd = new MainWindow();
            mainwnd.Closed += (o, args) =>
                {
                    mainwnd = null;
                };
            mainwnd.Show();
        }
    }
}
