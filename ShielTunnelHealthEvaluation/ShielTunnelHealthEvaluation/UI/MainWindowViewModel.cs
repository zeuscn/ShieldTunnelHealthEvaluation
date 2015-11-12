using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShielTunnelHealthEvaluation.CORE.FuzzyAHP;

namespace ShielTunnelHealthEvaluation.UI
{
    public class MainWindowViewModel
    {
         public List<AHPIndexHierarchy> MyAHPIndexHierarachys { get; set; }
         public MainWindowViewModel()
        {
            MyAHPIndexHierarachys = new List<AHPIndexHierarchy>();
            MyAHPIndexHierarachys.Add( XMLIO.ReadIndexHierarchyXml());
            Calculation cal = new Calculation(MyAHPIndexHierarachys[0]);
        }
    }
}
