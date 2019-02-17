using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.LibraryClient;


namespace LibraryManagementUI.Repositories
{
    public interface ICheckInRepository : IBaseRepository
    {
        List<Fairy> GetAllFairies();

        bool IsCheckInValid(CheckInRecord checkInRecord, string barcode);

        CheckInRecord GetCheckInRecord(string barcode);
        bool IsIssueValid(CheckInRecord checkInRecord, string barcode);
    }
}
