using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class PaymentTypesController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: PaymentTypes
        public ActionResult Index()
        {
            return View(db.PaymentTypes.ToList());
        }

        // GET: PaymentTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentTypes paymentTypes = db.PaymentTypes.Find(id);
            if (paymentTypes == null)
            {
                return HttpNotFound();
            }
            return View(paymentTypes);
        }

        // GET: PaymentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentTypes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentTypeId,PaymentTypeName")] PaymentTypes paymentTypes)
        {
            if (ModelState.IsValid)
            {
                db.PaymentTypes.Add(paymentTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentTypes);
        }

        // GET: PaymentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentTypes paymentTypes = db.PaymentTypes.Find(id);
            if (paymentTypes == null)
            {
                return HttpNotFound();
            }
            return View(paymentTypes);
        }

        // POST: PaymentTypes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentTypeId,PaymentTypeName")] PaymentTypes paymentTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentTypes);
        }

        // GET: PaymentTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentTypes paymentTypes = db.PaymentTypes.Find(id);
            if (paymentTypes == null)
            {
                return HttpNotFound();
            }
            return View(paymentTypes);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentTypes paymentTypes = db.PaymentTypes.Find(id);
            db.PaymentTypes.Remove(paymentTypes);
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
    }
}
