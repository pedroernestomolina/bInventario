using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Traslado.Consultar
{

    public class ProductoPorDebajoNivelMinimo
    {

        public string autoProducto { get; set; }
        public string autoDepartamento { get; set; }
        public string autoGrupo { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public decimal cntUndReponer { get; set; }
        public string categoria { get; set; }
        public string empaqueCompra { get; set; }
        public int contenidEmpCompra { get; set; }
        public string decimales { get; set; }
        public decimal costoFinalUnd { get; set; }
        public decimal costoFinalCompra { get; set; }

    }

}