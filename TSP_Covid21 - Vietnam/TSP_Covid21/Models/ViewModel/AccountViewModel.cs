using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Models.ViewModel
{
    public class AccountViewModel
    {
        public ACCOUNT account { get; set; }
        /*
         Ý nghĩa của code: giúp page personal nhận biết người đang muốn vào thư mục nào trong page
         1: Vào thư mục chính (Hiện thông tin tài khoản)
         2: Vào thư mục địa chỉ
         3: Vào thư mục hóa đơn
             */
        public int code { get; set; }
    }
}