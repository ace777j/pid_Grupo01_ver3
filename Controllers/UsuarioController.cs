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

namespace ProyectoPidG01.Controllers
{
    public class UsuarioController : Controller
    {
        private DB_PIGrupo01Entities1 db = new DB_PIGrupo01Entities1();

        UsuarioDAO objUsu = new UsuarioDAO();
        
        public ActionResult Create()
        {
            ViewBag.roles = new SelectList(db.usp_RolListar(), "id", "nombre");
            return View("Create", "_Layout",new usuario());
        }

        [HttpPost]
        public ActionResult Create(usuario reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objUsu.InsertarUsuario(reg);
                    return RedirectToAction("Create");
                }
                return RedirectToAction("Create");
            }
            catch
            { return View(); }
        }
    }
}