using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.Kardex
{
    
    public class Mov
    {

        private string auto { get; set; }
        private string codigo { get; set; }
        private string nombre { get; set; }
        private string referencia { get; set; }
        private string modelo { get; set; }
        private string decimales { get; set; }

        private DateTime fecha { get; set; }
        private string hora { get; set; }
        private string modulo { get; set; }
        private string siglas { get; set; }
        private string documento { get; set; }
        private string nombre_deposito { get; set; }
        private decimal cantidad_und { get; set; }
        private string nombre_concepto { get; set; }
        private int signo { get; set; }
        private string entidad { get; set; }
        private decimal? exInicial { get; set; }


        public string autoPrd { get { return auto; } }
        public string codigoPrd { get { return codigo; } }
        public string nombrePrd { get { return nombre; } }
        public string referenciaPrd { get { return referencia; } }
        public string modeloPrd { get { return modelo; } }
        public string decimalesPrd { get { return decimales; } }
        public DateTime fechaMov { get { return fecha; } }
        public string horaMov { get { return hora; } }
        public string moduloMov { get { return modulo; } }
        public string siglasMov { get { return siglas; } }
        public string documentoNro { get { return documento; } }
        public string deposito { get { return nombre_deposito; } }
        public decimal cantidadUnd { get { return cantidad_und; } }
        public string concepto { get { return nombre_concepto; } }
        public int signoMov { get { return signo; } }
        public string entidadMov { get { return entidad; } }
        public decimal existenciaInicial 
        { 
            get 
            {
                var rt = 0.0m;
                if (exInicial.HasValue)
                    rt = exInicial.Value;
                return rt;
            } 
        }


        public Mov() 
        {
            auto = "";
            codigo="";
            nombre="";
            referencia="";
            modelo="";
            decimales="";
            fecha = DateTime.Now.Date;
            hora = "";
            modulo = "";
            siglas = "";
            documento = "";
            nombre_deposito = "";
            cantidad_und = 0m;
            nombre_concepto = "";
            signo = 1;
            entidad = "";
            exInicial = 0m;
        }

    }

}