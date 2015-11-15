using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.CORE.FuzzyAHP
{
    public enum TunnelHealthIndex
    {
        //monitoring
        Undefined=0,
        Settlement=1,
        Convergence=2,
        Crack=3,
        SteelStress,
        ConcreteStress,
        SoilPressure
    }
}
