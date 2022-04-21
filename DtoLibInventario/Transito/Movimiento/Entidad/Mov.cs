using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Transito.Movimiento.Entidad
{
    
    public class Mov
    {

        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string idSucOrigen { get; set; }
        public string idSucDestino { get; set; }
        public string idDepOrigen { get; set; }
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
        public string desMov { get; set; }
        public string desSucOrigen { get; set; }
        public string desSucDestino { get; set; }
        public string desDepOrigen { get; set; }
        public string desDepDestino { get; set; }
        public string desConcepto { get; set; }
        public string desUsuario { get; set; }
        public string estacionEquipo { get; set; }


        public Mov()
        {
            id = -1;
            fecha = DateTime.Now.Date;
            idConcepto = "";
            idDepOrigen = "";
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
            desMov = "";
            desSucDestino = "";
            desSucOrigen = "";
            desUsuario = "";
            desDepDestino = "";
            desDepOrigen = "";
            desUsuario = "";
            estacionEquipo = "";
        }

    }

}