using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class BoardManufacturersController : Controller
    {
        private SDCEntities db = new SDCEntities();

       [HttpGet]
        public ActionResult Index()
        {
         return View(db.BoardManufacturers.OrderBy(s => s.Name).ToList());
        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardManufacturers m = db.BoardManufacturers.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.BoardType = new SelectList(db.BoardTypes, "SMIBoardTypeID", "Name");
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SMIB_CardManufacturerID,SMIBoardTypeID,Name")] BoardManufacturers m)
        {
            if (ModelState.IsValid)
            {
                db.BoardManufacturers.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index" , "BoardManufacturers");
            }

            ViewBag.BoardType = new SelectList(db.BoardTypes, "SMIBoardTypeID", "Name", m.SMIBoardTypeID);
            return View(m);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardManufacturers m = db.BoardManufacturers.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardType = new SelectList(db.BoardTypes, "SMIBoardTypeID", "Name", m.SMIBoardTypeID);
            return View(m);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SMIB_CardManufacturerID,SMIBoardTypeID,Name")] BoardManufacturers m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "BoardManufacturers");
            }
            ViewBag.BoardType = new SelectList(db.BoardTypes, "SMIBoardTypeID", "Name", m.SMIBoardTypeID);
            return View(m);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardManufacturers m = db.BoardManufacturers.Find(id);
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
            BoardManufacturers m = db.BoardManufacturers.Find(id);
            db.BoardManufacturers.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index" , "BoardManufacturers");
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
