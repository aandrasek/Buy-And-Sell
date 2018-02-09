using BuyAndSell.Models;
using BuyAndSell.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace BuyAndSell.Controllers
{
    public class HomeController : Controller
    {
        Random random = new Random();
        private BSContext context = new BSContext();
        public ActionResult Index()
        {
            var items = context.Products.ToList();
            return View(items);
        }

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
        public ActionResult Homepage()
        {
            var items = context.Categories.Where(c => c.ParentID == null).ToList();
            return PartialView(items);
        }
        public ActionResult SubCategory(int ID)
        {
            var items = context.Categories.Where(c => c.ParentID == ID).ToList();
            return View(items);
        }
        public ActionResult SubCategories(int ID)
        {
            var items = context.Categories.Where(c => c.ParentID == ID).ToList();
            return View(items);
        }
        public ActionResult Products(int ID, int? page, string location, DateTime? from, DateTime? to, decimal? pricefrom, decimal? priceto)
        {
            var item = context.Categories.First(c => c.ID == ID);
            ViewBag.headline = item.Name;
            var items = context.Products.Where(c => c.CategoryID == ID).ToList();
            if (string.IsNullOrEmpty(location) == false)
            {
                ViewBag.location = location;
                items = items.Where(c => c.Area.ToUpper() == location.ToUpper()).ToList();
            }
            if (from.HasValue)
            {
                ViewBag.from = from?.ToString("yyyy/MM/dd");
                items = items.Where(c => c.DateofRel.Date >= from).ToList();
            }
            if (to.HasValue)
            {
                ViewBag.to = to?.ToString("yyyy/MM/dd");
                items = items.Where(c => c.DateofRel.Date <= to).ToList();
            }
            if (pricefrom.HasValue)
            {
                ViewBag.pricefrom = pricefrom;
                items = items.Where(c => c.Price >= pricefrom).ToList();
            }
            if (priceto.HasValue)
            {
                ViewBag.priceto = priceto;
                items = items.Where(c => c.Price <= priceto).ToList();
            }
            return View(items.ToPagedList(page ?? 1, 6));
        }
        public ActionResult ProductInfo(int ID)
        {
            ViewBag.MainImagee = context.Products.First(c => c.ID == ID);
            var items = context.Imagess.Where(c => c.ProductId == ID).ToList();
            ViewBag.data = items;
            return View(context.Products.First(c => c.ID == ID));

        }
        public ActionResult Search(string Search, int? page, string location, DateTime? from, DateTime? to, decimal? pricefrom, decimal? priceto)
        {
            if (string.IsNullOrEmpty(Search) == false)
            {
                ViewBag.search = Search;
                var items = context.Products.Where(c => c.Name.ToUpper().Contains(Search.ToUpper())).ToList();
                ViewBag.headline = Search;
                if (string.IsNullOrEmpty(location) == false)
                {
                    ViewBag.location = location;
                    items = items.Where(c => c.Area.ToUpper() == location.ToUpper()).ToList();
                }
                if (from.HasValue)
                {
                    ViewBag.from = from?.ToString("yyyy/MM/dd");
                    items = items.Where(c => c.DateofRel.Date >= from).ToList();
                }
                if (to.HasValue)
                {
                    ViewBag.to = to?.ToString("yyyy/MM/dd");
                    items = items.Where(c => c.DateofRel.Date <= to).ToList();
                }
                if (pricefrom.HasValue)
                {
                    ViewBag.pricefrom = pricefrom;
                    items = items.Where(c => c.Price >= pricefrom).ToList();
                }
                if (priceto.HasValue)
                {
                    ViewBag.priceto = priceto;
                    items = items.Where(c => c.Price <= priceto).ToList();
                }
                return View(items.ToPagedList(page ?? 1, 6));
            }
            return RedirectToAction("Index");
        }
        public ActionResult ProductsOf(string email)
        {
            var items = context.Products.Where(c => c.Email == email).ToList();
            ViewBag.headline = email;
            return View(items);
        }
    }
}