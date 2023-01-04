using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EntitySqlServer
{

    public partial class FoxInvEntities : DbContext
    {

        public FoxInvEntities(string cn)
            : base(cn)
        {
        }

    }

}