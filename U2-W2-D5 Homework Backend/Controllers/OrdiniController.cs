using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U2_W2_D5_Homework_Backend.Models;

namespace U2_W2_D5_Homework_Backend.Controllers
{
    [Authorize]
    public class OrdiniController : Controller
    {
        public ActionResult AddOrdine()
        {
            ViewBag.DropdownServizi = Servizi.DropDownServizi();
            return View();
        }

        [HttpPost]
        public ActionResult AddOrdine(Ordini ordine, int id)
        {
            try
            {
                Ordini.AddOrdine(ordine, id);
                return RedirectToAction("Index", "Gestione");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ServiziUsufruiti(int id)
        {
            return View(Ordini.GetOrdinibyServizio(id));
        }

        public ActionResult PartialViewServiziCheckout(int id) 
        {
            return PartialView("_PartialViewServiziCheckout", Ordini.GetOrdinibyServizio(id));
        }
    }
}