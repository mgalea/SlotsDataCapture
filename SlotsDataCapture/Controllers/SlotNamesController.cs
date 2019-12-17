using SlotsDataCapture.ENTITIES;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class SlotNamesController : Controller
    {
        private SDCEntities db = new SDCEntities();

        
        [HttpGet]
        public ActionResult Index()
        {
          return View(db.SlotNames.OrderBy(x => x.Name).ToList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlotNameID,Name")] SlotName m)
        {
            if (ModelState.IsValid)
            {
                db.SlotNames.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "SlotNames");
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
            SlotName m = db.SlotNames.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlotNameID,Name")] SlotName m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "SlotNames");
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
            SlotName m = db.SlotNames.Find(id);
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
            SlotName slotName = db.SlotNames.Find(id);
            db.SlotNames.Remove(slotName);
            db.SaveChanges();
            return RedirectToAction("Index", "SlotNames");
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
