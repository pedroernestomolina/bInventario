using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Kardex.Movimiento.Detalle
{
    
    public class Data
    {

        public string autoDeposito { get; set; }
        public string autoConcepto { get; set; }
        public string autoDocumento { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string documento { get; set; }
        public string moduloDoc { get; set; }
        public string codigoDoc { get; set; }
        public string siglasDoc { get; set; }
        public int signoDoc { get; set; }
        public decimal total { get; set; }
        public string entidad { get; set; }
        public decimal cantidad { get; set; }
        public decimal cantidadBono { get; set; }
        public decimal cantidadUnd { get; set; }
        public decimal costoUnd { get; set; }
        public bool isAnulado { get; set; }
        public string nota { get; set; }
        public decimal precioUnd { get; set; }
        public string codigoSucursal { get; set; }
        public Enumerados.EnumModulo Modulo { get; set; }
        public Enumerados.EnumSiglas Siglas { get; set; }

    }

}