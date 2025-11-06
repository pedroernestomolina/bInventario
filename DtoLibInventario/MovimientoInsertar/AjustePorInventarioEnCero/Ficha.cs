using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.MovimientoInsertar.AjustePorInventarioEnCero
{
    public class Ficha: BaseFicha
    {
        public List<Deposito> movDeposito { get; set; }
        public string idDepositoVerificar { get; set; }
        public Ficha()
            : base()
        {
            movDeposito = new List<Deposito>();
            idDepositoVerificar = "";
        }
    }
}
