using System.Collections.Generic;
using System.Linq;
using LibraryManagement.LibraryClient;

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
    }
}