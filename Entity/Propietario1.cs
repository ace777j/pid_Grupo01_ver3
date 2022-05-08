using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoDSWI.Entity
{
    public class Propietario1
    {
        public int idProp { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Nombre")]
        public string nomProp { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Apellido")]
        public string apeProp { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("DNI")]
        public string dniProp { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Correo")]
        public string correoProp { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Número Movil")]
        public string movilProp { get; set; }

        public DateTime fechaRegistro { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Usuario")]
        public int usuReg { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Departamento")]
        public int idDepa { get; set; }
    }
}