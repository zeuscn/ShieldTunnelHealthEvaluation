﻿using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShielTunnelHealthEvaluation.CORE.FuzzyAHP
{
    public class Calculation
    {
        AHPIndexHierarchy _ahpIndexHierarchy;
        JudgementMatrixInfosSet _judgementMatrixInfosSet;
        AHPIndexHierarchyUtil _ahpIndexUtil;
        public Calculation(AHPIndexHierarchy ahp)
        {
            _ahpIndexHierarchy = ahp;
            _judgementMatrixInfosSet = BinaryIO.ReadMatrixInfosSet();
            _ahpIndexUtil=new AHPIndexHierarchyUtil(ahp);
            InitialBaseData();
            CalculateWeightVctor();
            CalculateFuzzyMatrix();
            ShowResult();
        }

        private bool CheckHierarchyMatrixCorrespond() //todo
        {
            bool result = true;
            return result;
        }
        private void InitialBaseData()
        {
            List<AHPIndexHierarchy> baseAhpIndex = _ahpIndexUtil.FindbyLevel(AHPIndexHierarchyUtil.totalLevelCount - 1);
            foreach(AHPIndexHierarchy ahpIndex in baseAhpIndex)
            {
                ahpIndex.Value = 90;
            }
        }
        private void CalculateWeightVctor()
        {
            int _judgementMatrixCount = _judgementMatrixInfosSet.JudgementMatrixInfosList.Count; 
           foreach(JudgementMatrixInfos _judgementMatrixInfos in _judgementMatrixInfosSet.JudgementMatrixInfosList)
           {
               Dictionary<string, JudgementMatrixInfo> tempMatrixInfosDic = _judgementMatrixInfos.JudgeMatrixDic;
               foreach( KeyValuePair <string,JudgementMatrixInfo> kvp in tempMatrixInfosDic)
               {
                   DenseVector dv=kvp.Value.WeightVector;
                   List<string> childNames = kvp.Value.IndexsSequence;
                   AHPIndexHierarchy ahpIndexSelf = _ahpIndexUtil.FindbyName(kvp.Key);
                   ahpIndexSelf.ChildrenWeightVector = dv;
                   for (int i = 0; i < childNames.Count;i++ )
                   {
                       AHPIndexHierarchy ahpIndex = _ahpIndexUtil.FindbyName(childNames[i]);
                       ahpIndex.Weight += dv[i];
                   }
               }
           }
            foreach(AHPIndexHierarchy ahpIndex in _ahpIndexUtil.ahpIndexList)
            {
                if(ahpIndex.Weight!=null)
                {
                    ahpIndex.Weight /= _judgementMatrixCount;
                }
                if(ahpIndex.ChildrenWeightVector!=null)
                {
                    ahpIndex.ChildrenWeightVector  /= _judgementMatrixCount;
                }
            }
        }
        private void CalculateFuzzyMatrix()
        {
            MemberShipFun _memeberShipFun = new MemberShipFun();
            for(int i=AHPIndexHierarchyUtil.totalLevelCount-2;i>=0;i--)
            {
                List<AHPIndexHierarchy> iLevelAhpIndexs=_ahpIndexUtil.FindbyLevel(i);
                foreach(AHPIndexHierarchy _iLevelAhpIndex in iLevelAhpIndexs )
                {
                    List<string> childrenNames = _iLevelAhpIndex.ChildrenNames;
                    DenseMatrix _iLevelMatrix = new DenseMatrix(childrenNames.Count, MemberShipFun.HealthLevelCount);
                    DenseVector _childrenValue = new DenseVector(childrenNames.Count);  
                    for (int j = 0; j < childrenNames.Count;j++ )
                    {
                        string name = childrenNames[j];
                        AHPIndexHierarchy _ahpIndex = _ahpIndexUtil.FindbyName(name);
                        if(i==AHPIndexHierarchyUtil.totalLevelCount-2)//是底层
                        {
                            _ahpIndex.FuzzyValue = _memeberShipFun.TrapezoiMebership(_ahpIndex.Value);
                        }

                        _iLevelMatrix = (DenseMatrix) _iLevelMatrix.InsertRow(j, _ahpIndex.FuzzyValue);
                        _iLevelMatrix = (DenseMatrix)_iLevelMatrix.RemoveRow(j + 1);
                        //_ahpIndex.ChildrenFuzzyMatrix = (DenseMatrix)_iLevelMatrix;
                        _childrenValue[j] = _ahpIndex.Value;
                    }
                    _iLevelAhpIndex.ChildrenFuzzyMatrix = _iLevelMatrix;
                    _iLevelAhpIndex.Value = _iLevelAhpIndex.ChildrenWeightVector * _childrenValue;
                    _iLevelAhpIndex.FuzzyValue = FuzzyOperator.WeightedAverage(_iLevelAhpIndex.ChildrenWeightVector, _iLevelAhpIndex.ChildrenFuzzyMatrix);
                }
            }
        }
        public void ShowResult()
        {
            List<AHPIndexHierarchy> tunnelHealthIndex = _ahpIndexUtil.FindbyLevel(0);
            MessageBox.Show(tunnelHealthIndex[0].FuzzyValue.ToString());
        }
    }
}
