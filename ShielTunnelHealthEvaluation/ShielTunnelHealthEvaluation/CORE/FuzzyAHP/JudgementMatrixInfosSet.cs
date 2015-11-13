using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.CORE.FuzzyAHP
{
    [Serializable]
    public class JudgementMatrixInfosSet
    {
        public List<JudgementMatrixInfos> JudgementMatrixInfosList { get; set; }
        public JudgementMatrixInfosSet()
        {
            this.JudgementMatrixInfosList = new List<JudgementMatrixInfos>();
        }
    }
}
