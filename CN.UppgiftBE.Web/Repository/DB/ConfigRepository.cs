using LinqToDB.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace CN.UppgiftBE.Web.Repository.DB
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
       
    }

    public class ConfigRepository : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();
        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                 new ConnectionStringSettings
                 {
                     Name = "CloudNineUppgift",
                     ProviderName = "System.Data.SqlClient",
                     ConnectionString = @"Data Source=(local);Initial Catalog=CloudNineUppgift;Integrated Security=False;User ID=sa;Password=1Fireside;Connect Timeout=10;MultipleActiveResultSets=True;Pooling=True;Max Pool Size=250;"
                 };
            }
        }
      

    }
}