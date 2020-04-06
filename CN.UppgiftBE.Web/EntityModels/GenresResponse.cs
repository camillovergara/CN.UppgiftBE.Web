using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CN.UppgiftBE.Web.EntityModels
{
    public class GenresResponse
    {
        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

    }
}