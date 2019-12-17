using PagedList;
using SlotsDataCapture.ENTITIES;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class SimCardBatchesController : Controller
    {
        private SDCEntities db = new SDCEntities();

        [HttpGet]
        public ActionResult Index(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.SimCardBatches.AsQueryable();
            ViewBag.Batch_Manufacturer = sortBy == "Manufacturer_Model" ? "Manufacturer_Model sortBy" : "Manufacturer_Model";
            ViewBag.Batch_Reference = sortBy == "Batch_Ref" ? "Batch_Ref sortBy" : "Batch_Ref";
            ViewBag.TotalRecords = m.Count();



            if (searchBy == "Manufacturer_Model")
            {
                m = m.Where(x=> x.Tbl_SimCardManufacturers.Name == search || search == null);
            }
            else
            {
                m = m.Where(x => x.BatchReference.StartsWith(search) || search == null);
            }


            if (searchBy == "Batch_Ref")
            {
                m = m.Where(x => x.BatchReference == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Tbl_SimCardManufacturers.Name.StartsWith(search) || search == null);
            }
         
            
            switch (sortBy)
            {
                case "Manufacturer_Model sortBy":
                    m = m.OrderBy(x => x.Tbl_SimCardManufacturers.Name);
                    break;

               case "Batch_Ref sortBy":
                    m = m.OrderBy(x => x.BatchReference);
                    break;

                default:
                    m = m.OrderBy(x => x.ReceivedOn);
                    break;
            }

            return View(m.ToPagedList(page ?? 1, 7));
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           SimCardBatches m = db.SimCardBatches.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.SIM_ManufacturerID = new SelectList(db.SimCardManufacturers, "SIM_CardManufacturerID", "Name");
            ViewBag.SIM_CardTypeID = new SelectList(db.SimCardTypes, "SIM_CardTypeID", "Name");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SimBatchID,SIM_ManufacturerID,SIM_CardTypeID,ReceivedOn,BatchReference,UnitsReceived,CardLocation")] SimCardBatches m)
        {
            if (ModelState.IsValid)
            {
                db.SimCardBatches.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "SimCardBatches");
            }

            ViewBag.SIM_ManufacturerID = new SelectList(db.SimCardManufacturers, "SIM_CardManufacturerID", "Name", m.SIM_ManufacturerID);
            ViewBag.SIM_CardTypeID = new SelectList(db.SimCardTypes, "SIM_CardTypeID", "Name", m.SIM_CardTypeID);
            return View(m);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SimCardBatches m = db.SimCardBatches.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.SIM_ManufacturerID = new SelectList(db.SimCardManufacturers, "SIM_CardManufacturerID", "Name", m.SIM_ManufacturerID);
            ViewBag.SIM_CardTypeID = new SelectList(db.SimCardTypes, "SIM_CardTypeID", "Name", m.SIM_CardTypeID);
            return View(m);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SimBatchID,SIM_ManufacturerID,SIM_CardTypeID,ReceivedOn,BatchReference,UnitsReceived,CardLocation")] SimCardBatches m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "SimCardBatches");
            }
            ViewBag.SIM_ManufacturerID = new SelectList(db.SimCardManufacturers, "SIM_CardManufacturerID", "Name", m.SIM_ManufacturerID);
            ViewBag.SIM_CardTypeID = new SelectList(db.SimCardTypes, "SIM_CardTypeID", "Name", m.SIM_CardTypeID);
            return View(m);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SimCardBatches m = db.SimCardBatches.Find(id);
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
            SimCardBatches m = db.SimCardBatches.Find(id);
            db.SimCardBatches.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "SimCardBatches ");
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