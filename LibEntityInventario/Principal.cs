using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibEntityInventario
{
    public partial class invEntities : DbContext
    {
        public invEntities (string cn)
            : base(cn)
        {
        }
    }
}