using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U2_W2_D5_Homework_Backend.Models;

namespace U2_W2_D5_Homework_Backend.Controllers
{
    [Authorize]
    public class CamereController : Controller
    {
        // GET: Camere
        public ActionResult PartialViewIndex()
        {
            return PartialView("_PartialViewIndex", Camera.GetCamere());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Camera stanza)
        {
            try
            {
               Camera.CreateCamera(stanza);
                return RedirectToAction("Index", "Gestione");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            return View(Camera.GetCamera(id));
        }

        public ActionResult Edit(int id)
        {
            return View(Camera.GetCamera(id));
        }

        [HttpPost]
        public ActionResult Edit(Camera stanza, int id)
        {
            try
            {
                Camera.EditCamera(stanza, id);
                return RedirectToAction("Index", "Gestione");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Camera.DeleteCamera(id);
            return RedirectToAction("Index", "Gestione");
        }
    }
}
