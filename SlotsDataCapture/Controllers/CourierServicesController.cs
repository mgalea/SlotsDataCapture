﻿using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class CourierServicesController : Controller
    {
        private SDCEntities db = new SDCEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View(db.CourierServices.ToList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourierID,Name")] CourierService m)
        {
            if (ModelState.IsValid)
            {
                db.CourierServices.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "CourierServices");
            }

            return View(m);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourierService m = db.CourierServices.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourierID,Name")] CourierService m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "CourierServices");
            }
            return View(m);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourierService m = db.CourierServices.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourierService m = db.CourierServices.Find(id);
            db.CourierServices.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "CourierServices");
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
