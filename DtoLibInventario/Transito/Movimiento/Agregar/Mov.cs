using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Transito.Movimiento.Agregar
{
    
    public class Mov
    {

        public string idSucOrigen { get; set; }
        public string idSucDestino { get; set; }
        public string idDeOrigen { get; set; }
        public string idDepDestino { get; set; }
        public string idConcepto { get; set; }
        public string autoriza { get; set; }
        public string motivo { get; set; }
        public decimal monto { get; set; }
        public decimal montoDivisa { get; set; }
        public decimal factorCambio { get; set; }
        public int cntRenglones { get; set; }
        public string codigoMov { get; set; }
        public string tipoMov { get; set; }
        public string descMov { get; set; }
        public string descSucOrigen { get; set; }
        public string descSucDestino { get; set; }
        public string descDepOrigen { get; set; }
        public string descDepDestino { get; set; }
        public string descConcepto { get; set; }
        public string descUsuario { get; set; }
        public string estacionEquipo { get; set; }


        public Mov()
        {
            idConcepto = "";
            idDeOrigen = "";
            idDepDestino = "";
            idSucDestino = "";
            idSucOrigen = "";
            autoriza = "";
            motivo = "";
            monto = 0m;
            montoDivisa = 0m;
            factorCambio = 0m;
            cntRenglones = 0;
            codigoMov = "";
            tipoMov = "";
            descMov = "";
            descSucDestino = "";
            descSucOrigen = "";
            descUsuario = "";
            descDepDestino = "";
            descDepOrigen = "";
            descUsuario = "";
            estacionEquipo = "";
        }

    }

}