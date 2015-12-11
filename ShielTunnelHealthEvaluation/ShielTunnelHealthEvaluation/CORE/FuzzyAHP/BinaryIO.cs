using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation.CORE.FuzzyAHP
{
    public static class BinaryIO
    {
        const string _matrixFilePath = @"H:\MyResearch\ShielTunnelHealthEvaluation\ShielTunnelHealthEvaluation\Resources\MatrixInfos.data";
        public static void OutputMatrix(DenseMatrix ds)
        {
            BinarySerialization<DenseMatrix>.Serialization(_matrixFilePath, ds);
        }
        public static DenseMatrix ReadMatrix()
        {
           return BinarySerialization<DenseMatrix>.Deserialization(_matrixFilePath);
        }
        public static void OutputMatrixInfosSet(JudgementMatrixInfosSet _matrixInfosSet)
        {
            BinarySerialization<JudgementMatrixInfosSet>.Serialization(_matrixFilePath,_matrixInfosSet);
        }
        public  static JudgementMatrixInfosSet ReadMatrixInfosSet()
        {
            if(File.Exists(_matrixFilePath))
            {
                return BinarySerialization<JudgementMatrixInfosSet>.Deserialization(_matrixFilePath);
            }
            else
            {
                return new JudgementMatrixInfosSet();
            }
        }
    }
}
