using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Traslado.CapturaMov
{
    public class DataDepDestino
    {
        public string idPrd { get; set; }
        public string codigoPrd { get; set; }
        public string descPrd { get; set; }
        public string idDep { get; set; }
        public string codigoDep { get; set; }
        public string descDep { get; set; }
        public decimal exFisica { get; set; }
        public int contEmpCompra { get; set; }
        public string descEmpCompra { get; set; }
        public DataDepDestino() 
        {
            idPrd = "";
            codigoPrd = "";
            descPrd = "";
            idDep = "";
            codigoDep = "";
            descDep = "";
            exFisica = 0m;
            contEmpCompra = 0;
            descEmpCompra = "";
        }
    }
}