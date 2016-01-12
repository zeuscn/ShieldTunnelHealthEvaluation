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
using ShieldTunnelHealthEvaluation.CORE;

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
    public class GradeColorConverter:IValueConverter
    {
        List<Brush> colors = new List<Brush>()
        {
            Brushes.White,
            Brushes.Blue,
            Brushes.Yellow,
            Brushes.Orange,
            Brushes.Red
        };
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int index = EvaluatedGradeInfo.EvaluatedFiveGrades.FindIndex(grade=>grade==value.ToString());
            return colors[index];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
