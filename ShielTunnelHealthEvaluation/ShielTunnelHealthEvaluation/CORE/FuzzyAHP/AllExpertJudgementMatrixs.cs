using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.CORE.FuzzyAHP
{
    /// <summary>
    /// 一个工程不同专家的判断矩阵
    /// </summary>
    [Serializable]
    public class AllExpertJudgementMatrixs
    {
        private List<JudgementMatrixsGroup> judgementMatrixInfosList = new List<JudgementMatrixsGroup>();
        public List<JudgementMatrixsGroup> JudgementMatrixInfosList { get { return judgementMatrixInfosList; } set { judgementMatrixInfosList = value; } }
        public AllExpertJudgementMatrixs()
        {
        }
    }
}
