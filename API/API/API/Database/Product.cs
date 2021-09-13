using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Product
    {
        public Product()
        {
            Accountlike = new HashSet<Accountlike>();
            Billdetail = new HashSet<Billdetail>();
            Cart = new HashSet<Cart>();
            Comment = new HashSet<Comment>();
            Configdetail = new HashSet<Configdetail>();
            PicProduct = new HashSet<PicProduct>();
            Ratingproduct = new HashSet<Ratingproduct>();
            Viewnumber = new HashSet<Viewnumber>();
        }

        public int Productid { get; set; }
        public string Productname { get; set; }
        public int? Brandid { get; set; }
        public int? Producttypeid { get; set; }
        public bool? Statusproduct { get; set; }
        public int? Productprice { get; set; }
        public int? Productamount { get; set; }
        public string Decription { get; set; }
        public DateTime? Dateadd { get; set; }

        public Brand Brand { get; set; }
        public Producttype Producttype { get; set; }
        public ICollection<Accountlike> Accountlike { get; set; }
        public ICollection<Billdetail> Billdetail { get; set; }
        public ICollection<Cart> Cart { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<Configdetail> Configdetail { get; set; }
        public ICollection<PicProduct> PicProduct { get; set; }
        public ICollection<Ratingproduct> Ratingproduct { get; set; }
        public ICollection<Viewnumber> Viewnumber { get; set; }
    }
}
