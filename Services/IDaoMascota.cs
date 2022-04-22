using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDSWI.Services
{
    public interface IDaoMascota<T>
    {
        void InsertarMascota(T p);
        void ActualizarMascota(T p);
        void BajaMascota(T p);
        List<T> ListarMascotas();
        T BuscarMascota(int id);
    }
}
