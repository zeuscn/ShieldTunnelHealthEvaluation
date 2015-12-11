using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShieldTunnelHealthEvaluation.CORE.FuzzyAHP
{
    [Serializable]
    public class JudgementMatrixsSetting
    {
        public string ExpertName { get; set; }
        public DateTime Time { get; set; }
        [XmlIgnore]
        public Dictionary<string, JudgementMatrixInfo> JudgeMatrixDic { get; set; }
        public JudgementMatrixsSetting()
        {
        }
        public JudgementMatrixsSetting(AHPIndexHierarchy ahpIndexHierarchy)
        {
            JudgeMatrixDic = new Dictionary<string, JudgementMatrixInfo>();
            InitialData(ahpIndexHierarchy);
        }
        private void InitialData(AHPIndexHierarchy ahpIndexHierarchy)
        {
            if (ahpIndexHierarchy.Children == null || ahpIndexHierarchy.Children.Count < 1)
            {
                return;
            }
            JudgementMatrixInfo judgeMatrixInfo = new JudgementMatrixInfo();
            judgeMatrixInfo.IndexsSequence = ahpIndexHierarchy.ChildrenNames;
            JudgeMatrixDic.Add(ahpIndexHierarchy.Name, judgeMatrixInfo);
            if (ahpIndexHierarchy.Children != null && ahpIndexHierarchy.Children.Count > 0)
            {
                foreach (AHPIndexHierarchy item in ahpIndexHierarchy.Children)
                {
                    InitialData(item);
                }
            }
        }
    }
}
