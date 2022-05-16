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
    public class PagoDDAO : IDaoPagoD<PagoD1>
    {
        public void ActualizarPagoDepartamento(PagoD1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PagoDActualizar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPagoD", p.idPagoD);
            cmd.Parameters.AddWithValue("@propietario", p.propietario);
            cmd.Parameters.AddWithValue("@departamento", p.departamento);
            cmd.Parameters.AddWithValue("@precio", p.precio);
            cmd.Parameters.AddWithValue("@fechaPago", p.fechaPago);
            cmd.Parameters.AddWithValue("@fechaVencimiento", p.fechaVencimiento);

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

        public void BajaPagoDepartamento(PagoD1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PagoDEliminar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPagoD", p.idPagoD);

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

        public PagoD1 BuscarPagoDepartamento(int id)
        {
            PagoD1 reg = null;
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PagoDDatos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPagoD", id);

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    reg = new PagoD1()
                    {
                        idPagoD = Convert.ToInt32(dr[0]),
                        propietario = dr[1].ToString(),
                        departamento = dr[2].ToString(),
                        precio = Convert.ToDecimal(dr[3]),
                        fechaPago = Convert.ToDateTime(dr[4]),
                        fechaVencimiento = Convert.ToDateTime(dr[5])
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

        public void InsertarPagoDepartamento(PagoD1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PagoDInsertar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPagoD", p.idPagoD);
            cmd.Parameters.AddWithValue("@propietario", p.propietario);
            cmd.Parameters.AddWithValue("@departamento", p.departamento);
            cmd.Parameters.AddWithValue("@precio", p.precio);
            cmd.Parameters.AddWithValue("@fechaPago", p.fechaPago);
            cmd.Parameters.AddWithValue("@fechaVencimiento", p.fechaVencimiento);
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

        public List<PagoD1> ListarPagoDepartamento()
        {
            List<PagoD1> lista = new List<PagoD1>();
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PagoDListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    PagoD1 reg = new PagoD1()
                    {
                        idPagoD = Convert.ToInt32(dr[0].ToString()),
                        propietario = dr[1].ToString(),
                        departamento = dr[2].ToString(),
                        precio = Convert.ToDecimal(dr[3].ToString()),
                        fechaPago = Convert.ToDateTime(dr[4].ToString()),
                        fechaVencimiento = Convert.ToDateTime(dr[5].ToString())
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