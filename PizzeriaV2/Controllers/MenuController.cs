using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaV2.Models;

namespace PizzeriaV2.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            List<Matratt> matratts;
            List<MatrattTyp> matrattTyps;
            MatrattTyp matrattTyp = new MatrattTyp();
            using(PizzaConnection db = new PizzaConnection())
            {
                matratts = db.Matratts.Select(p => p).Include(p => p.Produkts).ToList();
                matrattTyps = db.MatrattTyps.ToList();
            }
            Matratt matratt = new Matratt();
            MenuModel model = new MenuModel()
            {
                categories = matrattTyps.Select(x => new SelectListItem()
                {
                    Value = x.MatrattTyp1.ToString(),
                    Text = x.Beskrivning
                }),
                Matratts = matratts,
                Matratt = matratt,
                MatrattTyp = matrattTyp
            };
            //@Html.DropDownListFor(x => x.article.CategoryId, Model.categories)
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MenuModel tempModel)
        {
            List<Matratt> matratts;
            List<MatrattTyp> matrattTyps;
            
            using (PizzaConnection db = new PizzaConnection())
            {
                matratts = db.Matratts.Select(p => p).Include(p => p.Produkts).ToList();
                matratts = matratts.Where(p => p.MatrattTyp1.MatrattTyp1 == tempModel.MatrattTyp.MatrattTyp1).ToList();
                matrattTyps = db.MatrattTyps.ToList();
            }
            Matratt matratt = new Matratt();
            MenuModel model = new MenuModel()
            {
                categories = matrattTyps.Select(x => new SelectListItem()
                {
                    Value = x.MatrattTyp1.ToString(),
                    Text = x.Beskrivning
                }),
                Matratts = matratts,
                Matratt = matratt
            };

            return View(model);
        }
    }
}