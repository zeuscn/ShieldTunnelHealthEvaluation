using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.CORE.FuzzyAHP
{
    /// <summary>
    /// 不同专家的判断矩阵
    /// </summary>
    [Serializable]
    public class AllExpertJudgementMatrixs
    {
        private List<JudgementMatrixsSetting> judgementMatrixInfosList = new List<JudgementMatrixsSetting>();
        public List<JudgementMatrixsSetting> JudgementMatrixInfosList { get { return judgementMatrixInfosList; } set { judgementMatrixInfosList = value; } }
        public AllExpertJudgementMatrixs()
        {
        }
    }
}
