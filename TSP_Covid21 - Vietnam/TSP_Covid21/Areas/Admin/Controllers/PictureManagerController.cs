using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost]
        public ActionResult upPic(HttpPostedFileBase[] files)
        {
            foreach (HttpPostedFileBase file in files)
            {
                //Checking file is available to save.  
                if (file != null)
                {
                    var InputFileName = Path.GetFileName(file.FileName);
                    var ServerSavePath = Path.Combine(Server.MapPath("~/Asset/images/Loading/") + InputFileName);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                }

            }
            return RedirectToAction("PictureManager","Admin");
        }

        public JsonResult SaveFile(HttpPostedFileBase file)
        {
            string returnImagePath = string.Empty;
            if (file.ContentLength > 0)
            {
                string fileName, fileExtension, imaageSavePath;
                fileName = Path.GetFileNameWithoutExtension(file.FileName);
                fileExtension = Path.GetExtension(file.FileName);

                imaageSavePath = Server.MapPath("~/Asset/images/Loading/") + fileName + fileExtension;
                //Save file
                file.SaveAs(imaageSavePath);

                //Path to return
                returnImagePath = "/uploadedImages/" + fileName + fileExtension;
            }
            return Json(returnImagePath, JsonRequestBehavior.AllowGet);
        }
    }
}