using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.UI
{
    class IndexTreeViewModel
    {
           public List<AHPIndexHierarchy> MyAHPIndexHierarachys { get; set; }
           public IndexTreeViewModel()
        {
            MyAHPIndexHierarachys = new List<AHPIndexHierarchy>();
             MyAHPIndexHierarachys.Add(XMLIO.ReadIndexHierarchyXml());
        }
    }
}
