using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class SimCardManufacturersController : Controller
    {
        private SDCEntities db = new SDCEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View(db.SimCardManufacturers.ToList().OrderBy(x=>x.Name));
        }

       
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SIM_CardManufacturerID,Name")] SimCardManufacturers m)
        {
            if (ModelState.IsValid)
            {
                db.SimCardManufacturers.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "SimCardManufacturers");
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
            SimCardManufacturers m = db.SimCardManufacturers.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SIM_CardManufacturerID,Name")] SimCardManufacturers m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "SimCardManufacturers");
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
            SimCardManufacturers m = db.SimCardManufacturers.Find(id);
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
            SimCardManufacturers m = db.SimCardManufacturers.Find(id);
            db.SimCardManufacturers.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index" , "SimCardManufacturers");
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
