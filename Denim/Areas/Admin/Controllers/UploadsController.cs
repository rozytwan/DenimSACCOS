using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services;
using Microsoft.SqlServer.Server;

namespace Denim.Areas.Admin.Controllers
{
    public class UploadsController : Controller
    {
        // GET: Admin/Uploads
        //Insert For the files
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Areas/Files/"), fileName);
                file.SaveAs(path);
            }

            return View();
        }

        public ActionResult Downloads()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Areas/Files/"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");
            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            return View(items);
        }
        //
        //Insert for Gallary Images

        public ActionResult Uploadgal()
        {
            return View();
        }

        public ActionResult UploadGallary(HttpPostedFileBase file)
        {
            string path = Server.MapPath("~/Gallery/" + file.FileName);
            //file path
            file.SaveAs(path);//save file
            ViewBag.path = path;
            return View();

        }

        public ActionResult DownloadGallary()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Gallery/"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");
            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            return View(items);
        }

        public bool isFileValid(HttpPostedFileBase file)
        {
            Bitmap bitmp = new Bitmap(file.InputStream);
            if (bitmp.Width == 1140 | bitmp.Height == 350)
            {

                return true;
            }
            else
            {

                return false;
            }
        }


        //Insert for Slider Images
        public ActionResult UploadSlid()
        {
            return View();
        }
        public ActionResult UploadSlider(HttpPostedFileBase file)
        {
            if (isFileValid(file))
            {
                string path = Server.MapPath("~/Images/" + file.FileName);
                file.SaveAs(path);
                ViewBag.path = path;
            }
            else
            {
                ViewBag.path = "Invalid Dimensions";
                return View("UploadSlid");
            }

            return View();
        }
        public ActionResult DownloadImages()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Images/"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");
            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            return View(items);
        }
        //public FileResult DownloadGal(string ImageName)
        //{
        //    var FileVirtualPath = "~/Gallery/" + ImageName;
        //    return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));

        //}





        //Delete for all files, Images And Slider
        public ActionResult Delete(string ImageName)
        {

            string fullPath = Request.MapPath("~/Areas/Files/" + ImageName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);

            }
            return RedirectToAction("Downloads");
        }
        public ActionResult DeleteImg(string ImageName)
        {

            string fullPath = Request.MapPath("~/Images/" + ImageName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);

            }
            return RedirectToAction("DownloadImages");
        }
        public ActionResult DeleteGal(string ImageName)
        {

            string fullPath = Request.MapPath("~/Gallery/" + ImageName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);

            }
            return RedirectToAction("DownloadGallary");
        }


        public JsonResult ImageUpload(Denim.Models.Gallery model)
        {
            var file = model.ImageFile;
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);
                file.SaveAs(Server.MapPath("~/Gallery/" + file.FileName));
            }
            return Json(file.FileName, JsonRequestBehavior.AllowGet);
        }
    }
}
