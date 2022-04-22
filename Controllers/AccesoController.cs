using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoPidG01.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String User, string Pass)
        {
            try
            {
                using (Models.DB_PIGrupo01Entities1 db = new Models.DB_PIGrupo01Entities1())
                {
                    var oUser = (from d in db.usuario
                                where d.email == User && d.password == Pass.Trim()
                                select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usario o Contraseña Invalida";
                        return View();
                    }

                    Session["User"] = oUser;
                       
                }
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }


    }
}