using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace U2_W2_D5_Homework_Backend.Models
{
    [Authorize]
    public class PrenotazioniController : Controller
    {
        // GET: Prenotazioni
        public ActionResult PartialViewIndex()
        {
            return PartialView("_PartialViewIndex", Prenotazione.GetPrenotazioni());
        }

        public ActionResult Details(int id)
        {
            return View(Prenotazione.GetPrenotazione(id));
        }

        public ActionResult Create()
        {
            ViewBag.DropdownClienti = Cliente.DropDownCliente();
            ViewBag.DropdownCamere = Camera.DropDownCamera();
            ViewBag.DropdownPensioni = Pensione.DropDownPensione();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Prenotazione prenot)
        {
            try
            {
                Prenotazione.CreatePrenotazione(prenot);
                return RedirectToAction("Index", "Gestione");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.DropdownCamere = Camera.DropDownCamera();
            ViewBag.DropdownPensioni = Pensione.DropDownPensione();
            return View(Prenotazione.GetPrenotazione(id));
        }

        [HttpPost]
        public ActionResult Edit(Prenotazione prenot, int id)
        {
            try
            {
                Prenotazione.EditPrenotazione(prenot, id);
                return RedirectToAction("Index", "Gestione");
            }
            catch
            {
                return View();
            }
        }

        // GET: Prenotazioni/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Prenotazioni/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Checkout(int id) 
        {
            ViewBag.TotaleDaPagare = Prenotazione.GetCostoPrenotazioneCliente(id);
            return View(Prenotazione.GetPrenotazioneInfoCheckout(id));
        }
    }
}
