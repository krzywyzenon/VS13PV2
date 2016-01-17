using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzeriaV2.Models
{
    public class MenuModel
    {
        public IEnumerable<SelectListItem> categories { get; set; }
        public List<Matratt> Matratts { get; set; }
        public Matratt Matratt { get; set; }
        public MatrattTyp MatrattTyp { get; set; }
    }
}