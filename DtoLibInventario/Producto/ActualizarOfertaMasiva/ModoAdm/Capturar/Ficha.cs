using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.ActualizarOfertaMasiva.ModoAdm.Capturar
{
    public class Ficha
    {
        public string idPrd { get; set; }
        public int idPrecio { get; set; }
        public decimal costoUnd { get; set; }
        public decimal pnetoVtaUnd { get; set; }
        public int contEmpVta { get; set; }
    }
}