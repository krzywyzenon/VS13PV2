//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PizzeriaV2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Matratt
    {
        public Matratt()
        {
            this.BestallningMatratts = new HashSet<BestallningMatratt>();
            this.Produkts = new HashSet<Produkt>();
        }
    
        public int MatrattID { get; set; }
        public string MatrattNamn { get; set; }
        public string Beskrivning { get; set; }
        public int Pris { get; set; }
        public int MatrattTyp { get; set; }
    
        public virtual ICollection<BestallningMatratt> BestallningMatratts { get; set; }
        public virtual MatrattTyp MatrattTyp1 { get; set; }
        public virtual ICollection<Produkt> Produkts { get; set; }
    }
}