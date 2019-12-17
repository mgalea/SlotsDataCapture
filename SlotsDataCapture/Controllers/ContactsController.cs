using PagedList;
using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers
{
    public class ContactsController : Controller
    {
        private SDCEntities db = new SDCEntities();

        [HttpGet]
        public ActionResult Index(int? id, int? page, string searchBy, string sortBy, string search)
        {
            var m = db.Contacts.AsQueryable().Where(v => v.VenueID == id);

            ViewBag.Contact_FName = sortBy == "Contact_FName" ? "Contact_FName sortBy" : "Contact_FName";
            ViewBag.Contact_LName = sortBy == "Contact_LName" ? "Contact_LName sortBy" : "Contact_LName";
            ViewBag.Contact_Company = sortBy == "Contact_Company" ? "Contact_Company sortBy" : "Contact_Company";
            ViewBag.Activity = sortBy == "Activity" ? "Activity sortBy" : "Activity";
            ViewBag.Total = m.Count();


            if (searchBy == "Contact_FName")
            {
                m = m.Where(x => x.FName == search || search == null);
            }
            else
            {
                m = m.Where(x => x.LName.StartsWith(search) || search == null);
            }

            if (searchBy == "Contact_LName")
            {
                m = m.Where(x => x.LName == search || search == null);
            }

            else
            {
                m = m.Where(x => x.FName.StartsWith(search) || search == null);
            }

            if (searchBy == "Contact_Company")
            {
                m = m.Where(x => x.Company == search || search == null);
            }

            else
            {
                m = m.Where(x => x.Company.StartsWith(search) || search == null);
            }


            if (searchBy == "Activity")
            {
                m = m.Where(x => x.IsActive == true);
            }

            
            switch (sortBy)
            {
                case "Contact_FName sortBy":
                    m = m.OrderBy(c => c.FName);
                    break;

                case "Contact_LName sortBy":
                    m = m.OrderBy(c => c.LName);
                    break;

                case "Contact_Company sortBy":
                    m = m.OrderBy(c => c.Company);
                    break;

                case "Activity sortBy":
                    m = m.OrderBy(x => x.IsActive);
                    break;
            
                default:
                    m = m.OrderBy(c => c.Company);
                    break;
            }
     
            return View(m.ToPagedList(page ?? 1, 7));
        }

        
        [HttpGet]
        public ActionResult ContactListing(int? id, int? page, string searchBy, string sortBy, string search)
        {
            var m = db.Contacts.AsQueryable();

            ViewBag.Contact_Company = sortBy == "Contact_Company" ? "Contact_Company sortBy" : "Contact_Company";
            ViewBag.Activity = sortBy == "Activity" ? "Activity sortBy" : "Activity";
           
            ViewBag.TotalRecords = m.Count();
            ViewBag.TotalActiveRecords = m.Where(x => x.IsActive).Count();

            if (searchBy == "Contact_Company")
            {
                m = m.Where(c => c.Company == search || search == null);
            }

            else
            {
                m = m.Where(c => c.Company.StartsWith(search) || search == null);
            }
            
            if (searchBy == "Activity")
            {
                m = m.Where(x => x.IsActive == true);
            }
            
            switch (sortBy)
            {
                
                case "Contact_Company sortBy":
                    m = m.OrderBy(c => c.Company);
                    break;

                case "Activity sortBy":
                    m = m.OrderBy(x => x.IsActive);
                    break;

                
                default:
                    m = m.OrderBy(c => c.Company).OrderBy(n => n.Company);
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
            Contacts contacts = db.Contacts.Find(id);
            if (contacts == null)
            {
                return HttpNotFound();
            }
            return View(contacts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name");
            ViewBag.VenueID = new SelectList(db.OperatorVenues, "VenueID", "Name");
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name");
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactID,VenueID,TitleID,CreatedOn,IsActive,FName,LName,Company,Position,Email,Phone,Mobile,CountryID")] Contacts contacts)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contacts);
                db.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }

            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", contacts.CountryID);
            ViewBag.VenueID = new SelectList(db.OperatorVenues, "VenueID", "Name", contacts.VenueID);
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name", contacts.TitleID);
            return View(contacts);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacts contacts = db.Contacts.Find(id);
            if (contacts == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", contacts.CountryID);
            ViewBag.VenueID = new SelectList(db.OperatorVenues, "VenueID", "Name", contacts.VenueID);
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name", contacts.TitleID);
            return View(contacts);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactID,VenueID,TitleID,CreatedOn,IsActive,FName,LName,Company,Position,Email,Phone,Mobile,CountryID")] Contacts contacts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contacts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", contacts.CountryID);
            ViewBag.VenueID = new SelectList(db.OperatorVenues, "VenueID", "Name", contacts.VenueID);
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name", contacts.TitleID);
            return View(contacts);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacts contacts = db.Contacts.Find(id);
            if (contacts == null)
            {
                return HttpNotFound();
            }
            return View(contacts);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contacts contacts = db.Contacts.Find(id);
            db.Contacts.Remove(contacts);
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
