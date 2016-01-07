using ShieldTunnelHealthEvaluation.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using MathNet.Numerics.LinearAlgebra.Double;

namespace ShieldTunnelHealthEvaluation.DataBaseManager
{
    public class TableCriteriaUtil
    {
        DbConnection dbConn;
        string tableName = "TunnelHealthCriteria";
        const string fieldProjectName = "Project";
        const string fieldIndexName = "IndexName";
        const string fieldLevelType = "LevelType";
        const string fieldCriterias = "Criterias";
        const string fieldIsAbsolute = "IsAbsolute";
        public TableCriteriaUtil(DbConnection dbC)
        {
            this.dbConn = dbC;
        }
        public List<SingleIndexCriteria> Read(string projectName)
        {
            if (!IsProjectExist(projectName))
            {
                var AllIndexCriterias=Read("test");//use the default criterias
                Insert(AllIndexCriterias, projectName);//update the project criteria with default criterias
                return AllIndexCriterias;
            }
            string sql = string.Format("select * from {0} where {1}=\"{2}\"", tableName,fieldProjectName,projectName);
            var reader= dbConn.ExcuteReader(sql);
            var criterias = Reader2Criterias(reader);
            return criterias;
        }
        public List<SingleIndexCriteria> Read()
        {
            string projectName="test";//todo
            string sql = string.Format("select * from {0} where {1}=\"{2}\"", tableName, fieldProjectName, projectName);
            var reader = dbConn.ExcuteReader(sql);
            var criterias = Reader2Criterias(reader);
            return criterias;
        }
        public void Update(List<SingleIndexCriteria> criterias,string projectName)
        {
            foreach(var singleCriteria in criterias)
            {
                string criteriaValues=singleCriteria.ToCriteriaValueString();
                string updateSql = string.Format("update {0} set {1}=\"{2}\" where {3}=\"{4}\" and {5}=\"{6}\"", tableName, fieldCriterias,criteriaValues , fieldProjectName, projectName, fieldIndexName, singleCriteria.IndexName);
                dbConn.ExcuteNonQuery(updateSql);
            }
        }
        public void Insert(List<SingleIndexCriteria> criterias, string projectName)
        {
            foreach (var singleCriteria in criterias)
            {
                string criteriaValues = singleCriteria.ToCriteriaValueString();
                string insertSql = string.Format("insert into {0} ( {1},{2},{3},{4},{5}) values (\"{6}\",\"{7}\",{8},\"{9}\",{10})", tableName, fieldProjectName, fieldIndexName,fieldLevelType,fieldCriterias,fieldIsAbsolute,projectName,singleCriteria.IndexName,singleCriteria.LevelType,criteriaValues,singleCriteria.IsAbsolute);
                dbConn.ExcuteNonQuery(insertSql);
            }
        }
        public bool IsProjectExist(string projectName)
        {
            string sql = string.Format("select * from {0} where {1}=\"{2}\"", tableName, fieldProjectName, projectName);
            var reader=dbConn.ExcuteReader(sql);
            if (reader.HasRows)
            {
                return true;
            }
            return false;
        }
        private DenseVector ConvertCriterias2Vector(string criterias)
        {
            try
            {
                var criteriaValues = criterias.Split(',');
                var criteriaDoubles = new double[criteriaValues.Length];
                for (int i = 0; i < criteriaValues.Length; i++)
                {
                    var singleCriteriaDouble = double.Parse(criteriaValues[i]);
                    criteriaDoubles[i] = singleCriteriaDouble;
                }
                var cirteriaVector = new DenseVector(criteriaDoubles);
                return cirteriaVector;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        private List<CriteriaDividing> ConvertCriterias2DivideValue(string criterias)
        {
            try
            {
                var criteriaDividings = new List<CriteriaDividing>();
                var criteriaValues = criterias.Split(',');
                //var criteriaDoubles = new double[criteriaValues.Length];
                for (int i = 0; i < criteriaValues.Length; i++)
                {
                    var singleCriteriaDouble = double.Parse(criteriaValues[i]);
                    //criteriaDoubles[i] = singleCriteriaDouble;
                    criteriaDividings.Add(new CriteriaDividing() { DividingValue = singleCriteriaDouble });
                }
                return criteriaDividings;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private List<SingleIndexCriteria> Reader2Criterias(OleDbDataReader reader)
        {
              if (reader==null)
           {
               return null;
           }
            if(reader.IsClosed)
            {
                throw (new Exception("Reader is closed!"));
            }
            List<SingleIndexCriteria> criterias = new List<SingleIndexCriteria>();
            while(reader.Read())
            {
                SingleIndexCriteria singleCriteria = new SingleIndexCriteria();
                for(int fieldCounter=0;fieldCounter<reader.FieldCount;fieldCounter++)
                {
                    switch(reader.GetName(fieldCounter))
                    {
                        case fieldProjectName:
                            singleCriteria.ProjectName = reader[fieldCounter].ToString();
                            break;
                        case fieldIndexName:
                            singleCriteria.IndexName = reader[fieldCounter].ToString();
                            break;
                        case fieldLevelType:
                            singleCriteria.LevelType = int.Parse(reader[fieldCounter].ToString());
                            break;
                        case fieldCriterias:
                            singleCriteria.CriteriaValues = ConvertCriterias2DivideValue(reader[fieldCounter].ToString());
                            break;
                        case fieldIsAbsolute:
                            singleCriteria.IsAbsolute = bool.Parse(reader[fieldCounter].ToString());
                            break;
                        default:
                            throw(new Exception("unexpected field!"));
                    }
                }
                criterias.Add(singleCriteria);
            }
            reader.Close();
            dbConn.Close();
            return criterias;
        }
    }
}
