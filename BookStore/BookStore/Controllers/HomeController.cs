using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.Util;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();

        public ActionResult Index()
        {
            IEnumerable<Book> books = db.Books;

            Session["name"] = "Tom";

            ViewBag.Books = books;

            ViewBag.Message = "Это вызов частичного представления";

            return View();
        }
        public ActionResult Partial()
        {
            ViewBag.Message = "Это частичное представление";
            return PartialView();
        }
        public ActionResult Model()
        {
            return View(db.Books);
        }
        //асинхр метод
        public async Task<ActionResult> BookList()
        {
            IEnumerable<Book> books = await db.Books.ToListAsync();
            ViewBag.Books = books;
            return View();
        }
        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку!";
        }
        [HttpGet]
        public string Square(int a=10, int h=3)
        {
            double s = a * h / 2;
            return "<h2>Площадь треугольника с основанием " + a +
            " и высотой " + h + " равна " + s + "</h2>";
        }
        public ActionResult GetHtml()
        {
            return new HtmlResult("<h2>Привет мир</h2>");
        }
        public ActionResult GetImage()
        {
            string path = "../Images/Emperor.jpg";
            return new ImageResult(path);
        }
        public string UserInformation()
        {
            string browser = HttpContext.Request.Browser.Browser;
            string user_agent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            return "<p>Browser: " + browser + "</p><p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
                "</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p>";
        }
        public string GetName()
        {
            var val = Session["name"];
            return val.ToString();
        }
    }
}