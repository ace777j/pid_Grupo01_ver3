using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDSWI.Services
{
    interface IDaoTipoDepartamento<T>
    {
        List<T> ListarTipoDepartamentos();

    }
}
