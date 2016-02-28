using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ShieldTunnelHealthEvaluation.CORE.FuzzyAHP
{
    public static class XMLIO
    {
        //string filePath=@"..\..\Resources\Hierarchy.xml";
        //string _hierarchyFilePath = @"H:\MyResearch\ShielTunnelHealthEvaluation\ShielTunnelHealthEvaluation\Resources\Hierarchy.xml";
        //string filePath = @"..\..\Resources\MatrixInfos.xml";
        const string _matrixFilePath = @"E:\百度云同步盘\MyWorkGit\YRTTHE\ShielTunnelHealthEvaluation\ShielTunnelHealthEvaluation\Resources\MatrixInfos.xml";
        public readonly static string _hierarchyFilePath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString() + @"\Hierarchy.xml";
            //TunnelHealIndex = new AHPIndexHierarchy()
            //{
            //    Name = "TunnelHealth",
            //    IndexType = AHPIndexValueType.SingleValue
            //};
            //AHPIndexHierarchy testLevel1 = new AHPIndexHierarchy()
            //{
            //    Name = "Level1",
            //    IndexType = AHPIndexValueType.SingleValue
            //};
            //TunnelHealIndex.Children.Add(testLevel1);
            //testLevel1.Parent = TunnelHealIndex;
            //AHPIndexHierarchy testLevel2Node1 = new AHPIndexHierarchy()
            //{
            //    Name = "level2Node1",
            //    IndexType = AHPIndexValueType.Text
            //};
            //AHPIndexHierarchy testLevel2Node2 = new AHPIndexHierarchy()
            //{
            //    Name = "level2Node2",
            //    IndexType = AHPIndexValueType.SeriesValue
            //};
            //testLevel1.Children.Add(testLevel2Node1);
            //testLevel1.Children.Add(testLevel2Node2);
            //testLevel2Node1.Parent = testLevel1;
            //testLevel2Node1.Parent = testLevel1;
        public static void OutputIndexHierarchyXml(AHPIndexHierarchy TunnelHealIndex)
        {
            Serialization<AHPIndexHierarchy> AHPIndex2Xml = new Serialization<AHPIndexHierarchy>();
            AHPIndex2Xml.XMLSerialization(_hierarchyFilePath, TunnelHealIndex);
        }
        public static   AHPIndexHierarchy ReadIndexHierarchyXml()
        {
            AHPIndexHierarchy TunnelHealIndex = new AHPIndexHierarchy();
            Serialization<AHPIndexHierarchy> Xml2AHPIndex = new Serialization<AHPIndexHierarchy>();
            TunnelHealIndex = Xml2AHPIndex.XMLDeserialization(_hierarchyFilePath);
            SetAhpParent(TunnelHealIndex);
            return TunnelHealIndex;
        }
        private static void SetAhpParent(AHPIndexHierarchy ahpIndexHierarchy)
        {
            if(ahpIndexHierarchy.Children!=null)
            {
                foreach(AHPIndexHierarchy ahpIndexChild in ahpIndexHierarchy.Children)
                {
                    ahpIndexChild.Parent = ahpIndexHierarchy;
                    SetAhpParent(ahpIndexChild);
                }
            }
        }
        public static void OutputMatrixXml(JudgementMatrixsGroup judgementMatrixInfos)
        {
            Serialization<JudgementMatrixsGroup> Matrix2Xml = new Serialization<JudgementMatrixsGroup>();
           // string _matrixFilePath = Directory.GetCurrentDirectory().ToString() + @"tools\Hierarchy.xml";
            Matrix2Xml.XMLSerialization(_matrixFilePath, judgementMatrixInfos);
        }
        public static  JudgementMatrixsGroup ReadMatriXml()
        {
            JudgementMatrixsGroup judgementMatrixInfos = new JudgementMatrixsGroup();
            Serialization<JudgementMatrixsGroup> xml2Matrix = new Serialization<JudgementMatrixsGroup>();
            judgementMatrixInfos = xml2Matrix.XMLDeserialization(_matrixFilePath);
            return judgementMatrixInfos;
        }
        public static void OutputDatatable(DataTable dt)
        {
            Serialization<DataTable> dt2Xml = new Serialization<DataTable>();
            dt2Xml.XMLSerialization(_matrixFilePath, dt);
        }
        public static void OutputTestMatrix(DenseMatrix ds)
        {
            Serialization<DenseMatrix> dt2Xml = new Serialization<DenseMatrix>();
            dt2Xml.XMLSerialization(_matrixFilePath, ds);
        }
    }
}
