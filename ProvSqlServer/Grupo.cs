using EntitySqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqlServer
{
    public partial class Provider : ILibInventario.IProvider
    {
        public DtoLib.ResultadoLista<DtoLibInventario.Grupo.Resumen> 
            Grupo_GetListaByDepartamento(string id)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Grupo.Resumen>();
            try
            {
                using (var cnn = new FoxInvEntities(_cnInv.ConnectionString))
                {
                    var sql = @"SELECT 
                                    pg.auto, 
                                    pg.nombre, 
                                    '' as codigo 
                                from productos_grupo as pg
                                join productos as p on pg.auto=p.auto_grupo 
                                join productos_departamento as pDepart on pDepart.auto=p.auto_departamento 
                                where pDepart.auto= @idDepart 
                                group by pg.auto, pg.nombre";
                    var p1 = new SqlParameter("@idDepart", id);
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Grupo.Resumen>(sql, p1).ToList();
                    result.Lista = list;
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