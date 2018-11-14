using System.Collections.Generic;
using System.Web.Mvc;
using LibraryManagement.LibraryClient;
using LibraryManagementUI.Repositories;

namespace LibraryManagementUI.Controllers
{
    public class CheckInController : Controller
    {
        private ICheckInRepository checkInRepository;

        public CheckInController(ICheckInRepository _checkInRepository)
        {
            this.checkInRepository = _checkInRepository;
        }

        // GET: CheckIn
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllFairies()
        {
            var fairies = checkInRepository.GetAllFairies();
            return Json(fairies, JsonRequestBehavior.AllowGet);
        }

        public void AddCheckInRecords(CheckInRecord addCheckIn, List<string> Barcodes)
        {
            checkInRepository.Create<CheckInRecord>("CheckInRecords", addCheckIn);
        }
    }
}