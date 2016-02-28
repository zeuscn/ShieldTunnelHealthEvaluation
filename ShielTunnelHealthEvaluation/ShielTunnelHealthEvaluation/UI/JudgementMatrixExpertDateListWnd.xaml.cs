using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShieldTunnelHealthEvaluation.UI
{
    /// <summary>
    /// JudgementMatrixExpertDateList.xaml 的交互逻辑
    /// </summary>
    public partial class JudgementMatrixExpertDateListWnd : Window
    {
        private AllExpertJudgementMatrixs _allExpertJudgementMatrix;//所有有关信息
        private JudgementMatrixsGroup _judgementMatrixGroup;
        public JudgementMatrixExpertDateListWnd()
        {
            InitializeComponent();
            _allExpertJudgementMatrix = BinaryIO.ReadMatrixInfosSet();//读取已有的
            this.lvJudgeMatrixDetailList.ItemsSource = _allExpertJudgementMatrix.JudgementMatrixInfosList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _judgementMatrixGroup = new JudgementMatrixsGroup(HealthEvaluationGlobals.MyAHPIndexHierarchys[0]);
            JudgementMatrixWnd addJudgementMatrixWnd = new JudgementMatrixWnd(_judgementMatrixGroup);
            addJudgementMatrixWnd.Show();
            addJudgementMatrixWnd.Closed += AddAndRefresh_Closed;
        }
        void AddAndRefresh_Closed(object sender, EventArgs e)
        {
            _allExpertJudgementMatrix.JudgementMatrixInfosList.Add(_judgementMatrixGroup);//添加到全集中
            BinaryIO.OutputMatrixInfosSet(_allExpertJudgementMatrix);//存到硬盘
            this.lvJudgeMatrixDetailList.ItemsSource = null;
            this.lvJudgeMatrixDetailList.ItemsSource = _allExpertJudgementMatrix.JudgementMatrixInfosList;
        }
        void SaveAndRefresh_Closed(object sender, EventArgs e)
        {
            BinaryIO.OutputMatrixInfosSet(_allExpertJudgementMatrix);//存到硬盘
            this.lvJudgeMatrixDetailList.ItemsSource = null;
            this.lvJudgeMatrixDetailList.ItemsSource = _allExpertJudgementMatrix.JudgementMatrixInfosList;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = this.lvJudgeMatrixDetailList.SelectedIndex;
            _allExpertJudgementMatrix.JudgementMatrixInfosList.RemoveAt(selectedIndex);
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex=this.lvJudgeMatrixDetailList.SelectedIndex;
            if(selectedIndex<0)
            {
                MessageBox.Show("未选择！");
                return;
            }
            _judgementMatrixGroup = _allExpertJudgementMatrix.JudgementMatrixInfosList[selectedIndex];
            JudgementMatrixWnd viewWnd = new JudgementMatrixWnd(_judgementMatrixGroup);
            viewWnd.Show();
            viewWnd.Closed += SaveAndRefresh_Closed;
        }
    }
}
