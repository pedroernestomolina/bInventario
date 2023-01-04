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
        public DtoLib.ResultadoLista<DtoLibInventario.Marca.Resumen> 
            Marca_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Marca.Resumen>();
            try
            {
                using (var cnn = new FoxInvEntities(_cnInv.ConnectionString))
                {
                    var sql = @"SELECT 
                                    auto, 
                                    nombre
                                FROM productos_marca";
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Marca.Resumen>(sql).ToList();
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
    }
}