using System.Web.Mvc;

namespace SlotsDataCapture.Controllers.Administration
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

       
        
    }
}
