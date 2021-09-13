using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Comment
    {
        public int Commentid { get; set; }
        public int? Productid { get; set; }
        public string User { get; set; }
        public string Commenttext { get; set; }
        public DateTime? Datecomment { get; set; }

        public Product Product { get; set; }
        public Account UserNavigation { get; set; }
    }
}
