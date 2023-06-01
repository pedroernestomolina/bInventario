using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.TomaInv.GenerarToma
{
    public class Ficha
    {
        public string IdDepositoTomaInv { get; set; }
        public List<PrdToma> ProductosTomarInv { get; set; }
    }
}