using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzeriaV2.Models
{
    public class OrderSingleModel
    {
        public Matratt Matratt { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Ett antal måste vara positiv")]
        public int Amount { get; set; }
    }
}