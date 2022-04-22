using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDSWI.Services
{
    public interface IDaoPropietario<T>
    {
        void InsertarPropietario(T p);
        void ActualizarPropietario(T p);
        void BajaPropietario(T p);
        List<T> ListarPropietarios();
        T BuscarPropietario(int id);
    }
}
