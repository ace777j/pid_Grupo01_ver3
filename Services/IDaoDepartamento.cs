using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDSWI.Services
{
    public interface IDaoDepartamento<T>
    {
        void InsertarDepartamento(T p);
        void ActualizarDepartamento(T p);
        void BajaDepartamento(T p);
        List<T> ListarDepartamentos();
        T BuscarDepartamento(int id);
    }
}
