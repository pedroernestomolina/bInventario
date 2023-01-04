using EntitySqlServer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqlServer
{
    
    public class Helpers
    {

        static public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Modulo(string autoGrupoUsuario, string codigoFuncion)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();
            try
            {
                using (var cnn = new FoxInvEntities( Provider._cnInv.ConnectionString))
                {
                    var p1 = new SqlParameter("@p1",autoGrupoUsuario);
                    var p2 = new SqlParameter("@p2", codigoFuncion);
                    var sql = @"select 
                                    estatus, 
                                    seguridad 
                                from grupo_opciones
                                where codigo_grupo=@p1 and codigo_opcion=@p2";
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>(sql, p1, p2).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
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