using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSP_Covid21.Models.BUS;
using TSP_Covid21.Models.ShopEntity;

namespace TSP_Covid21.Areas.Admin.Controllers
{
    public class PictureManagerController : Controller
    {
        private Picture_BUS PB;

        public PictureManagerController()
        {
            PB = new Picture_BUS();
        }

        public IEnumerable<PICTURE> loadPic()
        {
            return PB.listPic();
        }

        public ActionResult listPic()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult SaveFile(HttpPostedFileBase file) 
        {
            if (file.ContentLength > 0 && file != null)
            {
                string fileName, fileExtension, imaageSavePath;
                fileName = Path.GetFileNameWithoutExtension(file.FileName);
                fileExtension = Path.GetExtension(file.FileName);

                imaageSavePath = Server.MapPath("~/Asset/images/Products/") + fileName + fileExtension;
                string path = "/Asset/images/Products/" + fileName + fileExtension;
                string link = "https://covid21tsp.space" + path; 
                //Save file
                if (!System.IO.File.Exists(imaageSavePath))
                {
                    file.SaveAs(imaageSavePath);
                    PB.InsertPic(link, path);
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public void DelPic(int id)
        {
            PICTURE pic = PB.pic(id);
            string path = Server.MapPath(pic.PATH); // giúp tìm đường dẫn tuyệt đối
            System.IO.File.Delete(path);
            PB.del(id);
        }
    }
}