using PagedList;
using SlotsDataCapture.ENTITIES;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers
{
    public class BoardsController : Controller
    {
        private SDCEntities db = new SDCEntities();



        [HttpGet]
        public ActionResult Index(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Boards.AsQueryable();

            ViewBag.Board_BatchReference = sortBy == "Board_BatchReference" ? "Board_BatchReference sortBy" : "Board_BatchReference";
            ViewBag.Board_SerialNumber = sortBy == "Board_SerialNumber" ? "Board_SerialNumber sortBy" : "Board_SerialNumber";
           
            ViewBag.TotalRecords = m.Count();
            ViewBag.TotalActiveRecords = m.Where(sm => sm.IsActive == true).Count();
            ViewBag.TotalInactiveRecords = m.Where(sm => sm.IsActive == false).Count();

            if (searchBy == "Board_BatchReference")
            { m = m.Where(x => x.Tbl_BoardBatches.BatchNumber == search || search == null); }

            else
            { m = m.Where(x => x.SerialNumber.StartsWith(search) || search == null); }

            if (searchBy == "Board_SerialNumber")
            { m = m.Where(x => x.SerialNumber == search || search == null); }

            else
            { m = m.Where(x => x.Tbl_BoardBatches.BatchNumber.StartsWith(search) || search == null); }

           
            switch (sortBy)
            {
                case "Board_BatchReference sortBy":
                    m = m.OrderBy(x => x.Tbl_BoardBatches.BatchNumber);
                    break;

                case "Board_SerialNumber sortBy":
                    m = m.OrderBy(x => x.SerialNumber);
                    break;
           
                default:
                    m = m.OrderBy(x => x.SerialNumber);
                    break;
            }

            return View(m.ToPagedList(page ?? 1, 7));
        }



        [HttpGet]
        public ActionResult ActiveBoards(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Boards.AsQueryable().Where(x => x.IsActive == true);

            ViewBag.Board_BatchReference = sortBy == "Board_BatchReference" ? "Board_BatchReference sortBy" : "Board_BatchReference";
            ViewBag.Board_SerialNumber = sortBy == "Board_SerialNumber" ? "Board_SerialNumber sortBy" : "Board_SerialNumber";
                    
            ViewBag.TotalActiveRecords = m.Where(a => a.IsActive == true).Count();

            var i = db.Boards.Where(ia => ia.IsActive == false).Count();
            ViewBag.TotalInactiveRecords = i;

         if (searchBy == "Board_BatchReference")
            { m = m.Where(x =>x.Tbl_BoardBatches.BatchNumber == search || search == null);}

            else
            { m = m.Where(x => x.SerialNumber.StartsWith(search) || search == null);}

         if (searchBy == "Board_SerialNumber")
            {m = m.Where(x => x.SerialNumber == search || search == null);}

         else
            { m = m.Where(x => x.Tbl_BoardBatches.BatchNumber.StartsWith(search) || search == null);}



        switch (sortBy)
            {
                case "Board_BatchReference sortBy":
                   m = m.OrderBy(x=> x.Tbl_BoardBatches.BatchNumber);
                    break;

                case "Board_SerialNumber sortBy":
                    m = m.OrderBy(x=> x.SerialNumber);
                    break;

                            
                default:
                    m = m.OrderBy(x => x.IsActive == true);
                    break;
            }



            return View(m.ToPagedList(page ?? 1, 20));
        }








        [HttpGet]
        public ActionResult InactiveBoards(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Boards.AsQueryable().Where(x => x.IsActive == false);

            ViewBag.Board_BatchReference = sortBy == "Board_BatchReference" ? "Board_BatchReference sortBy" : "Board_BatchReference";
            ViewBag.Board_SerialNumber = sortBy == "Board_SerialNumber" ? "Board_SerialNumber sortBy" : "Board_SerialNumber";

            
            ViewBag.TotalInactiveRecords = m.Where(sm => sm.IsActive == false).Count();
           
            var i = db.Boards.Where(x => x.IsActive == true).Count();
            ViewBag.TotalActiveRecords = i;

            if (searchBy == "Board_BatchReference")
            { m = m.Where(x => x.Tbl_BoardBatches.BatchNumber == search || search == null); }

            else
            { m = m.Where(x => x.SerialNumber.StartsWith(search) || search == null); }

            if (searchBy == "Board_SerialNumber")
            { m = m.Where(x => x.SerialNumber == search || search == null); }

            else
            { m = m.Where(x => x.Tbl_BoardBatches.BatchNumber.StartsWith(search) || search == null); }



            switch (sortBy)
            {
                case "Board_BatchReference sortBy":
                    m = m.OrderBy(x => x.Tbl_BoardBatches.BatchNumber);
                    break;

                case "Board_SerialNumber sortBy":
                    m = m.OrderBy(x => x.SerialNumber);
                    break;


                default:
                    m = m.OrderBy(x => x.IsActive == true);
                    break;
            }



            return View(m.ToPagedList(page ?? 1, 20));
        }















        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boards m = db.Boards.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

      [HttpGet]
        public ActionResult Create()
        {
          
            ViewBag.SMIBAerialID = new SelectList(db.BoardAerials, "SMIBAerialID", "Name");
            ViewBag.SMIBatchID = new SelectList(db.BoardBatches, "SMIBatchID", "BatchNumber");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SMIBID,SMIBatchID,SMIBAerialID,SerialNumber,IsAvailable,IsActive")] Boards m)
        {
            m.IsAvailable = true;
            if (ModelState.IsValid)
            {
                db.Boards.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "Boards");
            }

        
            ViewBag.SMIBAerialID = new SelectList(db.BoardAerials, "SMIBAerialID", "Name", m.SMIBAerialID);
            ViewBag.SMIBatchID = new SelectList(db.BoardBatches, "SMIBatchID", "BatchNumber", m.SMIBatchID);
            return View(m);
        }

         [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boards m = db.Boards.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            ViewBag.SMIBAerialID = new SelectList(db.BoardAerials, "SMIBAerialID", "Name", m.SMIBAerialID);
            ViewBag.SMIBatchID = new SelectList(db.BoardBatches, "SMIBatchID", "BatchNumber", m.SMIBatchID);
            return View(m);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SMIBID,SMIBatchID,SMIBAerialID,SerialNumber,IsAvailable,IsActive")] Boards m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Boards");
            }
           
            ViewBag.SMIBAerialID = new SelectList(db.BoardAerials, "SMIBAerialID", "Name", m.SMIBAerialID);
            ViewBag.SMIBatchID = new SelectList(db.BoardBatches, "SMIBatchID", "BatchNumber", m.SMIBatchID);
            return View(m);
        }
          [HttpGet]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boards m = db.Boards.Find(id);
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
            Boards m = db.Boards.Find(id);
            db.Boards.Remove(m);
            db.SaveChanges();
            return RedirectToAction("Index", "Boards");
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
