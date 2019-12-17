using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class ImageTypesController : Controller
    {
        private SDCEntities db = new SDCEntities();

    
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.ImageTypes.ToList());
        }

        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageTypeID,Name")] ImageTypes imageTypes)
        {
            if (ModelState.IsValid)
            {
                db.ImageTypes.Add(imageTypes);
                db.SaveChanges();
                return RedirectToAction("Index" , " ImageTypes");
            }

            return View(imageTypes);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageTypes imageTypes = db.ImageTypes.Find(id);
            if (imageTypes == null)
            {
                return HttpNotFound();
            }
            return View(imageTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageTypeID,Name")] ImageTypes m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "ImageTypes");
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
            ImageTypes m = db.ImageTypes.Find(id);
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
            ImageTypes imageTypes = db.ImageTypes.Find(id);
            db.ImageTypes.Remove(imageTypes);
            db.SaveChanges();
            return RedirectToAction("Index", "ImageTypes");
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
