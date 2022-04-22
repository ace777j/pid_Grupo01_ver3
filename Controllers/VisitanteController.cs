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
    public class VisitanteController : Controller
    {
        private DB_PIGrupo01Entities1 db = new DB_PIGrupo01Entities1();


        SqlConnection cn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DB_PIGrupo011"].ConnectionString);


        PropietarioDAO objpro = new PropietarioDAO();
        VisitanteDAO objvis = new VisitanteDAO();

        List<Visitante1> Visitantes()
        {
            List<Visitante1> temporal = new List<Visitante1>();
            SqlCommand cmd = new SqlCommand("usp_VisitanteListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Visitante1 reg = new Visitante1
                {
                    idVisi = dr.GetInt32(0),

                    nomVisi = dr.GetString(1),

                    apeVisi = dr.GetString(2),

                    dniVisi = dr.GetString(3),

                    movilVisi = dr.GetString(4),

                    fechaRegistro = dr.GetDateTime(5),

                    usuReg = dr.GetString(6),

                    idProp = dr.GetInt32(7)
                };
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }



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
                    idProp = dr.GetInt32(0)
                };
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }


        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Index()
        {
            return View(objvis.ListarVisitantes().ToList());
        }

        public ActionResult Details(int id)
        {
            return View(objvis.BuscarVisitante(id));
        }

        public ActionResult Create()
        {

            ViewBag.propietarios = new SelectList(Propietarios(), "idProp", "idProp");
            return View(new Visitante1());
        }

        [HttpPost]

        public ActionResult Create(Visitante1 reg)
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
                SqlCommand cmd = new SqlCommand("usp_VisitanteInsertar", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nomVisi", reg.nomVisi);
                cmd.Parameters.AddWithValue("@apeVisi", reg.apeVisi);
                cmd.Parameters.AddWithValue("@dniVisi", reg.dniVisi);
                cmd.Parameters.AddWithValue("@movilVisi", reg.movilVisi);
                cmd.Parameters.AddWithValue("@fechaRegistro", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@usuReg", reg.usuReg);
                cmd.Parameters.AddWithValue("@idProp", reg.idProp);
                int q = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.mensaje = q.ToString() + " Visitante Agregado";
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
           
            ViewBag.propietarios = new SelectList(Propietarios(), "idProp", "idProp", reg.idProp);
            return View(reg);
        }

        public ActionResult Edit(int id)
        {
            Visitante1 reg = objvis.BuscarVisitante(id);
            ViewBag.propietarios = new SelectList(objpro.ListarPropietarios(),
                "idProp", "idProp", reg.idProp);
            return View(objvis.BuscarVisitante(id));
        }

        [HttpPost]
        public ActionResult Edit(Visitante1 reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objvis.ActualizarVisitante(reg);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            { return View(); }
        }

        public ActionResult Delete(int id)
        {
            Visitante1 pro = objvis.BuscarVisitante(id);
            return View(pro);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Visitante1 pro = objvis.BuscarVisitante(id);
            objvis.BajaVisitante(pro);
            return RedirectToAction("Index");
        }
    }
}