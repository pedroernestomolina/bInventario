using LibEntityInventario;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public static EntityConnectionStringBuilder _cnInv ;
        static MySqlConnectionStringBuilder _cnn2;
        private string _Instancia;
        private string _BaseDatos;
        private string _Usuario;
        private string _Password;



        public Provider(string instancia, string bd, string usu="root")
        {
            _Usuario = usu;
            _Password = "123";
            _Instancia = instancia;
            _BaseDatos = bd;
            setConexion();

            //CONEXION BD REMOTA: GODADDY
            //_Usuario = "leonuxBD";
            //_Password = "ghx_k!kibx+D";
            //_Instancia = "107.180.50.172";
            //_BaseDatos = "0001";
            //setConexion();
            
            _cnn2 = new MySqlConnectionStringBuilder();
            _cnn2.Database = "pitaTest";
            _cnn2.UserID = "leonuxBD";
            _cnn2.Password = "ghx_k!kibx+D";
            _cnn2.Server = "107.180.50.172";
        }


        private void setConexion()
        {
            _cnInv = new EntityConnectionStringBuilder();
            _cnInv.Metadata = "res://*/ModelLibInventario.csdl|res://*/ModelLibInventario.ssdl|res://*/ModelLibInventario.msl";
            _cnInv.Provider = "MySql.Data.MySqlClient";
            _cnInv.ProviderConnectionString = "data source=" + _Instancia + ";initial catalog=" + _BaseDatos + ";user id=" + _Usuario + ";Password=" + _Password + ";Convert Zero Datetime=True;";
        }

        public DtoLib.ResultadoEntidad<DateTime> 
            FechaServidor()
        {
            var result = new DtoLib.ResultadoEntidad<DateTime>();

            try
            {
                using (var ctx = new invEntities(_cnInv.ConnectionString))
                {
                    var fechaSistema = ctx.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                    result.Entidad = fechaSistema.Date;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Empresa.Data.Ficha> 
            Empresa_Datos()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Empresa.Data.Ficha>();

            try
            {
                using (var ctx = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = ctx.empresa.FirstOrDefault();
                    if (ent == null) 
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "REGISTRO ENTIDAD [ EMPRESA ] NO DEFINIDO";
                        return result;
                    }
                    var nr = new DtoLibInventario.Empresa.Data.Ficha()
                    {
                        CiRif = ent.rif,
                        DireccionFiscal = ent.direccion,
                        Nombre = ent.nombre,
                        Telefono = ent.telefono,
                    };
                    result.Entidad = nr;
                    var sql = @"select 
                                    deposito_principal as idDepPrincipal, 
                                    codigo_empresa as codEmpresa
                                from sistema";
                    var ent2= ctx.Database.SqlQuery<DtoLibInventario.Empresa.Data.FichaExtra>(sql).FirstOrDefault();
                    nr.extra = ent2;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            Empresa_Sucursal_TipoPrecioManejar(string codEmpresa)
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var ctx = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySqlParameter("@codSuc", codEmpresa);
                    var sql = @"SELECT 
                                    empHndPrc.codigo as codPrecio
                                FROM empresa_sucursal as empSuc
                                join empresa_grupo_ext as empGrpExt on empGrpExt.auto_empresagrupo=empSuc.autoempresagrupo
                                join empresa_hnd_precios empHndPrc on empHndPrc.id=empGrpExt.idempresahndprecio
                                where empSuc.codigo=@codSuc";
                    var ent = ctx.Database.SqlQuery<string>(sql,p1).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "CODIGO SUCURSAL [ EMPRESA ] NO DEFINIDO";
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