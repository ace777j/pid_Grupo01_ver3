using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoDSWI.Entity
{
    public class Visitante1
    {
        public int idVisi { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Nombre")]
        public string nomVisi { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Apellido")]
        public string apeVisi { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("DNI")]
        public string dniVisi { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Número Movil")]
        public string movilVisi { get; set; }

        public DateTime fechaRegistro { get; set; }

        [Required(ErrorMessage = "Esta celda es necesaria de completar")]
        [DisplayName("Propietario")]
        public int idProp { get; set; }
    }
}