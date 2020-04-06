using System;
using System.Collections.Generic;
using System.Web;
using AutoMapper;
using NLog;
using CN.UppgiftBE.Web.EntityModels;
using CN.UppgiftBE.Web.Log;
using CN.UppgiftBE.Web.Models;
using CN.UppgiftBE.Web.Repository.DB;
using System.Linq;
using LinqToDB;
using CN.UppgiftBE.Web.Service;
using CN.UppgiftBE.Web.Util;
using RestSharp;

namespace CN.UppgiftBE.Web.Repository

{ 
    public class DataRepository : IDataRepository
    {

        private readonly HttpContextBase _httpContext;
        private readonly ITokenService _tokenService;
        private readonly IRestMethods _restMethods;
        private readonly IRestRequestCreator _restRequestCreator;
        private readonly IProductService _productService;
        private readonly ICacheManagerService _cacheManagerService;

        public DataRepository(HttpContextBase httpContext,IRestRequestCreator restRequestCreator, IRestMethods restMethods,IProductService ProductService, ICacheManagerService CacheManagerService, ITokenService tokenService)
        {
            _httpContext = httpContext;
            _restRequestCreator = restRequestCreator;
            _restMethods = restMethods;
            _productService = ProductService;
            _cacheManagerService = CacheManagerService;
            _tokenService = tokenService;
         
        }

        public SearchArtistResponse SearchSpotifyList()
        {
            var spotifySearchArtistResponse = _restMethods.Get<SearchArtistResponse>(_restRequestCreator.CreateRequestSearchArtists(_tokenService.GetToken()));
            return spotifySearchArtistResponse;
        }
        public GenresResponse GetAvailableGenres()
        {
            var genresResponse = _restMethods.Get<GenresResponse>(_restRequestCreator.CreateRequestGetAvailableGenres(_tokenService.GetToken()));
            return genresResponse;
        }


        public List<ProductDB> GetProductListDB()
        {
            var cache =  _cacheManagerService.Get<List<ProductDB>>("userID");
            if (cache != null) return cache;
            
            _productService.RemoveAllProducts();
            var productDB = _productService.GetProducts();
            _cacheManagerService.Add<List<ProductDB>>("userID", productDB, DateTime.Now.AddHours(1));

            return productDB;
        }


        public SearchArtistResponse SearchSpotifyList(string genre)
        {
            var spotifySearchArtistResponse = _restMethods.Get<SearchArtistResponse>(_restRequestCreator.CreateRequestSearchArtists(_tokenService.GetToken(),genre));
            return spotifySearchArtistResponse; ;
        }

        public SearchArtistResponse SearchSpotifyList(string genre, string year)
        {
            var spotifySearchArtistResponse = _restMethods.Get<SearchArtistResponse>(_restRequestCreator.CreateRequestSearchArtists(_tokenService.GetToken(),genre ,year));
            return spotifySearchArtistResponse; ;

        }

    }

}