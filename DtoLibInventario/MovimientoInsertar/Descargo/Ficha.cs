using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.MovimientoInsertar.Descargo
{
    public class Ficha: BaseFicha
    {
        public List<Deposito> movDeposito { get; set; }
        public Ficha()
            : base()
        {
            movDeposito = new List<Deposito>();
        }
    }
}