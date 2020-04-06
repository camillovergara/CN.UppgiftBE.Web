using CN.UppgiftBE.Web.EntityModels;
using CN.UppgiftBE.Web.Models;
using RestSharp;
namespace CN.UppgiftBE.Web.Repository
{
    public interface IRestRequestCreator
    {
        RestSharp.Serializers.ISerializer Serializer { get; }
        RestRequest CreateRequestToken();
        RestRequest CreateRequestSearchArtists(Token token);
        RestRequest CreateRequestSearchArtists(Token token,string genres);
        RestRequest CreateRequestSearchArtists(Token token, string genre, string year); 
        RestRequest CreateRequestGetAvailableGenres(Token token);
    }
}
