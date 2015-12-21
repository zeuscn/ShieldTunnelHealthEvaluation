using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using IS3.Core;
using IS3.Core.Serialization;
using System.Data;
using System.IO;
using System.Reflection;

namespace ShieldTunnelHealthEvaluation.DataBaseManager
{
    public sealed class DbConnection
    {
        public OleDbConnection conn;
        public DbConnection()
        {
            string dbSource = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"\\TunnelHealthEvaluation.mdb" ;
            string connectString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbSource;
            conn=new OleDbConnection();
            conn.ConnectionString = connectString;
        }
        public void Open()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }
        public void Close()
        {
            if(conn.State==ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public int ExcuteNonQuery(string sql)
        {
            Open();
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            var result= cmd.ExecuteNonQuery();
            Close();
            return result;
        }
        public OleDbDataReader ExcuteReader(string sql)
        {
            Open();
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            var result=cmd.ExecuteReader();
            //Close();
            return result;
        }
        public object ExecuteScalar(string sql)
        {
            Open();
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            var result = cmd.ExecuteScalar();
            Close();
            return result;
        }
    }
}
