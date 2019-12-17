using SlotsDataCapture.ENTITIES;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class SlotModelsController : Controller
    {
        private SDCEntities db = new SDCEntities();

        [HttpGet]
        public ActionResult Index()
        {
           return View(db.SlotModels.OrderBy(x => x.Name).ToList());
        }

        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlotModelID,Name")] SlotModels m)
        {
            if (ModelState.IsValid)
            {
                db.SlotModels.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "SlotMachines");
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
            SlotModels m = db.SlotModels.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlotModelID,Name")] SlotModels m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "SlotModels");
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
            SlotModels m = db.SlotModels.Find(id);
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
            SlotModels slotModels = db.SlotModels.Find(id);
            db.SlotModels.Remove(slotModels);
            db.SaveChanges();
            return RedirectToAction("Index", "SlotModels");
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
