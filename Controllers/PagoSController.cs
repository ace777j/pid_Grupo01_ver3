using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using ProyectoDSWI.Entity;
using ProyectoDSWI.Models;
using ProyectoPidG01.Models;
using ProyectoDSWI.Filters;

namespace ProyectoDSWI.Controllers
{
    public class PagoSController : Controller
    {
        private DB_PIGrupo01Entities1 db = new DB_PIGrupo01Entities1();

        SqlConnection cn = new SqlConnection(
        ConfigurationManager.ConnectionStrings["DB_PIGrupo011"].ConnectionString);
        PropietarioDAO objprop = new PropietarioDAO();
        TipoServicioDAO objtipser = new TipoServicioDAO();
        PagoSDAO objpagos = new PagoSDAO();

        // GET: PagoS
        List<PagoS1> PagoS()
        {
            List<PagoS1> temporal = new List<PagoS1>();
            SqlCommand cmd = new SqlCommand("usp_PagoSListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                PagoS1 reg = new PagoS1
                {
                    idPagoS = dr.GetInt32(0),

                    idProp = dr.GetInt32(1),

                    idTipoS = dr.GetInt32(2),

                    precio = dr.GetDecimal(3),

                    fechaPago = dr.GetDateTime(4),

                };
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }

        List<TipoServicio1> TipoServicio()
        {
            List<TipoServicio1> temporal = new List<TipoServicio1>();
            SqlCommand cmd = new SqlCommand("usp_TipoServicioListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TipoServicio1 reg = new TipoServicio1
                {
                    idTipoS = dr.GetInt32(0),
                    descripcion = dr.GetString(1)
                };
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }

        List<Propietario> Propietarios()
        {
            List<Propietario> temporal = new List<Propietario>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Propietario", cn);
            cmd.CommandType = CommandType.Text;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Propietario reg = new Propietario
                {
                    idProp = dr.GetInt32(0),
                    nomProp = dr.GetString(1)
                };
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }


        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Index()
        {
            return View(objpagos.ListarPagoServicio().ToList());
        }

        public ActionResult Details(int id)
        {
            return View(objpagos.BuscarPagoServicio(id));
        }

        public ActionResult Create()
        {
            ViewBag.propietarios = new SelectList(Propietarios(), "idProp", "nomProp");
            ViewBag.tiposervicio = new SelectList(TipoServicio(), "idTipoS", "descripcion");
            return View(new PagoS1());
        }

        [HttpPost]
        public ActionResult Create(PagoS1 reg)
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
                SqlCommand cmd = new SqlCommand("usp_PagoSInsertar", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idProp", reg.idProp);
                cmd.Parameters.AddWithValue("@idTipoS", reg.idTipoS);
                cmd.Parameters.AddWithValue("@precio", reg.precio);
                cmd.Parameters.AddWithValue("@fechaPago", DateTime.Now.ToString());
                int q = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.mensaje = q.ToString() + " Pago Servicio Agregado";
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
            ViewBag.propietarios = new SelectList(Propietarios(), "idProp", "nomProp", reg.idProp);
            ViewBag.tiposervicio = new SelectList(TipoServicio(), "idTipoS", "descripcion", reg.idTipoS);
            return View(reg);
        }

        public ActionResult Edit(int id)
        {
            PagoS1 reg = objpagos.BuscarPagoServicio(id);
            ViewBag.propietarios = new SelectList(objprop.ListarPropietarios(),
                "idProp", "nomProp", reg.idProp);
            ViewBag.tiposervicio = new SelectList(objtipser.ListarTipoServicio(),
                "idTipoS", "descripcion", reg.idTipoS);
            return View(objpagos.BuscarPagoServicio(id));
        }

        [HttpPost]
        public ActionResult Edit(PagoS1 reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objpagos.ActualizarPagoServicio(reg);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            { return View(); }
        }

        public ActionResult Delete(int id)
        {
            PagoS1 pro = objpagos.BuscarPagoServicio(id);
            return View(pro);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PagoS1 pro = objpagos.BuscarPagoServicio(id);
            objpagos.BajaPagoServicio(pro);
            return RedirectToAction("Index");
        }

    }
}