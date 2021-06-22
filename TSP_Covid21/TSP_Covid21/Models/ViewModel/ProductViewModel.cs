using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Models.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { set; get; }
        public string ProductName { set; get; }
        public string BrandName { set; get; }
        public string ProductTypeName { set; get; }
        public string MainPic { set; get; }
        public string Pic1 { set; get; }
        public string Pic2 { set; get; }
        public string Pic3 { set; get; }
        public string Pic4 { set; get; }
        public Nullable<int> ProductPrice { set; get; }
        public Nullable<int> ProductView { set; get; }
        public Nullable<int> ProductAmount { set; get; }
        public string Decription { set; get; }
        public Nullable<DateTime> DateAdd { set; get; }
        public virtual ICollection<CONFIGDETAIL> ProductConfig { set; get; }
    }
}