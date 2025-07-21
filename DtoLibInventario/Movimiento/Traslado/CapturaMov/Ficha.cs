using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Traslado.CapturaMov
{
    public class Ficha
    {
        public Data data { get; set; }
        public DataDepDestino dataDepDestino { get; set; }
        public Ficha()
        {
            data = new Data();
            dataDepDestino = new DataDepDestino();
        }
    }
}