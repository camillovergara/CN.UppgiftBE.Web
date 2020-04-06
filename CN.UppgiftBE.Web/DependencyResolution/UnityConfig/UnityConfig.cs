using System;
using System.Configuration;
using System.Web;
using CN.UppgiftBE.Web.Log;
using CN.UppgiftBE.Web.Repository;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace CN.UppgiftBE.Web.DependencyResolution.UnityConfig
{
    public class UnityConfig
    {
        public static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });
        private static void RegisterTypes(UnityContainer container)
        {
            var clientAdress = "https://accounts.spotify.com/";
            var connectionStrings = "";

            var _clientId = "9bbbfe31194c4df1ad8330dd2692aaa2";//;ConfigurationManager.AppSettings[""];
            var _clientSecret = "5eb99108fbd44027b6e257627eec3a0a";//;ConfigurationManager.AppSettings[""];

            container.RegisterFactory<IRestClient>("namn",c => new RestClient(clientAdress), new ContainerControlledLifetimeManager());
            container
                .RegisterType<IRestMethods, RestMethods>(
                    "DataRepositoryRestMethods",
                    new InjectionConstructor(
                        new ResolvedParameter<IRestClient>("namn"),
                        new ResolvedParameter<ILog<RestMethods>>()));
      


            container.RegisterFactory<HttpContextBase>(m => new HttpContextWrapper(HttpContext.Current));
        }
        public static IUnityContainer IocContainer => Container.Value;
    }
}