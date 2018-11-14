using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using LibraryManagement.ObjectModel;

namespace LibraryManagementAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Title>("Titles");
            builder.EntitySet<Level>("Levels");
            builder.EntitySet<TitleCategory>("TitleCategories");
            builder.EntitySet<Book>("Books");
            builder.EntitySet<BookCondition>("BookConditions");
            builder.EntitySet<Fairy>("Fairies");
            builder.EntitySet<TitleList>("TitleLists");
            builder.EntitySet<CheckInRecord>("CheckInRecords");

            //var addUpdateTitle = builder.Entity<Title>().Collection.Action("AddUpdateTitle");
            //addUpdateTitle.Parameter<Title>(typeof(Title).Name);

            config.AddODataQueryFilter();
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            config.Formatters.Clear();
            config.Formatters.Add(new XmlMediaTypeFormatter());
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.Add(new FormUrlEncodedMediaTypeFormatter());
        }
    }
}
