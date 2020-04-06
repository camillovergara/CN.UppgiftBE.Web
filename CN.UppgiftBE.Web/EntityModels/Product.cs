using System;
using RestSharp.Deserializers;
using Newtonsoft.Json;

namespace CN.UppgiftBE.Web.EntityModels
{
    public class Product
    {
        [JsonProperty(PropertyName = "Id")] 
        [DeserializeAs(Name = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        [DeserializeAs(Name = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Price")]
        [DeserializeAs(Name = "Price")]
        public string Price { get; set; }
    }
}
