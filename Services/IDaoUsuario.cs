using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDSWI.Services
{
    public interface IDaoUsuario<T>
    {
        void InsertarUsuario(T p);
    }
}
