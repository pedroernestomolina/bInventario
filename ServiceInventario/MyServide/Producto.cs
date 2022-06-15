using ServiceInventario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.MyService
{

    public partial class Service: IService
    {

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Resumen> 
            Producto_GetLista(DtoLibInventario.Producto.Filtro filtro)
        {
            return ServiceProv.Producto_GetLista(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Ficha> 
            Producto_GetFicha(string autoPrd)
        {
            return ServiceProv.Producto_GetFicha(autoPrd);
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Estatus.Lista.Resumen> 
            Producto_Estatus_Lista()
        {
            return ServiceProv.Producto_Estatus_Lista();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Origen.Resumen> 
            Producto_Origen_Lista()
        {
            return ServiceProv.Producto_Origen_Lista();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Categoria.Resumen> 
            Producto_Categoria_Lista()
        {
            return ServiceProv.Producto_Categoria_Lista();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.AdmDivisa.Resumen> 
            Producto_AdmDivisa_Lista()
        {
            return ServiceProv.Producto_AdmDivisa_Lista();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Pesado.Resumen>
            Producto_Pesado_Lista()
        {
            return ServiceProv.Producto_Pesado_Lista();
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Oferta.Resumen> 
            Producto_Oferta_Lista()
        {
            return ServiceProv.Producto_Oferta_Lista();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Existencia> 
            Producto_GetExistencia(string autoPrd)
        {
            return ServiceProv.Producto_GetExistencia(autoPrd);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Precio> 
            Producto_GetPrecio(string autoPrd)
        {
            return ServiceProv.Producto_GetPrecio(autoPrd);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Costo> 
            Producto_GetCosto(string autoPrd)
        {
            return ServiceProv.Producto_GetCosto(autoPrd);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Depositos.Lista.Ficha> 
            Producto_GetDepositos(string autoPrd)
        {
            return ServiceProv.Producto_GetDepositos(autoPrd);
        }

        public DtoLib.Resultado 
            Producto_AsignarRemoverDepositos(DtoLibInventario.Producto.Depositos.Asignar.Ficha ficha)
        {
            var rs = new DtoLib.Resultado();

            var rt = ServiceProv.Producto_Verificar_EsBienServicio(ficha.autoProducto);
            if (rt.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rs.Mensaje = rt.Mensaje;
                rs.Result = DtoLib.Enumerados.EnumResult.isError;
                return rs;
            }

            if (rt.Entidad == true) 
            {
                rs.Mensaje = "PROBLEMA AL ASIGNAR DEPOSITOS" + Environment.NewLine + "CATEGORIA DEL PRODUCTO NO PERMITE TENER DEPOSITOS";
                rs.Result = DtoLib.Enumerados.EnumResult.isError;
                return rs;
            }

            if (ficha.depRemover != null)
            {
                foreach (var it in ficha.depRemover)
                {
                    var rt1 = ServiceProv.Producto_Verificar_DepositoRemover(ficha.autoProducto, it.autoDeposito);
                    if (rt1.Result == DtoLib.Enumerados.EnumResult.isError)
                    {
                        rs.Mensaje = rt1.Mensaje;
                        rs.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rs;
                    }
                }
            }

            return ServiceProv.Producto_AsignarRemoverDepositos (ficha);
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Clasificacion.Resumen> 
            Producto_Clasificacion_Lista()
        {
            return ServiceProv.Producto_Clasificacion_Lista();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Editar.Obtener.Ficha> 
            Producto_Editar_GetFicha(string autoPrd)
        {
            return ServiceProv.Producto_Editar_GetFicha(autoPrd);
        }

        public DtoLib.Resultado 
            Producto_Editar_Actualizar(DtoLibInventario.Producto.Editar.Actualizar.Ficha ficha)
        {
            var rs = new DtoLib.Resultado();

            var r1 = ServiceProv.Producto_Verificar_CodigoProductoYaRegistrado(ficha.codigo, ficha.auto);
            if (r1.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rs.Mensaje = r1.Mensaje;
                rs.Result = DtoLib.Enumerados.EnumResult.isError;
                return rs;
            }
            if (r1.Entidad == true)
            {
                rs.Mensaje = "[ CODIGO ] YA REGISTRADO, VERIFIQUE POR FAVOR";
                rs.Result = DtoLib.Enumerados.EnumResult.isError;
                return rs;
            }

            var r2 = ServiceProv.Producto_Verificar_CodigoPluProductoYaRegistrado(ficha.plu, ficha.auto);
            if (r2.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rs.Mensaje = r2.Mensaje;
                rs.Result = DtoLib.Enumerados.EnumResult.isError;
                return rs;
            }
            if (r2.Entidad == true)
            {
                rs.Mensaje = "[ PLU ] YA REGISTRADO, VERIFIQUE POR FAVOR";
                rs.Result = DtoLib.Enumerados.EnumResult.isError;
                return rs;
            }

            if (ficha.categoria.Trim().ToUpper() == "BIEN DE SERVICIO") 
            {
                var rt= ServiceProv.Producto_Verificar_HayDepositosAsignado(ficha.auto);
                if (rt.Result == DtoLib.Enumerados.EnumResult.isError) 
                {
                    rs.Mensaje = rt.Mensaje;
                    rs.Result = DtoLib.Enumerados.EnumResult.isError;
                    return rs;
                }

                if (rt.Entidad == true) 
                {
                    rs.Mensaje = "PROBLEMA AL ACTUALIZAR FICHA"+Environment.NewLine+"CATEGORIA SELECCIONADA NO PUEDE TENER DEPOSITOS";
                    rs.Result = DtoLib.Enumerados.EnumResult.isError;
                    return rs;
                }
            }

            return ServiceProv.Producto_Editar_Actualizar(ficha);
        }

        public DtoLib.ResultadoAuto 
            Producto_Nuevo_Agregar(DtoLibInventario.Producto.Agregar.Ficha ficha)
        {
            var rt = new DtoLib.ResultadoAuto();

            var r1 = ServiceProv.Producto_Verificar_CodigoProductoYaRegistrado(ficha.codigo, "");
            if (r1.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r1.Mensaje;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                return rt;
            }
            if (r1.Entidad == true) 
            {
                rt.Mensaje = "[ CODIGO ] YA REGISTRADO, VERIFIQUE POR FAVOR";
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                return rt;
            }

            var r2 = ServiceProv.Producto_Verificar_CodigoPluProductoYaRegistrado(ficha.plu,"");
            if (r2.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                rt.Mensaje = r2.Mensaje;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                return rt;
            }
            if (r2.Entidad == true)
            {
                rt.Mensaje = "[ PLU ] YA REGISTRADO, VERIFIQUE POR FAVOR";
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                return rt;
            }

            return ServiceProv.Producto_Nuevo_Agregar (ficha);
        }

        public DtoLib.Resultado 
            Producto_CambiarEstatusA_Activo(string auto)
        {
            return ServiceProv.Producto_CambiarEstatusA_Activo(auto);
        }

        public DtoLib.Resultado 
            Producto_CambiarEstatusA_Inactivo(string auto)
        {
            var rt = ServiceProv.Producto_Verificar_ExistenciaEnCero(auto);
            if (rt.Result == DtoLib.Enumerados.EnumResult.isError) 
            {
                return new DtoLib.Resultado() { Mensaje = rt.Mensaje, Result = DtoLib.Enumerados.EnumResult.isError, };
            }
            return ServiceProv.Producto_CambiarEstatusA_Inactivo(auto);
        }

        public DtoLib.Resultado 
            Producto_CambiarEstatusA_Suspendido(string auto)
        {
            return ServiceProv.Producto_CambiarEstatusA_Suspendido (auto);
        }


        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Depositos.Ver.Ficha> Producto_GetDeposito(DtoLibInventario.Producto.Depositos.Ver.Filtro filtro)
        {
            return ServiceProv.Producto_GetDeposito(filtro);
        }

        public DtoLib.Resultado Producto_DepositoEditar(DtoLibInventario.Producto.Depositos.Editar.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();

            rt = ServiceProv.Producto_Verificar_QueExista_EstatusActivo_NoSeaBienServicio(ficha.autoProducto);
            if (rt.Result == DtoLib.Enumerados.EnumResult.isError) { return rt; }

            return ServiceProv.Producto_EditarDeposito(ficha);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.Estatus.Actual.Ficha> Producto_Estatus_GetFicha(string autoPrd)
        {
            return ServiceProv.Producto_Estatus_GetFicha(autoPrd);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Imagen> Producto_GetImagen(string autoPrd)
        {
            return ServiceProv.Producto_GetImagen(autoPrd);
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Producto.Plu.Lista.Resumen> Producto_Plu_Lista()
        {
            return ServiceProv.Producto_Plu_Lista ();
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Identificacion> Producto_GetIdentificacion(string autoPrd)
        {
            return ServiceProv.Producto_GetIdentificacion (autoPrd);
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Producto.VerData.Proveedor.Ficha> 
            Producto_GetProveedores(string autoPrd)
        {
            return ServiceProv.Producto_GetProveedores(autoPrd);
        }


        public DtoLib.Resultado 
            Producto_Deposito_AsignacionMasiva(DtoLibInventario.Producto.Depositos.AsignacionMasiva.Ficha ficha)
        {
            return ServiceProv.Producto_Deposito_AsignacionMasiva(ficha);
        }

        public DtoLib.ResultadoEntidad<string> 
            Producto_GetId_ByCodigoBarra(string codBarra)
        {
            return ServiceProv.Producto_GetId_ByCodigoBarra(codBarra);
        }

    }

}