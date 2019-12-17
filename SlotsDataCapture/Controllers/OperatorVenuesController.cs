using PagedList;
using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers
{
    public class OperatorVenuesController : Controller
    {
        private SDCEntities db = new SDCEntities();

        
        [HttpGet]
        public ActionResult Index(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            //var m = db.OperatorVenues.AsQueryable().Where(x => x.OperatorID == Id).OrderBy(x=>x.Name);
            var m = db.OperatorVenues.Where(x => x.OperatorID == Id).OrderBy(x => x.Name).AsQueryable();

            ViewBag.Venue_Name = sortBy == "Venue_Name" ? "Venue_Name sortBy" : "Venue_Name";
            ViewBag.Venue_Permit = sortBy == "Venue_Permit" ? "Venue_Permit sortBy" : "Venue_Permit";
            ViewBag.Venue_Location = sortBy == "Venue_Location" ? "Venue_Location sortBy" : "Venue_Location";
            ViewBag.Activity = sortBy == "Activity" ? "Activity sortBy" : "Activity";

            ViewBag.TotalRecords = db.OperatorVenues.Count();
            ViewBag.TotalActiveRecords = db.OperatorVenues.Where(a => a.IsActive == true && a.OperatorID == Id).Count();
                     

            if (searchBy == "Venue_Name")
            {
                m = m.Where(x => x.Name == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Permit.StartsWith(search) || search == null);
            }

            if (searchBy == "Venue_Permit")
            {
                m = m.Where(x => x.Permit == search || search == null);
            }

            else
            {
                m = m.Where(x => x.Name.StartsWith(search) || search == null);
            }

            if (searchBy == "Venue_Location")
            {
                m = m.Where(x => x.Town == search || search == null);
            }

            else
            {
                m = m.Where(x => x.Town.StartsWith(search) || search == null);
            }
     
            if (searchBy == "Activity")
            {
                m = m.Where(x => x.IsActive == true);
            }
           switch (sortBy)
            {
                case "Venue_Name sortBy":
                    m = m.OrderBy(x => x.Name);
                    break;

                case "Venue_Permit sortBy":
                    m = m.OrderBy(x => x.Permit);
                    break;

                case "Venue_Location sortBy":
                    m = m.OrderBy(x => x.Town);
                    break;


                case "Activity sortBy":
                    m = m.OrderBy(x => x.IsActive);
                   break;
                default:
                    m = m.OrderBy(x => x.Name);
                    break;
            }

            return View(m.ToPagedList(page ?? 1, 18));
        }


        [HttpGet]
        public ActionResult VenueListing(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.OperatorVenues.AsQueryable();

            ViewBag.Venue_Name = sortBy == "Venue_Name" ? "Venue_Name sortBy" : "Venue_Name";
            ViewBag.Venue_Permit = sortBy == "Venue_Permit" ? "Venue_Permit sortBy" : "Venue_Permit";
            ViewBag.Venue_Location = sortBy == "Venue_Location" ? "Venue_Location sortBy" : "Venue_Location";
            ViewBag.Activity = sortBy == "Activity" ? "Activity sortBy" : "Activity";

            ViewBag.TotalRecords = db.OperatorVenues.Count();
            ViewBag.TotalActiveRecords = db.OperatorVenues.Where(a => a.IsActive == true).Count();


            if (searchBy == "Venue_Name")
            {
                m = m.Where(x => x.Name == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Permit.StartsWith(search) || search == null);
            }

            if (searchBy == "Venue_Permit")
            {
                m = m.Where(x => x.Permit == search || search == null);
            }

            else
            {
                m = m.Where(x => x.Name.StartsWith(search) || search == null);
            }

            if (searchBy == "Venue_Location")
            {
                m = m.Where(x => x.Town == search || search == null);
            }

            else
            {
                m = m.Where(x => x.Town.StartsWith(search) || search == null);
            }

            if (searchBy == "Activity")
            {
                m = m.Where(x => x.IsActive == true);
            }
            switch (sortBy)
            {
                case "Venue_Name sortBy":
                    m = m.OrderBy(x => x.Name);
                    break;

                case "Venue_Permit sortBy":
                    m = m.OrderBy(x => x.Permit);
                    break;

                case "Venue_Location sortBy":
                    m = m.OrderBy(x => x.Town);
                    break;


                case "Activity sortBy":
                    m = m.OrderBy(x => x.IsActive);
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
            OperatorVenues m = db.OperatorVenues.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

       [HttpGet]
        public ActionResult Create()
        {
            ViewBag.OperatorID = new SelectList(db.Operators, "OperatorID", "Name");
            ViewBag.OperatorTypeID = new SelectList(db.OperatorTypes, "OperatorTypeID", "Name");
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name");
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VenueID,OperatorID,OperatorTypeID,IsActive,Permit,Manager,Name,Phone,Email,AddressOne,AddressTwo,PostCode,Town,CountryID")] OperatorVenues m)
        {
            if (ModelState.IsValid)
            {
                db.OperatorVenues.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }

            ViewBag.OperatorID = new SelectList(db.Operators, "OperatorID", "Name", m.OperatorID);
            ViewBag.OperatorTypeID = new SelectList(db.OperatorTypes, "OperatorTypeID", "Name", m.OperatorTypeID);
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", m.CountryID);
            return View(m);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperatorVenues m = db.OperatorVenues.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.OperatorID = new SelectList(db.Operators, "OperatorID", "Name", m.OperatorID);
            ViewBag.OperatorTypeID = new SelectList(db.OperatorTypes, "OperatorTypeID", "Name", m.OperatorTypeID);
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", m.CountryID);
            return View(m);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VenueID,OperatorID,OperatorTypeID,IsActive,Permit,Manager,Name,Phone,Email,AddressOne,AddressTwo,PostCode,Town,CountryID")] OperatorVenues m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }
            ViewBag.OperatorID = new SelectList(db.Operators, "OperatorID", "Name", m.OperatorID);
            ViewBag.OperatorTypeID = new SelectList(db.OperatorTypes, "OperatorTypeID", "Name", m.OperatorTypeID);
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", m.CountryID);
            return View(m);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperatorVenues m = db.OperatorVenues.Find(id);
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
            OperatorVenues m = db.OperatorVenues.Find(id);
            db.OperatorVenues.Remove(m);
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
