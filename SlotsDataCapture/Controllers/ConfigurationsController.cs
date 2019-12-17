using PagedList;
using SlotsDataCapture.ENTITIES;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SlotsDataCapture.Controllers
{
    public class ConfigurationsController : Controller
    {
        private SDCEntities db = new SDCEntities();

       [HttpGet]
        public ActionResult Index(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Configurations.AsQueryable().Where(x => x.SlotID == Id);

            ViewBag.Configuration_SlotSerial = sortBy == "Config_SlotSerial" ? "Config_SlotSerial sortBy" : "Config_SlotSerial";
            ViewBag.Configuration_SimCardSerial = sortBy == "Config_SimCardSerial" ? "Config_SimCardSerial sortBy" : "Config_SimCardSerial";
            ViewBag.Configuration_BoardSerial = sortBy == "Config_BoardSerial" ? "Config_BoardSerial sortBy" : "Config_BoardSerial";
            ViewBag.TotalRecords = m.Count();

           if (searchBy == "Config_SlotSerial")
            {
                m = m.Where(x => x.Tbl_Slots.SerialNumber == search || search == null);
            }
            else
            {
                m = m.Where(x => x.Tbl_SimCards.SerialNumber.StartsWith(search) || search == null);
            }

            if (searchBy == "Config_SimCardSerial")
            {
                m = m.Where(x=>x.Tbl_Slots.SerialNumber == search || search == null);
            }

            else
            {
                m = m.Where(x=> x.Tbl_SimCards.SerialNumber.StartsWith(search) || search == null);
            }

            if (searchBy == "Config_BoardSerial")
            {
                m = m.Where(x=> x.Tbl_Boards.SerialNumber == search || search == null);
            }

            else
            {
                m = m.Where(c=> c.Tbl_Boards.SerialNumber.StartsWith(search) || search == null);
            }
                      
            switch (sortBy)
            {
                case "Config_SlotSerial sortBy":
                    m = m.OrderBy(x => x.Tbl_Slots.SerialNumber);
                    break;

                case "Config_SimCardSerial sortBy":
                    m = m.OrderBy(x => x.Tbl_SimCards.SerialNumber);
                    break;

                case "Config_BoardSerial sortBy":
                    m = m.OrderBy(x => x.Tbl_Boards.SerialNumber);
                    break;
         
                default:
                    m = m.OrderBy(x => x.Tbl_Slots.SerialNumber);
                    break;
            }

           return View(m.ToPagedList(page ?? 1, 7));
        }


        [HttpGet]
        public ActionResult ConfigurationListing(int? Id, int? page, string sortBy, string searchBy, string search)
        {
            var m = db.Configurations.AsQueryable();

            ViewBag.Configuration_SlotSerial = sortBy == "Config_SlotSerial" ? "Config_SlotSerial sortBy" : "Config_SlotSerial";
            ViewBag.Configuration_SimCardSerial = sortBy == "Config_SimCardSerial" ? "Config_SimCardSerial sortBy" : "Config_SimCardSerial";
            ViewBag.Configuration_BoardSerial = sortBy == "Config_BoardSerial" ? "Config_BoardSerial sortBy" : "Config_BoardSerial";
            ViewBag.TotalRecords = m.Count();
           

            if (searchBy == "Config_SlotSerial")
            {
                m = m.Where(c => c.Tbl_Slots.SerialNumber == search || search == null);
            }
            else
            {
                m = m.Where(c => c.Tbl_SimCards.SerialNumber.StartsWith(search) || search == null);
            }

            if (searchBy == "Config_SimCardSerial")
            {
                m = m.Where(c => c.Tbl_Slots.SerialNumber == search || search == null);
            }

            else
            {
                m = m.Where(c => c.Tbl_SimCards.SerialNumber.StartsWith(search) || search == null);
            }

            if (searchBy == "Config_BoardSerial")
            {
                m = m.Where(c => c.Tbl_Boards.SerialNumber == search || search == null);
            }

            else
            {
                m = m.Where(c => c.Tbl_Boards.SerialNumber.StartsWith(search) || search == null);
            }
                                 
            switch (sortBy)
            {
                case "Config_SlotSerial sortBy":
                    m = m.OrderBy(x => x.Tbl_Slots.SerialNumber);
                    break;

                case "Config_SimCardSerial sortBy":
                    m = m.OrderBy(x => x.Tbl_SimCards.SerialNumber);
                    break;

                case "Config_BoardSerial sortBy":
                    m = m.OrderBy(x => x.Tbl_Boards.SerialNumber);
                    break;

                default:
                    m = m.OrderBy(x => x.Tbl_Slots.SerialNumber);
                    break;
            }

            return View(m.ToPagedList(page ?? 1, 15));
        }

        // LOOKUP FUNCTIONS 

        [HttpGet]
        public ActionResult FindCard(string term)
        {
            var list = db.SimCards.Where(c => c.SerialNumber.StartsWith(term))
                .Where(a => a.IsActive == true && a.IsAvailable == true)
                .Select(lbl => new { label = lbl.SerialNumber, id = lbl.SIMCardID }).Take(30).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult FindBoard(string term)
        {
            var list = db.Boards.Where(b => b.SerialNumber.StartsWith(term))
                .Where(a => a.IsActive == true && a.IsAvailable == true)
                .Select(lbl => new { label = lbl.SerialNumber, id = lbl.SMIBID }).Take(30).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult FindSlotMachine(string term)
        {
            var list = db.Slots.Where(s => s.SerialNumber.StartsWith(term))
                .Where(a => a.IsActive == true && a.IsAvailable == true)
                .Select(lbl => new { label = lbl.SerialNumber, id = lbl.SlotID }).Take(30).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }





        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuration m = db.Configurations.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

       [HttpGet]
        public ActionResult Create()
        {

            int available_cards = db.SimCards.Where(c => c.IsActive == false).Count();
            int available_boards = db.Boards.Where(b => b.IsActive == false).Count();
            int available_slots = db.Slots.Where(s => s.IsActive == true).Count();

            ViewBag.SimCardRecordCount = available_cards;
            ViewBag.SMIBoardRecordCount = available_boards;
            ViewBag.DeviceRecordCount = available_slots;

            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConfigurationID,SIMCardID,SIM_ActivatedOn,SIM_DeactivatedOn,SIMCardNowAssigned,SMIBID,SMIB_ActivatedOn,SMIB_DeactivatedOn,SMIBoardNowAssigned,SlotID")] Configuration m)
        {

            //// (1) I start off by looking up the dropdown list’s selected value for the sim card.
            var find_sim = db.SimCards.Find(m.SIMCardID);
            //// (2) Next, I want to remove the sim card’s future appearance within the global listing category, by changing its default value from TRUE to FALSE
            //// Within the respective table 
            find_sim.SIMCardID = m.SIMCardID;
            find_sim.IsAvailable = false; // Remove Availability 
            find_sim.IsActive = true;    // Now show as being active 
            db.Entry(find_sim).State = EntityState.Modified;
            db.SaveChanges();



            var find_board = db.Boards.Find(m.SMIBID);
            find_board.SMIBID = m.SMIBID;
            find_board.IsAvailable = false; // Made sure the selected board is no longer available in the global listings category 
            find_board.IsActive = true;  // Next set this board to being active
            db.Entry(find_board).State = EntityState.Modified;
            db.SaveChanges();



            //// Finally I want to take the selected sim card and board, assign it to a slot machine and then show the S.M as being configured


            var find_slot = db.Slots.Find(m.SlotID);
            find_slot.IsAvailable = false;
            find_slot.IsActive = true;

            db.Entry(find_slot).State = EntityState.Modified;
            db.SaveChanges();


            if (ModelState.IsValid)
            {
                db.Configurations.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }
              return View(m);

        }

         [HttpGet]
        public ActionResult Edit(int? id)
        {
            

            var active_simCards = db.SimCards.Where(card =>
                card.IsActive == true && card.IsAvailable);

            var active_boards = db.Boards.Where(board =>
                board.IsActive == true && board.IsAvailable);

            var active_slots = db.Slots.Where(s => s.IsActive == true);



            var availableSimCard = db.SimCards.Where(card =>
                card.IsActive == true && card.IsAvailable == true);

            var availableBoardCount = db.Boards.Where(board =>
                board.IsActive == true && board.IsAvailable == true);

            
            int SimNum = availableSimCard.Count();
            int boardNum = availableBoardCount.Count();
            int slotNum = db.Slots.Where(x => x.IsActive == true).Count();

            ViewBag.SimCardRecordCount = SimNum;
            ViewBag.SMIBoardRecordCount = boardNum;
            ViewBag.DeviceRecordCount = slotNum;

            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuration c = db.Configurations.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }

 
            ViewBag.SIMCardID = new SelectList(active_simCards, "SIMCardID", "SerialNumber", c.SIMCardID);
            ViewBag.SlotID = new SelectList(active_slots, "SlotID", "SerialNumber", c.SlotID);
            ViewBag.SMIBID = new SelectList(db.Boards, "SMIBID", "SerialNumber", c.SMIBID);

            return View(c);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConfigurationID,SIMCardID,SIM_ActivatedOn,SIM_DeactivatedOn,SIMCardNowAssigned,SMIBID,SMIB_ActivatedOn,SMIB_DeactivatedOn,SMIBoardNowAssigned,SlotID")] Configuration m)
        {
            var active_simCards = db.SimCards.Where(card =>
                card.IsActive == true && card.IsAvailable == true);

            var active_boards = db.Boards.Where(board =>
                board.IsActive == true && board.IsAvailable);

            var remove_card = db.SimCards.Find(m.SIMCardID);

            remove_card.SIMCardID = m.SIMCardID;
            remove_card.IsAvailable = false;
            remove_card.IsActive = false;
            db.Entry(remove_card).State = EntityState.Modified;
            db.SaveChanges();


            var remove_board = db.Boards.Find(m.SMIBID);

            remove_board.SMIBID = m.SMIBID;
            remove_board.IsAvailable = false;
            remove_board.IsActive = false;
            db.Entry(remove_board).State = EntityState.Modified;
            db.SaveChanges();


            var activated_slot = db.Slots.Find(m.SlotID);
            activated_slot.IsActive = false;
            db.Entry(activated_slot).State = EntityState.Modified;
            db.SaveChanges();
            


            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Companies");
            }
            ViewBag.SIMCardID = new SelectList(db.SimCards, "SIMCardID", "SerialNumber", m.SIMCardID);
            ViewBag.SlotID = new SelectList(db.Slots, "SlotID", "SerialNumber",m.SlotID);
            ViewBag.SMIBID = new SelectList(db.Boards, "SMIBID", "SerialNumber", m.SMIBID);
            return View(m);
        }

         [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuration m = db.Configurations.Find(id);
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
            Configuration m = db.Configurations.Find(id);
            db.Configurations.Remove(m);
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
