using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using System.ComponentModel;

namespace ShieldTunnelHealthEvaluation.CORE
{
    public class SingleIndexCriteria:INotifyPropertyChanged
    {
        private List<CriteriaDividing> criteriaValues;
        public string ProjectName { get; set; }
        public string  IndexName { get; set; }
        public int LevelType { get; set; }
        public List<CriteriaDividing>CriteriaValues 
        { 
            get
            {
                return this.criteriaValues;
            }
            set
            {
                this.criteriaValues = value;
                propertyChanged("CriteriaValues");
            }
        }
        public bool IsAbsolute { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void propertyChanged(string name)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public string ToCriteriaValueString()
        {
            string result=string.Empty;
            for (int i = 0; i < criteriaValues.Count;i++ )
            {
                result += CriteriaValues[i].DividingValue ;
                if (i < CriteriaValues.Count - 1)
                {
                    result += ",";
                }
            }
            return result;
        }
    }
    public class CriteriaDividing:INotifyPropertyChanged
    {
        private double dividingValue;
        public double DividingValue 
        { 
            get
            {
                return this.dividingValue;
            }
            set
            {
                this.dividingValue = value;
                propertyChanged("DividingValue");
            }
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
