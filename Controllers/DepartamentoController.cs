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
    public class DepartamentoController : Controller
    {
        private DB_PIGrupo01Entities1 db = new DB_PIGrupo01Entities1();

        SqlConnection cn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DB_PIGrupo011"].ConnectionString);
        // GET: Departamento
        EstadoDAO objest = new EstadoDAO();
        TipoDepartamentoDAO objtipdep = new TipoDepartamentoDAO();
        DepartamentoDAO objdep = new DepartamentoDAO();

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
                    idDepa = dr.GetInt32(0),

                    nroPiso = dr.GetString(1),

                    fechaRegistro = dr.GetDateTime(2),

                    usuReg = dr.GetInt32(3),

                    idEstado = dr.GetInt32(4),

                    idTipo = dr.GetInt32(5)
                };
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }

        List<Estado1> Estados()

        {

            List<Estado1> temporal = new List<Estado1>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Estado", cn);

            cmd.CommandType = CommandType.Text;

            cn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())

            {

                Estado1 reg = new Estado1

                {

                    idEstado = dr.GetInt32(0),

                    descripcion = dr.GetString(1)

                };

                temporal.Add(reg);

            }

            dr.Close(); cn.Close();

            return temporal;

        }

        List<TipoDepartamento1> TipoDepartamentos()

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
            return View(objdep.ListarDepartamentos().ToList());
        }

        public ActionResult Details(int id)
        {
            return View(objdep.BuscarDepartamento(id));
        }

        public ActionResult Create()
        {
            ViewBag.estados = new SelectList(Estados(), "idEstado", "descripcion");
            ViewBag.tipodepartamentos = new SelectList(TipoDepartamentos(), "idTipo", "descripcion");
            ViewBag.usuarios = new SelectList(Usuarios(), "id", "nombre");
            return View(new Departamento1());
        }

        [HttpPost]

        public ActionResult Create(Departamento1 reg)
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
                SqlCommand cmd = new SqlCommand("usp_DepartamentoInsertar", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nroPiso", reg.nroPiso);
                cmd.Parameters.AddWithValue("@usuReg", reg.usuReg);
                cmd.Parameters.AddWithValue("@idEstado", reg.idEstado);
                cmd.Parameters.AddWithValue("@idTipo", reg.idTipo);
                int q = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.mensaje = q.ToString() + " Departamento Agregado";
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

            ViewBag.estados = new SelectList(Estados(), "idEstado", "descripcion", reg.idEstado);
            ViewBag.tipodepartamentos = new SelectList(TipoDepartamentos(), "idTipo", "descripcion", reg.idTipo);
            ViewBag.usuarios = new SelectList(Usuarios(), "id", "nombre", reg.usuReg);
            return View(reg);
        }

        public ActionResult Edit(int id)
        {
            Departamento1 reg = objdep.BuscarDepartamento(id);
            ViewBag.estados = new SelectList(objest.ListarEstados(),
                "idEstado", "descripcion", reg.idEstado);
            ViewBag.tipodepartamentos = new SelectList(objtipdep.ListarTipoDepartamentos(),
                "idTipo", "descripcion", reg.idTipo);
            return View(objdep.BuscarDepartamento(id));
        }

        [HttpPost]
        public ActionResult Edit(Departamento1 reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objdep.ActualizarDepartamento(reg);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            { return View(); }
        }

        public ActionResult Delete(int id)
        {
            Departamento1 pro = objdep.BuscarDepartamento(id);
            return View(pro);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Departamento1 pro = objdep.BuscarDepartamento(id);
            objdep.BajaDepartamento(pro);
            return RedirectToAction("Index");
        }
    }
}