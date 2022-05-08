using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDSWI.Services
{
    public interface IDaoRol<T>
    {
        List<T> ListarRoles();
    }
}