using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShielTunnelHealthEvaluation.CORE.FuzzyAHP
{
    public static class XMLIO
    {
        
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
            string filePath=@"..\..\Resources\Hierarchy.xml";
            //AHPIndex2Xml.XMLSerialization(filePath, TunnelHealIndex);
            AHPIndex2Xml.XMLSerialization(filePath, TunnelHealIndex);
        }
        public static   AHPIndexHierarchy ReadIndexHierarchyXml()
        {
            AHPIndexHierarchy TunnelHealIndex = new AHPIndexHierarchy();
            Serialization<AHPIndexHierarchy> Xml2AHPIndex = new Serialization<AHPIndexHierarchy>();
            string filePath = @"..\..\Resources\Hierarchy.xml"; 
            TunnelHealIndex = Xml2AHPIndex.XMLDeserialization(filePath);
            return TunnelHealIndex;
        }
        public static void OutputMatrixXml(JudgementMatrixInfos judgementMatrixInfos)
        {
            Serialization<JudgementMatrixInfos> Matrix2Xml = new Serialization<JudgementMatrixInfos>();
            string filePath = @"..\..\Resources\MatrixInfos.xml";
            Matrix2Xml.XMLSerialization(filePath, judgementMatrixInfos);
        }
        public static  JudgementMatrixInfos ReadMatriXml()
        {
            JudgementMatrixInfos judgementMatrixInfos = new JudgementMatrixInfos();
            Serialization<JudgementMatrixInfos> xml2Matrix = new Serialization<JudgementMatrixInfos>();
            string filePath = @"..\..\Resources\MatrixInfos.xml";
            judgementMatrixInfos = xml2Matrix.XMLDeserialization(filePath);
            return judgementMatrixInfos;
        }
    }
}
