using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.UppgiftBE.Web.Repository.DB
{
    public interface IDbContextFactory
    {
        DbProduct CreateDbProduct();
     
    }
}
