using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.MovimientoInsertar.Traslado
{
    public class Ficha: BaseFicha
    {
        public List<Deposito> movDepositoOrigen{ get; set; }
        public List<Deposito> movDepositoDestino { get; set; }
        public Ficha()
            :base()
        {
            movDepositoOrigen = new List<Deposito>();
            movDepositoDestino = new List<Deposito>();
        }
    }
}