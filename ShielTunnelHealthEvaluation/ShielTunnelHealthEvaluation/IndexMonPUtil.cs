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
        public static Tuple<string,string> Index2MonP(string index)
        {
            string component=string.Empty;
            string monTarget=string.Empty;
            if(index=="level2Node1")
            {
                component = "DSPZ";
                monTarget = "Segment";
            }
            if(index=="level2Node2")
            {
                component = "STRS";
                monTarget = "Segment";
            }
            return Tuple.Create<string, string>(component, monTarget);
        }
        public static string MonP2Index(Tuple<string,string> component_monTargetPair)
        {
            Tuple<string, string> tuple=Tuple.Create<string,string>("DSPZ","Segment");
            if (component_monTargetPair.Item1 == "DSPZ"&&component_monTargetPair.Item2=="Segment")
            {
                return "level2Node1";
            }
            else
            {
                return "level2Node2";
            }
        }
    }
}
