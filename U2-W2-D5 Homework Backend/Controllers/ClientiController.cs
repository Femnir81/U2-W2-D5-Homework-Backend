using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U2_W2_D5_Homework_Backend.Models;

namespace U2_W2_D5_Homework_Backend.Controllers
{
    [Authorize]
    public class ClientiController : Controller
    {
        // GET: Clienti
        public ActionResult PartialViewIndex()
        {
            return PartialView("_PartialViewIndex", Cliente.GetClienti());
        }

        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Cliente client)
        {
            Cliente.CreateCliente(client);
            return RedirectToAction("Index", "Gestione");
        }

        public ActionResult Edit(int id) 
        {
            return View(Cliente.GetCliente(id));
        }

        [HttpPost]
        public ActionResult Edit(Cliente client, int id) 
        {
            Cliente.EditCliente(client, id);
            return RedirectToAction("Index", "Gestione");
        }
    }
}