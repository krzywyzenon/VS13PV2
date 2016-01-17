using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaV2.Models;

namespace PizzeriaV2.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult ShowProfile()
        {
            int id = (int) Session["CustomerId"];
            Kund kund;
            using (PizzaConnection db = new PizzaConnection())
            {
                kund = db.Kunds.SingleOrDefault(p => p.KundID == id);
            }
            return View(kund);
        }

        public ActionResult Update(Kund k)
        {
            Kund kund;
            using (PizzaConnection db = new PizzaConnection())
            {
                kund = db.Kunds.SingleOrDefault(p => p.KundID == k.KundID);
                kund.Namn = k.Namn;
                kund.Gatuadress = k.Gatuadress;
                kund.Postnr = k.Postnr;
                kund.Postort = k.Postort;
                kund.Email = k.Email;
                kund.Telefon = k.Telefon;
                db.SaveChanges();
            }
            return View();
        }
    }
}