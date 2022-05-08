using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoDSWI.Entity
{
    public class Mascota1
    {
        public int idMascota { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [StringLength(20, ErrorMessage = "Esta celda tiene un máximo de 20 carácteres")]
        [DisplayName("Tipo")]
        public string tipoMascota { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [Range(0, 10, ErrorMessage = "Se puede tener un máximo de 10 tipos de una mascota")]
        [DisplayName("Cantidad")]
        public string nroMascota { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Propietario")]
        public int idProp { get; set; }
        public string nomProp { get; set; }
        public string apeProp { get; set; }
    }
}


