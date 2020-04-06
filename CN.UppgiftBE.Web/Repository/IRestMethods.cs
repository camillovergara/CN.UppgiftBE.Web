using RestSharp;

namespace CN.UppgiftBE.Web.Repository
{

    public interface IRestMethods
    {
        T Get<T>(IRestRequest request) where T : class, new();

        T Post<T>(IRestRequest request) where T : class, new();

        T Delete<T>(IRestRequest request) where T : class, new();

        string Post(IRestRequest request);

        string Put(IRestRequest request);
    }
}
