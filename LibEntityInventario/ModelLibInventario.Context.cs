﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibEntityInventario
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class invEntities : DbContext
    {
        public invEntities()
            : base("name=invEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<empresa> empresa { get; set; }
        public virtual DbSet<empresa_departamentos> empresa_departamentos { get; set; }
        public virtual DbSet<empresa_depositos> empresa_depositos { get; set; }
        public virtual DbSet<empresa_grupo> empresa_grupo { get; set; }
        public virtual DbSet<empresa_sucursal> empresa_sucursal { get; set; }
        public virtual DbSet<empresa_tasas> empresa_tasas { get; set; }
        public virtual DbSet<productos_alterno> productos_alterno { get; set; }
        public virtual DbSet<productos_costos> productos_costos { get; set; }
        public virtual DbSet<productos_deposito> productos_deposito { get; set; }
        public virtual DbSet<productos_extra> productos_extra { get; set; }
        public virtual DbSet<productos_grupo> productos_grupo { get; set; }
        public virtual DbSet<productos_marca> productos_marca { get; set; }
        public virtual DbSet<productos_medida> productos_medida { get; set; }
        public virtual DbSet<productos_movimientos> productos_movimientos { get; set; }
        public virtual DbSet<productos_movimientos_extra> productos_movimientos_extra { get; set; }
        public virtual DbSet<productos_movimientos_transito> productos_movimientos_transito { get; set; }
        public virtual DbSet<productos_precios> productos_precios { get; set; }
        public virtual DbSet<productos_precios_ext> productos_precios_ext { get; set; }
        public virtual DbSet<productos_proveedor> productos_proveedor { get; set; }
        public virtual DbSet<productos_subgrupo> productos_subgrupo { get; set; }
        public virtual DbSet<proveedores> proveedores { get; set; }
        public virtual DbSet<sistema_configuracion> sistema_configuracion { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }
        public virtual DbSet<cxc_medio_pago> cxc_medio_pago { get; set; }
        public virtual DbSet<pos_arqueo> pos_arqueo { get; set; }
        public virtual DbSet<productos_kardex> productos_kardex { get; set; }
        public virtual DbSet<productos_movimientos_detalle> productos_movimientos_detalle { get; set; }
        public virtual DbSet<productos_conceptos> productos_conceptos { get; set; }
        public virtual DbSet<productos> productos { get; set; }
        public virtual DbSet<productos_ext> productos_ext { get; set; }
        public virtual DbSet<productos_movimientos_transito_detalle> productos_movimientos_transito_detalle { get; set; }
    }
}
