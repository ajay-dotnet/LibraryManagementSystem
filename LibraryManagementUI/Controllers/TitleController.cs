using System.Web.Mvc;
using LibraryManagement.LibraryClient;
using LibraryManagementUI.Repositories;

namespace LibraryManagementUI.Controllers
{
    public class TitleController : Controller
    {
        private ITitleRepository titleRepository;

        public TitleController(ITitleRepository _titleRepository)
        {
            this.titleRepository = _titleRepository;
        }

        // GET: Title
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets all associates
        /// </summary>
        /// <returns>
        /// Json string with all associates details
        /// </returns>
        public JsonResult GetAllTitles()
        {
            var titleList = titleRepository.GetTitleList();
            return Json(titleList, JsonRequestBehavior.AllowGet);
        }

        ///// <summary>
        ///// Gets all Specializations
        ///// </summary>
        ///// <returns>
        ///// Json string with all Specializations details
        ///// </returns>
        public JsonResult GetTitleCategories()
        {
            var titleCategoryList = titleRepository.GetTitleCategoryList();
            return Json(titleCategoryList, JsonRequestBehavior.AllowGet);
        }

        ///// <summary>
        ///// Gets all Specializations
        ///// </summary>
        ///// <returns>
        ///// Json string with all Specializations details
        ///// </returns>
        public JsonResult GetLevels()
        {
            var levelList = titleRepository.GetLevelList();
            return Json(levelList, JsonRequestBehavior.AllowGet);
        }

        ///// <summary>
        ///// Adds or updates Title depending on the input.
        ///// </summary>
        ///// <param name="Title">Title to be added or updated</param>
        public JsonResult AddUpdateTitle(Title title)
        {
            var success = true;
            var message = string.Empty;
            title.IsActive = true;
            title.Level_Id = title.Level == null ? title.Level_Id : title.Level.Id;
            title.TitleCategory_Id = title.TitleCategory == null ? title.TitleCategory_Id : title.TitleCategory.Id;

            if (title.Id > 0)
            {
                titleRepository.Update<Title>("Titles", title);
            }
            else
            {
                if (!titleRepository.DoesTitleExist(title))
                    titleRepository.Create<Title>("Titles", title);
                else
                {
                    success = false;
                    message = "Title name already exists";
                }
            }
            return Json(new { Success = success, Message = message });
        }

        ///// <summary>
        ///// Deletes an Title
        ///// </summary>
        ///// <param name="id">Id of the associate to be deleted.</param>
        public void DeleteTitle(int id)
        {
            titleRepository.DeleteTitle(id);
        }
    }
}