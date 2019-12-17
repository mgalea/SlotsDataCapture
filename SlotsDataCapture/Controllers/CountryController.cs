using SlotsDataCapture.ENTITIES;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class CountryController : Controller
    {
        private SDCEntities db = new SDCEntities();

       [HttpGet]
        public ActionResult Index()
        {
            var list = db.Countries.OrderBy(x => x.Name).ToList();
            int count = list.Count();
            ViewBag.TotalRecords = count;
            return View(list);
        }
       
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryID,Name")] Countries m)
        {
            if (ModelState.IsValid)
            {
                db.Countries.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "Country");
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
            Countries m = db.Countries.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

        [HttpGet]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryID,Name")] Countries m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index" , "Country");
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
            Countries m = db.Countries.Find(id);
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
            Countries m = db.Countries.Find(id);
            db.Countries.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "Country");
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
