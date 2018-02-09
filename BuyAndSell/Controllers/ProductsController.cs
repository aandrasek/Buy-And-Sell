using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BuyAndSell.Models;
using BuyAndSell.VModels;

namespace BuyAndSell.Controllers
{
    public class ProductsController : Controller
    {
        private BSContext context = new BSContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(context.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [HttpPost]
        public ActionResult Create(ListModel model)
        {
            ListModel svm = new ListModel();
            var kat = model.SelectedID.ElementAt(0);
            svm.Items = context.Categories.Where(c => c.ParentID == kat).Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            return View(svm);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Createe(ListModel product, HttpPostedFileBase main)
        {
            if (ModelState.IsValid)
            {
                Product s = new Product();
                s.Name = product.Name;
                s.Description = product.Description;
                s.Address = product.Address;
                s.Area = product.Area;
                s.PhoneNum = product.PhoneNum;
                s.Price = product.Price;
                s.Email = product.Email;
                s.DateofRel = DateTime.Now;

                var fileNamee = Path.GetFileName(main.FileName);
                var name_without_extt = Path.GetFileNameWithoutExtension(main.FileName);
                var extt = Path.GetExtension(main.FileName);
                s.MainImage = name_without_extt + DateTime.Now.ToString("MMddyyHmmss") + extt;
                var pathh = Path.Combine(Server.MapPath("~/Images/"), s.MainImage);
                main.SaveAs(pathh);


                var cat = product.SelectedID.ElementAt(0);
                s.CategoryID = cat;
                context.Products.Add(s);
                context.SaveChanges();
                var ss= context.Products.OrderByDescending(c => c.ID).FirstOrDefault();
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {

                        foreach (var item in product.File)
                        {

                            var fileName = Path.GetFileName(item.FileName);
                            var name_without_ext = Path.GetFileNameWithoutExtension(item.FileName);
                            var ext = Path.GetExtension(item.FileName);

                            fileName = name_without_ext + DateTime.Now.ToString("MMddyyHmmss") + ext;
                            var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            item.SaveAs(path);
                            Images img = new Images();
                            img.ProductId = ss.ID;
                            img.Image = fileName;
                            context.Imagess.Add(img);
                            context.SaveChanges();
                            

                        }
                    }

                }
                return View();
            }

            return View();
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Address,Area,PhoneNum,Price,Email,DateofRel,CategoryID")] Product product)
        {
            if (ModelState.IsValid)
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {   Product product = context.Products.Find(id);
            var img = context.Imagess.Where(c => c.ProductId == id).ToList();
            foreach (var item in img)
            {
                string fullPathh = Request.MapPath("~/Images/" + item.Image);
                if (System.IO.File.Exists(fullPathh))
                {
                    System.IO.File.Delete(fullPathh);
                }
                context.Imagess.Remove(item);
            }


            string fullPath = Request.MapPath("~/Images/" + product.MainImage);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult SelectCategory()
        {
            ListModel svm = new ListModel();
            svm.Items = context.Categories.Where(c => c.ParentID == null).Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            return View(svm);
        }
        [HttpPost]
        public ActionResult SelectSubCategory(ListModel model)
        {
            ListModel svm = new ListModel();
            var kat = model.SelectedID.ElementAt(0);
            svm.Items = context.Categories.Where(c => c.ParentID == kat).Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            return View(svm);
        }
        public ActionResult test()
        {

            return View();
        }

    }
}
