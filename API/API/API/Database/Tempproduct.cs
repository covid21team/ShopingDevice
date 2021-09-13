using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Tempproduct
    {
        public int Producttypeid { get; set; }
        public int Brandid { get; set; }
        public bool? Tempproductstatus { get; set; }

        public Brand Brand { get; set; }
        public Producttype Producttype { get; set; }
    }
}
