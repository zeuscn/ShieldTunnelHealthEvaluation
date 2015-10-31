using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using unvell.ReoGrid;
using mathNet = MathNet.Numerics;

namespace ShielTunnelHealthEvaluation.UI
{
    /// <summary>
    /// Weight.xaml 的交互逻辑
    /// </summary>
    public partial class Weight : Window
    {
        DataTable dt;
        List<string> sequences;
        DenseMatrix weighMatrix;
        public Weight(List<string> sequences, DenseMatrix weighMatrix)
        {
            Debug.Assert(sequences != null && sequences.Count > 0);
            this.sequences = sequences;
            if(weighMatrix!=null)
            {
                this.weighMatrix = weighMatrix;
            }
            else
            {
                weighMatrix = new DenseMatrix(sequences.Count);
            }
            InitializeComponent();
            //addCol();
            //this.dgWeight.ItemsSource = dt.DefaultView;
            List<string> test = new List<string>() { "a", "b" };
            DenseMatrix dm = new DenseMatrix(2);
            dm[0, 0] = 1;
            dm[0, 1] = 2;
            initialData();
        }
        private void addCol()
        {
            //List<string> test = new List<string>() { "tests1", "test2" };
            //DataGridTextColumn testcolumn = new DataGridTextColumn();
            //testcolumn.hea
            //this.dgWeight.Columns.Add();
            dt = new DataTable();
            dt.Columns.Add("Col1", System.Type.GetType("System.String"));
            dt.Columns.Add("Col2", System.Type.GetType("System.String"));
            DataRow row1 = dt.NewRow();
            row1["Col1"] = "Rc1";
            row1["Col2"] = "Rc2";
            dt.Rows.Add(row1);
        }
        private void initialData()
        {
            Worksheet mySheet = this.reoWeigh.CurrentWorksheet;
            mySheet.SetRows(sequences.Count);
            mySheet.SetCols(sequences.Count);
            for (int i = 0; i < sequences.Count; i++)
            {
                mySheet.RowHeaders[i].Text = sequences[i];
                mySheet.ColumnHeaders[i].Text = sequences[i];
                if (weighMatrix != null)
                {
                    for (int j = 0; j < sequences.Count; j++)
                    {
                        mySheet[i, j] = weighMatrix[i, j];
                    }
                }
            }

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Worksheet mySheet = this.reoWeigh.CurrentWorksheet;
            for (int i = 0; i < sequences.Count; i++)
            {
                for (int j = 0; j < sequences.Count; j++)
                {
                    mySheet[i, j] = weighMatrix[i, j];
                }
            }
        }
    }
}
