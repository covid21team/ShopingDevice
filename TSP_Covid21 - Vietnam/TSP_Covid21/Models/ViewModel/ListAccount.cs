using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSP_Covid21.Models.ViewModel
{
    public class ListAccount
    {
        public string USER { get; set; }
        public string FULLNAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }
        public Nullable<int> QUANTITYPRODUCT { get; set; }
        public Nullable<int> TOTAL { get; set; }
    }
}