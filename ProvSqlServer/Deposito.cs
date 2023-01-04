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
        public DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen> 
            Deposito_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen>();
            try
            {
                using (var cnn = new FoxInvEntities(_cnInv.ConnectionString))
                {
                    var sql = @"SELECT 
                                    ed.auto, 
                                    ed.codigo, 
                                    ed.nombre, 
                                    '1' as estatusActivo, 
                                    CASE WHEN auto='0000000001' then '1' ELSE '0' END
                                FROM depositos as ed";
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Deposito.Resumen>(sql).ToList();
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
        public DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen> 
            Deposito_GetListaBySucursal(string codSuc)
        {
            throw new NotImplementedException();
        }
    }

}