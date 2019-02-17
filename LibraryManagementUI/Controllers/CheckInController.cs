using System.Collections.Generic;
using System.Web.Mvc;
using LibraryManagement.LibraryClient;
using LibraryManagementUI.Repositories;
using System.Text;
using System;

namespace LibraryManagementUI.Controllers
{
    public class CheckInController : Controller
    {
        private ICheckInRepository checkInRepository;
        private IBookRepository bookRepository;

        public CheckInController(ICheckInRepository _checkInRepository, IBookRepository _bookRepository)
        {
            this.checkInRepository = _checkInRepository;
            this.bookRepository = _bookRepository;
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

        public JsonResult AddCheckInRecords(CheckInRecord addCheckIn, List<string> Barcodes)
        {
            var success = true;
            StringBuilder message = new StringBuilder(string.Empty);

            if (addCheckIn.IssueDate == default(DateTime))
            {
                foreach (var item in Barcodes)
                {                    
                    if(!bookRepository.IsBarcodeValid(item))
                    {
                        message.AppendLine($"Book with barcode:{item} doesn't exist.");
                        message.AppendLine();
                    }
                    else if (!checkInRepository.IsCheckInValid(addCheckIn, item))
                    {
                        message.AppendLine($"Book with barcode:{item} is already checked in or never issued.");
                        message.AppendLine();
                    }
                    else
                    {
                        var checkIn = checkInRepository.GetCheckInRecord(item);
                        checkIn.CheckInDate = addCheckIn.CheckInDate;
                        checkInRepository.Update<CheckInRecord>("CheckInRecords", checkIn);                        
                    }
                }
            }
            else
            {
                addCheckIn.Fairy_Id = addCheckIn.Fairy.Id;

                foreach (var item in Barcodes)
                {
                    if (!bookRepository.IsBarcodeValid(item))
                    {
                        message.AppendLine($"Book with barcode:{item} doesn't exist.");
                        message.AppendLine();
                    }
                    else if (!checkInRepository.IsIssueValid(addCheckIn, item))
                    {
                        message.AppendLine($"Book with barcode:{item} is not checked in. So, unable to issue.");
                        message.AppendLine();
                    }
                    else
                    {
                        addCheckIn.Book_Id = bookRepository.GetBookId(item);
                        checkInRepository.Create<CheckInRecord>("CheckInRecords", addCheckIn);
                    }
                }
            }

            return Json(new { Success = success, Message = message.ToString() });
        }
    }
}