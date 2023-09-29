using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U6_w1_progetto.Models;

namespace U6_w1_progetto.Controllers
{
    public class RegistoVerbaliController : Controller
    {
        // GET: RegistoVerbali
        public ActionResult RegistoVerbali()
        {
            Verbale verbale = new Verbale();

            int n = verbale.Totverbali();
            ViewBag.n = n;
            return View();
        }

        public ActionResult GetPartialViewVerbaliNome()
        {
            Verbale verbale = new Verbale();
            List<Registoverbali> r = verbale.getverbaliPernome();

            return PartialView("_GetPartialViewVerbaliNome", r);
        }

        public ActionResult showpartial1(string parameter)
        {
            if (parameter == null)
            {
                Session["MessaggioDiConferma1"] = "a";
            }
            else
            {
                Session["MessaggioDiConferma1"] = null;
            }
            return RedirectToAction("RegistoVerbali"); ;
        }

        public ActionResult GetPartialViewVerbaliPunti()
        {
            Verbale verbale = new Verbale();
            List<Registoverbali> r = verbale.getVerbaliPunti();

            return PartialView("_GetPartialViewVerbaliPunti", r);
        }

        public ActionResult showpartial2(string parameter)
        {
            if (parameter == null)
            {
                Session["MessaggioDiConferma2"] = "a";
            }
            else
            {
                Session["MessaggioDiConferma2"] = null;
            }
            return RedirectToAction("RegistoVerbali"); ;
        }

        public ActionResult showpartial3(string parameter)
        {
            if (parameter == null)
            {
                Session["MessaggioDiConferma3"] = "a";
            }
            else
            {
                Session["MessaggioDiConferma3"] = null;
            }
            return RedirectToAction("RegistoVerbali"); ;
        }

        public ActionResult showpartial4(string parameter)
        {
            if (parameter == null)
            {
                Session["MessaggioDiConferma4"] = "a";
            }
            else
            {
                Session["MessaggioDiConferma4"] = null;
            }
            return RedirectToAction("RegistoVerbali"); ;
        }

        public ActionResult GetPartialViewVerbaliPuntiMaggiore10()
        {
            Verbale verbale = new Verbale();
            List<Registoverbali> r = verbale.getverbalisopra10Punti();

            return PartialView("_GetPartialViewVerbaliPuntiMaggiore10", r);
        }

        public ActionResult GetPartialViewVerbali400()
        {
            Verbale verbale = new Verbale();
            List<Registoverbali> r = verbale.getverbalisopra400();

            return PartialView("_GetPartialViewVerbali400", r);
        }
    }
}