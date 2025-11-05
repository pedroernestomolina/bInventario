using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.MovimientoInsertar
{
    public class Detalle 
    {
        public string autoProducto { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public decimal cantidad { get; set; }
        public decimal cantidadBono { get; set; }
        public decimal cantidadUnd { get; set; }
        public string categoria { get; set; }
        public string tipo { get; set; }
        public string estatusAnulado { get; set; }
        public int contEmpaque { get; set; }
        public string empaque { get; set; }
        public string decimales { get; set; }
        public decimal costoUnd { get; set; }
        public decimal total { get; set; }
        public decimal costoCompra { get; set; }
        public string estatusUnidad { get; set; }
        public int signo { get; set; }
        public string autoDepartamento { get; set; }
        public string autoGrupo { get; set; }
        public string cierreFtp { get; set; }
        public Detalle() 
        {
            autoProducto = "";
            codigoProducto = "";
            nombreProducto = "";
            cantidad=0m;
            cantidadBono = 0m;
            cantidadUnd = 0m;
            categoria = "";
            tipo = "";
            estatusAnulado = "";
            contEmpaque = 0;
            empaque = "";
            decimales = "";
            costoUnd = 0m;
            total = 0m;
            costoCompra = 0m;
            estatusUnidad = "";
            signo = 1;
            autoDepartamento = "";
            autoGrupo = "";
            cierreFtp = "";
        }
    }
}