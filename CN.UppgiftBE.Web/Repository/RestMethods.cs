using CN.UppgiftBE.Web.EntityModels;
using CN.UppgiftBE.Web.Log;
using NLog;
using RestSharp;
using System;
using System.Collections.Generic;

namespace CN.UppgiftBE.Web.Repository
{
    public class RestMethods : IRestMethods
    {
        private readonly IRestClient _restClient;
        public RestMethods(IRestClient restClient)
        {
            _restClient = restClient;
          
        }
    
        public T Get<T>(IRestRequest request) where T : class, new()
        {
          
            var result = _restClient.Get<T>(request);
           

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
               
                throw new Exception(result.ErrorMessage, result.ErrorException);
            }
            if (result.ResponseStatus == ResponseStatus.Error)
            {
               
                throw new Exception(result.Content);
            }

            return result.Data; 
        }

        public T Post<T>(IRestRequest request) where T : class, new()
        {
            var result = _restClient.Post<T>(request);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
               
                throw new Exception(result.ErrorMessage, result.ErrorException);
            }
            if (result.ResponseStatus == ResponseStatus.Error)
            {
              
                throw new Exception(result.Content);
            }

            return result.Data;
        }

        public T Delete<T>(IRestRequest request) where T : class, new()
        {
            var result = _restClient.Delete<T>(request);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
               
                throw new Exception(result.ErrorMessage, result.ErrorException);
            }
            if (result.ResponseStatus == ResponseStatus.Error)
            {
               
                throw new Exception(result.Content);
            }

            return result.Data;
        }

        public string Put(IRestRequest request)
        {
            var result = _restClient.Put(request);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
               
                throw new Exception(result.ErrorMessage, result.ErrorException);
            }
            if (result.ResponseStatus == ResponseStatus.Error)
            {
                throw new Exception(result.Content);
            }

            return result.Content;
        }

        public string Post(IRestRequest request)
        {
            var result = _restClient.Post(request);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
               
                throw new Exception(result.ErrorMessage, result.ErrorException);
            }
            if (result.ResponseStatus == ResponseStatus.Error)
            {
               
                throw new Exception(result.Content);
            }

            return result.Content;
        }
    }
}
