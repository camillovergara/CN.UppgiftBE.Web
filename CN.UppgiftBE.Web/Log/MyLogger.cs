using NLog;
namespace CN.UppgiftBE.Web.Log
{
    public class MyLogger<T> : ILog<T>
    {
        private readonly Logger _logger;
        public MyLogger()
        {
            _logger = LogManager.GetLogger(typeof(T).Name);
        }

        public ILogger GetLogger()
        {
            return _logger;
        }
    }
}