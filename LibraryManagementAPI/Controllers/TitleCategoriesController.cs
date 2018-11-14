using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using LibraryManagement.Data.DataContext;
using LibraryManagement.ObjectModel;

namespace LibraryManagementAPI.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using LibraryManagement.ObjectModel;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<TitleCategory>("TitleCategories");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TitleCategoriesController : ODataController
    {
        private LibraryManagementDbContext db = new LibraryManagementDbContext();

        // GET: odata/TitleCategories
        [EnableQuery]
        public IQueryable<TitleCategory> GetTitleCategories()
        {
            return db.TitleCategories;
        }

        // GET: odata/TitleCategories(5)
        [EnableQuery]
        public SingleResult<TitleCategory> GetTitleCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.TitleCategories.Where(titleCategory => titleCategory.Id == key));
        }

        // PUT: odata/TitleCategories(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<TitleCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TitleCategory titleCategory = db.TitleCategories.Find(key);
            if (titleCategory == null)
            {
                return NotFound();
            }

            patch.Put(titleCategory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TitleCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(titleCategory);
        }

        // POST: odata/TitleCategories
        public IHttpActionResult Post(TitleCategory titleCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TitleCategories.Add(titleCategory);
            db.SaveChanges();

            return Created(titleCategory);
        }

        // PATCH: odata/TitleCategories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<TitleCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TitleCategory titleCategory = db.TitleCategories.Find(key);
            if (titleCategory == null)
            {
                return NotFound();
            }

            patch.Patch(titleCategory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TitleCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(titleCategory);
        }

        // DELETE: odata/TitleCategories(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            TitleCategory titleCategory = db.TitleCategories.Find(key);
            if (titleCategory == null)
            {
                return NotFound();
            }

            db.TitleCategories.Remove(titleCategory);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TitleCategoryExists(int key)
        {
            return db.TitleCategories.Count(e => e.Id == key) > 0;
        }
    }
}
