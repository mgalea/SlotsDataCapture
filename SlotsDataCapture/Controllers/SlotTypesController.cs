using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class SlotTypesController : Controller
    {
        private SDCEntities db = new SDCEntities();

      
        [HttpGet]
        public ActionResult Index()
        {
         return View(db.SlotTypes.OrderBy(n => n.Name).ToList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlotTypeID,Name")] SlotTypes m)
        {
            if (ModelState.IsValid)
            {
                db.SlotTypes.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "SlotTypes");
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
            SlotTypes m = db.SlotTypes.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }
     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlotTypeID,Name")] SlotTypes m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            SlotTypes m = db.SlotTypes.Find(id);
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
            SlotTypes m = db.SlotTypes.Find(id);
            db.SlotTypes.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "SlotTypes");
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
