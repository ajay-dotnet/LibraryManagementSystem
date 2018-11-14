using System.Collections.Generic;
using System.Linq;
using LibraryManagement.LibraryClient;

namespace LibraryManagementUI.Repositories
{
    public class TitleRepository : BaseRepository, ITitleRepository
    {
        public List<TitleList> GetTitleList()
        {
            var titles = Container.TitleLists.ToList();

            return titles;
        }

        public List<Level> GetLevelList()
        {
            var levels = Container.Levels.ToList();
            return levels;
        }

        public List<TitleCategory> GetTitleCategoryList()
        {
            var titleCategories = Container.TitleCategories.ToList();
            return titleCategories;
        }

        public void DeleteTitle(int titleId)
        {
            var title = Container.Titles.Where(x => x.Id == titleId).Single();
            title.IsActive = false;
            Update<Title>("Titles", title, false);
        }

        public bool DoesTitleExist(Title title)
        {
            return Container.Titles.Where(x => x.Name == title.Name).ToList().Count() > 0;
        }
    }
}