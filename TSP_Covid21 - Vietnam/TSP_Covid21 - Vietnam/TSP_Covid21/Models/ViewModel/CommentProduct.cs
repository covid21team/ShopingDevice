using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSP_Covid21.Models.ViewModel
{
    public class CommentProduct
    {
        public string fullName { get; set;}
        public Nullable<System.DateTime> dateComment { get; set;}
        public string commentText { get; set; }

        public float rating { get; set; }
    }
}