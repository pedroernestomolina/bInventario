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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Empresa.Data.Ficha> 
            Empresa_Datos()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Empresa.Data.Ficha>();
            try
            {
                using (var ctx = new FoxInvEntities(_cnInv.ConnectionString))
                {
                    var sql = @"select top(1)
                                    nombre as nombre, 
                                    direccion as direccionFiscal, 
                                    rif as cirif, 
                                    telefono_1  as telefono 
                                from empresa";
                    var ent = ctx.Database.SqlQuery<DtoLibInventario.Empresa.Data.Ficha>(sql).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "TABLA [ EMPRESA ] NO DEFINIDO";
                        return result;
                    }
                    ent.extra = new DtoLibInventario.Empresa.Data.FichaExtra()
                    {
                        codEmpresa = "01",
                        idDepPrincipal = "0000000001",
                    };
                    result.Entidad = ent;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
    }
}