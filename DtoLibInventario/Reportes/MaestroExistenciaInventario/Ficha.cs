using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.MaestroExistenciaInventario
{
    
    public class Ficha
    {

        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public int contEmpCompra { get; set; }
        public int contEmpInv { get; set; }
        public string nombreEmpCompra { get; set; }
        public string nombreEmpInv { get; set; }
        public string nombreDepart { get; set; }
        public string nombreGrupo { get; set; }
        public string nombreDeposito { get; set; }
        public decimal eFisica { get; set; }


        public Ficha() 
        {
            codigoPrd = "";
            contEmpCompra = 0;
            contEmpInv = 0;
            eFisica = 0m;
            nombreDepart = "";
            nombreEmpCompra = "";
            nombreEmpInv = "";
            nombreGrupo = "";
            nombrePrd = "";
            nombreDeposito = "";
        }

    }

}