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
    public class IncidenteDAO : IDaoIncidente<Incidente1>
    {
        public void ActualizarIncidente(Incidente1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_IncidenteActualizar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idIncidente", p.idIncidente);
            cmd.Parameters.AddWithValue("@idDepa", p.idDepa);
            cmd.Parameters.AddWithValue("@idCausa", p.idCausa);
            cmd.Parameters.AddWithValue("@idEstado", p.idEstado);
            cmd.Parameters.AddWithValue("@comentario", p.comentario);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally { cn.Close(); }
        }

        public Incidente1 BuscarIncidente(int id)
        {
            Incidente1 reg = null;
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_IncidenteDatos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idIncidente", id);

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    reg = new Incidente1()
                    {
                        idIncidente = Convert.ToInt32(dr[0]),
                        idDepa = Convert.ToInt32(dr[1]),
                        descripcionC = dr[2].ToString(),
                        descripcionE = dr[3].ToString(),
                        comentario = dr[4].ToString()
                    };
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally { cn.Close(); }
            return reg;
        }

        public void InsertarIncidente(Incidente1 p)
        {
            throw new NotImplementedException();
        }

        public List<Incidente1> ListarIncidentes(string depa, string causa, string estado)
        {
            List<Incidente1> lista = new List<Incidente1>();
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_IncidenteListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idDepa", depa);
            cmd.Parameters.AddWithValue("@idCausa", causa);
            cmd.Parameters.AddWithValue("@idEstado", estado);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Incidente1 reg = new Incidente1()
                    {
                        idIncidente = Convert.ToInt32(dr[0]),
                        idDepa = Convert.ToInt32(dr[1]),
                        descripcionC = dr[2].ToString(),
                        descripcionE = dr[3].ToString()
                    };
                    lista.Add(reg);
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally { cn.Close(); }
            return lista;
        }
    }
}