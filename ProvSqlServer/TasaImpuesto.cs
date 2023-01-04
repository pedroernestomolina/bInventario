using EntitySqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqlServer
{
    public partial class Provider : ILibInventario.IProvider
    {
        class fiscal 
        {
            public decimal tasa1 { get; set; }
            public decimal tasa2 { get; set; }
            public decimal tasa3 { get; set; }
        }
        public DtoLib.ResultadoLista<DtoLibInventario.TasaImpuesto.Resumen> 
            TasaImpuesto_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.TasaImpuesto.Resumen>();
            try
            {
                using (var cnn = new FoxInvEntities(_cnInv.ConnectionString))
                {
                    var sql="select top(1) tasa1, tasa2, tasa3 from fiscal";
                    var ent = cnn.Database.SqlQuery<fiscal>(sql).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "TASAS FISCAL NO DEFINIDA";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    var lst = new List<DtoLibInventario.TasaImpuesto.Resumen>();
                    var r1 = new DtoLibInventario.TasaImpuesto.Resumen() { auto = "01", nombre = "IVA", tasa = ent.tasa1 };
                    var r2 = new DtoLibInventario.TasaImpuesto.Resumen() { auto = "02", nombre = "IVA REDUCIDO", tasa = ent.tasa2 };
                    var r3 = new DtoLibInventario.TasaImpuesto.Resumen() { auto = "03", nombre = "IMPUETO AL LUJO", tasa = ent.tasa3 };
                    var r4 = new DtoLibInventario.TasaImpuesto.Resumen() { auto = "04", nombre = "EXENTO", tasa = 0m };
                    lst.Add(r1);
                    lst.Add(r2);
                    lst.Add(r3);
                    lst.Add(r4);
                    result.Lista = lst;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.TasaImpuesto.Resumen> 
            TasaImpuesto_GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}