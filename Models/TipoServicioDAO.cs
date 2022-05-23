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
    public class TipoServicioDAO : IDaoTipoServicio<TipoServicio1>
    {
        public List<TipoServicio1> ListarTipoServicio()
        {
            List<TipoServicio1> lista = new List<TipoServicio1>();
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_TipoServicioListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TipoServicio1 reg = new TipoServicio1()
                    {
                        idTipoS = Convert.ToInt32(dr[0]),
                        descripcion = dr[1].ToString(),
                        precS = Convert.ToDecimal(dr[2])
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