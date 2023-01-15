using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U2_W2_D5_Homework_Backend.Models;

namespace U2_W2_D5_Homework_Backend.Controllers
{
    [Authorize]
    public class GestioneController : Controller
    {
        // GET: Gestione
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPrenotazioniByCodFisc(string codicefiscale) 
        {
            List<Prenotazione> ListaPrenotazioni = Prenotazione.GetPrenotazioniByCodFisc(codicefiscale);
            return Json(ListaPrenotazioni, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPrenotazioniPensCompl()
        {
            List<Prenotazione> ListaPrenotazioni = Prenotazione.GetPrenotazioniPensCompl();
            return Json(ListaPrenotazioni, JsonRequestBehavior.AllowGet);
        }
    }
}