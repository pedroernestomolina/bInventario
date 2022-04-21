using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.CompraVentaAlmacen
{
    
    public class FichaCompra
    {

        public string documento { get; set; }
        public DateTime fecha { get; set; }
        public decimal cnt { get; set; }
        public string empaque { get; set; }
        public int contenido { get; set; }
        public decimal cntUnd { get; set; }
        public decimal factor { get; set; }
        public int signoDoc { get; set; }
        public string tipoDoc { get; set; }
        public decimal tneto { get; set; }
        public decimal xcostoUnd { get; set; }
        public string estatusAnulado { get; set; }
        public decimal costoUnd 
        { 
            get 
            {
                var rt = 0.0m;
                if (cntUnd != 0.0m) 
                {
                    rt = tneto / cntUnd; 
                }
                return rt; 
            }
        }
        public decimal costoDivisaUnd { get { return (costoUnd / factor); } }
        public bool isAnulado { get { return estatusAnulado == "1"; } }

    }

}