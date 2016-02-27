using ShieldTunnelHealthEvaluation.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ShieldTunnelHealthEvaluation.UI
{
    public class GradeColorConverter : IValueConverter
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
            int index = EvaluatedGradeInfo.EvaluatedFiveGrades.FindIndex(grade => grade == value.ToString());
            return colors[index];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
