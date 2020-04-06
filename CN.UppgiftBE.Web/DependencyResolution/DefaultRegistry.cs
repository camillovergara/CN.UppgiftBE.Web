// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CN.UppgiftBE.Web.DependencyResolution
{
    using CN.UppgiftBE.Web.Log;
    using CN.UppgiftBE.Web.Repository;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using System.Web;
    using RestSharp;
    using RestSharp.Serializers;
    using StructureMap.Pipeline;
    using CN.UppgiftBE.Web.Repository.DB;
    using CN.UppgiftBE.Web.Service;
    using CN.UppgiftBE.Web.Util;
    using CN.UppgiftBE.Web.Data;

    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            var clientAdress = "https://api.spotify.com/";
            var connectionStrings = "";
            var unique = new SingletonLifecycle();
            var _clientId = "9bbbfe31194c4df1ad8330dd2692aaa2";//;ConfigurationManager.AppSettings[""];
            var _clientSecret = "5eb99108fbd44027b6e257627eec3a0a";//;ConfigurationManager.AppSettings[""];


        Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });
;
            For<HttpContextBase>().Use((ctx=> new HttpContextWrapper(HttpContext.Current)));
            For<IRestClient>()
                    .Use(ctx =>
                        new RestClient(clientAdress));
           For<IRestMethods>()
                  .LifecycleIs(unique)
                  .Use(ctx =>
                      new RestMethods(
                          ctx.GetInstance<IRestClient>()));
           For<IRestRequestCreator>()
                    .Use(ctx =>
                        new RestRequestCreator(_clientId, _clientSecret));

            For<IFormatUtils>().Use<FormatUtils>();

            For<ICacheManagerService>().Use<CacheManagerService>();

            For<IDbContextFactory>()
              .Use(ctx =>
              new DbContextFactory(connectionStrings));

            For<IProductService>()
                   .LifecycleIs(unique)
                    .Use(ctx =>
                      new ProductService(
                          ctx.GetInstance<IDbContextFactory>()));

          
            For<IDataRepository>()
               .LifecycleIs(unique)
               .Use(ctx => 
             //      new DataRepository(new HttpContextWrapper(HttpContext.Current),
                     new DataRepository(
                       new HttpContextWrapper(HttpContext.Current),
                       ctx.GetInstance<IRestRequestCreator>(),
                       ctx.GetInstance<IRestMethods>(),
                       ctx.GetInstance<IProductService>(), 
                       ctx.GetInstance<ICacheManagerService>(),
                       ctx.GetInstance<ITokenService>()
                       ));

          

            #endregion
        }
    }
}