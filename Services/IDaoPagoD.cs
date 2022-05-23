using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDSWI.Services
{
    public interface IDaoPagoD<T>
    {
        void InsertarPagoDepartamento(T p);
        void ActualizarPagoDepartamento(T p);
        void BajaPagoDepartamento(T p);
        List<T> ListarPagoDepartamento();
        T BuscarPagoDepartamento(int id);
    }

}