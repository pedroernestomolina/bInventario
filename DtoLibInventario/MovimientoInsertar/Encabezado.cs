using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.MovimientoInsertar
{
    public class Encabezado
    {
        public string autoConcepto { get; set; }
        public string autoDepositoOrigen { get; set; }
        public string autoDepositoDestino { get; set; }
        public string autoRemision { get; set; }
        public string autoUsuario { get; set; }
        public string nota { get; set; }
        public string estatusAnulado { get; set; }
        public string usuario { get; set; }
        public string codUsuario { get; set; }
        public string estacion { get; set; }
        public string codConcepto { get; set; }
        public string desConcepto { get; set; }
        public string codDepositoOrigen { get; set; }
        public string desDepositoOrigen { get; set; }
        public string codDepositoDestino { get; set; }
        public string desDepositoDestino { get; set; }
        public string tipo { get; set; }
        public int renglones { get; set; }
        public string documentoNombre { get; set; }
        public string autorizado { get; set; }
        public decimal total { get; set; }
        public string situacion { get; set; }
        public string codigoSucursal { get; set; }
        public string cierreFtp { get; set; }
        public string estatusCierreContable { get; set; }
        public decimal factorCambio { get; set; }
        public decimal montoDivisa { get; set; }
        public Encabezado()
        {
            autoConcepto = "";
            autoDepositoDestino = "";
            autoDepositoOrigen = "";
            autoRemision = "";
            autoUsuario = "";
            nota = "";
            estatusAnulado = "";
            usuario = "";
            codUsuario = "";
            estacion = "";
            codConcepto = "";
            desConcepto = "";
            codDepositoOrigen = "";
            desDepositoOrigen = "";
            codDepositoDestino = "";
            desDepositoDestino = "";
            tipo = "";
            renglones = 0;
            documentoNombre = "";
            autorizado = "";
            total = 0m;
            situacion = "";
            codigoSucursal="";
            cierreFtp="";
            estatusCierreContable="";
            factorCambio=0m;
            montoDivisa=0m;
        }
    }
}