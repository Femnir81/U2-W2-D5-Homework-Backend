using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using U2_W2_D5_Homework_Backend.Models;

namespace U2_W2_D5_Homework_Backend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserTab utente)
        {
            if (UserTab.UserAutenticato(utente.Username, utente.Password))
            {
                FormsAuthentication.SetAuthCookie(utente.Username, false);
                return Redirect(FormsAuthentication.DefaultUrl);
            }
            else
            {
                ViewBag.messaggio = "Credenziali errate, riprova.";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }
    }
}