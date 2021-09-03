using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSP_Covid21.Areas.Admin.Models.ViewModel
{
    public class ListPic_Edit
    {
        public int pictureId { set; get; }
        public string path { set; get; }
        public bool ? mainPic { set; get; }
        public bool Pic { set; get; }
    }
}