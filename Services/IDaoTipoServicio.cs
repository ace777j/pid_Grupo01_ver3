using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDSWI.Services
{
    interface IDaoTipoServicio<T>
    {
        List<T> ListarTipoServicio();

    }
}