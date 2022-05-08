using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using ProyectoDSWI.Entity;
using ProyectoPidG01.Models;
using ProyectoDSWI.Models;
using ProyectoDSWI.Filters;

namespace ProyectoDSWI.Controllers
{
    public class PropietarioController : Controller
    {
        private DB_PIGrupo01Entities1 db = new DB_PIGrupo01Entities1();
        

        SqlConnection cn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DB_PIGrupo011"].ConnectionString);
        

        DepartamentoDAO objdep = new DepartamentoDAO();
        PropietarioDAO objpro = new PropietarioDAO();

        List<Propietario1> Propietarios()
        {
            List<Propietario1> temporal = new List<Propietario1>();
            SqlCommand cmd = new SqlCommand("usp_PropietarioListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Propietario1 reg = new Propietario1
                {
                    idProp = dr.GetInt32(0),

                    nomProp = dr.GetString(1),

                    apeProp = dr.GetString(2),

                    dniProp = dr.GetString(3),

                    correoProp = dr.GetString(4),

                    movilProp = dr.GetString(5),

                    fechaRegistro = dr.GetDateTime(6),

                    usuReg = dr.GetInt32(7),

                    idDepa = dr.GetInt32(8)
                };
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }

        List<Departamento1> Departamentos()

        {

            List<Departamento1> temporal = new List<Departamento1>();


            SqlCommand cmd = new SqlCommand("usp_DepartamentoListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())

            {

                Departamento1 reg = new Departamento1

                {

                    idDepa = dr.GetInt32(0)

                };

                temporal.Add(reg);

            }

            dr.Close(); cn.Close();

            return temporal;

        }

        List<usuario> Usuarios()

        {

            List<usuario> temporal = new List<usuario>();

            SqlCommand cmd = new SqlCommand("usp_UsuarioListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())

            {

                usuario reg = new usuario

                {

                    id = dr.GetInt32(0),

                    nombre = dr.GetString(1)

                };

                temporal.Add(reg);

            }

            dr.Close(); cn.Close();

            return temporal;

        }

        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Index()
        {
            return View(objpro.ListarPropietarios().ToList());
        }

        public ActionResult Details(int id)
        {
            return View(objpro.BuscarPropietario(id));
        }

        public ActionResult Create()
        {
            ViewBag.departamentos = new SelectList(Departamentos(), "idDepa", "idDepa");
            ViewBag.usuarios = new SelectList(Usuarios(), "id", "nombre");
            return View(new Propietario1());
        }

        [HttpPost]

        public ActionResult Create(Propietario1 reg)
        {
            if (!ModelState.IsValid)
            {
                return View(reg);
            }
            ViewBag.mensaje = " ";
            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("usp_PropietarioInsertar", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nomProp", reg.nomProp);
                cmd.Parameters.AddWithValue("@apeProp", reg.apeProp);
                cmd.Parameters.AddWithValue("@dniProp", reg.dniProp);
                cmd.Parameters.AddWithValue("@correoProp", reg.correoProp);
                cmd.Parameters.AddWithValue("@movilProp", reg.movilProp);
                cmd.Parameters.AddWithValue("@usuReg", reg.usuReg);
                cmd.Parameters.AddWithValue("@idDepa", reg.idDepa);
                int q = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.mensaje = q.ToString() + " Propietario Agregado";
            }
            catch (SqlException ex)
            {
                ViewBag.mensaje = ex.Message;
                tr.Rollback();
            }
            finally
            {
                cn.Close();
            }

            ViewBag.departamentos = new SelectList(Departamentos(), "idDepa", "idDepa", reg.idDepa);
            ViewBag.usuarios = new SelectList(Usuarios(), "id", "nombre", reg.usuReg);
            return View(reg);
        }

        public ActionResult Edit(int id)
        {
            Propietario1 reg = objpro.BuscarPropietario(id);
            ViewBag.departamentos = new SelectList(objdep.ListarDepartamentos(),
                "idDepa", "idDepa", reg.idDepa);
            return View(objpro.BuscarPropietario(id));
        }

        [HttpPost]
        public ActionResult Edit(Propietario1 reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objpro.ActualizarPropietario(reg);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            { return View(); }
        }

        public ActionResult Delete(int id)
        {
            Propietario1 pro = objpro.BuscarPropietario(id);
            return View(pro);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Propietario1 pro = objpro.BuscarPropietario(id);
            objpro.BajaPropietario(pro);
            return RedirectToAction("Index");
        }
    }
}