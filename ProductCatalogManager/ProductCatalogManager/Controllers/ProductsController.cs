using ProductCatalogManager.Bus;
using ProductCatalogManager.Bus.Models;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProductCatalogManager.Controllers
{
    public class ProductsController : Controller
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Products
        public ActionResult Index(string searchString)
        {
            return View(_productService.GetProducts(searchString));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = _productService.GetProduct(id.Value);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Photo = GetProductPhotoFromRequest();
                _productService.Add(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = _productService.GetProduct(id.Value);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        // POST: Products/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Photo = GetProductPhotoFromRequest();

                _productService.Update(product);

                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = _productService.GetProduct(id.Value);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = _productService.GetProduct(id).Photo;

            if (cover == null)
                return null;

            return File(cover, "image/jpg");
        }

        private byte[] GetProductPhotoFromRequest()
        {
            HttpPostedFileBase requestFile = Request.Files["ProductPhoto"];

            if (requestFile == null)
                return new byte[0];

            using (BinaryReader reader = new BinaryReader(requestFile.InputStream))
            {
                return reader.ReadBytes(requestFile.ContentLength);
            }
        }

        public FileContentResult ExportToExcel()
        {
            byte[] fileContent = _productService.ExportAsExcel();
            string fileDownloadName = typeof(Product).Name + DateTime.Now.Ticks + ".xlsx";

            return
                new FileContentResult(fileContent, "application/vnd.ms-excel")
                {
                    FileDownloadName = fileDownloadName
                };
        }
    }
}
