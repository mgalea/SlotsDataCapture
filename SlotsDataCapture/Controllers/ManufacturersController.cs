using PagedList;
using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class ManufacturersController : Controller
    {
        private SDCEntities db = new SDCEntities();

        [HttpGet]
        public ActionResult Index(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Manufacturers.AsQueryable();

            ViewBag.Manufacturer_Name = sortBy == "Manufacturer_Name" ? "Manufacturer_Name sortBy" : "Manufacturer_Name";
            ViewBag.TotalRecords = m.Count();



            if (searchBy == "Manufacturer_Name")
            {
                m=m.Where(x => x.Name == search || search == null);
            }
            else
            {
                m=m.Where(x=>x.Name.StartsWith(search) || search == null);
            }


            switch (sortBy)
            {
                case "Manufacturer_Name sortBy":
                    m=m.OrderBy(x=>x.Name);
                    break;


                default:
                    m = m.OrderBy(x => x.Name);
                    break;
            }


            return View(m.ToPagedList(page ?? 1, 7));
        }
               

        [HttpGet]
        public ActionResult ManufacturerListing(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Manufacturers.AsQueryable();

            ViewBag.Manufacturer_Name = sortBy == "Manufacturer_Name" ? "Manufacturer_Name sortBy" : "Manufacturer_Name";
            ViewBag.TotalRecords = m.Count();



            if (searchBy == "Manufacturer_Name")
            {
                m = m.Where(x => x.Name == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Name.StartsWith(search) || search == null);
            }


            switch (sortBy)
            {
                case "Manufacturer_Name sortBy":
                    m = m.OrderBy(x => x.Name);
                    break;


                default:
                    m = m.OrderBy(x => x.Name);
                    break;
            }


            return View(m.ToPagedList(page ?? 1, 7));
        }
                     


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManufacturerID,ManufacturedDate,Name")] Manufacturers m)
        {
            if (ModelState.IsValid)
            {
                db.Manufacturers.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "Manufacturers");
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
            Manufacturers m = db.Manufacturers.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }


        [HttpGet]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManufacturerID,ManufacturedDate,Name")] Manufacturers m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Manufacturers");
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
            Manufacturers m = db.Manufacturers.Find(id);
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
            Manufacturers m = db.Manufacturers.Find(id);
            db.Manufacturers.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "Manufacturers");
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
