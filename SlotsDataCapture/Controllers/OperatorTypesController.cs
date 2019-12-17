using SlotsDataCapture.ENTITIES;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class OperatorTypesController : Controller
    {
        private SDCEntities db = new SDCEntities();

     
        [HttpGet]
        public ActionResult Index()
        {
          return View(db.OperatorTypes.OrderBy(n => n.Name).ToList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OperatorTypeID,Name")] OperatorTypes m)
        {
            if (ModelState.IsValid)
            {
                db.OperatorTypes.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "OperatorTypes");
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
            OperatorTypes operatorTypes = db.OperatorTypes.Find(id);
            if (operatorTypes == null)
            {
                return HttpNotFound();
            }
            return View(operatorTypes);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OperatorTypeID,Name")] OperatorTypes m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "OperatorTypes");
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
            OperatorTypes operatorTypes = db.OperatorTypes.Find(id);
            if (operatorTypes == null)
            {
                return HttpNotFound();
            }
            return View(operatorTypes);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OperatorTypes m = db.OperatorTypes.Find(id);
            db.OperatorTypes.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "OperatorTypes");
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
