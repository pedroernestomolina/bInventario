using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Transito.Movimiento.Entidad
{
    
    public class Detalle
    {

        public string autoProd { get; set; }
        public string autoDepart { get; set; }
        public string autoGrupo { get; set; }
        public string codigoProd { get; set; }
        public string nombreProd { get; set; }
        public string categoriaProd { get; set; }
        public decimal exFisica { get; set; }
        public int contEmpaque { get; set; }
        public string descEmpaque { get; set; }
        public string decimales { get; set; }
        public decimal costo { get; set; }
        public decimal costoUnd { get; set; }
        public string esAdmDivisa { get; set; }
        public decimal costoDivisaUnd { get; set; }
        public decimal costoDivisa { get; set; }
        public string autoTasa { get; set; }
        public string descTasa { get; set; }
        public decimal valorTasa { get; set; }
        public string fechaUltActCosto { get; set; }
        //
        public decimal cantSolicitada { get; set; }
        public decimal costoSolicitado { get; set; }
        public string empaqueIdSolicitado { get; set; }
        public string ajusteIdSolicitado { get; set; }
        //
        public decimal nivelMinimo { get; set; }
        public decimal nivelOptimo { get; set; }
        public decimal exFisicaDestino { get; set; }
        //
        public int contEmpaqueInv { get; set; }
        public string descEmpaqueInv { get; set; }


        public Detalle()
        {
            autoProd = "";
            autoDepart = "";
            autoGrupo = "";
            codigoProd = "";
            nombreProd = "";
            categoriaProd = "";
            exFisica = 0m;
            contEmpaque= 0;
            descEmpaque= "";
            decimales = "";
            costoUnd = 0m;
            costo = 0m;
            esAdmDivisa= "";
            costoDivisa = 0m;
            costoDivisaUnd = 0m;
            autoTasa = "";
            descTasa = "";
            valorTasa = 0m;
            fechaUltActCosto = "";
            exFisicaDestino = 0m;
            nivelMinimo = 0m;
            nivelOptimo = 0m;
            //
            cantSolicitada = 0m;
            empaqueIdSolicitado = "";
            ajusteIdSolicitado = "";
            costoSolicitado = 0m;
            //
            contEmpaqueInv = 0;
            descEmpaqueInv = "";
        }

    }

}
