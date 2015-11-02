using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS3.Core.Serialization
{
    public class TreeLoader
    {
        protected DbDataLoader _dataLoader;

        public TreeLoader(string dbFileName)
        {
            OdbcAdapter adapter = new OdbcAdapter(dbFileName);
            _dataLoader = new DbDataLoader(adapter);
        }

        public virtual void LoadObjs(Tree tree)
        {
            string tableNameSQL = tree.TableNameSQL;
            if (tableNameSQL == null)
                tableNameSQL = tree.Name;

            List<DGObject> objs = _dataLoader.ReadDGObjects(tableNameSQL,
                tree.DefNamesSQL, tree.OrderSQL, tree.ConditionSQL);

            tree.Objs = objs;
        }
    }
}
