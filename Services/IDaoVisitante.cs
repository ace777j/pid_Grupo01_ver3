using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDSWI.Services
{
    public interface IDaoVisitante<T>
    {
        void InsertarVisitante(T p);
        void ActualizarVisitante(T p);
        void BajaVisitante(T p);
        List<T> ListarVisitantes();
        T BuscarVisitante(int id);
    }
}
