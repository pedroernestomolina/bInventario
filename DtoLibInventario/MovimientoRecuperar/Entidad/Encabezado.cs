using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.MovimientoRecuperar.Entidad
{
    public class Encabezado
    {
        public string movId { get; set; }
        public string movNumero { get; set; }
        public DateTime movFecha { get; set; }
        public string docCodigo { get; set; }
        public string docNombre { get; set; }
        public string conceptoDesc { get; set; }
        public string conceptoCodigo { get; set; }
        public string depositoOrigenDesc { get; set; }
        public string depositoOrigenCodigo { get; set; }
        public string depositoDestinoDesc { get; set; }
        public string depositoDestinoCodigo { get; set; }
        public string movNotas { get; set; }
        public string movHora { get; set; }
        public string movEstacionEquipo { get; set; }
        public string movPersonaAutoriza { get; set; }
        public decimal movTotalMonedaLocal { get; set; }
        public decimal? movTotalMonedaRef { get; set; }
        public decimal? movFactorCambio { get; set; }
        public string movEstatusAnulado { get; set; }
        public string usuarioNombre { get; set; }
        public string usuarioCodigo { get; set; }
        public string sucursalDesc { get; set; }
        public string sucursalCodigo { get; set; }
    }
}