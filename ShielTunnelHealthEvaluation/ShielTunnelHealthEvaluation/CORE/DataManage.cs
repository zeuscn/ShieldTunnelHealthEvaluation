using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS3.Core;
using System.Diagnostics;
using System.Data;

namespace ShieldTunnelHealthEvaluation.CORE
{
    public class DataManage
    {
        private readonly string _tiemFieldName = "time";
        Project _prj;                       // the project
        Domain _monitorDomain;              // the geology domain of the project
        IMainFrame _mainFrame;              // the main frame
        Dictionary<string, List<DGObject>> _allIndexDataDic;
        List<DGObject> _monPoint;
        List<DGObject> _monGroup;
        List<DifferentDateIndexData> _allDateIndexData;
        public DataManage()
        {
            _allIndexDataDic = new Dictionary<string, List<DGObject>>();
            _prj = Globals.project;
            _mainFrame = Globals.mainframe;
            if(_prj==null||_mainFrame==null)
            {
                return;//no data;
            }
            var _monDomain = _prj.getDomain(DomainType.Monitoring);
            _monGroup = _monDomain.getObjects("MonGroup").merge();
            _monPoint = _monDomain.getObjects("MonPoint").merge();
            GetAllIndexData();
        }
        /// <summary>
        /// add monitoring data which is health evalutaion to dictionary;
        /// </summary>
        public void GetAllIndexData()
        {
            List<DGObject> _indexPointData = new List<DGObject>();
            foreach(var _p in _monPoint)
            {
                if(IsIndex(_p))
                {
                    _indexPointData.Add(_p);
                }
            }
            _allIndexDataDic.Add("MonPoint", _indexPointData);
            List<DGObject> _indexGroupData = new List<DGObject>();
            foreach(var _g in _monGroup)
            {
                if(IsIndex(_g))
                {
                    _indexGroupData.Add(_g);
                }
            }
            _allIndexDataDic.Add("MonGroup", _indexGroupData);
        }
        /// <summary>
        /// judge whether a dgobject  is index;
        /// </summary>
        /// <param name="_dgObj"></param>
        /// <returns></returns>
        private bool IsIndex(DGObject _dgObj)
        {
            bool result = false;

            return result;
        }
        public void GetAllDateTime()
        {

        }
        /// <summary>
        /// get the values of all indexs at a pecific date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private DifferentDateIndexData GetAllIndexValueAtDate(DateTime date)
        {
            DifferentDateIndexData theDateIndexData = new DifferentDateIndexData() { UpdateDate=date};

            return theDateIndexData;
        }
        /// <summary>
        /// get the value of a specific index at the specific date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        //private double GetValueOfAIndex(DateTime date,int index)
        //{

        //}
        private List<IGrouping<string,DataRow>> GetDataRowsFromDgobject(DGObject dgobj)
        {
            try
            {
                DataTable _dt = dgobj.tableViews(new List<DGObject>() { dgobj })[1].Table;
                var _datarows = (from d in _dt.AsEnumerable()
                                 group d by d.Field<DateTime>(_tiemFieldName).ToShortDateString() into _dateGroups
                                 orderby _dateGroups.Key descending
                                 select _dateGroups).ToList();
                return _datarows;
            }
            catch(Exception e)
            {
                return null;
            }
            
            
        }
    }
}
