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
    public class PagoSDAO : IDaoPagoS<PagoS1>
    {
        public void ActualizarPagoServicio(PagoS1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PagoSActualizar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPagoS", p.idPagoS);
            cmd.Parameters.AddWithValue("@idProp", p.idProp);
            cmd.Parameters.AddWithValue("@idTipoS", p.idTipoS);
            cmd.Parameters.AddWithValue("@precio", p.precio);
            cmd.Parameters.AddWithValue("@fechaPago", p.fechaPago);

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

        public void BajaPagoServicio(PagoS1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PagoSEliminar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPagoS", p.idPagoS);

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

        public PagoS1 BuscarPagoServicio(int id)
        {
            PagoS1 reg = null;
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PagoSDatos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPagoS", id);

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    reg = new PagoS1()
                    {
                        idPagoS = Convert.ToInt32(dr[0]),
                        propietario = dr[1].ToString(),
                        servicio = dr[2].ToString(),
                        precio = Convert.ToDecimal(dr[3]),
                        fechaPago = Convert.ToDateTime(dr[4])

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

        public void InsertarPagoServicio(PagoS1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PagoSInsertar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idProp", p.idProp);
            cmd.Parameters.AddWithValue("@idTipoS", p.idTipoS);
            cmd.Parameters.AddWithValue("@precio", p.precio);
            cmd.Parameters.AddWithValue("@fechaPago", p.fechaPago);

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

        public List<PagoS1> ListarPagoServicio()
        {
            List<PagoS1> lista = new List<PagoS1>();
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PagoSListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    PagoS1 reg = new PagoS1()
                    {
                        idPagoS = Convert.ToInt32(dr[0]),
                        propietario = dr[5].ToString(),
                        servicio = dr[6].ToString(),
                        precio = Convert.ToDecimal(dr[3]),
                        fechaPago = Convert.ToDateTime(dr[4]),

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