using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CN.UppgiftBE.Web.Models;

namespace CN.UppgiftBE.Web.ViewModels
{
    public class HomeViewModel
    {
        public IReadOnlyList<Product> ProductList { get; set; }
        public IReadOnlyList<Artist> ArtistList { get; set; }
        public IReadOnlyList<string> Genres { get; set; }
        public List<string> Genress { get; set; }
    }
}