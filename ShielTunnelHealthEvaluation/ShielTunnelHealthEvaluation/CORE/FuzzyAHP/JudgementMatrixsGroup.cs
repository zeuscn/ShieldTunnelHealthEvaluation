using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShieldTunnelHealthEvaluation.CORE.FuzzyAHP
{
    /// <summary>
    /// 一个工程各个判断矩阵构成一个判断矩阵组，包含专家名字，打分时间等信息
    /// </summary>
    [Serializable]
    public class JudgementMatrixsGroup
    {
        public string ExpertName { get; set; }
        public string Time { get; set; }
        [XmlIgnore]
        public Dictionary<string, SingleBasicJudgementMatrixInfo> JudgeMatrixDic { get; set; }
        public JudgementMatrixsGroup()
        {
        }
        public JudgementMatrixsGroup(AHPIndexHierarchy ahpIndexHierarchy)
        {
            JudgeMatrixDic = new Dictionary<string, SingleBasicJudgementMatrixInfo>();
            InitialData(ahpIndexHierarchy);
        }
        private void InitialData(AHPIndexHierarchy ahpIndexHierarchy)
        {
            if (ahpIndexHierarchy.Children == null || ahpIndexHierarchy.Children.Count < 1)
            {
                return;
            }
            SingleBasicJudgementMatrixInfo judgeMatrixInfo = new SingleBasicJudgementMatrixInfo();
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
