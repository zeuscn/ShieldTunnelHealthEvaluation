using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ShieldTunnelHealthEvaluation
{
    public class Serialization<T>
    {
       public void XMLSerialization(string filePath,T item)  //todo:修改为在xml后面直接添加而不是重头添加
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read);
            xs.Serialize(stream, item);
            stream.Close();
        }
        public T XMLDeserialization(string filePath)
       {
           T item;
           XmlSerializer xs = new XmlSerializer(typeof(T));
           Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
           item =(T) xs.Deserialize(stream);
           stream.Close();
           return item;
       }
    }
}
