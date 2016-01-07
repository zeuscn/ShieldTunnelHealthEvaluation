using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShieldTunnelHealthEvaluation.DataBaseManager;
using ShieldTunnelHealthEvaluation.UI;


namespace TestDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            DbConnection dbCon = new DbConnection();
        }

        private void btnTestCriteriaRead_Click(object sender, EventArgs e)
        {
            DbConnection dbConn = new DbConnection();
            TableCriteriaUtil tableCriteriaUtil = new TableCriteriaUtil(dbConn);
            tableCriteriaUtil.Read("test");
        }

        private void btnCriterias_Click(object sender, EventArgs e)
        {
        }

    }
}
