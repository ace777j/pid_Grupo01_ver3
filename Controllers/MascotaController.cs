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
    public class MascotaController : Controller
    {
        private DB_PIGrupo01Entities1 db = new DB_PIGrupo01Entities1();


        SqlConnection cn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DB_PIGrupo011"].ConnectionString);


        PropietarioDAO objpro = new PropietarioDAO();
        MascotaDAO objmas = new MascotaDAO();
        

        List<Mascota1> Mascotas()
        {
            List<Mascota1> temporal = new List<Mascota1>();
            SqlCommand cmd = new SqlCommand("usp_MascotaListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Mascota1 reg = new Mascota1
                {
                    idMascota = dr.GetInt32(0),

                    tipoMascota = dr.GetString(1),

                    nroMascota = dr.GetString(2),

                    idProp = dr.GetInt32(3),

                    nomProp = dr.GetString(4),
                    apeProp= dr.GetString(5)
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
                    idProp = dr.GetInt32(0),
                    nomProp=dr.GetString(1),
                    apeProp = dr.GetString(2)
                };
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }

        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Index()
        {
            return View(objmas.ListarMascotas().ToList());
        }

        public ActionResult Details(int id)
        {
            return View(objmas.BuscarMascota(id));
        }

        public ActionResult Create()
        {

            ViewBag.propietarios = new SelectList(Propietarios(), "idProp", "apeProp");
            return View(new Mascota1());
        }

        [HttpPost]

        public ActionResult Create(Mascota1 reg)
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
                SqlCommand cmd = new SqlCommand("usp_MascotaInsertar", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tipoMascota", reg.tipoMascota);
                cmd.Parameters.AddWithValue("@nroMascota", reg.nroMascota);
                cmd.Parameters.AddWithValue("@idProp", reg.idProp);
                int q = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.mensaje = q.ToString() + " Mascota Agregada Correctamente";
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



            ViewBag.propietarios = new SelectList(Propietarios(), "idProp", "apeProp", reg.idProp);
            return View(reg);
        }

        public ActionResult Edit(int id)
        {
            Mascota1 reg = objmas.BuscarMascota(id);
            ViewBag.propietarios = new SelectList(objpro.ListarPropietarios(),
                "idProp", "nomProp", reg.idProp);
            return View(objmas.BuscarMascota(id));
        }

        [HttpPost]
        public ActionResult Edit(Mascota1 reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objmas.ActualizarMascota(reg);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            { return View(); }
        }

        public ActionResult Delete(int id)
        {
            Mascota1 pro = objmas.BuscarMascota(id);
            return View(pro);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Mascota1 pro = objmas.BuscarMascota(id);
            objmas.BajaMascota(pro);
            return RedirectToAction("Index");
        }
    }
}