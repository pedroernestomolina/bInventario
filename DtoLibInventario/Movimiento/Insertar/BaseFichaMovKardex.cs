using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Insertar
{
    
    abstract public class BaseFichaMovKardex
    {

        public string autoProducto { get; set; }
        public decimal total { get; set; }
        public string autoDeposito { get; set; }
        public string autoConcepto { get; set; }
        public string modulo { get; set; }
        public string entidad { get; set; }
        public int signoMov { get; set; }
        public decimal cantidad { get; set; }
        public decimal cantidadBono { get; set; }
        public decimal cantidadUnd { get; set; }
        public decimal costoUnd { get; set; }
        public string estatusAnulado { get; set; }
        public string nota { get; set; }
        public decimal precioUnd { get; set; }
        public string codigoMov { get; set; }
        public string siglasMov { get; set; }
        public string codigoSucursal { get; set; }
        public string codigoConcepto { get; set; }
        public string nombreConcepto { get; set; }
        public string codigoDeposito { get; set; }
        public string nombreDeposito { get; set; }
        public decimal factorCambio { get; set; }

    }

}