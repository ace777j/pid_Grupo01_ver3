//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoPidG01.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Departamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Departamento()
        {
            this.Propietario = new HashSet<Propietario>();
        }
    
        public int idDepa { get; set; }
        public string nroPiso { get; set; }
        public Nullable<System.DateTime> fechaRegistro { get; set; }
        public string usuReg { get; set; }
        public Nullable<int> idEstado { get; set; }
        public Nullable<int> idTipo { get; set; }
    
        public virtual Estado Estado { get; set; }
        public virtual TipoDepartamento TipoDepartamento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Propietario> Propietario { get; set; }
    }
}