using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ShieldTunnelHealthEvaluation
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
            biFormattrr.Binder = new BindChanger();
            var item= (T)(biFormattrr.Deserialize(stream));
            stream.Close();
            return item;
        }
    }
    class BindChanger : System.Runtime.Serialization.SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            // Define the new type to bind to
            Type typeToDeserialize = null;
            // Create the new type and return it
            typeToDeserialize = Type.GetType(typeName);
            if (typeToDeserialize == null)
            {
                typeToDeserialize = Type.GetType(string.Format("{0},{1}", typeName, assemblyName));
            }
            return typeToDeserialize;
        }
    }
}
