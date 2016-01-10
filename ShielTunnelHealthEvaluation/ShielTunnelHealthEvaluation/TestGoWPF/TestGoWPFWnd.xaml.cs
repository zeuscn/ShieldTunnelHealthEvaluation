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
using Northwoods.GoXam.Model;
using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;

namespace ShieldTunnelHealthEvaluation.TestGoWPF
{
    /// <summary>
    /// TestGoWPF.xaml 的交互逻辑
    /// </summary>
    public partial class TestGoWPFWnd : Window
    {
        public static TreeModel<AHPIndexHierarchy, string> model ; 
        public TestGoWPFWnd()
        {
            // create the initial model
            InitializeComponent();
            myDiagram.Model = model;
        }
    }
}
