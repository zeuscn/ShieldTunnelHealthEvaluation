using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShielTunnelHealthEvaluation.CORE.FuzzyAHP
{
    [Serializable]
    public class Hierarchy
    {
        public AHPIndex TunnelHealIndex { get; set; }
        public Hierarchy()
        {
            //TunnelHealIndex = new AHPIndex()
            //{
            //    Name = "TunnelHealth",
            //    IndexType = AHPIndexType.SingleValue
            //};
            //AHPIndex testLevel1 = new AHPIndex()
            //{
            //    Name = "Level1",
            //    IndexType = AHPIndexType.SingleValue
            //};
            //TunnelHealIndex.Children.Add(testLevel1);
            //testLevel1.Parent = TunnelHealIndex;
            //AHPIndex testLevel2Node1 = new AHPIndex()
            //{
            //    Name = "level2Node1",
            //    IndexType = AHPIndexType.Text
            //};
            //AHPIndex testLevel2Node2 = new AHPIndex()
            //{
            //    Name = "level2Node2",
            //    IndexType = AHPIndexType.SeriesValue
            //};
            //testLevel1.Children.Add(testLevel2Node1);
            //testLevel1.Children.Add(testLevel2Node2);
            //testLevel2Node1.Parent = testLevel1;
            //testLevel2Node1.Parent = testLevel1;
        }
        public void OutputXml()
        {
            Serialization<AHPIndex> AHPIndex2Xml = new Serialization<AHPIndex>();
            string filePath=@"..\..\Resources\Hierarchy.xml";
            //AHPIndex2Xml.XMLSerialization(filePath, TunnelHealIndex);
            AHPIndex2Xml.XMLSerialization(filePath, TunnelHealIndex);
        }
        public void ReadXml()
        {
            Serialization<AHPIndex> Xml2AHPIndex = new Serialization<AHPIndex>();
            string filePath = @"..\..\Resources\Hierarchy.xml"; ;
            TunnelHealIndex = Xml2AHPIndex.XMLDeserialization(filePath);
        }
    }
}
