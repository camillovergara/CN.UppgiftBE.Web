using System;
using CN.UppgiftBE.Web.EntityModels;
using CN.UppgiftBE.Web.Models;
using CN.UppgiftBE.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CN.UppgiftBE.Web.Classes.BusinessLayer;

namespace CN.UppgiftBE.Web.Classes.SpotifySearchProvider
{
    public interface ISpotifySearchProvider
    {
        IReadOnlyList<Models.Artist> GetArtists();
        IReadOnlyList<Models.Artist> GetRecomendation(string Genre);
        IReadOnlyList<Models.Artist> GetRecomendation(SearchRecomendation SearchRecomendation);
        IReadOnlyList<string> GetGenres();

    }
    public class SpotifySearchProvider : ISpotifySearchProvider
    {
    
        IReadOnlyList<string> _genres;
        private readonly IDataRepository _datarepository;
        readonly IRecommendationService _recommendationService;
        public SpotifySearchProvider(IDataRepository DataRepository, IRecommendationService recommendationService)
        {
            _datarepository = DataRepository;
            _genres = Map(_datarepository.GetAvailableGenres());
            _recommendationService = recommendationService;

        }
        public IReadOnlyList<CN.UppgiftBE.Web.Models.Artist> GetArtists() => _recommendationService.Map(_datarepository.SearchSpotifyList());
        public IReadOnlyList<string> GetGenres() => _genres;

        private List<string> Map(GenresResponse GenresResponse) => GenresResponse?.Genres.ToList() ?? new List<string>();
       
        public IReadOnlyList<Models.Artist> GetRecomendation(string Genre) => _recommendationService.GetArtistByGenre(Genre).ToList();
      
        public IReadOnlyList<Models.Artist> GetRecomendation(SearchRecomendation SearchRecomendation) => _recommendationService.GetArtistBySearchRecomendation(SearchRecomendation).ToList();
      
    }
}