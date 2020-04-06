using CN.UppgiftBE.Web.EntityModels;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CN.UppgiftBE.Web.Repository.DB
{
    public class DbProduct : LinqToDB.Data.DataConnection
    {
        public DbProduct(string name) : base(name) { }
        public ITable<ProductDB> Products => GetTable<ProductDB>();
      
    }
}