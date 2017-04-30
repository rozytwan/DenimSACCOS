using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Denim.Models;
using System.IO;
using System.Web.Services;
using PagedList.Mvc;
using PagedList;



namespace Denim.Controllers
{
    public class HomeController : Controller
    {
        DenimEntities Db = new DenimEntities();
        public ActionResult Index(int? page)
        {
            files = imageslist();
            eventList = EventList();
            newsList = NewsList();
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            EventGallery objeventGallery = new EventGallery();
            objeventGallery.eventList = eventList.Where(s => s.Status == true).OrderByDescending(i => i.Date).ToList();
            objeventGallery.files = files;

            objeventGallery.NewsList = newsList.Where(s => s.status == true).OrderByDescending(i => i.Date).ToPagedList(pageNumber, pageSize);


            return View("Index", objeventGallery);
        }
        List<Gallery> files;
        List<Event> eventList;
        List<News> newsList;

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Services()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Notice()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Saving()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Loan()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Remittance()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Gallery()
        {
            files = GalleyList();
            //objHomeGallery.ImageList = files;
            //objHomeGallery = files;
            return View("Gallery", files);
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";

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
        public FileResult Download(string ImageName)
        {
            var FileVirtualPath = "~/Areas/Files/" + ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }


        static string tablehtml = "";
        [WebMethod]
        public string SendAmmortization(string table)
        {
            ViewBag.AmmorTable = table;
            tablehtml = table;
            return table;
        }

        public ActionResult viewTable()
        {
            ViewBag.AmmorTable = tablehtml;
            return View();
        }

        [WebMethod]
        public ActionResult RedirectToTable()
        {
            ViewBag.AmmorTable = tablehtml;
            return redirectToView();
        }

        public ActionResult redirectToView()
        {
            return Redirect(Url.Action("viewTable", "Home"));
        }

        public List<Event> EventList()
        {
            int currMonth = DateTime.Now.Month;
            List<Event> objEventList = Db.Events.Where(a => a.Date.Month.Equals(currMonth)).ToList();
            //var objevent= Db.Events.ToList().Where(a => a.Date.Month.Equals(currMonth));

            return objEventList;
        }

        public List<News> NewsList()
        {
            int currMonth = DateTime.Now.Month;
            List<News> objNewsList = Db.News.Where(a => a.Date.Month.Equals(currMonth)).OrderBy(i => i.Date.Day).ToList();
            //var objevent= Db.Events.ToList().Where(a => a.Date.Month.Equals(currMonth));

            return objNewsList;
        }

        public List<Gallery> imageslist()
        {
            string folderPath = Server.MapPath("~/Images/");
            string[] filePaths = Directory.GetFiles(folderPath);
            files = new List<Gallery>();
            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileName(filePath);
                files.Add(new Gallery
                {
                    ImageName = fileName.Split('.')[0].ToString(),
                    ImagePath = "../Images/" + fileName
                });
            }
            List<Gallery> objimg = files.ToList();
            return objimg;
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
                    ImagePath = "Gallery/" + fileName
                });
            }
            List<Gallery> objimg = files.ToList();
            return objimg;
        }
        public ActionResult Display(int? Id, int page = 1)
        {
            //News objNews = new News();
            //objNews.NewsList = NewsList();
            //return View("Notice", objNews);
            News objNews = new News();
            newsList = NewsList();
            objNews.NewsList = newsList.OrderByDescending(i => i.Date).ToList();

            if (Id.HasValue)
            {
                objNews.Id = Db.News.Find(Id).Id;
                objNews.Title = Db.News.Find(Id).Title;
                objNews.Date = Db.News.Find(Id).Date;
                objNews.Description = Db.News.Find(Id).Description;
                return View("Notice", objNews);
            }
            else
            {
                int? intIdt = Db.News.Max(u => (int?)u.Id);
                objNews.Id = Db.News.Find(intIdt).Id;
                objNews.Title = Db.News.Find(intIdt).Title;
                objNews.Date = Db.News.Find(intIdt).Date;
                objNews.Description = Db.News.Find(intIdt).Description;

                //    return Request.IsAjaxRequest()
                //? (ActionResult)PartialView("ProductList", products.ToPagedList(page, pageSize))
                //: View(products.ToPagedList(page, pageSize));


                 return PartialView("Notice", objNews);
               
             


            }
        }
        public ActionResult LinksDisplay(int id)
        {


            News news = Db.News.Find(id);
            return PartialView("_NewsDescription", news);

        }


    }
}