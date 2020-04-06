using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CN.UppgiftBE.Web.Repository.DB
{
    public class DbContextFactory : IDbContextFactory
    {
        readonly string _connStringName;
        public DbContextFactory(string connectionString)
        {
            _connStringName = connectionString;
        }
        public DbProduct CreateDbProduct()
        {
            return new DbProduct(_connStringName);
        }

    }
}