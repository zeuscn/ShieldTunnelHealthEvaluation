using ShieldTunnelHealthEvaluation.CORE;
using ShieldTunnelHealthEvaluation.DataBaseManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.UI
{
    class CriteriaSettingViewModel:INotifyPropertyChanged
    {
       private List<bool> yesNo=new List<bool>(){true,false};
       private List<SingleIndexCriteria> allIndexCriterias;
       public  List<SingleIndexCriteria> AllIndexCriterias 
       { 
           get
           {
               return allIndexCriterias;
           }
           set
           {
               allIndexCriterias = value;
               
           }
       }
       public List<bool> YesNo { get { return yesNo; } }
        public CriteriaSettingViewModel(string projectName)
       {
           TableCriteriaUtil tableCriteriaUtil = new TableCriteriaUtil(new DbConnection());
           AllIndexCriterias=tableCriteriaUtil.Read(projectName);
       }
        public event PropertyChangedEventHandler PropertyChanged;
        public void propertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
