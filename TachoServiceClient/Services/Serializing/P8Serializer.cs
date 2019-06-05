using Newtonsoft.Json;

using System;


namespace TachoHelper.Services.Serializing
{
    public class P8Serializer : ISerializer
    {
        private Newtonsoft.Json.JsonSerializer serializer;

        public P8Serializer()
        {
            serializer = new Newtonsoft.Json.JsonSerializer();
        }

        public P8Serializer(JsonSerializerSettings settings)
        {
            serializer = Newtonsoft.Json.JsonSerializer.Create(settings);
        }

        public object Deserialize(Type type, byte[] data)
        {
            using (var ms = new System.IO.MemoryStream(data))
            using (var reader = new System.IO.StreamReader(ms))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return serializer.Deserialize(jsonReader, type);
            }
        }

        public T Deserialize<T>(byte[] data)
        {
            using (var ms = new System.IO.MemoryStream(data))
            using (var reader = new System.IO.StreamReader(ms))
            using (var jsonReader = new JsonTextReader(reader))
            {
                return serializer.Deserialize<T>(jsonReader);
            }
        }
        public string SerializeToBase64(object obj)
        {
           return  Convert.ToBase64String(Serialize(obj));
        }

        public T DeserializeFromBase64<T>(string  base64)
        {
            return Deserialize<T>(Convert.FromBase64String(base64));
        }

        public byte[] Serialize(object obj)
        {
            using (var ms = new System.IO.MemoryStream())
            using (var writer = new System.IO.StreamWriter(ms))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, obj);
                jsonWriter.Flush();

                return ms.ToArray();
            }
        }


    }
}