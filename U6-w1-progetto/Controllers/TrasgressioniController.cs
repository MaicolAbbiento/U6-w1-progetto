using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using U6_w1_progetto.Models;

namespace U6_w1_progetto.Controllers
{
    public class TrasgressioniController : Controller
    {
        // GET: Trasgressioni
        [HttpGet]
        public ActionResult tragressioni()
        {
            return View();
        }

        [HttpPost]
        public ActionResult tragressioni(Trasgressione p)
        {
            Trasgressione trasgressore = new Trasgressione();
            trasgressore.addDb(p);
            return RedirectToAction("tragressioni");
        }

        public ActionResult GetPartialViewTrasgressioni()
        {
            Trasgressione trasgressione = new Trasgressione();
            List<Trasgressione> p = trasgressione.GetTrasgressione();
            return PartialView("_GetPartialViewTrasgressioni", p);
        }
    }
}