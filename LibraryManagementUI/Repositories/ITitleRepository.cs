using System.Collections.Generic;
using LibraryManagement.LibraryClient;

namespace LibraryManagementUI.Repositories
{
    public interface ITitleRepository : IBaseRepository
    {
        List<TitleList> GetTitleList();

        List<Level> GetLevelList();

        List<TitleCategory> GetTitleCategoryList();

        void DeleteTitle(int titleId);

        bool DoesTitleExist(Title title);
    }
}