using System.Collections.Generic;
using System.Linq;
using LibraryManagement.LibraryClient;
using System;

namespace LibraryManagementUI.Repositories
{
    public class CheckInRepository : BaseRepository, ICheckInRepository
    {
        public List<Fairy> GetAllFairies()
        {
            var fairies = Container.Fairies.ToList();
            return fairies;
        }

        public void CheckInBooks(CheckInRecord addCheckIn, List<string> Barcodes)
        {
            foreach (var item in Barcodes)
            {

            }
        }

        public bool IsCheckInValid(CheckInRecord checkInRecord, string barcode)
        {
            var dbrecord = GetCheckInRecord(barcode);

            if (dbrecord == null) return false;
            if (dbrecord.CheckInDate != null && dbrecord.CheckInDate != default(DateTime)) return false;
            return true;
        }

        public CheckInRecord GetCheckInRecord(string barcode)
        {
            var book = Container.Books.Where(x => x.Barcode == barcode).ToList();
            if (book.Any())
            {
                var dbrecord = Container.CheckInRecords.Where(x => x.Book_Id == book.First().Id)
                                                       .OrderByDescending(x => x.IssueDate).FirstOrDefault();
                return dbrecord;
            }
            return null;
        }

        public bool IsIssueValid(CheckInRecord checkInRecord, string barcode)
        {
            var dbrecord = GetCheckInRecord(barcode);

            if (dbrecord.CheckInDate == null || dbrecord.CheckInDate == default(DateTime)) return false;

            return true;
        }
    }
}