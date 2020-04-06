using NLog;

namespace CN.UppgiftBE.Web.Log
{
    public interface ILog<T>
    {
        ILogger GetLogger();
    }
}
