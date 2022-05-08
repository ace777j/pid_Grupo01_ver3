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
    public class DepartamentoDAO : IDaoDepartamento<Departamento1>
    {
        public void ActualizarDepartamento(Departamento1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_DepartamentoActualizar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idDepa", p.idDepa);
            cmd.Parameters.AddWithValue("@nroPiso", p.nroPiso);
            cmd.Parameters.AddWithValue("@fechaRegistro", p.fechaRegistro);
            cmd.Parameters.AddWithValue("@usuReg", p.usuReg);
            cmd.Parameters.AddWithValue("@idEstado", p.idEstado);
            cmd.Parameters.AddWithValue("@idTipo", p.idTipo);

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

        public void BajaDepartamento(Departamento1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_DepartamentoEliminar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idDepa", p.idDepa);

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

        public Departamento1 BuscarDepartamento(int id)
        {
            Departamento1 reg = null;
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_DepartamentoDatos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idDepa", id);

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    reg = new Departamento1()
                    {
                        idDepa = Convert.ToInt32(dr[0]),
                        nroPiso = dr[1].ToString(),
                        fechaRegistro = Convert.ToDateTime(dr[2]),
                        usuReg = dr[3].ToString(),
                        idEstado = Convert.ToInt32(dr[4]),
                        idTipo = Convert.ToInt32(dr[5])
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

        public void InsertarDepartamento(Departamento1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_DepartamentoInsertar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nroPiso", p.nroPiso);
            cmd.Parameters.AddWithValue("@fechaRegistro", p.fechaRegistro);
            cmd.Parameters.AddWithValue("@usuReg", p.usuReg);
            cmd.Parameters.AddWithValue("@idEstado", p.idEstado);
            cmd.Parameters.AddWithValue("@idTipo", p.idTipo);

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

        public List<Departamento1> ListarDepartamentos()
        {
            List<Departamento1> lista = new List<Departamento1>();
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_DepartamentoListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Departamento1 reg = new Departamento1()
                    {
                        idDepa = Convert.ToInt32(dr[0]),
                        nroPiso = dr[1].ToString(),
                        fechaRegistro = Convert.ToDateTime(dr[2]),
                        usuReg = dr[3].ToString(),
                        idEstado = Convert.ToInt32(dr[4]),
                        idTipo = Convert.ToInt32(dr[5]),
                         descripcion = dr[6].ToString(),
                        tipdescripcion  = dr[7].ToString()
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