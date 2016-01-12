using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace ShieldTunnelHealthEvaluation.CORE.FuzzyAHP
{
    /// <summary>
    /// 指标的数据结构
    /// </summary>
    [Serializable]
    public class AHPIndexHierarchy : INotifyPropertyChanged 
    {
        #region private property
        private List<string> _childNames;
        private List<AHPIndexHierarchy> _children = new List<AHPIndexHierarchy>();
        private double indexValue;
        #endregion
        public string Name
        {
            get;
            set;
        }
        public AHPIndexValueType IndexType { get; set; }
        [XmlIgnore]
        public IndexOptimizationType IndexOptimType { get; set; }
        [XmlIgnore]
        public object OriginValue { get; set; } ///标准化前的值
        public IndexStardrizationType StdType { get; set; }///标准化类型
        [XmlIgnore]
        public string Grade
        {
            get
            {
                if(FuzzyValue==null)
                {
                    string grade = EvaluatedGradeInfo.Indexvalue2Grade(IndexValue).Clone().ToString();
                    return grade;
                }
                else
                {
                    string grade = EvaluatedGradeInfo.Memebership2Grade(FuzzyValue).Clone().ToString();
                    return grade;
                }
            }
        }
        [XmlIgnore]
        public double Weight { get; set; }///权重
        [XmlIgnore]
        public double IndexValue {
            get { return indexValue; }
            set { if (indexValue != value) { this.indexValue = value; notifyPropertyChanged("IndexValue"); } } 
        }///值
        [XmlIgnore]
        public int level { get; set; }///所在层
        [XmlIgnore]
        public DenseVector ChildrenWeightVector { get; set; }///子节点的权重向量
        [XmlIgnore]
        public DenseMatrix ChildrenFuzzyMatrix { get; set; }///子节点的模糊矩阵
        [XmlIgnore]
        public DenseVector FuzzyValue { get; set; }///指标的模糊向量
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
        }///指标的子节点名字
        [XmlIgnore]
        public AHPIndexHierarchy Parent { get; set; }///父节点
        public List<AHPIndexHierarchy> Children { get { return _children; } set { _children = value; } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void notifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    /// <summary>
    /// 将指标体系转为list结构，计算各指标的层次，并提供根据name，层数的检索功能
    /// </summary>
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
        /// <summary>
        /// 将树状的指标体系转为list结构
        /// </summary>
        /// <param name="_ahpIndexHierarchy"></param>
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
        /// <summary>
        /// 迭代计算指标所在的层号
        /// </summary>
        /// <param name="_ahpIndexHierarchy"></param>
        private void  CalculateLevelIteration(AHPIndexHierarchy _ahpIndexHierarchy)
        {
            levelId++;
            if(_ahpIndexHierarchy.Parent==null)
            {
                return;
            }
            else
            {
                CalculateLevelIteration(_ahpIndexHierarchy.Parent);
            }
        }
        /// <summary>
        /// 计算评估体系的层数
        /// </summary>
        private void CalculateLevel()
        {
            int maxLevelId = -1;
            foreach(AHPIndexHierarchy _ahpIndex in ahpIndexList)
            {
                levelId = -1;
                CalculateLevelIteration(_ahpIndex);
                _ahpIndex.level = levelId;
                if(levelId>maxLevelId)
                {
                    maxLevelId = levelId;
                }
            }
            totalLevelCount = maxLevelId + 1;
        }
        /// <summary>
        /// 根据指标的name检索相应指标
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AHPIndexHierarchy FindbyName(string name)
        {
            return ahpIndexList.Find(a => a.Name == name);
        }
        /// <summary>
        /// 寻找某一层次的指标
        /// </summary>
        /// <param name="levelId"></param>
        /// <returns></returns>
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
    public enum IndexOptimizationType
    {
        Undefined=0,
        Positive=1,
        Middle=2,
        Negative=3
    }
}
