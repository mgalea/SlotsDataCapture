using PagedList;
using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers
{
    public class CompaniesController : Controller
    {
        private SDCEntities db = new SDCEntities();
     
      
        [HttpGet]
        public ActionResult Index(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Companies.AsQueryable();
            ViewBag.Company_Name = sortBy == "Company_Name" ? "Company_Name sortBy" : "Company_Name";
            ViewBag.Company_Registration = sortBy == "Company_Registration" ? "Company_Registration sortBy" : "Company_Registration";
            ViewBag.Company_Location = sortBy == "Company_Location" ? "Company_Location sortBy" : "Company_Location";
            ViewBag.Activity = sortBy == "Activity" ? "Activity sortBy" : "Activity";

            ViewBag.TotalRecords = db.Companies.Count();
           ViewBag.TotalActiveRecords = db.Companies.Where(a => a.IsActive == true).Count();

                                 
            if (searchBy == "Company_Name")
            {
                m = m.Where(x=> x.Name == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Number.StartsWith(search) || search == null);
            }

            if (searchBy == "Company_Registration")
            {
                m = m.Where(x => x.Number == search || search == null);
            }

            else
            {
                m = m.Where(x => x.Name.StartsWith(search) || search == null);
            }

            if (searchBy == "Company_Location")
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
                case "Company_Name sortBy":
                    m = m.OrderBy(x => x.Name);
                    break;

                case "Company_Registration sortBy":
                    m = m.OrderBy(x => x.Number);
                    break;

                case "Company_Location sortBy":
                    m = m.OrderBy(x => x.Town);
                    break;


                case "Activity sortBy":
                    m=m.OrderBy(x => x.IsActive);
                    break;
               
                default:
                    m = m.OrderBy(x => x.Name);
                    break;
            }
            

            return View(m.ToPagedList(page ?? 1, 7));
        }






        [HttpGet]
        public ActionResult CompanyListing(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Companies.AsQueryable();
            ViewBag.Company_Name = sortBy == "Company_Name" ? "Company_Name sortBy" : "Company_Name";
            ViewBag.Company_Registration = sortBy == "Company_Registration" ? "Company_Registration sortBy" : "Company_Registration";
            ViewBag.Company_Location = sortBy == "Company_Location" ? "Company_Location sortBy" : "Company_Location";
            ViewBag.Activity = sortBy == "Activity" ? "Activity sortBy" : "Activity";

            ViewBag.TotalRecords = db.Companies.Count();
            ViewBag.TotalActiveRecords = db.Companies.Where(a => a.IsActive == true).Count();


            if (searchBy == "Company_Name")
            {
                m = m.Where(x => x.Name == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Number.StartsWith(search) || search == null);
            }

            if (searchBy == "Company_Registration")
            {
                m = m.Where(x => x.Number == search || search == null);
            }

            else
            {
                m = m.Where(x => x.Name.StartsWith(search) || search == null);
            }

            if (searchBy == "Company_Location")
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
                case "Company_Name sortBy":
                    m = m.OrderBy(x => x.Name);
                    break;

                case "Company_Registration sortBy":
                    m = m.OrderBy(x => x.Number);
                    break;

                case "Company_Location sortBy":
                    m = m.OrderBy(x => x.Town);
                    break;


                case "Activity sortBy":
                    m = m.OrderBy(x => x.IsActive);
                    break;

                default:
                    m = m.OrderBy(x => x.Name);
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
            Company m = db.Companies.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name");
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,CreatedBy,CreatedOn,IsActive,Name,Number,Phone,Email,Website,AddressOne,AddressTwo,Town,PostCode,CountryID")] Company m)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }

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
            Company m = db.Companies.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", m.CountryID);
            return View(m);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,CreatedBy, CreatedOn, IsActive,Name,Number,Phone,Email,Website,AddressOne,AddressTwo,Town,PostCode,CountryID")] Company m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            Company m = db.Companies.Find(id);
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
            Company m = db.Companies.Find(id);
            db.Companies.Remove(m);
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
