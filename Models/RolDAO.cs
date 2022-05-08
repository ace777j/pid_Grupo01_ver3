using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ProyectoDSWI.Entity;
using ProyectoDSWI.Services;
using ProyectoDSWI.DataBase;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoPidG01.Models
{
    public class RolDAO : IDaoRol<Rol1>
    {
        public List<Rol1> ListarRoles()
        {
            List<Rol1> lista = new List<Rol1>();
            SqlConnection cn = DBAccess.getConecta();
            SqlCommand cmd = new SqlCommand("usp_RolListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Rol1 reg = new Rol1()
                    {
                        id = Convert.ToInt32(dr[0]),
                        nombre = dr[1].ToString()
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