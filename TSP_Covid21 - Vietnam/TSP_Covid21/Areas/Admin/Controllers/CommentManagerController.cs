using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.ViewModel;
using TSP_Covid21.Models.BUS;

namespace TSP_Covid21.Areas.Admin.Controllers
{
    public class CommentManagerController : Controller
    {
        Comment_BUS CB;

        public CommentManagerController()
        {
            CB = new Comment_BUS();
        }
        // GET: Admin/CommentManager
        public IEnumerable<ListComment> listComment()
        {
            return CB.listComment();
        }

        public ActionResult repComment()
        {
            return PartialView();
        }
    }
}