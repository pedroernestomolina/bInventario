using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibInventario.Sistema.TipoDocumento.Entidad.Ficha> 
            Sistema_TipoDocumento_GetFichaById(string autoId)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Sistema.TipoDocumento.Entidad.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value =autoId;
                    var sql = @"SELECT auto as autoId, tipo, codigo, nombre, signo, siglas
                                FROM sistema_documentos 
                                WHERE auto=@p1";
                    var ent = cnn.Database.SqlQuery<DtoLibInventario.Sistema.TipoDocumento.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID DOCUMENTO ] NO ENCONTRADO";
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
        public DtoLib.ResultadoLista<DtoLibInventario.Sistema.HndPrecios.Lista.Ficha> 
            Sistema_TipoPreciosDefinidos_Lista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Sistema.HndPrecios.Lista.Ficha>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql = @"SELECT id, descrpcion as descripcion, codigo 
                                FROM empresa_hnd_precios
                                WHERE estatus='1'";
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Sistema.HndPrecios.Lista.Ficha>(sql).ToList();
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