using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ShielTunnelHealthEvaluation.CORE.FuzzyAHP
{
    [Serializable]
    public class AHPIndex
    {
        public string Name { get; set; }
        public AHPIndexType IndexType { get; set; }
        [XmlIgnore]
        public object OriginValue { get; set; }
        public IndexStardrizationType StdType { get; set; }
        [XmlIgnore]
        public double Value { get; set; }
        [XmlIgnore]
        public AHPIndex Parent { get; set; }
        public List<AHPIndex> Children { get; set; }
        public AHPIndex()
        {
            Children = new List<AHPIndex>();
        }

    }
    public enum AHPIndexType
    {
        Undefined=0,
        Text=1,
        SingleValue=2,
        SeriesValue=3
    }
    public enum IndexStardrizationType
    {
        Undefined=0,
        Pessimistic=1,
        Normal=2,
        Optimistic=3
    }
}
