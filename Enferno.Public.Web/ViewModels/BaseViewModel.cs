
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Enferno.Public.Web.ViewModels
{
    [Serializable]
    public class BaseViewModel
    {
        public string ToJson()
        {
            var serializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                TypeNameHandling = TypeNameHandling.All
            });

            string json;

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            using (var textWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(textWriter, this);
                textWriter.Flush();
                stream.Position = 0;
                var reader = new StreamReader(stream);
                json = reader.ReadToEnd();
            }
            return json;
        }
    }
}
