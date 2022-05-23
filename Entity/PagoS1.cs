using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProyectoDSWI.Entity
{
    public class PagoS1
    {
        public int idPagoS { get; set; }
        [DisplayName("PROPIETARIO")]
        public int idProp { get; set; }
        [DisplayName("TIPO SERVICIO")]
        public int idTipoS { get; set; }
        [DisplayName("PRECIO")]
        public decimal precio { get; set; }
        [DisplayName("FECHA DE PAGO")]
        public DateTime fechaPago { get; set; }
        [DisplayName("PROPIETARIO")]
        public string propietario { get; set; }
        [DisplayName("SERVICIO")]
        public string servicio { get; set; }
    }
}
