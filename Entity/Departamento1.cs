using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoDSWI.Entity
{
    public class Departamento1
    {
        public int idDepa { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Piso")]
        public string nroPiso { get; set; }

        public DateTime fechaRegistro { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Usuario")]
        public int usuReg { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Estado")]
        public int idEstado { get; set; }
        public string descripcion { get; set; }
        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Tipo")]
        public int idTipo { get; set; }
        public string tipdescripcion { get; set; }
    }
}