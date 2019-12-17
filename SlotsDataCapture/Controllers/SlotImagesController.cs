using SlotsDataCapture.ENTITIES;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers
{
    public class SlotImagesController : Controller
    {
        private SDCEntities db = new SDCEntities();

        [HttpGet]
        public ActionResult Index(int? id)
        {
         return View(db.SlotImages.Where(x => x.ImageID == id).ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.ImageTypeID = new SelectList(db.ImageTypes, "ImageTypeID", "Name");
            ViewBag.VenueID = new SelectList(db.OperatorVenues, "VenueID", "Name");
            ViewBag.SlotID = new SelectList(db.Slots, "SlotID", "SerialNumber");
            return View();
        }

        [HttpPost]
        public ActionResult Add(SlotImages upload)
        {
            
            string fileName = Path.GetFileNameWithoutExtension(upload.ImageFile.FileName);
            string extension = Path.GetExtension(upload.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            upload.ImagePath = "~/Images/Uploads/SlotMachines" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Images/Uploads/SlotMachines"), fileName);

            ViewBag.ImageTypeID = new SelectList(db.ImageTypes, "ImageTypeID", "Name", upload.ImageTypeID);
            ViewBag.VenueID = new SelectList(db.OperatorVenues, "VenueID", "Name", upload.VenueID);
            ViewBag.SlotID = new SelectList(db.Slots, "SlotID", "SerialNumber", upload.SlotID);

            db.SlotImages.Add(upload);
            db.SaveChanges();

            
            ModelState.Clear();
            return View();
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlotImages m = db.SlotImages.Find(id);
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
            SlotImages m = db.SlotImages.Find(id);
            db.SlotImages.Remove(m);
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
