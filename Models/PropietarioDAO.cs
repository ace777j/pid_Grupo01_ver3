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
    public class PropietarioDAO : IDaoPropietario<Propietario1>
    {
        public void ActualizarPropietario(Propietario1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PropietarioActualizar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idProp", p.idProp);
            cmd.Parameters.AddWithValue("@nomProp", p.nomProp);
            cmd.Parameters.AddWithValue("@apeProp", p.apeProp);
            cmd.Parameters.AddWithValue("@dniProp", p.dniProp);
            cmd.Parameters.AddWithValue("@correoProp", p.correoProp);
            cmd.Parameters.AddWithValue("@movilProp", p.movilProp);
            cmd.Parameters.AddWithValue("@idDepa", p.idDepa);

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
        
        public void BajaPropietario(Propietario1 p)
            
        {
            
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PropietarioEliminar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idProp", p.idProp);

          
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
       
        public Propietario1 BuscarPropietario(int id)
        {
            Propietario1 reg = null;
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PropietarioDatos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idProp", id);

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    reg = new Propietario1()
                    {
                        idProp = Convert.ToInt32(dr[0]),
                        nomProp = dr[1].ToString(),
                        apeProp = dr[2].ToString(),
                        dniProp = dr[3].ToString(),
                        correoProp = dr[4].ToString(),
                        movilProp = dr[5].ToString(),
                        fechaRegistro = Convert.ToDateTime(dr[6]),
                        usuReg = Convert.ToInt32(dr[7]),
                        idDepa = Convert.ToInt32(dr[8])
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

        public void InsertarPropietario(Propietario1 p)
        {
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PropietarioInsertar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nomProp", p.nomProp);
            cmd.Parameters.AddWithValue("@apeProp", p.apeProp);
            cmd.Parameters.AddWithValue("@dniProp", p.dniProp);
            cmd.Parameters.AddWithValue("@correoProp", p.correoProp);
            cmd.Parameters.AddWithValue("@movilProp", p.movilProp);
            cmd.Parameters.AddWithValue("@usuReg", p.usuReg);
            cmd.Parameters.AddWithValue("@idDepa", p.idDepa);

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

        public List<Propietario1> ListarPropietarios()
        {
            List<Propietario1> lista = new List<Propietario1>();
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_PropietarioListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Propietario1 reg = new Propietario1()
                    {
                        idProp = Convert.ToInt32(dr[0]),
                        nomProp = dr[1].ToString(),
                        apeProp = dr[2].ToString(),
                        dniProp = dr[3].ToString(),
                        correoProp = dr[4].ToString(),
                        movilProp = dr[5].ToString(),
                        fechaRegistro = Convert.ToDateTime(dr[6]),
                        usuReg = Convert.ToInt32(dr[7]),
                        idDepa = Convert.ToInt32(dr[8])
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