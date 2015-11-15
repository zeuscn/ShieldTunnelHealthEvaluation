using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using IS3.Core;
using IS3.Core.Serialization;

namespace ShieldTunnelHealthEvaluation.DataBaseManager
{
    public class DbConnection
    {
        string _projectDatabasePath;
        public DbContext _dbContext;
        public DbConnection()
        {
            Project _pro = Globals.project;
            _projectDatabasePath = _pro.projDef.LocalDatabaseName;
            _dbContext = new DbContext(_projectDatabasePath);
        }
    }
}
