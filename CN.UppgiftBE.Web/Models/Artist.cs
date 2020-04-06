using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CN.UppgiftBE.Web.Models
{
    public class Artist
    {
        public string  Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Popularity { get; set; }
        public List<Object> Genres { get; set; }
    }
}