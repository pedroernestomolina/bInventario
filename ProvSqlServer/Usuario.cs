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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Usuario.Ficha> 
            Usuario_Buscar(DtoLibInventario.Usuario.Buscar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Usuario.Ficha>();
            try
            {
                using (var cnn = new FoxInvEntities(_cnInv.ConnectionString))
                {
                    var sql = @"select 
                                    usu.auto as autoUsu, 
                                    usu.nombre as nombreUsu, 
                                    '' as apellidoUsu,
                                    usu.codigo as codigoUsu, 
                                    usu.estatus as estatusUsu, 
                                    gru.auto as autoGru, 
                                    gru.nombre as nombreGru 
                                FROM usuarios as usu 
                                join grupo_usuario as gru on usu.auto=gru.auto 
                                where usu.codigo=@p1 and usu.clave=@p2";
                    var p1 = new SqlParameter("@p1", ficha.codigo);
                    var p2 = new SqlParameter("@p2", ficha.clave);
                    var ent = cnn.Database.SqlQuery<DtoLibInventario.Usuario.Ficha>(sql, p1, p2).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "USUARIO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    if (!ent.isActivo)
                    {
                        result.Mensaje = "USUARIO EN ESTADO INACTIVO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
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
        public DtoLib.Resultado 
            Usuario_ActualizarSesion(DtoLibInventario.Usuario.ActualizarSesion.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            return result;
        }
    }

}