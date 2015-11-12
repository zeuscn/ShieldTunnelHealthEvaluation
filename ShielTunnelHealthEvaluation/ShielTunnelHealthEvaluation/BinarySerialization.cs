using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ShielTunnelHealthEvaluation
{
    public static class BinarySerialization<T>
    {
        public static void Serialization(string filepath,T item)
        {
            Stream stream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            BinaryFormatter biFormattrr = new BinaryFormatter();
            biFormattrr.Serialize(stream, item);
            stream.Close();
        }
        public static T Deserialization(string filepath)
        {
            Stream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            BinaryFormatter biFormattrr = new BinaryFormatter();
            var item= (T)(biFormattrr.Deserialize(stream));
            stream.Close();
            return item;
        }
    }
}
