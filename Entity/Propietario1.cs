using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDSWI.Entity
{
    public class Propietario1
    {
        public int idProp { get; set; }
        public string nomProp { get; set; }
        public string apeProp { get; set; }
        public string dniProp { get; set; }
        public string correoProp { get; set; }
        public string movilProp { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string usuReg { get; set; }
        public int idDepa { get; set; }
    }
}