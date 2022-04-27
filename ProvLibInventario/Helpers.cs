using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibInventario
{
    
    public class Helpers
    {

        static public string 
            MYSQL_VerificaError(MySql.Data.MySqlClient.MySqlException ex) 
        {
            var msg = "";
            if (ex.Number == 1452)
            {
                msg= "FALLO EN CLAVE FORANEA" + Environment.NewLine + ex.Message;
                return msg;
            }
            if (ex.Number == 1451)
            {
                msg= "REGISTRO CONTIENE DATA RELACIONADA" + Environment.NewLine + ex.Message;
                return msg;
            }
            if (ex.Number == 1062)
            {
                msg= "CAMPO DUPLICADO" + Environment.NewLine + Environment.NewLine + ex.Message;
                return msg;
            }
            msg= ex.Message;
            return msg;
        }
        static public string 
            ENTITY_VerificaError(DbUpdateException ex)
        {
            var msg = "";
            var dbUpdateEx = ex as DbUpdateException;
            var sqlEx = dbUpdateEx.InnerException;
            if (sqlEx != null)
            {
                var exx = (MySql.Data.MySqlClient.MySqlException)sqlEx.InnerException;
                if (exx != null)
                {
                    if (exx.Number == 1452)
                    {
                        msg = "FALLO EN CLAVE FORANEA" + Environment.NewLine + exx.Message;
                        return msg;
                    }
                    if (exx.Number == 1451)
                    {
                        msg = "REGISTRO CONTIENE DATA RELACIONADA" + Environment.NewLine + exx.Message;
                        return msg;
                    }
                    if (exx.Number == 1062)
                    {
                        msg="CAMPO DUPLICADO" + Environment.NewLine + exx.Message;
                        return msg;
                    }
                }
            }
            msg = ex.Message;
            return msg;
        }
        static public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Modulo(string autoGrupoUsuario, string codigoFuncion)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities( Provider._cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1",autoGrupoUsuario);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", codigoFuncion);
                    var sql = @"select estatus, seguridad 
                                from usuarios_grupo_permisos 
                                where codigo_grupo=@p1 and codigo_funcion=@p2";
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