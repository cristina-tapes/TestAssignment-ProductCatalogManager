using ProductCatalogManager.Controllers.Helpers;
using ProductCatalogManager.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProductCatalogManager.Controllers
{
    public class ProductsController : Controller
    {
        private ProductDBContext db = new ProductDBContext();

        // GET: Products
        public ActionResult Index(string searchString)
        {
            var products = db.Products.ToList();
            if (!String.IsNullOrEmpty(searchString))
                products = products.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Photo = GetProductPhotoFromRequest();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Photo = GetProductPhotoFromRequest();

                Product uneditedProduct = db.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);

                if (uneditedProduct.NotEquals(product))
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = db.Products.FirstOrDefault(p => p.Id == id)?.Photo;

            if (cover != null)
                return File(cover, "image/jpg");

            return null;
        }

        private byte[] GetProductPhotoFromRequest()
        {
            HttpPostedFileBase photo = Request.Files["ProductPhoto"];

            BinaryReader reader = new BinaryReader(photo.InputStream);
            return reader.ReadBytes(photo.ContentLength);
        }

        public FileContentResult ExportToExcel()
        {
            var dataToExport = db.Products.Select(p => new { p.Id, p.Name, p.Price, p.LastUpdated });
            var fileContent = ExcelExporter.ExportDataToExcel(dataToExport.ToList(), typeof(Product).Name);
            return new FileContentResult(fileContent, "application/vnd.ms-excel") { FileDownloadName = typeof(Product).Name + DateTime.Now.Ticks + ".xlsx" };
        }
    }
}
