﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDSWI.Services
{
    public interface IDaoPagoS<T>
    {
        void InsertarPagoServicio(T p);
        void ActualizarPagoServicio(T p);
        void BajaPagoServicio(T p);
        List<T> ListarPagoServicio();
        T BuscarPagoServicio(int id);
    }

}