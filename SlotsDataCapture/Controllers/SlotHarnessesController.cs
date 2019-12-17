using PagedList;
using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers
{
    public class SlotHarnessesController : Controller
    {
        private SDCEntities db = new SDCEntities();
        [HttpGet]
        public ActionResult Index(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.SlotHarnesses.AsQueryable().Where(x => x.SlotID == Id).OrderBy(x=>x.Tbl_Slots.SerialNumber);
          //  ViewBag.TotalRecords = m.Count();
            return View(m.ToPagedList(page ?? 1, 8));
        }

        [HttpGet]
        public ActionResult HarnessListing(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.SlotHarnesses.AsQueryable();

            ViewBag.Harness_Name = sortBy == "Harness_Name" ? "Harness_Name sortBy" : "Harness_Name";
            ViewBag.Harness_SlotSerial = sortBy == "SlotMachine_Serial" ? "SlotMachine_Serial sortBy" : "SlotMachine_Serial";
          
            ViewBag.TotalRecords = m.Count();

            if (searchBy == "Harness_Name")
            {
                m = m.Where(x => x.Name == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Name.StartsWith(search) || search == null);
            }

            if (searchBy == "SlotMachine_Serial")
            {
                m = m.Where(x => x.Tbl_Slots.SerialNumber == search || search == null);
            }

            else
            {
                m = m.Where(x => x.Tbl_Slots.SerialNumber.StartsWith(search) || search == null);
            }

            
            switch (sortBy)
            {
                case "Harness_Name sortBy":
                    m = m.OrderBy(x => x.Name);
                    break;

                case "SlotMachine_Serial sortBy":
                    m = m.OrderBy(x => x.Tbl_Slots.SerialNumber);
                    break;
                    

                default:
                    m = m.OrderBy(x => x.Name);
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
            SlotHarnesses m = db.SlotHarnesses.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

       [HttpGet]
        public ActionResult Create()
        {
            ViewBag.SlotID = new SelectList(db.Slots, "SlotID", "SerialNumber");
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlotHarnessID,SlotID,Name,CableLength,COM_Jumper_Type,COM_Jumper_Number,Power_Jumper_Number,BS_DataConnect,BS_PowerConnect,BS_DataConnectType,BS_DataConnect_PinOut,BS_PowerConnect_PinOut,DS_DataConnect,DS_PowerConnect,DS_DataConnectType,DS_DataConnect_PinOut,DS_PowerConnect_PinOut")] SlotHarnesses m)
        {
            if (ModelState.IsValid)
            {
                db.SlotHarnesses.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }

            ViewBag.SlotID = new SelectList(db.Slots, "SlotID", "SerialNumber", m.SlotID);
            return View(m);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlotHarnesses m = db.SlotHarnesses.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.SlotID = new SelectList(db.Slots, "SlotID", "SerialNumber", m.SlotID);
            return View(m);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlotHarnessID,SlotID,Name,CableLength,COM_Jumper_Type,COM_Jumper_Number,Power_Jumper_Number,BS_DataConnect,BS_PowerConnect,BS_DataConnectType,BS_DataConnect_PinOut,BS_PowerConnect_PinOut,DS_DataConnect,DS_PowerConnect,DS_DataConnectType,DS_DataConnect_PinOut,DS_PowerConnect_PinOut")] SlotHarnesses m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }
            ViewBag.SlotID = new SelectList(db.Slots, "SlotID", "SerialNumber", m.SlotID);
            return View(m);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlotHarnesses m = db.SlotHarnesses.Find(id);
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
            SlotHarnesses m = db.SlotHarnesses.Find(id);
            db.SlotHarnesses.Remove(m);
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
