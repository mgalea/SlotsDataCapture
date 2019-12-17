using PagedList;
using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class ManufacturerModelsController : Controller
    {
        private SDCEntities db = new SDCEntities();

   
     [HttpGet]
     public ActionResult Index(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.ManufacturerModels.AsQueryable();

            ViewBag.Manufacturer_Name = sortBy == "Manufacturer_Name" ? "Manufacturer_Name sortBy" : "Manufacturer_Name";
            ViewBag.Manufacturer_Model = sortBy == "Manufacturer_Model" ? "Manufacturer_Model sortBy" : "Manufacturer_Model";
            ViewBag.TotalRecords = m.Count();

           
            if (searchBy == "Manufacturer_Name")

            { m=m.Where(x => x.Tbl_Manufacturers.Name == search || search == null);   }
         else

            { m=m.Where(x=>x.Name.StartsWith(search) || search == null);}

         if (searchBy == "Manufacturer_Model")
         {
             m=m.Where(x => x.Name == search || search == null);
         }
         else
         {
             m=m.Where(x => x.Tbl_Manufacturers.Name.StartsWith(search) || search == null);
         }
                              
            switch (sortBy)
         {
             case "Manufacturer_Name sortBy":
                 m=m.OrderBy(x => x.Tbl_Manufacturers.Name);
                 break;

             case "Manufacturer_Model sortBy":
                    m = m.OrderBy(x => x.Name);
                 break;

                default:
                 m=m.OrderBy(x => x.Name).OrderBy(n => n.Name);
                 break;
         }



         return View(m.ToPagedList(page ?? 1, 7));
     }

    [HttpGet]
    public ActionResult ModelListing(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.ManufacturerModels.AsQueryable();

            ViewBag.Manufacturer_Name = sortBy == "Manufacturer_Name" ? "Manufacturer_Name sortBy" : "Manufacturer_Name";
            ViewBag.Manufacturer_Model = sortBy == "Manufacturer_Model" ? "Manufacturer_Model sortBy" : "Manufacturer_Model";
            ViewBag.TotalRecords = m.Count();


            if (searchBy == "Manufacturer_Name")

            { m = m.Where(x => x.Tbl_Manufacturers.Name == search || search == null); }
            else

            { m = m.Where(x => x.Name.StartsWith(search) || search == null); }

            if (searchBy == "Manufacturer_Model")
            {
                m = m.Where(x => x.Name == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Tbl_Manufacturers.Name.StartsWith(search) || search == null);
            }

            switch (sortBy)
            {
                case "Manufacturer_Name sortBy":
                    m = m.OrderBy(x => x.Tbl_Manufacturers.Name);
                    break;

                case "Manufacturer_Model sortBy":
                    m = m.OrderBy(x => x.Name);
                    break;

                default:
                    m = m.OrderBy(x => x.Name).OrderBy(n => n.Name);
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
            ManufacturerModels m = db.ManufacturerModels.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

    [HttpGet]
    public ActionResult Create()
        {
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name");
            return View();
        }

      
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "ManufacturerModelID,ManufacturerID,Name")] ManufacturerModels m)
        {
            if (ModelState.IsValid)
            {
                db.ManufacturerModels.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "ManufacturerModels");
            }

            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name", m.ManufacturerID);
            return View(m);
        }

    [HttpGet]
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManufacturerModels m = db.ManufacturerModels.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name", m.ManufacturerID);
            return View(m);
        }

  
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "ManufacturerModelID,ManufacturerID,Name")] ManufacturerModels m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "ManufacturerModels");
            }
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Name", m.ManufacturerID);
            return View(m);
        }

    [HttpGet]
    public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManufacturerModels m = db.ManufacturerModels.Find(id);
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
            ManufacturerModels m = db.ManufacturerModels.Find(id);
            db.ManufacturerModels.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "ManufacturerModels");
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
