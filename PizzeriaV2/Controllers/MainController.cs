using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaV2.Models;

namespace PizzeriaV2.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Start()
        {
            if (Session["Logged"] == null)
            {
                Session["Logged"] = -1;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Start(string something)
        {
            string u = Request["username"];
            string p = Request["password"];
            Kund kund;
            using (PizzaConnection db = new PizzaConnection())
            {
                kund = db.Kunds.SingleOrDefault(x => x.AnvandarNamn == u && x.Losenord == p);
            }

            if (kund != null)
            {
                Session["Logged"] = 1;
                Session["Customer"] = kund.Namn;
                Session["CustomerId"] = kund.KundID;
            }
            else
            {
                ViewBag.ErrorMessage = "Login invalid";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Start");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Kund k)
        {
            if (ModelState.IsValid == true)
            {
                
                using (PizzaConnection db = new PizzaConnection())
                {
                    db.Kunds.Add(k);
                    db.SaveChanges();
                }
                @ViewBag.ErrorMessage = null;
                return View(k);
            }
            else
            {
                @ViewBag.ErrorMessage = "wrong";
                return View(k);
            }
        }
    }
}