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
            
            string sql = string.Format("select * from {0} where {1}=\"{2}\"", tableName,fieldProjectName,projectName);
            var reader= dbConn.ExcuteReader(sql);
            var criterias = Reader2Criterias(reader);
            return criterias;
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
                            singleCriteria.CriteriaValues = ConvertCriterias2Vector(reader[fieldCounter].ToString());
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
