using System.IO;
using Newtonsoft.Json;
using RestSharp.Serializers;

namespace CN.UppgiftBE.Web.Serializers
{
    public class RestSharpJsonNetSerializer : ISerializer
    {
        private readonly Newtonsoft.Json.JsonSerializer _serializer;

        public RestSharpJsonNetSerializer(MissingMemberHandling missingMemberHandling, NullValueHandling nullValueHandling, DefaultValueHandling defaultValueHandling)
        {
            ContentType = "application/json";
            _serializer = new Newtonsoft.Json.JsonSerializer
            {
                MissingMemberHandling = missingMemberHandling,
                NullValueHandling = nullValueHandling,
                DefaultValueHandling = defaultValueHandling
            };
        }

        public RestSharpJsonNetSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            ContentType = "application/json";
            _serializer = serializer;
        }

        public string Serialize(object obj)
        {
            var stringWriter = new StringWriter();
            using (var jsonTextWriter = new JsonTextWriter(stringWriter))
            {
                jsonTextWriter.Formatting = Formatting.Indented;
                jsonTextWriter.QuoteChar = '"';

                _serializer.Serialize(jsonTextWriter, obj);

                var result = stringWriter.ToString();
                return result;
            }
        }

        public string DateFormat { get; set; }
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string ContentType { get; set; }
    }
}