using PagedList;
using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers
{
    public class ReturnsController : Controller
    {
        private SDCEntities db = new SDCEntities();

        [HttpGet]
        public ActionResult Index(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Returns.AsQueryable().Where(x => x.ReportID == Id);

            ViewBag.Returns_CourierName = sortBy == "Returns_CourierName" ? "Returns_CourierName sortBy" : "Returns_CourierName";
            ViewBag.Returns_DeviceSerial = sortBy == "Returns_DeviceSerial" ? "Returns_DeviceSerial sortBy" : "Returns_DeviceSerial";
            ViewBag.Returns_DeviceType = sortBy == "Returns_DeviceType" ? "Returns_DeviceType sortBy" : "Returns_DeviceType";
            ViewBag.Returns_TrackingNumber = sortBy == "Returns_TrackingNumber" ? "Returns_TrackingNumber sortBy" : "Returns_TrackingNumber";

            ViewBag.TotalRecords = db.Returns.Count();

            if (searchBy == "Returns_CourierName")
            {
                m = m.Where(x => x.Tbl_CourierService.Name == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Tbl_CourierService.Name.StartsWith(search) || search == null);
            }

            if (searchBy == "Returns_DeviceSerial")
            {
                m = m.Where(x => x.Tbl_Reports.DeviceSerialNumber == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Tbl_Reports.DeviceSerialNumber.StartsWith(search) || search == null);
            }

            if (searchBy == "Returns_DeviceType")
            {
                m = m.Where(x => x.Tbl_Reports.DeviceType == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Tbl_Reports.DeviceType.StartsWith(search) || search == null);
            }
           

            if (searchBy == "Returns_TrackingNumber")
            {
                m = m.Where(x => x.TrackingNumber == search || search == null);
            }

            else
            {
                m = m.Where(x => x.TrackingNumber.StartsWith(search) || search == null);
            }
                                 

            switch (sortBy)
            {
                case "Returns_CourierName sortBy":
                    m = m.OrderBy(x => x.Tbl_CourierService.Name);
                    break;

                case "Returns_DeviceType sortBy":
                    m = m.OrderBy(x=>x.Tbl_Reports.DeviceType);
                    break;

                    
               case "Returns_DeviceSerial sortBy":
                    m = m.OrderBy(x => x.Tbl_Reports.DeviceSerialNumber);
                    break;

                case "Returns_TrackingNumber sortBy":
                    m = m.OrderBy(x => x.TrackingNumber);
                    break;


                default:
                    m = m.OrderBy(r=> r.ReturnDate);
                    break;
            }

            return View(m.ToPagedList(page ?? 1, 7));
        }

        [HttpGet]
        public ActionResult ReturnsListing(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Returns.AsQueryable();

            ViewBag.Returns_DeviceSerial = sortBy == "Returns_DeviceSerial" ? "Returns_DeviceSerial sortBy" : "Returns_DeviceSerial";
            ViewBag.Returns_TrackingNumber = sortBy == "Returns_TrackingNumber" ? "Returns_TrackingNumber sortBy" : "Returns_TrackingNumber";

            ViewBag.TotalRecords = db.Returns.Count();

            
            if (searchBy == "Returns_DeviceSerial")
            {
                m = m.Where(x => x.Tbl_Reports.DeviceSerialNumber == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Tbl_Reports.DeviceSerialNumber.StartsWith(search) || search == null);
            }
                        
            if (searchBy == "Returns_TrackingNumber")
            {
                m = m.Where(x => x.TrackingNumber == search || search == null);
            }

            else
            {
                m = m.Where(x => x.TrackingNumber.StartsWith(search) || search == null);
            }


            switch (sortBy)
            {

                case "Returns_DeviceSerial sortBy":
                    m = m.OrderBy(x => x.Tbl_Reports.DeviceSerialNumber);
                    break;

                case "Returns_TrackingNumber sortBy":
                    m = m.OrderBy(x => x.TrackingNumber);
                    break;


                default:
                    m = m.OrderBy(r => r.ReturnDate);
                    break;
            }

            return View(m.ToPagedList(page ?? 1, 20));
        }












        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Returns m = db.Returns.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }
         [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CourierID = new SelectList(db.CourierServices, "CourierID", "Name");
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "CreatedBy");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReturnID,ReportID,CourierID,ReturnDate,ReturnedBy,TrackingNumber,Dispatched")] Returns m)
        {
            if (ModelState.IsValid)
            {
                db.Returns.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "Returns");
            }

            ViewBag.CourierID = new SelectList(db.CourierServices, "CourierID", "Name", m.CourierID);
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "CreatedBy", m.ReportID);
            return View(m);
        }

         [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Returns m = db.Returns.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourierID = new SelectList(db.CourierServices, "CourierID", "Name", m.CourierID);
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "CreatedBy", m.ReportID);
            return View(m);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReturnID,ReportID,CourierID,ReturnDate,ReturnedBy,TrackingNumber,Dispatched")] Returns m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Returns");
            }
            ViewBag.CourierID = new SelectList(db.CourierServices, "CourierID", "Name", m.CourierID);
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "CreatedBy", m.ReportID);
            return View(m);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Returns m = db.Returns.Find(id);
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
            Returns m = db.Returns.Find(id);
            db.Returns.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "Returns");
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
