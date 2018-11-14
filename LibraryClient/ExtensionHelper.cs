using OM = LibraryManagement.ObjectModel;

namespace LibraryManagement.LibraryClient
{
    public static class ExtensionHelper
    {
        public static OM.Title ToObjectModel(this Title title)
        {
            var objectModel = new OM.Title();
            if (title != null)
            {
                objectModel.Author = title.Author;
                objectModel.Id = title.Id;
                objectModel.IsActive = title.IsActive;
                objectModel.Level_Id = title.Level_Id;
                objectModel.Name = title.Name;
                objectModel.Price = title.Price;
                objectModel.Publisher = title.Publisher;
                objectModel.TitleCategory_Id = title.TitleCategory_Id;
            }

            return objectModel;
        }
    }
}
