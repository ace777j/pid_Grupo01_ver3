using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDSWI.Entity
{
    public class Visitante1
    {
        public int idVisi { get; set; }
        public string nomVisi { get; set; }
        public string apeVisi { get; set; }
        public string dniVisi { get; set; }
        public string movilVisi { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string usuReg { get; set; }
        public int idProp { get; set; }
    }
}