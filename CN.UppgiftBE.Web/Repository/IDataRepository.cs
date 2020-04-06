using CN.UppgiftBE.Web.EntityModels;
using CN.UppgiftBE.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.UppgiftBE.Web.Repository
{
    public interface IDataRepository
    {
        SearchArtistResponse SearchSpotifyList();
        SearchArtistResponse SearchSpotifyList(string genre);
        SearchArtistResponse SearchSpotifyList(string genre, string year); 
        List<ProductDB> GetProductListDB();
        GenresResponse GetAvailableGenres();
    }
}
