using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSP_Covid21.Models.ViewModel
{
    public class ListComment
    {
        public string USER { get; set; }
        public string NAME { get; set; }
        public Nullable<int> PRODUCTID { get; set; } 
        public string PRODUCTNAME { get; set; }
        public string COMMENTTEXT { get; set; } 
        public Nullable<DateTime> COMMENTDATE { get; set; }
    }
}