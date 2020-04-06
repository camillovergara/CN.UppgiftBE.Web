using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CN.UppgiftBE.Web.Classes.BusinessLayer;
using CN.UppgiftBE.Web.Classes.SpotifySearchProvider;
using CN.UppgiftBE.Web.Data;
using CN.UppgiftBE.Web.Models;
using CN.UppgiftBE.Web.Repository;
using CN.UppgiftBE.Web.ViewModels;
using Newtonsoft.Json;

namespace CN.UppgiftBE.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly ISpotifySearchProvider _spotifySearchProvider;

        public HomeController(ISpotifySearchProvider spotifySearchProvider)
        {
            _spotifySearchProvider = spotifySearchProvider;
        }

        public ActionResult Index()
        {
            var artistList = new List<Models.Artist>(); 
            var genres = _spotifySearchProvider.GetGenres();
            var homeViewModel = new HomeViewModel { ArtistList = artistList, Genres = genres };

            return View(homeViewModel);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetRecomendation(FormCollection collection)
        {
            var searchRecomendation = new SearchRecomendation();
            searchRecomendation.Year = collection["year"];
            collection.Remove("year");
            searchRecomendation.Genres = collection.AllKeys;
            
            var artistModel = _spotifySearchProvider.GetRecomendation(searchRecomendation);
            return PartialView("ListArtist", artistModel);
        }
      
    }
}