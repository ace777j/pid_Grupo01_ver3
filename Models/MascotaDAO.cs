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
    public class MascotaDAO : IDaoMascota<Mascota1>
    {
        public void ActualizarMascota(Mascota1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_MascotaActualizar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idMascota", p.idMascota);
            cmd.Parameters.AddWithValue("@tipoMascota", p.tipoMascota);
            cmd.Parameters.AddWithValue("@nroMascota", p.nroMascota);
            cmd.Parameters.AddWithValue("@idProp", p.idProp);

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

        public void BajaMascota(Mascota1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_MascotaEliminar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idMascota", p.idMascota);

            try
            {
                cn.Open();
                bool ires = cmd.ExecuteNonQuery() == 1 ? true : false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally { cn.Close(); }
        }

        public Mascota1 BuscarMascota(int id)
        {
            Mascota1 reg = null;
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_MascotaDatos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idMascota", id);

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    reg = new Mascota1()
                    {
                        idMascota = Convert.ToInt32(dr[0]),
                        tipoMascota = dr[1].ToString(),
                        nroMascota = dr[2].ToString(),
                        idProp = Convert.ToInt32(dr[3])
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

        public void InsertarMascota(Mascota1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_MascotaInsertar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tipoMascota", p.tipoMascota);
            cmd.Parameters.AddWithValue("@nroMascota", p.nroMascota);
            cmd.Parameters.AddWithValue("@idProp", p.idProp);

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

        public List<Mascota1> ListarMascotas()
        {
            List<Mascota1> lista = new List<Mascota1>();
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_MascotaListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Mascota1 reg = new Mascota1()
                    {
                        idMascota = Convert.ToInt32(dr[0]),
                        tipoMascota = dr[1].ToString(),
                        nroMascota = dr[2].ToString(),
                        idProp = Convert.ToInt32(dr[3])
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