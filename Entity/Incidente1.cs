using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoDSWI.Entity
{
    public class Incidente1
    {
        public int idIncidente { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Comentario")]
        public string comentario { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Departamento")]
        public int idDepa { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Causa")]
        public int idCausa { get; set; }
        public string descripcionC { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Estado")]
        public int idEstado { get; set; }
        public string descripcionE { get; set; }
    }
}