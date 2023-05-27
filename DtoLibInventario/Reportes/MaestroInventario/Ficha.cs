using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.MaestroInventario
{
    public class Ficha
    {
        private int contenidoCompras { get; set; }
        private decimal costoDivisa { get; set; }


        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string referenciaPrd { get; set; }
        public string modeloPrd { get; set; }
        public string estatusPrd { get; set; }
        private string estatusDivisaPrd { get; set; }
        private string estatusCambioPrd { get; set; }
        public string departamento { get; set; }
        public string grupo { get; set; }
        public decimal? existencia { get; set; }
        public decimal costoUnd { get; set; }
        public string decimales { get; set; }
        public decimal? pn1 { get; set; }
        public decimal? pn2 { get; set; }
        public decimal? pn3 { get; set; }
        public decimal? pn4 { get; set; }
        public decimal? pn5 { get; set; }
        public string codigoSuc { get; set; }
        public string nombreGrupo { get; set; }
        public string precioId { get; set; }


        public decimal costoDivisaUnd 
        {
            get 
            {
                var rt = 0.0m;
                rt = costoDivisa / contenidoCompras;
                return rt;
            } 
        }

        public enumerados.EnumAdministradorPorDivisa admDivisa
        {
            get
            {
                var rt = DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.No;
                if (estatusDivisaPrd.Trim().ToUpper() == "1")
                    rt = DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.Si;
                return rt;
            }
        }
    }
}