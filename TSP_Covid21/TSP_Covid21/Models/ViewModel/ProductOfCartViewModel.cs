using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSP_Covid21.Models.ViewModel
{
    public class ProductOfCartViewModel
    {
        public int ProductId { set; get; }
        public string ProductName { set; get; }
        public string MainPic { set; get; }
        public Nullable<int> ProductPrice { set; get; }
        public Nullable<int> ProductAmount { set; get; }

        // Giúp kiểm tra sản phẩm có muốn mua hay không
        public Nullable<bool> Status { get; set; }
    }
}