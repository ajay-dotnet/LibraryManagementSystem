using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LibraryManagement.LibraryClient;
using LibraryManagementUI.App_Start;
using LibraryManagementUI.Repositories;

namespace LibraryManagementUI.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository bookRepository;

        public BookController(IBookRepository _bookRepository)
        {
            this.bookRepository = _bookRepository;
        }

        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Barcode()
        {
            return View();
        }

        public JsonResult GetAllTitles()
        {
            var titleList = bookRepository.GetTitleList();
            return Json(titleList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBookConditionList()
        {
            var bookConditionList = bookRepository.GetBookConditionList();
            return Json(bookConditionList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddBooks(Book book, int noOfBooks)
        {
            book.Title_Id = book.Title.Id;
            book.BookCondition_Id = book.BookCondition.Id;
            book.DamLostDate = (book.DamLostDate == default(DateTime)) ? null : book.DamLostDate;
            var barcodeList = new List<String>();
            for (int i = 0; i < noOfBooks; i++)
            {
                book.Barcode = bookRepository.GetBarcode(book.Title_Id);
                //book.Barcode = (++barcode).ToString();
                bookRepository.CreateMultiple<Book>("Books", book);
                var barcode = BarCode39.getBarcodeImage(book.Barcode, "Book");
                var barcodeUrl = "data:image/jpg;base64," + Convert.ToBase64String(barcode);
                barcodeList.Add(barcodeUrl);
            }

            return Json(barcodeList, JsonRequestBehavior.AllowGet);
        }
    }
}