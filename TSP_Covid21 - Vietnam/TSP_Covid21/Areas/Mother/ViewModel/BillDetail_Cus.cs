using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Areas.Mother.ViewModel
{
    public class BillDetail_Cus
    {
        public virtual ICollection<ww_BillDetail> BillDetail { get; set; }
        public virtual ww_Customer Customer { get; set; }
    }
}