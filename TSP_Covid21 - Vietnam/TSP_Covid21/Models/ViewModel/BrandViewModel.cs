using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Models.ViewModel
{
    public class BrandViewModel
    {
        public int ? BrandId { set; get; }
        public string BrandName { set; get; }

        public virtual ICollection<PRODUCT> Product { set; get; }
    }
}