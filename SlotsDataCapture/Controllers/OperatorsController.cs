using PagedList;
using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers
{
    public class OperatorsController : Controller
    {
        private SDCEntities db = new SDCEntities();


        [HttpGet]
        public ActionResult Index(int? Id, int? page, string sortBy, string searchBy, string search)
        {

            var m = db.Operators.AsQueryable().Where(x => x.CompanyID == Id);

            ViewBag.Operator_Name = sortBy == "Operator_Name" ? "Operator_Name sortBy" : "Operator_Name";
            ViewBag.Operator_Location = sortBy == "Operator_Location" ? "Operator_Location sortBy" : "Operator_Location";
            ViewBag.Operator_Country = sortBy == "Operator_Country" ? "Operator_Country sortBy" : "Operator_Country";
            ViewBag.Activity = sortBy == "Activity" ? "Activity sortBy" : "Activity";

            ViewBag.TotalRecords = db.Operators.Count();
            ViewBag.TotalActiveRecords = db.Operators.Where(a => a.IsActive == true && a.CompanyID == Id).Count();
                                


            if (searchBy == "Operator_Name")
            {
                m = m.Where(c => c.Name == search || search == null);
            }
            else
            {
                m = m.Where(c => c.Town.StartsWith(search) || search == null);
            }

            if (searchBy == "Operator_Location")
            {
                m = m.Where(x => x.Town == search || search == null);
            }

            else
            {
                m = m.Where(x => x.Name.StartsWith(search) || search == null);
            }
            if (searchBy == "Operator_Country")
            {
                m = m.Where(x => x.Tbl_Countries.Name == search || search == null);
            }

            else
            {
                m = m.Where(x => x.Tbl_Countries.Name.StartsWith(search) || search == null);
            }

            if (searchBy == "Activity")
            {
                m = m.Where(x => x.IsActive == true);
            }

            
            switch (sortBy)
            {
                case "Operator_Name sortBy":
                    m = m.OrderBy(x => x.Name);
                    break;

                case "Operator_Location sortBy":
                    m = m.OrderBy(x => x.Town);
                    break;

                case "Operator_Country sortBy":
                    m = m.OrderBy(x => x.Tbl_Countries.Name);
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
        public ActionResult OperatorListing(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Operators.AsQueryable();


            ViewBag.Operator_Name = sortBy == "Operator_Name" ? "Operator_Name sortBy" : "Operator_Name";
            ViewBag.Activity = sortBy == "Activity" ? "Activity sortBy" : "Activity";
            ViewBag.TotalRecords = db.Operators.Count();
            ViewBag.TotalActiveRecords = db.Operators.Where(a => a.IsActive == true).Count();
          
            if (searchBy == "Operator_Name")
            {
                m = m.Where(x => x.Name == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Name.StartsWith(search) || search == null);
            }
            

            if (searchBy == "Activity")
            {
                m = m.Where(x => x.IsActive == true);
            }
           

            switch (sortBy)
            {
                case "Operator_Name sortBy":
                    m = m.OrderBy(x => x.Name);
                    break;

                case "activity sortBy":
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
            Operators m = db.Operators.Find(id);
            if (m== null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name");
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OperatorID,CompanyID,CreatedOn,IsActive,Name,Website,AddressOne,AddressTwo,Town,CountryID")] Operators m)
        {
            if (ModelState.IsValid)
            {
                db.Operators.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }

            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name", m.CompanyID);
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
            Operators m = db.Operators.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name", m.CompanyID);
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", m.CountryID);
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OperatorID,CompanyID,CreatedOn,IsActive,Name,Website,AddressOne,AddressTwo,Town,CountryID")] Operators m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name", m.CompanyID);
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
            Operators m = db.Operators.Find(id);
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
            Operators m = db.Operators.Find(id);
            db.Operators.Remove(m);
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
