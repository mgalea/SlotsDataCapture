using SlotsDataCapture.ENTITIES;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class SimCardTypesController : Controller
    {
        private SDCEntities db = new SDCEntities();

       
        [HttpGet]
        public ActionResult Index()
        {
          return View(db.SimCardTypes.OrderBy(x => x.Name).ToList());
        }



        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SIM_CardTypeID,Name")] SimCardTypes m)
        {
            if (ModelState.IsValid)
            {
                db.SimCardTypes.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "SimCardTypes");
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
            SimCardTypes m = db.SimCardTypes.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SIM_CardTypeID,Name")] SimCardTypes m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "SimCardTypes");
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
            SimCardTypes m = db.SimCardTypes.Find(id);
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
            SimCardTypes m = db.SimCardTypes.Find(id);
            db.SimCardTypes.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "SimCardTypes");
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
