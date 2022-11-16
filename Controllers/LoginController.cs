using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;


namespace WebDemo.Controllers
{
    public class LoginController : Controller
    {
        private DemoTibox dt = new DemoTibox();
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.sesion = false;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario usu, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Mensaje = "Datos de acceso incorrectos";
                return View(usu);
            }
            if (usu.passUsuario == null || usu.idUsuario== null)
            {
                ViewBag.Mensaje = "Datos de acceso incorrectos";
                return View();
            }
            Usuario usuario = dt.Usuario.Find(usu.idUsuario);
            if (usu.idUsuario == usuario.idUsuario && usu.passUsuario == usuario.passUsuario)
            {
                ViewBag.sesion = true;
                Session["sesion"]=true;
                Session["nombre"] = usuario.nomUsuario;
                return RedirectToAction("index", "Home");
            }
            else {
                ViewBag.Mensaje = "Datos de acceso incorrectos";
                return View();
            }
        }


        public ActionResult Logout()
        {
            Session["sesion"] = false;
            Session["nombre"] = "";
            return RedirectToAction("Login", "Login");
        }
    }
}