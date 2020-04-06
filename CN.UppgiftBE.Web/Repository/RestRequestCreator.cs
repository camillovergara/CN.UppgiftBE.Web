using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CN.UppgiftBE.Web.EntityModels;
using CN.UppgiftBE.Web.Models;
using CN.UppgiftBE.Web.Serializers;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;

namespace CN.UppgiftBE.Web.Repository
{
    public class RestRequestCreator: IRestRequestCreator
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public RestRequestCreator(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }
        public RestSharp.Serializers.ISerializer Serializer => new RestSharpJsonNetSerializer(
              MissingMemberHandling.Ignore,
              NullValueHandling.Ignore,
              DefaultValueHandling.Ignore);

        public RestRequest CreateRequestToken()
        {
            var request = new RestRequest("api/token", Method.POST);
            request.AddParameter("grant_type", "client_credentials");
            request.AddHeader("Authorization", "Basic " + Base64Encode(_clientId + ":" + _clientSecret));
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = Serializer;
            return request;
        }
        public RestRequest CreateRequestSearchArtists(Token token)
        {
            var request = new RestRequest("v1/search", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("q", "genre:\"swedish hip hop\""); 
            request.AddParameter("type", "artist");
            request.AddParameter("market", "SE");
            request.AddHeader("Authorization", "Bearer " + token.AccessToken);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = Serializer;
            return request;
        }
        public RestRequest CreateRequestGetAvailableGenres(Token token)
        {
            var request = new RestRequest("v1/recommendations/available-genre-seeds", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("market", "SE");
            request.AddHeader("Authorization", "Bearer " + token.AccessToken);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = Serializer;
            return request;
        }
        public  string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public RestRequest CreateRequestSearchArtists(Token token,string genres)
        {
            var request = new RestRequest("v1/search", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("q", "year:1980-1990 genre:\"" + genres+"\"");
            request.AddParameter("type", "artist");
            request.AddParameter("market", "SE");
            request.AddHeader("Authorization", "Bearer " + token.AccessToken);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = Serializer;
            return request;
        }

        public RestRequest CreateRequestSearchArtists(Token token, string genre, string year)
        {
            var request = new RestRequest("v1/search", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("q", "year:"+year+" genre:\"" + genre + "\"");
            request.AddParameter("type", "artist");
            request.AddParameter("market", "SE");
            request.AddHeader("Authorization", "Bearer " + token.AccessToken);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = Serializer;
            return request;
        }
    }
}