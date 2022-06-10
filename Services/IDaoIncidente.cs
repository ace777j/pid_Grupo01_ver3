using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDSWI.Services
{
    public interface IDaoIncidente<T>
    {
        void InsertarIncidente(T p);
        void ActualizarIncidente(T p);
        List<T> ListarIncidentes(string depa, string causa, string estado);
        T BuscarIncidente(int id);
    }
}
