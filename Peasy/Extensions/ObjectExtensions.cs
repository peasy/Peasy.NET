using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Facile.Extensions 
{
    public static class ObjectExtensions
    {
        public static string SerializeObject(object obj)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, obj);
            string value = System.Convert.ToBase64String(stream.ToArray());
            return value;
        }

        public static object DeserializeObject(string obj)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            string value = System.Text.Encoding.Default.GetString(Convert.FromBase64String(obj));
            MemoryStream stream = new MemoryStream(System.Text.Encoding.Default.GetBytes(value));
            return serializer.Deserialize(stream);
        }

        public static T DeserializeObject<T>(string obj)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            string value = System.Text.Encoding.Default.GetString(Convert.FromBase64String(obj));
            MemoryStream stream = new MemoryStream(System.Text.Encoding.Default.GetBytes(value));
            return (T)serializer.Deserialize(stream);
        }

        public static string ToStringHandleNull(this object value)
        {
            return value == null ? string.Empty : value.ToString();
        }
    }
}
