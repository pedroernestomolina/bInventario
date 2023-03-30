using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.HistoricoPrecio.ModoAdm
{
    public class Data
    {
        public string empDescripcion { get; set; }
        public int empCont { get; set; }
        public string nota { get; set; }
        public DateTime fecha { get; set; }
        public string estacion { get; set; }
        public string hora { get; set; }
        public string usuCodigo { get; set; }
        public string usuNombre { get; set; }
        public decimal factorCambio { get; set; }
        public decimal netoMonLocal { get; set; }
        public decimal fullDivisa { get; set; }
        public string tipoEmpVta { get; set; }
        public string tipoPrecioVta { get; set; }
    }
}