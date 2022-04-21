using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Configuracion.MetodoCalculoUtilidad.CapturarData
{
    
    public class Ficha
    {

        public string idProducto { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costoDivisa { get; set; }
        public int contenidoEmpCompra { get; set; }
        public decimal tasaIva { get; set; }
        public string estatusDivisa { get; set; }
        public decimal utilidad_1 { get; set; }
        public decimal utilidad_2 { get; set; }
        public decimal utilidad_3 { get; set; }
        public decimal utilidad_4 { get; set; }
        public decimal utilidad_5 { get; set; }
        public int contenido_1 { get; set; }
        public int contenido_2 { get; set; }
        public int contenido_3 { get; set; }
        public int contenido_4 { get; set; }
        public int contenido_5 { get; set; }
        public decimal precio_1 { get; set; }
        public decimal precio_2 { get; set; }
        public decimal precio_3 { get; set; }
        public decimal precio_4 { get; set; }
        public decimal precio_5 { get; set; }


        public Ficha()
        {
            idProducto = "";
            costoUnd = 0.0m;
            costoDivisa=0.0m;
            tasaIva = 0.0m;
            estatusDivisa = "";
            contenidoEmpCompra = 0;
            utilidad_1 = 0.0m;
            utilidad_2 = 0.0m;
            utilidad_3 = 0.0m;
            utilidad_4 = 0.0m;
            utilidad_5 = 0.0m;
            contenido_1 = 0;
            contenido_2 = 0;
            contenido_3 = 0;
            contenido_4 = 0;
            contenido_5 = 0;
            precio_1 = 0.0m;
            precio_2 = 0.0m;
            precio_3 = 0.0m;
            precio_4 = 0.0m;
            precio_5 = 0.0m;
        }

    }

}