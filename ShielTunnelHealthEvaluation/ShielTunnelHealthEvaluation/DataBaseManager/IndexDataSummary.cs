using IS3.Core.Serialization;
using ShieldTunnelHealthEvaluation.CORE;
using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShieldTunnelHealthEvaluation.DataBaseManager
{
    class IndexDataSummary
    {
        DbContext _dbcontext;
        string _indexTableName = "dbo_TunnelHealthEvaluation_IndexTableSummary";
        List<IndexTabelSummaryInfo> _indexTableSumInfos = new List<IndexTabelSummaryInfo>();
        public List<IndexTabelSummaryInfo> IndexTableSumInfos { get{return _indexTableSumInfos;} set; }
        string _openDbFail = "数据库中缺少表dbo_TunnelHealthEvaluation_IndexTableSummary";
        public IndexDataSummary()
        {
            DbConnection _dbConnection = new DbConnection();
            _dbcontext = _dbConnection._dbContext;
            GetIndexTableSummary();
        }
        #region 读取indexsummarytable
        /// <summary>
        /// read table dbo_TunnelHealthEvaluation_IndexTableSummary and get values
        /// </summary>
        private void GetIndexTableSummary()
        {
            try
            {
                if (_dbcontext.Open())
                {
                    if (_dbcontext.IsTableExist(_indexTableName))
                    {
                        string _sql = string.Format("select * from {0}", _indexTableName);
                        DbDataReader _reader = _dbcontext.ExecuteCommand(_sql);
                        while (_reader.Read())
                        {
                            IndexTabelSummaryInfo _indexInfo = new IndexTabelSummaryInfo()
                            {
                                MyTunnelHealthIndex = (TunnelHealthIndex)_reader["IndexName"],
                                //MyTunnelHealthIndex=(TunnelHealthIndex)Enum.Parse(typeof(TunnelHealthIndex), (string)_reader["IndexName"]),
                                MyMonitorItemType = (MonitorItemType)_reader["IndexDataType"]
                            };
                            GetTableNameList((string)_reader["TableNames"], _indexInfo);
                            _indexTableSumInfos.Add(_indexInfo);
                        }
                        _dbcontext.Close();
                    }
                    else
                    {
                        Exception e = new Exception("数据库中缺少表dbo_TunnelHealthEvaluation_IndexTableSummary");
                        throw (e);
                    }
                }
                else
                {
                    Exception e = new Exception(_openDbFail);
                    throw (e);
                }
            }
            catch (Exception e)
            {
            }
        }
        /// <summary>
        /// convert mixed table names
        /// </summary>
        /// <param name="s"></param>
        /// <param name="_indexInfo"></param>
        private void GetTableNameList(string s, IndexTabelSummaryInfo _indexInfo)
        {
            var tableNameGroups = s.Split(new char[] { ';' });
            foreach (var _names in tableNameGroups)
            {
                AddTableName(_names, _indexInfo);
            }
        }
        /// <summary>
        /// differ the group, point, data tables of a group
        /// </summary>
        /// <param name="s"></param>
        /// <param name="_indexInfo"></param>
        private void AddTableName(string s, IndexTabelSummaryInfo _indexInfo)
        {
            var _names = s.Split(new char[] { ',' });
            switch (_indexInfo.MyMonitorItemType)
            {
                case MonitorItemType.MonitorGroup:
                    {
                        Debug.Assert(_names.Length == 3);
                        _indexInfo.GroupTableNames.Add(_names[0]);
                        _indexInfo.PointTableNames.Add(_names[1]);
                        _indexInfo.DataTableNames.Add(_names[2]);
                        break;
                    }
                case MonitorItemType.MonitorPoint:
                    {
                        Debug.Assert(_names.Length == 2);
                        _indexInfo.PointTableNames.Add(_names[0]);
                        _indexInfo.DataTableNames.Add(_names[1]);
                        break;
                    }
            }
        }
        #endregion

        #region 读取每个指标不同时期最差数值
        public void ReadAllWorstTable()
        {
           
        }
        private void ReadWorstData(IndexTabelSummaryInfo _indexInfo)
        {
            DbConnection _dbConnection = new DbConnection();
            DbContext _dbContext = _dbConnection._dbContext;
            try
            {
                if (_dbcontext.Open())
                {
                    switch(_indexInfo.MyMonitorItemType)
                    {
                        case MonitorItemType.MonitorGroup:
                            {

                                break;
                            }
                        case MonitorItemType.MonitorPoint:
                            {
                                var _dataTables=_indexInfo.DataTableNames;
                                double _maxValue=double.MinValue;
                                double _minValue=double.MaxValue;
                                foreach(var _aTable in _dataTables)//todo:选出每个时间下最不利值；
                                {
                                    string sql = string.Format("select max(value),min(value) from {0}", _aTable);
                                    var _value=_dbcontext.ExecuteCommand(sql);
                                    while(_value.Read())
                                    {
                                        _maxValue=_maxValue>(double)_value[0]?_maxValue:(double)_value[0];
                                        _minValue = _minValue < (double)_value[1] ? _minValue : (double)_value[1];
                                    }
                                    
                                }
                                
                                break;
                            }
                    }
                    _dbcontext.Close();
                }
                else
                {
                    throw new Exception(_openDbFail);
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion
    }
}
