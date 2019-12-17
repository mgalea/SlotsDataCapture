using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class BoardBatchesController : Controller
    {
        private SDCEntities db = new SDCEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View(db.BoardBatches.OrderBy(x => x.ReceivedOn).ToList());
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardBatches m = db.BoardBatches.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.BoardManufacturer = new SelectList(db.BoardManufacturers, "SMIB_CardManufacturerID", "Name");
            ViewBag.BoardType = new SelectList(db.BoardTypes, "SMIBoardTypeID", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SMIBatchID,SMIB_ManufacturerID,SMIBoardTypeID,ReceivedOn,BatchNumber,UnitsReceived,BoardLocation")] BoardBatches m)
        {
            if (ModelState.IsValid)
            {
                db.BoardBatches.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "BoardBatches");
            }

            ViewBag.BoardManufacturer = new SelectList(db.BoardManufacturers, "SMIB_CardManufacturerID", "Name", m.SMIB_ManufacturerID);
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
            BoardBatches m = db.BoardBatches.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardManufacturer = new SelectList(db.BoardManufacturers, "SMIB_CardManufacturerID", "Name", m.SMIB_ManufacturerID);
            ViewBag.BoardType = new SelectList(db.BoardTypes, "SMIBoardTypeID", "Name", m.SMIBoardTypeID);
            return View(m);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SMIBatchID,SMIB_ManufacturerID,SMIBoardTypeID,ReceivedOn,BatchNumber,UnitsReceived,BoardLocation")] BoardBatches m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "BoardBatches");
            }
            ViewBag.BoardManufacturer = new SelectList(db.BoardManufacturers, "SMIB_CardManufacturerID", "Name", m.SMIB_ManufacturerID);
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
            BoardBatches m = db.BoardBatches.Find(id);
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
            BoardBatches m = db.BoardBatches.Find(id);
            db.BoardBatches.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "BoardBatches");
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
