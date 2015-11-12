using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ShielTunnelHealthEvaluation.CORE.FuzzyAHP
{
    public static class ExtensionMethod
    {
        public static void WriteXml(this DenseMatrix denseMatrix,XmlWriter xmlWriter)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(double));
            xmlWriter.WriteStartElement("testMatrix");
            xmlWriter.WriteEndElement();
        }
    }
}
