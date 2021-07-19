using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.BUS;

namespace TSP_Covid21.Areas.Admin.Controllers
{
    public class PictureManagerController : Controller
    {
        private Picture_BUS PB;

        public PictureManagerController()
        {
            PB = new Picture_BUS();
        }

        public IEnumerable<string> listPic()
        {
            return PB.listPic();
        }
    }
}