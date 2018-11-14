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
    }
}