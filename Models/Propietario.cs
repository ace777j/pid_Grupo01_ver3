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
    
    public partial class Propietario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Propietario()
        {
            this.Mascota = new HashSet<Mascota>();
            this.Visitante = new HashSet<Visitante>();
        }
    
        public int idProp { get; set; }
        public string nomProp { get; set; }
        public string apeProp { get; set; }
        public string dniProp { get; set; }
        public string correoProp { get; set; }
        public string movilProp { get; set; }
        public Nullable<System.DateTime> fechaRegistro { get; set; }
        public string usuReg { get; set; }
        public Nullable<int> idDepa { get; set; }
    
        public virtual Departamento Departamento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mascota> Mascota { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visitante> Visitante { get; set; }
    }
}