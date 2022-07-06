using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class ItemsController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: Items
        [Authorize]
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Category);
            return View(items.ToList());
        }
        

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.ItemCategory = new SelectList(db.Category, "idCategory", "NameCategory");
            return View();
        }

        // POST: Items/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,ItemName,ItemPrice,itemImg,ItemCategory")] Items items)
        {
            HttpPostedFileBase FileBase = Request.Files[0];
            //HttpFileCollectionBase CollectionBase = Request.Files;
            if (FileBase.ContentLength == 0)
            {
                ModelState.AddModelError("itemImg","El necesario seleccionar una imagen.");
            }
            else
            {
                if (FileBase.FileName.EndsWith(".jpg"))
                {
                    WebImage image = new WebImage(FileBase.InputStream);

                    items.itemImg = image.GetBytes();
                }
                else
                {
                    ModelState.AddModelError("itemImg", "El sistema unicamente acepta imagenes con formato JPG.");
                }
               
            }           
            if (ModelState.IsValid)
            {
                db.Items.Add(items);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemCategory = new SelectList(db.Category, "idCategory", "NameCategory", items.itemCategory);
            return View(items);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemCategory = new SelectList(db.Category, "idCategory", "NameCategory", items.itemCategory);
            return View(items);
        }

        // POST: Items/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,ItemName,ItemPrice,itemImg,ItemCategory")] Items items)
        {
            //byte[] imagenActual = null;
            Items _items = new Items();
            HttpPostedFileBase FileBase = Request.Files[0];
            if (FileBase.ContentLength == 0)
            {
                //imagenActual = db.Items.SingleOrDefault(t=>t.ItemId == items.ItemId).itemImg;
                _items = db.Items.Find(items.itemId);
                //items.itemImg = imagenActual;
                items.itemImg = _items.itemImg;
            }
            else
            {

                if (FileBase.FileName.EndsWith(".jpg"))
                {
                    WebImage image = new WebImage(FileBase.InputStream);

                    items.itemImg = image.GetBytes();
                }
                else
                {
                    ModelState.AddModelError("itemImg", "El sistema unicamente acepta imagenes con formato JPG.");
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(_items).State = EntityState.Detached;
                db.Entry(items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemCategory = new SelectList(db.Category, "idCategory", "NameCategory", items.itemCategory);
            return View(items);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Items items = db.Items.Find(id);
            db.Items.Remove(items);
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
        public ActionResult getImage(int id)
        {
            Items items = db.Items.Find(id);
            byte[] byteImage = items.itemImg;
            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);
            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
        }

        //[HttpPost]
        //public JsonResult Index(OrderViewModel objOrderViewModel)
        //{
        //    OrderRepository objOrderRepository = new OrderRepository();
        //    bool isStatus = objOrderRepository.AddOrder(objOrderViewModel);
        //    string SuccessMessage = String.Empty;

        //    if (isStatus)
        //    {
        //        SuccessMessage = "Your Order Has Been Successfully Placed.";
        //    }
        //    else
        //    {
        //        SuccessMessage = "There Is Some Issue While Placing Order.";
        //    }
        //    return Json(SuccessMessage, JsonRequestBehavior.AllowGet);
        //}
    }
}
