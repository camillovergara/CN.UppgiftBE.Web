using CN.UppgiftBE.Web.EntityModels;
using CN.UppgiftBE.Web.Repository;
using CN.UppgiftBE.Web.Util;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CN.UppgiftBE.Web.Service
{
    public interface ITokenService
    {
        Token GetToken();
    }
    public class TokenService : ITokenService
    {
        private readonly ICacheManagerService _cacheManagerService; 
        private readonly IRestRequestCreator _restRequestCreator;

        public TokenService(ICacheManagerService cacheManagerService, IRestRequestCreator restRequestCreator)
        {
            _cacheManagerService = cacheManagerService;
            _restRequestCreator = restRequestCreator;
        }
        public Token GetToken()
        {
            var client = new RestClient("https://accounts.spotify.com/");
            var CurrentToken = _cacheManagerService.Get<Token>("Token");

            if (!ValidateToken(CurrentToken))
            {
                var timeBeforeRequest = DateTime.Now;
                CurrentToken = client.Post<Token>(_restRequestCreator.CreateRequestToken()).Data;
                var expireTime = timeBeforeRequest.AddSeconds(CurrentToken.ExpiresIn);
                CurrentToken.Expires = expireTime;
                if (!ValidateToken(CurrentToken))
                    throw new Exception("Unable to validate token");

                _cacheManagerService.Add<Token>("Token", CurrentToken, CurrentToken.Expires);

            }
            return CurrentToken;

        }
        private bool ValidateToken(Token token)
        {

            return (!string.IsNullOrEmpty(token?.AccessToken) && token.Expires > DateTime.Now);
        }
        
        //private Token CurrentToken
        //{
        //    get
        //    {
        //        return _httpContext.Session?["Token"] as Token;
        //    }
        //    set
        //    {
        //        if (_httpContext.Session != null)
        //            _httpContext.Session["Token"] = value;
        //    }
        //}
    }
}