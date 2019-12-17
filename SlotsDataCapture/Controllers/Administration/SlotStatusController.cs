using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class SlotStatusController : Controller
    {
        private SDCEntities db = new SDCEntities();

      
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.SlotStatus.OrderBy(s => s.Status).ToList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlotStatusID,Status")] SlotStatus m)
        {
            if (ModelState.IsValid)
            {
                db.SlotStatus.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "SlotStatus");
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
            SlotStatus m = db.SlotStatus.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlotStatusID,Status")] SlotStatus m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "SlotStatus");
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
            SlotStatus m = db.SlotStatus.Find(id);
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
            SlotStatus m = db.SlotStatus.Find(id);
            db.SlotStatus.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "SlotStatus");
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
