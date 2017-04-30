using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Denim.Models;
using System.Web.Services;
using System.IO;

namespace Denim.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        DenimEntities Db = new DenimEntities();
        Event obj = new Event();
        public ActionResult Index()
        {
            return View();
        }

        List<Gallery> files;
        public ActionResult Gallery()
        {
            files = GalleyList();
            //objHomeGallery.ImageList = files;
            //objHomeGallery = files;
            return View("Gallery", files);
        }
        public List<Gallery> GalleyList()
        {
            string folderPath = Server.MapPath("~/Gallery/");
            string[] filePaths = Directory.GetFiles(folderPath);
            files = new List<Gallery>();
            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileName(filePath);
                files.Add(new Gallery
                {
                    ImageName = fileName.Split('.')[0].ToString(),
                    ImagePath = "~/Gallery/" + fileName
                });
            }
            List<Gallery> objimg = files.ToList();
            return objimg;
        }
        public ActionResult Calculator()
        {
            return View();
        }
        static string tablehtml = "";

        [WebMethod]
        public string SendAmmortization(string table)
        {
            ViewBag.AmmorTable = table;
            tablehtml = table;
            return table;
        }

        [WebMethod]
        public ActionResult viewTable()
        {
            ViewBag.AmmorTable = tablehtml;
            return View();
        }
    }
}