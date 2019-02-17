using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.LibraryClient;

namespace LibraryManagementUI.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public List<Title> GetTitleList()
        {
            var titles = Container.Titles.Where(x => x.IsActive == true).ToList();
            return titles;
        }

        public List<BookCondition> GetBookConditionList()
        {
            var conditions = Container.BookConditions.ToList();
            return conditions;
        }

        public string GetBarcode(int titleId)
        {
            //int.TryParse(Container.Books.OrderByDescending(x => x.Id).First().Barcode, out barcode);
            var bookCount = Container.Books.Where(x => x.Title_Id == titleId).ToList().Count;
            ++bookCount;
            return titleId.ToString() + 'A' + bookCount.ToString(); ;
        }

        public bool IsBarcodeValid(string barcode)
        {
            var book = Container.Books.Where(x => x.Barcode == barcode).ToList();
            if (book.Any()) return true;
            return false;
        }

        public int GetBookId(string barcode)
        {
            var book = Container.Books.Where(x => x.Barcode == barcode).ToList();
            if (book.Any()) return book.First().Id;
            return 0;
        }

        public void UpdateBookCondition(List<string> barcodes, BookCondition bookCondition, DateTime damLostDate)
        {
            foreach (var barcode in barcodes)
            {
                var book = Container.Books.Where(x => x.Barcode == barcode).FirstOrDefault();
                if (book != null)
                {
                    book.BookCondition_Id = bookCondition.Id;
                    book.DamLostDate = damLostDate;
                    Container.UpdateObject(book);
                }
            }
            Container.SaveChanges();
        }
    }
}