using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Lista
{
    public class Resumen
    {
        public string autoId { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string usuario { get; set; }
        public string estacion { get; set; }
        public string docNro { get; set; }
        public int docRenglones { get; set; }
        public decimal docMonto { get; set; }
        public string docSituacion { get; set; }
        public string docSucursal { get; set; }
        public string docConcepto { get; set; }
        public string docMotivo { get; set; }
        public string depositoOrigen { get; set; }
        public string depositoDestino { get; set; }
        public string estatusAnulado { get; set; }
        public string tipo { get; set; }
        public bool isDocAnulado { get { return estatusAnulado == "1"; } }
        public string idDepOrigen { get; set; }
        public string idDepDestino { get; set; }
        public decimal? montoDivisa { get; set; }
        public enumerados.EnumTipoDocumento docTipo
        {
            get
            {
                var xtipo = DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.SinDefinir;
                switch (tipo)
                {
                    case "01":
                        xtipo = DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Cargo;
                        break;
                    case "02":
                        xtipo = DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Descargo;
                        break;
                    case "03":
                        xtipo = DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Traslado;
                        break;
                    case "04":
                        xtipo = DtoLibInventario.Movimiento.enumerados.EnumTipoDocumento.Ajuste;
                        break;
                }
                return xtipo;
            }
        }


        public Resumen()
        {
            autoId = "";
            fecha = DateTime.Now.Date;
            hora = "";
            usuario = "";
            estacion = "";
            docNro = "";
            docRenglones = 0;
            docMonto = 0.0m;
            docSituacion = "";
            docSucursal = "";
            docConcepto = "";
            docMotivo = "";
            depositoDestino = "";
            depositoOrigen = "";
            idDepOrigen = "";
            idDepDestino = "";
            montoDivisa = 0m;
        }
    }
}