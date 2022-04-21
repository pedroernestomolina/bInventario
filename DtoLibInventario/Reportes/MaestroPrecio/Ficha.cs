using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.MaestroPrecio
{
    
    public class Ficha
    {

        private string codigo { get; set; }
        private string nombre { get; set; }
        private decimal tasa { get; set; }
        private string referencia { get; set; }
        private string nombreDepartamento { get; set; }
        private string modelo { get; set; }
        private string estatus { get; set; }
        private string estatus_divisa { get; set; }
        private decimal precio_1 { get; set; }
        private decimal precio_2 { get; set; }
        private decimal precio_3 { get; set; }
        private decimal precio_4 { get; set; }
        private decimal precio_pto { get; set; }
        private decimal pdf_1 { get; set; }
        private decimal pdf_2 { get; set; }
        private decimal pdf_3 { get; set; }
        private decimal pdf_4 { get; set; }
        private decimal pdf_pto { get; set; }
        private DateTime fecha_cambio { get; set; }


        public string codigoPrd { get { return codigo; } }
        public string nombrePrd { get { return nombre; } }
        public string departamento { get { return nombreDepartamento; } }
        public string referenciaPrd { get { return referencia; } }
        public string modeloPrd { get { return modelo; } }
        public decimal tasaIvaPrd { get { return tasa; } }
        public decimal precioNeto_1 { get { return precio_1; } }
        public decimal precioNeto_2 { get { return precio_2; } }
        public decimal precioNeto_3 { get { return precio_3; } }
        public decimal precioNeto_4 { get { return precio_4; } }
        public decimal precioNeto_5 { get { return precio_pto; } }
        public decimal precioDivisaFull_1 { get { return pdf_1; } }
        public decimal precioDivisaFull_2 { get { return pdf_2; } }
        public decimal precioDivisaFull_3 { get { return pdf_3; } }
        public decimal precioDivisaFull_4 { get { return pdf_4; } }
        public decimal precioDivisaFull_5 { get { return pdf_pto; } }
        public DateTime fechaUltCambioPrd { get { return fecha_cambio; } }
        public string grupo { get; set; }

        public enumerados.EnumAdministradorPorDivisa isAdmDivisaPrd
        {
            get
            {
                var rt = DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.No;
                if (estatus_divisa.Trim().ToUpper() == "1")
                    rt = DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.Si;
                return rt;
            }
        }

        public enumerados.EnumEstatus estatusPrd
        {
            get
            {
                var rt = DtoLibInventario.Reportes.enumerados.EnumEstatus.Activo;
                if (estatus.Trim().ToUpper() != "ACTIVO")
                {
                    rt = DtoLibInventario.Reportes.enumerados.EnumEstatus.Inactivo;
                }
                return rt;
            }
        }

    }

}