//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class productos_precios
    {
        public string auto_producto { get; set; }
        public string nota { get; set; }
        public System.DateTime fecha { get; set; }
        public string estacion { get; set; }
        public string hora { get; set; }
        public string usuario { get; set; }
        public string precio_id { get; set; }
        public decimal precio { get; set; }
        public int id { get; set; }
    
        public virtual productos productos { get; set; }
    }
}
