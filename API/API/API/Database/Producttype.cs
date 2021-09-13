using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Producttype
    {
        public Producttype()
        {
            Product = new HashSet<Product>();
            Tempproduct = new HashSet<Tempproduct>();
        }

        public int Producttypeid { get; set; }
        public string Producttypename { get; set; }
        public bool? Statusproducttype { get; set; }

        public ICollection<Product> Product { get; set; }
        public ICollection<Tempproduct> Tempproduct { get; set; }
    }
}
