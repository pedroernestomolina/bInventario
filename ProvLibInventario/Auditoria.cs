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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Auditoria.Entidad.Ficha> 
            Auditoria_Documento_GetFichaBy(DtoLibInventario.Auditoria.Buscar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Auditoria.Entidad.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value = ficha.autoDocumento;
                    p2.ParameterName = "@p2";
                    p2.Value = ficha.autoTipoDocumento;
                    var sql= @"SELECT auto_usuario as usuAuto, codigo as usuCodigo, usuario as usuNombre, 
                                fecha, hora, estacion as estacionEquipo, memo as motivo
                                FROM auditoria_documentos 
                                WHERE auto_documento=@p1  and auto_sistema_documentos=@p2";
                    var ent = cnn.Database.SqlQuery<DtoLibInventario.Auditoria.Entidad.Ficha>(sql,p1,p2).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID DOCUMENTO / ID TIPO DOCUMENTO ] NO ENCONTRADO";
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

    }

}