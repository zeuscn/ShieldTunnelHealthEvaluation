using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using ShieldTunnelHealthEvaluation.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShieldTunnelHealthEvaluation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel;
        public MainWindow()
        {
            viewModel = new MainWindowViewModel();
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnWeight_Click(object sender, RoutedEventArgs e)
        {
            JudgementMatrixWnd weightWnd = new JudgementMatrixWnd(viewModel.MyAHPIndexHierarchys[0]);
            weightWnd.Show();
        }

        private void btnTestt_Click(object sender, RoutedEventArgs e)
        {
            TestWnd _testWnd = new TestWnd();
            _testWnd.Show();
        }

        private void btnResult_Click(object sender, RoutedEventArgs e)
        {
            Calculation cal = new Calculation(viewModel.MyAHPIndexHierarchys[0], viewModel._monDataUtil.groupedDatas);
        }
    }
}
