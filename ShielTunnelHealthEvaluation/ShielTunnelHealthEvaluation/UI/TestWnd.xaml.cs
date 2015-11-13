using System;
using System.Collections.Generic;
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
using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Data;

namespace ShieldTunnelHealthEvaluation.UI
{
    /// <summary>
    /// TestWnd.xaml 的交互逻辑
    /// </summary>
    public partial class TestWnd : Window
    {
        
        public TestWnd()
        {
            InitializeComponent();
        }

        private void btnSerialization_Click(object sender, RoutedEventArgs e)
        {
            //testHierarchy = new XMLIO();
            //testHierarchy.OutputIndexHierarchyXml();
            //testHierarchy.ReadXml();
        }

        private void btnTestWeigh_Click(object sender, RoutedEventArgs e)
        {
           // testHierarchy = new XMLIO();
           // testHierarchy.ReadIndexHierarchyXml();
           //DenseMatrix tempMatrix=new DenseMatrix(testHierarchy.TunnelHealIndex.ChildrenNames.Count);
        }

        private void btnTestDataTable_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt=new DataTable("testTable");
            dt.Columns.Add("testCol",typeof(string));
            DataRow row = dt.NewRow();
            row["testCol"] = "1";
            dt.Rows.Add(row);
            XMLIO.OutputDatatable(dt);
        }

        private void btnTestMatrix_Click(object sender, RoutedEventArgs e)
        {
            DenseMatrix ds = new DenseMatrix(1);
            ds[0, 0] = 1;
            //XMLIO.OutputTestMatrix(ds);
            //BinaryIO.OutputMatrix(ds);
            DenseMatrix ds2 = BinaryIO.ReadMatrix();
        }

        private void btnTestMath_Click(object sender, RoutedEventArgs e)
        {
            double[,] temp=new double[,]{{1,2},{2,1},{1,1}};
            DenseMatrix ds = DenseMatrix.OfArray(temp); 
            DenseVector dv = DenseVector.OfArray(new double[]{1,2,3});
            DenseMatrix ds2 = DenseMatrix.OfArray(new double[,]{{1, 2, 3}});
            var var1=ds2.Transpose();
            var var2 = ds.Column(0).Add(dv);
            var var3=ds.InsertColumn(1, dv);
            var var5= var3.RemoveColumn(1);
            DenseMatrix var4 =(DenseMatrix) var3;
            var result = dv * ds;
        }
    }
}
