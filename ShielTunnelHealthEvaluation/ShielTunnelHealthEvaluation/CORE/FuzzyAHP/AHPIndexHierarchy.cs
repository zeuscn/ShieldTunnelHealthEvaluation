﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ShielTunnelHealthEvaluation.CORE.FuzzyAHP
{
    [Serializable]
    public class AHPIndexHierarchy
    {
        private List<string> _childNames;
        public string Name { get; set; }
        public AHPIndexValueType IndexType { get; set; }
        [XmlIgnore]
        public object OriginValue { get; set; }
        public IndexStardrizationType StdType { get; set; }
        [XmlIgnore]
        public double Value { get; set; }
        [XmlIgnore]
        public List<string> ChildrenNames
        {
            get
            {
                _childNames = new List<string>();
                if(Children!=null&&Children.Count>0)
                {
                    foreach(AHPIndexHierarchy child in Children)
                    {
                        _childNames.Add(child.Name);
                    }
                }
                return _childNames;
            }
        }
        [XmlIgnore]
        public AHPIndexHierarchy Parent { get; set; }
        public List<AHPIndexHierarchy> Children { get; set; }
        public AHPIndexHierarchy()
        {
            Children = new List<AHPIndexHierarchy>();
        }

    }
    public enum AHPIndexValueType
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
