using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Brand
    {
        public Brand()
        {
            Product = new HashSet<Product>();
            Tempproduct = new HashSet<Tempproduct>();
        }

        public int Brandid { get; set; }
        public string Brandname { get; set; }
        public bool? Statusbrand { get; set; }

        public ICollection<Product> Product { get; set; }
        public ICollection<Tempproduct> Tempproduct { get; set; }
    }
}
