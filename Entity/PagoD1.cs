using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProyectoDSWI.Entity
{
    public class PagoD1
    {
        public int idPagoD { get; set; }
        [DisplayName("PROPIETARIO")]
        public int idProp { get; set; }
        [DisplayName("TIPO DEPARTAMENTO")]
        public int idTipo { get; set; }
        [DisplayName("PRECIO")]
        public decimal precio { get; set; }
        [DisplayName("FECHA DE PAGO")]
        public DateTime fechaPago { get; set; }
        [Required]
        [Display(Name = "FECHA VENCIMIENTO")]
        public DateTime fechaVencimiento { get; set; }
        [DisplayName("PROPIETARIO")]
        public string propietario { get; set; }
        [DisplayName("DEPARTAMENTO")]
        public string departamento { get; set; }
    }
}
