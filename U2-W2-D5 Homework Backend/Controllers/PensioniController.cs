using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U2_W2_D5_Homework_Backend.Models;

namespace U2_W2_D5_Homework_Backend.Controllers
{
    [Authorize]
    public class PensioniController : Controller
    {
        // GET: Pensioni
        public ActionResult PartialViewIndex()
        {
            return PartialView("_PartialViewIndex", Pensione.GetPensioni());
        }

        public ActionResult Details(int id)
        {
            return View(Pensione.GetPensione(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Pensione pens)
        {
            try
            {
                Pensione.CreatePensione(pens);
                return RedirectToAction("Index", "Gestione");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(Pensione.GetPensione(id));
        }

        [HttpPost]
        public ActionResult Edit(Pensione pens, int id)
        {
            try
            {
                Pensione.EditPensione(pens, id);
                return RedirectToAction("Index", "Gestione");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Pensione.DeletePensione(id);
            return RedirectToAction("Index", "Gestione");
        }
    }
}
