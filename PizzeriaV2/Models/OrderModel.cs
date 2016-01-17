using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzeriaV2.Models
{
    public class OrderModel
    {
        public Dictionary<int, int> IdsOccurances { get; set; }
        public List<string> Names { get; set; }
        public List<int> Occurances { get; set; }
        public bool isEmpty { get; set; }
        public List<int> Prices { get; set; }

        public int TotalValue { get; set; }
        public int KundId { get; set; }
    }
}