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
    public class VisitanteDAO : IDaoVisitante<Visitante1>
    {
        public void ActualizarVisitante(Visitante1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_VisitanteActualizar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idVisi", p.idVisi);
            cmd.Parameters.AddWithValue("@nomVisi", p.nomVisi);
            cmd.Parameters.AddWithValue("@apeVisi", p.apeVisi);
            cmd.Parameters.AddWithValue("@dniVisi", p.dniVisi);
            cmd.Parameters.AddWithValue("@movilVisi", p.movilVisi);
            cmd.Parameters.AddWithValue("@fechaRegistro", p.fechaRegistro);
            cmd.Parameters.AddWithValue("@usuReg", p.usuReg);
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

        public void BajaVisitante(Visitante1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_VisitanteEliminar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idVisi", p.idVisi);

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

        public Visitante1 BuscarVisitante(int id)
        {
            Visitante1 reg = null;
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_VisitanteDatos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idVisi", id);

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    reg = new Visitante1()
                    {
                        idVisi = Convert.ToInt32(dr[0]),
                        nomVisi = dr[1].ToString(),
                        apeVisi = dr[2].ToString(),
                        dniVisi = dr[3].ToString(),
                        movilVisi = dr[4].ToString(),
                        fechaRegistro = Convert.ToDateTime(dr[5]),
                        usuReg = dr[6].ToString(),
                        idProp = Convert.ToInt32(dr[7])
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

        public void InsertarVisitante(Visitante1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_VisitanteInsertar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nomVisi", p.nomVisi);
            cmd.Parameters.AddWithValue("@nomVisi", p.nomVisi);
            cmd.Parameters.AddWithValue("@apeVisi", p.apeVisi);
            cmd.Parameters.AddWithValue("@dniVisi", p.dniVisi);
            cmd.Parameters.AddWithValue("@movilVisi", p.movilVisi);
            cmd.Parameters.AddWithValue("@fechaRegistro", p.fechaRegistro);
            cmd.Parameters.AddWithValue("@usuReg", p.usuReg);
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

        public List<Visitante1> ListarVisitantes()
        {
            List<Visitante1> lista = new List<Visitante1>();
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_VisitanteListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Visitante1 reg = new Visitante1()
                    {
                        idVisi = Convert.ToInt32(dr[0]),
                        nomVisi = dr[1].ToString(),
                        apeVisi = dr[2].ToString(),
                        dniVisi = dr[3].ToString(),
                        movilVisi = dr[4].ToString(),
                        fechaRegistro = Convert.ToDateTime(dr[5]),
                        usuReg = dr[6].ToString(),
                        idProp = Convert.ToInt32(dr[7])
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