using PagedList;
using SlotsDataCapture.ENTITIES;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers
{
    public class SlotsController : Controller
    {
        private SDCEntities db = new SDCEntities();
                          
        [HttpGet]
        public ActionResult Index(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Slots.AsQueryable().Where(x => x.VenueID == Id);

            ViewBag.SlotMachine_SerialNumber = sortBy == "Serial_Number" ? "Serial_Number sortBy" : "Serial_Number";
            ViewBag.SlotMachine_AssetNumber = sortBy == "Asset_Number" ? "Asset_Number sortBy" : "Asset_Number";

            ViewBag.Activity = sortBy == "Activity" ? "Activity sortBy" : "Activity";
            ViewBag.IsOriginal = sortBy == "IsOriginal" ? "IsOriginal sortBy" : "IsOriginal";
         
            ViewBag.TotalRecords = db.Slots.Count();
            ViewBag.TotalActiveRecords = m.Where(a => a.IsActive == true).Count();

                                 
            if (searchBy == "Serial_Number")
            {
                m = m.Where(x => x.SerialNumber == search || search == null);
            }
            else
            {
                m = m.Where(x => x.AssetNumber.StartsWith(search) || search == null);
            }

            if (searchBy == "Asset_Number")
            {
                m = m.Where(x => x.AssetNumber == search || search == null);
            }

            else
            {
                m = m.Where(x => x.SerialNumber.StartsWith(search) || search == null);
            }

            if (searchBy == "Activity")
            {
                m = m.Where(x => x.IsActive == true);
            }

            if (searchBy == "IsOriginal")
            {
                m = m.Where(x => x.IsOriginal == false);
            }

            switch (sortBy)
            {
                case "Serial_Number sortBy":
                    m = m.OrderBy(x => x.SerialNumber);
                    break;

                case "Asset_Number sortBy":
                    m = m.OrderBy(x => x.AssetNumber);
                    break;


                case "Activity sortBy":
                    m = m.OrderBy(x => x.IsActive);
                    break;


                case "IsOriginal sortBy":
                    m = m.OrderBy(x => x.IsOriginal);
                    break;

                default:
                    m = m.OrderBy(x => x.SerialNumber).OrderBy(n => n.SerialNumber);
                    break;
            }
            return View(m.ToPagedList(page ?? 1, 7));
        }


        [HttpGet]
        public ActionResult SlotListing(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Slots.AsQueryable();
            ViewBag.SlotMachine_SerialNumber = sortBy == "Serial_Number" ? "Serial_Number sortBy" : "Serial_Number";
            ViewBag.SlotMachine_AssetNumber = sortBy == "Asset_Number" ? "Asset_Number sortBy" : "Asset_Number";

            ViewBag.Activity = sortBy == "Activity" ? "Activity sortBy" : "Activity";
            ViewBag.IsOriginal = sortBy == "IsOriginal" ? "IsOriginal sortBy" : "IsOriginal";

            ViewBag.TotalRecords = db.Slots.Count();
            ViewBag.TotalActiveRecords = m.Where(a => a.IsActive == true).Count();


            if (searchBy == "Serial_Number")
            {
                m = m.Where(x => x.SerialNumber == search || search == null);
            }
            else
            {
                m = m.Where(x => x.AssetNumber.StartsWith(search) || search == null);
            }

            if (searchBy == "Asset_Number")
            {
                m = m.Where(x => x.AssetNumber == search || search == null);
            }

            else
            {
                m = m.Where(x => x.SerialNumber.StartsWith(search) || search == null);
            }

            if (searchBy == "Activity")
            {
                m = m.Where(x => x.IsActive == true);
            }

            if (searchBy == "IsOriginal")
            {
                m = m.Where(x => x.IsOriginal == false);
            }

            switch (sortBy)
            {
                case "Serial_Number sortBy":
                    m = m.OrderBy(x => x.SerialNumber);
                    break;

                case "Asset_Number sortBy":
                    m = m.OrderBy(x => x.AssetNumber);
                    break;

                case "Activity sortBy":
                    m = m.OrderBy(x => x.IsActive);
                    break;


                case "IsOriginal sortBy":
                    m = m.OrderBy(x => x.IsOriginal);
                    break;

                default:
                    m = m.OrderBy(x => x.SerialNumber).OrderBy(n => n.SerialNumber);
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
            Slots m = db.Slots.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }


        public JsonResult GetManufacturerModels(int manufacturerId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<ManufacturerModels> listModels = db.ManufacturerModels.Where(x => x.ManufacturerID == manufacturerId).ToList();
            return Json(listModels, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ManufacturerModelID = new SelectList(db.ManufacturerModels, "ManufacturerModelID", "Name");
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name");
            ViewBag.VenueID = new SelectList(db.OperatorVenues, "VenueID", "Name");
            ViewBag.SlotModelID = new SelectList(db.SlotModels, "SlotModelID", "Name");
            ViewBag.SlotNameID = new SelectList(db.SlotNames, "SlotNameID", "Name");
            ViewBag.SlotStatusID = new SelectList(db.SlotStatus, "SlotStatusID", "Status");
            ViewBag.SlotTypeID = new SelectList(db.SlotTypes, "SlotTypeID", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlotID,VenueID,SlotTypeID,SlotStatusID,SlotModelID,SlotNameID,ManufacturerID,ManufacturerModelID,IsOriginal,IsActive,DeviceCommissioned,DeviceDecommissioned,SerialNumber,AssetNumber,DeviceLocation")] Slots m)
        {
           // CHECK AGAINST CREATE AND EDIT FORMS
            m.IsAvailable = true;
            if (ModelState.IsValid)
            {
               
                db.Slots.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index","Companies");
            }

            ViewBag.ManufacturerModelID = new SelectList(db.ManufacturerModels, "ManufacturerModelID", "Name", m.ManufacturerModelID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name", m.ManufacturerID);
            ViewBag.VenueID = new SelectList(db.OperatorVenues, "VenueID", "Name", m.VenueID);
            ViewBag.SlotModelID = new SelectList(db.SlotModels, "SlotModelID", "Name", m.SlotModelID);
            ViewBag.SlotNameID = new SelectList(db.SlotNames, "SlotNameID", "Name", m.SlotNameID);
            ViewBag.SlotStatusID = new SelectList(db.SlotStatus, "SlotStatusID", "Status", m.SlotStatusID);
            ViewBag.SlotTypeID = new SelectList(db.SlotTypes, "SlotTypeID", "Name", m.SlotTypeID);
            
            return View(m);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slots m = db.Slots.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManufacturerModelID = new SelectList(db.ManufacturerModels, "ManufacturerModelID", "Name", m.ManufacturerModelID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name", m.ManufacturerID);
            ViewBag.VenueID = new SelectList(db.OperatorVenues, "VenueID", "Name", m.VenueID);
            ViewBag.SlotModelID = new SelectList(db.SlotModels, "SlotModelID", "Name", m.SlotModelID);
            ViewBag.SlotNameID = new SelectList(db.SlotNames, "SlotNameID", "Name", m.SlotNameID);
            ViewBag.SlotStatusID = new SelectList(db.SlotStatus, "SlotStatusID", "Status", m.SlotStatusID);
            ViewBag.SlotTypeID = new SelectList(db.SlotTypes, "SlotTypeID", "Name", m.SlotTypeID);
            return View(m);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlotID,VenueID,SlotTypeID,SlotStatusID,SlotModelID,SlotNameID,ManufacturerID,ManufacturerModelID,IsOriginal,IsActive,IsAvailable,DeviceCommissioned,DeviceDecommissioned,SerialNumber,AssetNumber,DeviceLocation")] Slots m)
        {
           

            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }
            ViewBag.ManufacturerModelID = new SelectList(db.ManufacturerModels, "ManufacturerModelID", "Name", m.ManufacturerModelID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name", m.ManufacturerID);
            ViewBag.VenueID = new SelectList(db.OperatorVenues, "VenueID", "Name", m.VenueID);
            ViewBag.SlotModelID = new SelectList(db.SlotModels, "SlotModelID", "Name", m.SlotModelID);
            ViewBag.SlotNameID = new SelectList(db.SlotNames, "SlotNameID", "Name", m.SlotNameID);
            ViewBag.SlotStatusID = new SelectList(db.SlotStatus, "SlotStatusID", "Status", m.SlotStatusID);
            ViewBag.SlotTypeID = new SelectList(db.SlotTypes, "SlotTypeID", "Name", m.SlotTypeID);
            return View(m);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slots m = db.Slots.Find(id);
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
            Slots m = db.Slots.Find(id);
            db.Slots.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "Companies");
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
