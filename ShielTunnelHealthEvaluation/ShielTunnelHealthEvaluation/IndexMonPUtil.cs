using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation
{
    /// <summary>
    /// 进行评估指标与（监测点的component，monTarget)转换
    /// </summary>
    class IndexMonPUtil  //未完成，只实现了最简单的情形
    {
        private static Dictionary<string, Tuple<string, string>> IndexMonPPairs = new Dictionary<string, Tuple<string, string>>
        {
        {"Settlement",Tuple.Create<string,string>("DSPZ","Segment")},
        {"Convergence",Tuple.Create<string,string>("DSPY","Segment")},
        {"SoilPressure",Tuple.Create<string,string>("STRS","Soil")},
        {"ConcreteStress",Tuple.Create<string,string>("STRS","Segment")},
        {"SteelStress",Tuple.Create<string,string>("STRS","Steel")},
        {"SteelCorrosion",Tuple.Create<string,string>("CRSN","Steel")}
        };
        /// <summary>
        /// 根据指标获得component和monTarget
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Tuple<string, string> Index2MonP(string index)
        {
            var monP = IndexMonPPairs[index];
            return monP;
        }
        public static string MonP2Index(Tuple<string, string> component_monTargetPair)
        {
            return IndexMonPPairs.First(imp => imp.Value.Equals( component_monTargetPair)).Key;
        }

    }

}
