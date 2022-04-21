using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Transito.Movimiento.Agregar
{
    
    public class Detalle
    {
        
        public string autoPrd { get; set; }
        public string autoDepart { get; set; }
        public string autoGrupo { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string catPrd { get; set; }
        public decimal exFisica { get; set; }
        public int contEmp { get; set; }
        public string nombreEmp { get; set; }
        public string decimales { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costo { get; set; }
        public string estatusDivisa {get;set;}
        public decimal costoDivisaUnd { get; set; }
        public decimal costoDivisa {get;set;}
        public string autoTasa {get;set;}
        public string descTasa {get;set;}
        public decimal valorTasa {get;set;}
        public string fechaUltActCosto { get; set; }
        //
        public decimal cantidadSolicitada { get; set; }
        public decimal costoSolicitada { get; set; }
        public string empaqueIdSolicitada { get; set; }
        public string ajusteIdSolicitada { get; set; }
        //
        public decimal nivelMinimo { get; set; }
        public decimal nivelOptimo { get; set; }
        public decimal exFisicaDestino{ get; set; }


        public Detalle() 
        {
            autoPrd = "";
            autoDepart = "";
            autoGrupo = "";
            codigoPrd = "";
            nombrePrd = "";
            catPrd = "";
            exFisica = 0m;
            contEmp = 0;
            nombreEmp = "";
            decimales = "";
            costoUnd = 0m;
            costo = 0m;
            estatusDivisa = "";
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
            cantidadSolicitada = 0m;
            empaqueIdSolicitada = "";
            ajusteIdSolicitada = "";
            costoSolicitada = 0m;
        }

    }

}