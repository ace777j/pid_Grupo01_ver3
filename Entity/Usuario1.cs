using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDSWI.Entity
{
    public class Usuario1
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime fecha { get; set; }
        public int idRol { get; set; }
    }
}