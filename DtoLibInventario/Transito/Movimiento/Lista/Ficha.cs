using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Transito.Movimiento.Lista
{
    
    public class Ficha
    {

        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string motivo { get; set; }
        public decimal monto { get; set; }
        public decimal montoDivisa { get; set; }
        public decimal factorCambio { get; set; }
        public int cntRenglones { get; set; }
        public string descMov { get; set; }
        public string descSucOrigen { get; set; }
        public string descSucDestino { get; set; }
        public string descDepOrigen { get; set; }
        public string descDepDestino { get; set; }
        public string descConcepto { get; set; }
        public string descUsuario { get; set; }


        public Ficha()
        {
            id = -1;
            fecha = DateTime.Now.Date;
            motivo = "";
            monto = 0m;
            montoDivisa = 0m;
            factorCambio = 0m;
            cntRenglones = 0;
            descMov = "";
            descSucDestino = "";
            descSucOrigen = "";
            descUsuario = "";
            descDepDestino = "";
            descDepOrigen = "";
            descUsuario = "";
        }

    }

}