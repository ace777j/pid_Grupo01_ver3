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
    
    public partial class Visitante
    {
        public int idVisi { get; set; }
        public string nomVisi { get; set; }
        public string apeVisi { get; set; }
        public string dniVisi { get; set; }
        public string movilVisi { get; set; }
        public Nullable<System.DateTime> fechaRegistro { get; set; }
        public string usuReg { get; set; }
        public Nullable<int> idProp { get; set; }
    
        public virtual Propietario Propietario { get; set; }
    }
}
