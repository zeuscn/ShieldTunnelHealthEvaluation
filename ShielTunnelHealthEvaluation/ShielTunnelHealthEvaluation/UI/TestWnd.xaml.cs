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
using System.Windows.Shapes;
using ShielTunnelHealthEvaluation.CORE.FuzzyAHP;

namespace ShielTunnelHealthEvaluation.UI
{
    /// <summary>
    /// TestWnd.xaml 的交互逻辑
    /// </summary>
    public partial class TestWnd : Window
    {
        public TestWnd()
        {
            InitializeComponent();
        }

        private void btnSerialization_Click(object sender, RoutedEventArgs e)
        {
            Hierarchy testHierarchy = new Hierarchy();
            testHierarchy.OutputXml();
        }
    }
}
