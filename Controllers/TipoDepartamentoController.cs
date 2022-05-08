using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using ProyectoDSWI.Models;

namespace ProyectoPidG01.Controllers
{
    public class TipoDepartamentoController : Controller
    {
        SqlConnection cn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DB_PIGrupo011"].ConnectionString);

        TipoDepartamentoDAO objtipdep = new TipoDepartamentoDAO();

        // GET: TipoDepartamento
        public ActionResult Index()
        {
            return View(objtipdep.ListarTipoDepartamentos().ToList());
        }
    }
}