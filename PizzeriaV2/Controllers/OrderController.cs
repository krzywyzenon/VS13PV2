using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaV2.Models;

namespace PizzeriaV2.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        
        public ActionResult AddToOrderro(int id)
        {
            int matId = id;
            Matratt matratt;
            List<Matratt> matratts;
            if (Session["Order"] == null)
            {
                Session["Order"] = new List<Matratt>();
            }

            matratts = (List<Matratt>) Session["Order"];


            using (PizzaConnection db = new PizzaConnection())
            {
                matratt = db.Matratts.SingleOrDefault(p => p.MatrattID == matId);
            }

            matratts.Add(matratt);

            Session["Order"] = matratts;
            return RedirectToAction("Index", "Menu");
        }

        //Test na elegancje
        public ActionResult AddToOrder(int id)
        {
            int matId = id;
            Matratt matratt;
            using (PizzaConnection db = new PizzaConnection())
            {
                matratt = db.Matratts.SingleOrDefault(p => p.MatrattID == matId);
            }
            OrderSingleModel orderSingleModel = new OrderSingleModel();
            orderSingleModel.Matratt = matratt;
            return View(orderSingleModel);
        }

        [HttpPost]
        public ActionResult AddToOrder(OrderSingleModel model)
        {
            if (ModelState.IsValid == true)
            {
                
                Dictionary<int, int> currentOrder;
                if (Session["Order"] == null)
                {
                    Session["Order"] = new Dictionary<int, int>();
                }

                currentOrder = (Dictionary<int, int>) Session["Order"];

                if (currentOrder.ContainsKey(model.Matratt.MatrattID))
                {
                    currentOrder[model.Matratt.MatrattID] += model.Amount;
                }
                else
                {
                    currentOrder.Add(model.Matratt.MatrattID, model.Amount);
                }

                return RedirectToAction("Index", "Menu");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult ShowOrder()
        {
            Dictionary<int, int> currentOrder = new Dictionary<int, int>();
            OrderModel orderModel = new OrderModel();
            List<string> matratNamn = new List<string>();
            List<int> matratCount = new List<int>();
            List<int> prices = new List<int>();

            if (Session["Order"] != null)
            {
                currentOrder = (Dictionary<int, int>)Session["Order"];
            }

            if (currentOrder.Count > 0)
            {
                foreach (KeyValuePair<int, int> entry in currentOrder)
                {
                    using (PizzaConnection db = new PizzaConnection())
                    {
                        string name = db.Matratts.SingleOrDefault(p => p.MatrattID == entry.Key).MatrattNamn;
                        int price = db.Matratts.SingleOrDefault(p => p.MatrattID == entry.Key).Pris;
                        matratNamn.Add(name);
                        prices.Add(price);
                    }
                    matratCount.Add(entry.Value);
                }
                orderModel.IdsOccurances = currentOrder;
                orderModel.Names = matratNamn;
                orderModel.Occurances = matratCount;
                orderModel.Prices = prices;
                orderModel.isEmpty = false;
            }
            else
            {
                orderModel.isEmpty = true;
            }

            return View(orderModel);
        }

        [HttpPost]
        public ActionResult PlaceOrder(OrderModel model)
        {
            int orderId;
            int totalValue = model.TotalValue;
            int customerId = model.KundId;
            //Dictionary<int, int> itemsInOrder = model.IdsOccurances; CZEMU NIE DZIALA ODKRYC LUB SPYTAC
            Dictionary<int, int> itemsInOrder = (Dictionary<int, int>) Session["Order"];

            Bestallning bestallning = new Bestallning();
            bestallning.KundID = customerId;
            bestallning.BestallningDatum = DateTime.Now;
            bestallning.Totalbelopp = totalValue;
            bestallning.Levererad = false;
            using (PizzaConnection db = new PizzaConnection())
            {
                db.Bestallnings.Add(bestallning);
                db.SaveChanges();

                orderId = bestallning.BestallningID;

                foreach (var itemAmount in itemsInOrder)
                {
                    BestallningMatratt bm = new BestallningMatratt();
                    bm.MatrattID = itemAmount.Key;
                    bm.Antal = itemAmount.Value;
                    bm.BestallningID = orderId;
                    db.BestallningMatratts.Add(bm);
                }

                db.SaveChanges();
            }
            Session["Order"] = null;
            return View(); //TODO: Wreszcie, validering, css i done!!!
        }

    }
}