using PagedList;
using SlotsDataCapture.ENTITIES;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers
{
    public class ReportsController : Controller
    {
        private SDCEntities db = new SDCEntities();

     
        [HttpGet]
        public ActionResult Index(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Reports.AsQueryable();
            ViewBag.Report_DeviceType = sortBy == "Report_DeviceType" ? "Report_DeviceType sortBy" : "Report_DeviceType";
            ViewBag.Report_DeviceSerialNumber = sortBy == "Report_DeviceSerialNumber" ? "Report_DeviceSerialNumber sortBy" : "Report_DeviceSerialNumber";
            ViewBag.TotalRecords = db.Reports.Count();


            ViewBag.TotalRecords = m.Count();

            if (searchBy == "Report_DeviceType")
            {
                m = m.Where(x => x.DeviceSerialNumber == search || search == null);
            }
            else
            {
                m = m.Where(x => x.DeviceType.StartsWith(search) || search == null);
            }

            if (searchBy == "Report_DeviceSerialNumber")
            {
                m = m.Where(x => x.DeviceType == search || search == null);
            }

            else
            {
                m = m.Where(x => x.DeviceSerialNumber.StartsWith(search) || search == null);
            }
            
            switch (sortBy)
            {
                case "Report_DeviceType sortBy":
                    m = m.OrderBy(x => x.DeviceType);
                    break;

                case "Report_DeviceSerialNumber sortBy":
                    m = m.OrderBy(x => x.DeviceSerialNumber);
                    break;

                              default:
                    m = m.OrderBy(x => x.CreatedOn);
                    break;
            }

            return View(m.ToPagedList(page ?? 1, 7));
        }

        public ActionResult SelectDeviceType()
        {
          var list = new List<SelectListItem>

         {

         new SelectListItem{ Text="Please Select..", Value = "0", Selected = true },
         new SelectListItem{ Text="Board Issue", Value = "Board Issue" },
         new SelectListItem{ Text="Sim Card Issue", Value = "Sim Card Issue" },
         new SelectListItem{ Text="Slot Machine Issue", Value = "Slot Machine Issue" },
         new SelectListItem{ Text="Slot Motherboard Issue", Value = "Slot Motherboard Issue" } };
         ViewData["ddList"] = list;
            return View();

        }








        [HttpGet]
        public ActionResult ReportListing(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Reports.AsQueryable();
            ViewBag.Report_DeviceType = sortBy == "Report_DeviceType" ? "Report_DeviceType sortBy" : "Report_DeviceType";
            ViewBag.Report_DeviceSerialNumber = sortBy == "Report_DeviceSerialNumber" ? "Report_DeviceSerialNumber sortBy" : "Report_DeviceSerialNumber";
            ViewBag.TotalRecords = m.Count();



            if (searchBy == "Report_DeviceType")
            {
                m = m.Where(x => x.DeviceSerialNumber == search || search == null);
            }
            else
            {
                m = m.Where(x => x.DeviceType.StartsWith(search) || search == null);
            }

            if (searchBy == "Report_DeviceSerialNumber")
            {
                m = m.Where(x => x.DeviceType == search || search == null);
            }

            else
            {
                m = m.Where(x => x.DeviceSerialNumber.StartsWith(search) || search == null);
            }

            switch (sortBy)
            {
                case "Report_DeviceType sortBy":
                    m = m.OrderBy(x => x.DeviceType);
                    break;

                case "Report_DeviceSerialNumber sortBy":
                    m = m.OrderBy(x => x.DeviceSerialNumber);
                    break;

                default:
                    m = m.OrderBy(x => x.CreatedOn);
                    break;
            }

            return View(m.ToPagedList(page ?? 1, 15));
        }

          
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reports m = db.Reports.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

        [HttpGet]
        public ActionResult Create()
        {
           return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportID,CreatedOn,CreatedBy,Comments,DeviceType,DeviceSerialNumber")] Reports m)
        {
            if (ModelState.IsValid)
            {
                db.Reports.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "Reports");
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
            Reports m = db.Reports.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
                      return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportID,CreatedOn,CreatedBy,Comments,DeviceType,DeviceSerialNumber")] Reports m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Reports");
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
            Reports m = db.Reports.Find(id);
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
            Reports m = db.Reports.Find(id);
            db.Reports.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "Reports");
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
