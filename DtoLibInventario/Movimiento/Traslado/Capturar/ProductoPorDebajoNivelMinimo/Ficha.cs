using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo
{
    
    public class Ficha
    {

        public string autoPrd { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string empCompra { get; set; }
        public int empCompraCont { get; set; }
        public string decimales { get; set; }
        public string autoDepartamento { get; set; }
        public string autoGrupo { get; set; }
        public string categoria { get; set; }
        //
        public string autoDeposito { get; set; }
        public string codigoDeposito { get; set; }
        public string nombreDeposito { get; set; }
        public decimal exDisponible { get; set; }
        public decimal exFisica { get; set; }
        public decimal exReserva { get; set; }
        public decimal nivelMinimo { get; set; }
        public decimal nivelOptimo { get; set; }
        //
        public string estatusDivisa { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal costoUnd { get; set; }
        //
        public string autoDepositoOrigen { get; set; }
        public string codigoDepositoOrigen { get; set; }
        public decimal exFisicaOrigen { get; set; }
        public decimal exDisponibleOrigen { get; set; }
        public decimal exReservaOrigen { get; set; }
        public string nombreDepositoOrigen { get; set; }
        //
        public DateTime fechaUltActualizacion { get; set; }
        public decimal tasaIva { get; set; }
        public string tasaIvaNombre { get; set; }
        //
        public decimal costo { get; set; }
        public string tasaAuto { get; set; }


        public Ficha() 
        {
            autoPrd = "";
            codigoPrd = "";
            nombrePrd = "";
            empCompra = "";
            empCompraCont = 0;
            decimales = "";
            autoDepartamento = "";
            autoGrupo = "";
            categoria = "";
            //
            autoDeposito = "";
            codigoDeposito = "";
            nombreDeposito = "";
            exDisponible = 0.0m;
            exFisica = 0.0m;
            exReserva = 0.0m;
            nivelMinimo = 0.0m;
            nivelOptimo = 0.0m;
            //
            estatusDivisa = "";
            costoDivisa = 0.0m;
            costoUnd = 0.0m;
            //
            autoDepositoOrigen = "";
            codigoDepositoOrigen = "";
            nombreDepositoOrigen = "";
            exFisicaOrigen = 0.0m;
            exDisponibleOrigen = 0.0m;
            exReservaOrigen = 0.0m;
            //
            fechaUltActualizacion = new DateTime(2000, 01, 01);
            tasaIva = 0.0m;
            tasaIvaNombre = "";
            //
            tasaAuto = "";
            costo = 0m;
        }

    }

}