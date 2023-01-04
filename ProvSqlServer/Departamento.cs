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
        public DtoLib.ResultadoLista<DtoLibInventario.Departamento.Resumen> 
            Departamento_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Departamento.Resumen>();
            try
            {
                using (var cnn = new FoxInvEntities(_cnInv.ConnectionString))
                {
                    var sql = @"select 
                                    auto, 
                                    nombre, 
                                    '' as codigo 
                                from productos_departamento";
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Departamento.Resumen>(sql).ToList();
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