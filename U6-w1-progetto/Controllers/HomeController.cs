using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U6_w1_progetto.Models;
using WebGrease;

namespace U6_w1_progetto.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult verbale()
        {
            Trasgressione trasgressione = new Trasgressione();
            List<Trasgressione> tragressioni = trasgressione.GetTrasgressione();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var t in tragressioni)
            {
                SelectListItem item = new SelectListItem { Text = t.descrizione, Value = t.id.ToString() };
                list.Add(item);
            }

            ViewBag.Listaviolazioni = list;
            Trasgressore trasgressore = new Trasgressore();
            List<Trasgressore> trasgressori = trasgressore.GetTrasgressione();
            List<SelectListItem> list2 = new List<SelectListItem>();
            foreach (var t in trasgressori)
            {
                SelectListItem item = new SelectListItem { Text = t.Nome + t.Cognome, Value = t.Idanagrafica.ToString() };
                list2.Add(item);
            }
            ViewBag.ListaPersone = list2;
            return View();
        }

        [HttpPost]
        public ActionResult verbale(Verbale p, string nomeTragressore, string nomeviolazione)
        {
            Verbale verbale = new Verbale();
            verbale.AddDb(p, nomeTragressore, nomeviolazione);

            return RedirectToAction("verbale");
        }
    }
}