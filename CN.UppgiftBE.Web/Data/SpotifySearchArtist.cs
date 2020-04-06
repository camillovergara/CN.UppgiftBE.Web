using CN.UppgiftBE.Web.EntityModels;
using CN.UppgiftBE.Web.Models;
using CN.UppgiftBE.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CN.UppgiftBE.Web.Data
{
    public interface ISpotifySearchArtist
    {
        IReadOnlyList<CN.UppgiftBE.Web.Models.Artist> Get();
    }
    public class SpotifySearchArtist : ISpotifySearchArtist
    {
        IReadOnlyList<CN.UppgiftBE.Web.Models.Artist> _searchList;
        private readonly IDataRepository _datarepository;
        public SpotifySearchArtist(IDataRepository DataRepository)
        {
            _datarepository = DataRepository;
            _searchList = Map(_datarepository.SearchSpotifyList());
        }
        public IReadOnlyList<CN.UppgiftBE.Web.Models.Artist> Get() => _searchList;
        private List<CN.UppgiftBE.Web.Models.Artist> Map(SearchArtistResponse searchArtistResponse)
        {
            var filterSearchArtistResponse = searchArtistResponse?.Artists.Items.Select(a => new CN.UppgiftBE.Web.Models.Artist
            {
                Name = a.Name,
                Genres = a.Genres,
                Id = a.Id,
                ImageUrl = a.Images.Select(i => i.Url).FirstOrDefault(),
                Popularity= a.Popularity

            }) ?? new List<CN.UppgiftBE.Web.Models.Artist>();

            return filterSearchArtistResponse.OrderByDescending(a=>a.Popularity).ToList();
        }
    }
}