using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleInv
{


    class Program
    {

        static void Main(string[] args)
        {
            ILibInventario.IProvider invPrv = new ProvLibInventario.Provider("localhost", "pita");
//            var r01 = invPrv.Producto_GetFicha("0000000450");

            //var filtro = new DtoLibInventario.MovPend.Lista.Filtro();
            //var r01 = invPrv.MovPend_GetLista(filtro);
            //var r01 = invPrv.MovPend_Anular(1);

            //var filtro = new DtoLibInventario.Producto.Filtro();
            //filtro.activarBusquedaPorTrasalado = true;
            //filtro.autoDepOrigen = "0000000001";
            //filtro.autoDepDestino = "0000000008";
            //filtro.estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo;
            //var r01 = invPrv.Producto_GetLista(filtro);

            //var r01 = invPrv.PrecioCosto_GetData("0000001410");
            //var r01 = invPrv.Usuario_GetId_ByClaveUsuGrupoAdm("168818288");

            //var depDest= new DtoLibInventario.Producto.Depositos.AsignacionMasiva.FichaDepositoDestino()
            //{
            //     autoDeposito="0000000024",
            //};
            //var lstDep= new List<DtoLibInventario.Producto.Depositos.AsignacionMasiva.FichaDepartamentos>();
            //lstDep.Add( new DtoLibInventario.Producto.Depositos.AsignacionMasiva.FichaDepartamentos(){ auto ="0000000004"});
            //lstDep.Add( new DtoLibInventario.Producto.Depositos.AsignacionMasiva.FichaDepartamentos(){ auto ="0000000015"});
            //var ficha = new DtoLibInventario.Producto.Depositos.AsignacionMasiva.Ficha()
            //{
            //    depositoDestino = depDest,
            //    departamentosNoIncluir= lstDep,
            //};
            //var r01 = invPrv.Producto_Deposito_AsignacionMasiva(ficha);

            //var filtroDto = new DtoLibInventario.Reportes.ResumenCostoInv.Filtro()
            //{
            //    autoDeposito = "0000000023",
            //    desde = new DateTime(2022, 06, 30),
            //    hasta = new DateTime(2022, 07, 10),
            //};
            //var r01 = invPrv.Reportes_ResumenCostoInventario(filtroDto);

            //var filtroDto = new DtoLibInventario.Visor.PrecioAjuste.Filtro() { autoDepart = "0000000002", autoGrupo = "", idEmpresaGrrupo = "0000000004" };
            //var r01 = invPrv.Visor_PrecioAjuste(filtroDto);

            //var r02 = invPrv.EmpresaGrupo_GetLista();

            //var r02 = invPrv.EmpresaGrupo_PrecioManejar_GetById("0000000004");

            //var filtro = new DtoLibInventario.Sucursal.Filtro() { idEmpresaGrupo = "0000000004" };
            //var r03 = invPrv.Sucursal_GetLista(filtro);

        }

    }

}