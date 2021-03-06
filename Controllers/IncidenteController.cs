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

namespace ProyectoPidG01.Controllers
{
    public class IncidenteController : Controller
    {
        SqlConnection cn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DB_PIGrupo011"].ConnectionString);

        IncidenteDAO objInc = new IncidenteDAO();

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

        List<CausaIncidente1> Causas()
        {
            List<CausaIncidente1> temporal = new List<CausaIncidente1>();
            SqlCommand cmd = new SqlCommand("usp_CausaIncidenteListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CausaIncidente1 reg = new CausaIncidente1
                {
                    idCausa = dr.GetInt32(0),
                    descripcion = dr.GetString(1)
                };
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }

        List<EstadoIncidente1> Estados()
        {
            List<EstadoIncidente1> temporal = new List<EstadoIncidente1>();
            SqlCommand cmd = new SqlCommand("usp_EstadoIncidenteListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                EstadoIncidente1 reg = new EstadoIncidente1
                {
                    idEstado = dr.GetInt32(0),
                    descripcion = dr.GetString(1)
                };
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }

        public ActionResult Index(string depa, string causa, string est)
        {
            if (depa == null) { depa = string.Empty; }
            if (causa == null) { causa = string.Empty; }
            if (est == null) { est = string.Empty; }
            ViewBag.depa = new SelectList(Departamentos(), "idDepa", "idDepa", depa);
            ViewBag.causa = new SelectList(Causas(), "idCausa", "descripcion", causa);
            ViewBag.estado = new SelectList(Estados(), "idEstado", "descripcion", est);

            return View(objInc.ListarIncidentes(depa, causa, est).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.depa = new SelectList(Departamentos(), "idDepa", "idDepa");
            ViewBag.causa = new SelectList(Causas(), "idCausa", "descripcion");
            return View(new Incidente1());
        }

        [HttpPost]
        public ActionResult Create(Incidente1 reg)
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
                SqlCommand cmd = new SqlCommand("usp_IncidenteInsertar", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idDepa", reg.idDepa);
                cmd.Parameters.AddWithValue("@idCausa", reg.idCausa);
                cmd.Parameters.AddWithValue("@descripcion", reg.comentario);
                int q = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.mensaje = q.ToString() + " Incidente Agregado";
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
            ViewBag.depa = new SelectList(Departamentos(), "idDepa", "idDepa");
            ViewBag.causa = new SelectList(Causas(), "idCausa", "descripcion");
            return View(reg);
        }

        public ActionResult Edit(int id)
        {
            Incidente1 reg = objInc.BuscarIncidente(id);
            ViewBag.causa = new SelectList(Causas(), "idCausa", "descripcion");
            ViewBag.estado = new SelectList(Estados(), "idEstado", "descripcion");
            return View(objInc.BuscarIncidente(id));
        }

        [HttpPost]
        public ActionResult Edit(Incidente1 reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objInc.ActualizarIncidente(reg);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            { return View(); }
        }
    }
}