using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ProyectoDSWI.Entity;
using ProyectoDSWI.Services;
using ProyectoDSWI.DataBase;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoDSWI.Models
{
    public class TipoDepartamentoDAO : IDaoTipoDepartamento<TipoDepartamento1>
    {

        public List<TipoDepartamento1> ListarTipoDepartamentos()
        {
            List<TipoDepartamento1> lista = new List<TipoDepartamento1>();
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_TipoDepartamentoListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TipoDepartamento1 reg = new TipoDepartamento1()
                    {
                        idTipo = Convert.ToInt32(dr[0]),
                        descripcion = dr[1].ToString(),
                        nroDormitorios = Convert.ToInt32(dr[2]),
                        nroBanos = Convert.ToInt32(dr[3]),
                        areaDepar = dr[4].ToString(),
                        precMens = Convert.ToDecimal(dr[5])
                    };
                    lista.Add(reg);
                }
                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            { throw ex; }
            return lista;
        }
    }
}
