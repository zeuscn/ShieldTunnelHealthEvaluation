using ShieldTunnelHealthEvaluation.DataBaseManager;
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
    /// CriteriaSettingWnd.xaml 的交互逻辑
    /// </summary>
    public partial class CriteriaSettingWnd : Window
    {
        CriteriaSettingViewModel viewModel;
        string projectName="test2";
        public CriteriaSettingWnd()
        {
            InitializeComponent();
            viewModel = new CriteriaSettingViewModel(projectName);
            this.DataContext = viewModel;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TableCriteriaUtil tableCriteriaUtil = new TableCriteriaUtil(new DbConnection());
            tableCriteriaUtil.Update(viewModel.AllIndexCriterias, projectName);//save the modefied criterias to the database
            this.Close();
        }
    }
}
