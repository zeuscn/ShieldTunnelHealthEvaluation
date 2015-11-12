using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public double Weight { get; set; }
        [XmlIgnore]
        public double Value { get; set; }
        [XmlIgnore]
        public int level { get; set; }
        [XmlIgnore]
        public DenseVector ChildrenWeightVector { get; set; }
        [XmlIgnore]
        public DenseMatrix ChildrenFuzzyMatrix { get; set; }
        [XmlIgnore]
        public DenseVector FuzzyValue { get; set; }
        [XmlIgnore]
        public List<string> ChildrenNames
        {
            get
            {
                _childNames = new List<string>();
                if (Children != null && Children.Count > 0)
                {
                    foreach (AHPIndexHierarchy child in Children)
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
    public  class AHPIndexHierarchyUtil
    {
        public List<AHPIndexHierarchy> ahpIndexList;
        private int levelId=-1;
        public static int totalLevelCount;
        public AHPIndexHierarchyUtil(AHPIndexHierarchy _ahpIndexHierarchy)
        {
            ahpIndexList=new List<AHPIndexHierarchy>();
            Convert2List(_ahpIndexHierarchy);
            CalculateLevel();
        }
        private void Convert2List( AHPIndexHierarchy _ahpIndexHierarchy)
        {
            ahpIndexList.Add(_ahpIndexHierarchy);
            if(_ahpIndexHierarchy.Children!=null&&_ahpIndexHierarchy.Children.Count>0)
            {
                foreach(AHPIndexHierarchy ahpIndex in _ahpIndexHierarchy.Children)
                {
                    Convert2List(ahpIndex);
                }
            }
        }
        private void  CalculatreLevelIteration(AHPIndexHierarchy _ahpIndexHierarchy)
        {
            levelId++;
            if(_ahpIndexHierarchy.Parent==null)
            {
                return;
            }
            else
            {
                CalculatreLevelIteration(_ahpIndexHierarchy.Parent);
            }
        }
        private void CalculateLevel()
        {
            int maxLevelId = -1;
            foreach(AHPIndexHierarchy _ahpIndex in ahpIndexList)
            {
                levelId = -1;
                CalculatreLevelIteration(_ahpIndex);
                _ahpIndex.level = levelId;
                if(levelId>maxLevelId)
                {
                    maxLevelId = levelId;
                }
            }
            totalLevelCount = maxLevelId + 1;
        }
        public AHPIndexHierarchy FindbyName(string name)
        {
            return ahpIndexList.Find(a => a.Name == name);
        }
        public List<AHPIndexHierarchy> FindbyLevel(int levelId)
        {
            Debug.Assert(levelId < totalLevelCount);
            return ahpIndexList.FindAll(a => a.level == levelId);
        }
    }
    public enum AHPIndexValueType
    {
        Undefined = 0,
        Text = 1,
        SingleValue = 2,
        SeriesValue = 3
    }
    public enum IndexStardrizationType
    {
        Undefined = 0,
        Pessimistic = 1,
        Normal = 2,
        Optimistic = 3
    }
}
