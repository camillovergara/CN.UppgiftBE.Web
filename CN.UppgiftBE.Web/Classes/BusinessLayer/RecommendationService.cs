using CN.UppgiftBE.Web.Data;
using CN.UppgiftBE.Web.EntityModels;
using CN.UppgiftBE.Web.Models;
using CN.UppgiftBE.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CN.UppgiftBE.Web.Classes.BusinessLayer
{
    public interface IRecommendationService
    {
        IEnumerable<Models.Artist> GetArtistByGenres(string[] genres);
        IEnumerable<Models.Artist> GetArtistBySearchRecomendation(SearchRecomendation searchRecomendation);
        IEnumerable<Models.Artist> GetArtistByGenreAndYear(string genre, string year);
        IEnumerable<Models.Artist> GetArtistByGenre( string genre);
        List<Models.Artist> Map(SearchArtistResponse searchArtistResponse);
    }
    public class RecommendationService : IRecommendationService
    {
        private readonly IDataRepository _datarepository;
        public RecommendationService(IDataRepository dataRepository) {
            _datarepository = dataRepository;
        }

        public IEnumerable<Models.Artist> GetArtistByGenre(string genre) => Map(_datarepository.SearchSpotifyList(genre));
        public IEnumerable<Models.Artist> GetArtistByGenreAndYear(string genre,string year) => Map(_datarepository.SearchSpotifyList(genre,year));

        public IEnumerable<Models.Artist> GetArtistByGenres(string[] genres)
        {
            var artistList = new List<Models.Artist>();
            foreach (var item in genres)
            {
                var recomendations = Map(_datarepository.SearchSpotifyList(item));
                artistList.AddRange(recomendations);

            }
            return artistList.Where(x => x.Popularity >= Constants.Popularity_70);
        }

        public IEnumerable<Models.Artist> GetArtistBySearchRecomendation(SearchRecomendation searchRecomendation)
        {
            var artistList = new List<Models.Artist>();
            foreach (var genre in searchRecomendation.Genres)
            {
                var recomendations = this.GetArtistByGenreAndYear(genre,searchRecomendation.Year);
                artistList.AddRange(recomendations);

            }
            return artistList.Where(x => x.Popularity >= Constants.Popularity_50);
        }

        public List<Models.Artist> Map(SearchArtistResponse searchArtistResponse)
        {

            var filterSearchArtistResponse = searchArtistResponse?.Artists.Items.Select(a => new Models.Artist
            {
                Name = a.Name,
                Genres = a.Genres,
                Id = a.Id,
                ImageUrl = a.Images.Select(i => i.Url).FirstOrDefault(),
                Popularity = a.Popularity

            }) ?? new List<Models.Artist>();

            return filterSearchArtistResponse.OrderByDescending(a => a.Popularity).ToList();

        }
    }
}