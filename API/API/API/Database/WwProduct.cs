using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class WwProduct
    {
        public WwProduct()
        {
            WwBillDetail = new HashSet<WwBillDetail>();
        }

        public int ProductId { get; set; }
        public string ProdutName { get; set; }
        public string Pic { get; set; }
        public int Price { get; set; }
        public double Quantity { get; set; }
        public bool ProductState { get; set; }

        public ICollection<WwBillDetail> WwBillDetail { get; set; }
    }
}
