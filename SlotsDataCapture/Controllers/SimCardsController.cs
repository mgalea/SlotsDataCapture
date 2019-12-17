using PagedList;
using SlotsDataCapture.ENTITIES;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class SimCardsController : Controller
    {
        private SDCEntities db = new SDCEntities();

      

        [HttpGet]
        public ActionResult Index(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.SimCards.AsQueryable().Where(x=> x.SIMCardID == Id);
                       
            ViewBag.SimCard_SerialNumber = sortBy == "SimCard_SerialNumber" ? "SimCard_SerialNumber sortBy" : "SimCard_SerialNumber";
            ViewBag.SimCard_BatchReference = sortBy == "SimCard_BatchReference" ? "SimCard_BatchReference sortBy" : "SimCard_BatchReference";
            ViewBag.IsActive = sortBy == "Activity" ? "Activity sortBy" : "Activity";
              
            ViewBag.TotalRecords = m.Count();
            ViewBag.TotalActiveRecords = db.SimCards.Where(c => c.IsActive == true).Count();
           


            if (searchBy == "SimCard_SerialNumber")
            {
                m = m.Where(sc => sc.SerialNumber == search || search == null);
            }
            else
            {
                m = m.Where(sc => sc.Tbl_SimCardBatches.BatchReference.StartsWith(search) || search == null);
            }

            if (searchBy == "SimCard_BatchReference")
            {
                m = m.Where(sc => sc.Tbl_SimCardBatches.BatchReference == search || search == null);
            }
            else
            {
                m = m.Where(sc => sc.SerialNumber.StartsWith(search) || search == null);
            }
                       
            switch (sortBy)
            {
                case "SimCard_SerialNumber sortBy":
                    m = m.OrderBy(sc => sc.SerialNumber);
                    break;

               case "SimCard_BatchReference sortBy":
                    m = m.OrderBy(sc => sc.Tbl_SimCardBatches.BatchReference);
                    break;

                case "Activity sortBy":
                    m = m.OrderBy(sc => sc.IsActive == true);
                    break;


                default:
                    m = m.OrderBy(x => x.ActivatedOn);
                    break;
            }

            return View(m.ToPagedList(page ?? 1, 25));
        }





        [HttpGet]
        public ActionResult ActiveSimCards(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.SimCards.AsQueryable().Where(x=> x.IsActive == true);

            ViewBag.SimCard_SerialNumber = sortBy == "SimCard_SerialNumber" ? "SimCard_SerialNumber sortBy" : "SimCard_SerialNumber";
            ViewBag.SimCard_BatchReference = sortBy == "SimCard_BatchNumber" ? "SimCard_BatchNumber sortBy" : "SimCard_BatchNumber";
          
            ViewBag.TotalActiveRecords = db.SimCards.Where(c => c.IsActive == true).Count();
            ViewBag.TotalUnassignedRecords = db.SimCards.Where(mn => mn.MobileNumber == "Not Activated").Count();
             

            if (searchBy == "SimCard_SerialNumber")
            {
                m = m.Where(x=> x.IsActive == true && x.SerialNumber == search || search == null);
            }

            else
            {
                m = m.Where((x => x.IsActive == true && x.SerialNumber.StartsWith(search) || search == null));
            }


            if (searchBy == "SimCard_BatchNumber")
            {
                m = m.Where(x=> x.Tbl_SimCardBatches.BatchReference == search || search == null);
            }

            else
            {
                m = m.Where((x=> x.Tbl_SimCardBatches.BatchReference.StartsWith(search) || search == null));
            }
                      
            switch (sortBy)
            {


                case "SimCard_SerialNumber sortBy":
                    m = m.OrderBy(b => b.SerialNumber);
                    break;

                 case "SimCard_BatchNumber sortBy":
                    m = m.OrderBy(b => b.Tbl_SimCardBatches.BatchReference);
                    break;


                default:
                    m = m.OrderBy(b => b.ActivatedOn);
                    break;
            }

            return View(m.ToPagedList(page ?? 1, 25));
        }


        [HttpGet]
        public ActionResult InactiveSimCards(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.SimCards.AsQueryable().Where(x => x.IsActive == false);

            ViewBag.SimCard_SerialNumber = sortBy == "SimCard_SerialNumber" ? "SimCard_SerialNumber sortBy" : "SimCard_SerialNumber";
            ViewBag.SimCard_BatchReference = sortBy == "SimCard_BatchNumber" ? "SimCard_BatchNumber sortBy" : "SimCard_BatchNumber";

            ViewBag.TotalActiveRecords = db.SimCards.Where(c => c.IsActive == true).Count();
            ViewBag.TotalUnassignedRecords = db.SimCards.Where(mn => mn.MobileNumber == "Not Activated").Count();


            if (searchBy == "SimCard_SerialNumber")
            {
                m = m.Where(x => x.IsActive == false && x.SerialNumber == search || search == null);
            }

            else
            {
                m = m.Where((x => x.IsActive == false && x.SerialNumber.StartsWith(search) || search == null));
            }


            if (searchBy == "SimCard_BatchNumber")
            {
                m = m.Where(x => x.Tbl_SimCardBatches.BatchReference == search || search == null);
            }

            else
            {
                m = m.Where((x => x.Tbl_SimCardBatches.BatchReference.StartsWith(search) || search == null));
            }

            switch (sortBy)
            {


                case "SimCard_SerialNumber sortBy":
                    m = m.OrderBy(b => b.SerialNumber);
                    break;

                case "SimCard_BatchNumber sortBy":
                    m = m.OrderBy(b => b.Tbl_SimCardBatches.BatchReference);
                    break;


                default:
                    m = m.OrderBy(b => b.ActivatedOn);
                    break;
            }

            return View(m.ToPagedList(page ?? 1, 25));
        }






        //public ActionResult CardActivationList()
        //{

        //    var m = db.SimCards.Where(x => x.IsActive == false).Take(1).ToList();



        //    return View(m);
        //}







        //[HttpPost]

        //public ActionResult CardActivationList(List<SimCards> m)
        //  {
        //   // DateTime now = new DateTime();
        //    if (ModelState.IsValid)
        //    {
        //        var dbEntity  = new SDCEntities();

        //        foreach (var i in m)
        //        {
        //            var card = db.SimCards.Where(x => x.SIMCardID.Equals(i.SIMCardID)).FirstOrDefault();
        //            if (card != null)
        //            {


        //                card.ActivatedOn =  i.ActivatedOn;
        //                card.SerialNumber = i.SerialNumber; // required 
        //                card.MobileNumber = i.MobileNumber; // should be added 
        //                card.SIMBatchID = i.SIMBatchID; // carry over
        //                card.IsAvailable =  i.IsAvailable; // global 
        //                card.IsActive = i.IsActive;    // local 
        //            }

        //            dbEntity.SaveChanges();
        //        }

        //        ViewBag.Message = "Successfully Saved";
        //        return View(m);
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Update Failed!";
        //        return View(m);
        //    }

        //}







        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SimCards m = db.SimCards.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

        [HttpGet]
        public ActionResult Create()
        {
             ViewBag.SIMBatchID = new SelectList(db.SimCardBatches, "SimBatchID", "BatchReference");
             return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SIMCardID,SIMBatchID,SerialNumber,MobileNumber,ActivatedOn,IsActive")] SimCards m)
        {
            if (ModelState.IsValid)
            {
               m.IsAvailable = true;
                db.SimCards.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "SimCards");
            }
            ViewBag.SIMBatchID = new SelectList(db.SimCardBatches, "SimBatchID", "BatchReference", m.SIMBatchID);
           return View(m);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SimCards m = db.SimCards.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
             ViewBag.SIMBatchID = new SelectList(db.SimCardBatches, "SimBatchID", "BatchReference", m.SIMBatchID);
             return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SIMCardID,SIMBatchID,SerialNumber,MobileNumber,ActivatedOn,IsAvailable,IsActive")] SimCards m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "SimCards");
            }
            ViewBag.SIMBatchID = new SelectList(db.SimCardBatches, "SimBatchID", "BatchReference", m.SIMBatchID);
             return View(m);
        }

        // GET: AdminSimCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SimCards simCards = db.SimCards.Find(id);
            if (simCards == null)
            {
                return HttpNotFound();
            }
            return View(simCards);
        }

        // POST: AdminSimCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SimCards simCards = db.SimCards.Find(id);
            db.SimCards.Remove(simCards);
            db.SaveChanges();
            return RedirectToAction("Index", "SimCards");
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
