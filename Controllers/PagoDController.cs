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

namespace ProyectoDSWI.Entity
{
    public class PagoDController : Controller
    {
        private DB_PIGrupo01Entities1 db = new DB_PIGrupo01Entities1();

        SqlConnection cn = new SqlConnection(
        ConfigurationManager.ConnectionStrings["DB_PIGrupo011"].ConnectionString);
        TipoDepartamentoDAO objtipd = new TipoDepartamentoDAO();
        PropietarioDAO objprop = new PropietarioDAO();
        PagoDDAO objpagod = new PagoDDAO();

        List<PagoD1> PagoD()
        {
            List<PagoD1> temporal = new List<PagoD1>();
            SqlCommand cmd = new SqlCommand("usp_PagoDListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                PagoD1 reg = new PagoD1
                {
                    idPagoD = dr.GetInt32(0),

                    idProp = dr.GetInt32(1),

                    idTipo = dr.GetInt32(2),

                    precio = dr.GetDecimal(3),

                    fechaPago = dr.GetDateTime(4),

                    fechaVencimiento = dr.GetDateTime(5),

                };
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }

        List<TipoDepartamento1> TipoDepartamento()
        {
            List<TipoDepartamento1> temporal = new List<TipoDepartamento1>();
            SqlCommand cmd = new SqlCommand("usp_TipoDepartamentoListar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TipoDepartamento1 reg = new TipoDepartamento1
                {
                    idTipo = dr.GetInt32(0),
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
            return View(objpagod.ListarPagoDepartamento().ToList());
        }

        public ActionResult Details(int id)
        {
            return View(objpagod.BuscarPagoDepartamento(id));
        }

        public ActionResult Create()
        {
            ViewBag.propietarios = new SelectList(Propietarios(), "idProp", "nomProp");
            ViewBag.tipodepartamentos = new SelectList(TipoDepartamento(), "idTipo", "descripcion");
            return View(new PagoD1());
        }

        [HttpPost]
        public ActionResult Create(PagoD1 reg)
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
                SqlCommand cmd = new SqlCommand("usp_PagoDInsertar", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idProp", reg.idProp);
                cmd.Parameters.AddWithValue("@idTipo", reg.idTipo);
                cmd.Parameters.AddWithValue("@precio", reg.precio);
                cmd.Parameters.AddWithValue("@fechaPago", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@fechaVencimiento", DateTime.Now.ToString());
                int q = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.mensaje = q.ToString() + " Pago de Departamento Agregado";
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
            ViewBag.tipodepartamentos = new SelectList(TipoDepartamento(), "idTipo", "descripcion", reg.idTipo);
            return View(reg);
        }

        public ActionResult Edit(int id)
        {
            PagoD1 reg = objpagod.BuscarPagoDepartamento(id);
            ViewBag.propietarios = new SelectList(objprop.ListarPropietarios(),
                "idProp", "nomProp", reg.idProp);
            ViewBag.tipodepartamentos = new SelectList(objtipd.ListarTipoDepartamentos(),
                "idTipo", "descripcion", reg.idTipo);
            return View(objpagod.BuscarPagoDepartamento(id));
        }

        [HttpPost]
        public ActionResult Edit(PagoD1 reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objpagod.ActualizarPagoDepartamento(reg);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            { return View(); }
        }

        public ActionResult Delete(int id)
        {
            PagoD1 pro = objpagod.BuscarPagoDepartamento(id);
            return View(pro);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PagoD1 pro = objpagod.BuscarPagoDepartamento(id);
            objpagod.BajaPagoDepartamento(pro);
            return RedirectToAction("Index");
        }
    }
}