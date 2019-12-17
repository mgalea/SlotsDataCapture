using SlotsDataCapture.ENTITIES;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class BoardImagesController : Controller
    {
        private SDCEntities db = new SDCEntities();

        [HttpGet]
        public ActionResult Index()
        {
         return View(db.BoardImages.OrderBy(dt => dt.CreatedOn).ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.ImageTypeID = new SelectList(db.ImageTypes, "ImageTypeID", "Name");
            ViewBag.VenueID = new SelectList(db.OperatorVenues, "VenueID", "Name");
            ViewBag.SMIBID = new SelectList(db.BoardImages, "SMIBID", "SerialNumber");
            return View();
        }

        [HttpPost]
        public ActionResult Add(BoardImages upload)
        {
           
            string fileName = Path.GetFileNameWithoutExtension(upload.ImageFile.FileName);
            string extension = Path.GetExtension(upload.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            upload.ImagePath = "~/Images/Uploads/Boards" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Images/Uploads/Baords"), fileName);

            ViewBag.ImageTypeID = new SelectList(db.ImageTypes, "ImageTypeID", "Name", upload.ImageTypeID);
            ViewBag.VenueID = new SelectList(db.OperatorVenues, "VenueID", "Name", upload.VenueID);
            ViewBag.SMIBID = new SelectList(db.BoardImages, "SMIBID", "SerialNumber", upload.SMIBID);

            upload.ImageFile.SaveAs(fileName);

            db.BoardImages.Add(upload);
            db.SaveChanges();
           
            ModelState.Clear();
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BoardImages m = db.BoardImages.Find(id);
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
            BoardImages m = db.BoardImages.Find(id);
            db.BoardImages.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "BoardImages");
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

