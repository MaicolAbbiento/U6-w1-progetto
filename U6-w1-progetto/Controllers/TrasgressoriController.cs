using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U6_w1_progetto.Models;

namespace U6_w1_progetto.Controllers
{
    public class TrasgressoriController : Controller
    {
        // GET: Trasgressori
        [HttpGet]
        public ActionResult Trasgressori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Trasgressori(Trasgressore p)
        {
            Trasgressore trasgressore = new Trasgressore();
            trasgressore.addDb(p);
            return View();
        }
    }
}