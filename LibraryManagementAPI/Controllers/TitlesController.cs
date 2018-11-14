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
    builder.EntitySet<Title>("Titles");
    builder.EntitySet<Level>("Levels"); 
    builder.EntitySet<TitleCategory>("TitleCategories"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TitlesController : ODataController
    {
        private LibraryManagementDbContext db = new LibraryManagementDbContext();

        // GET: odata/Titles
        [EnableQuery]
        public IQueryable<Title> GetTitles()
        {
            return db.Titles;
        }

        // GET: odata/Titles(5)
        [EnableQuery]
        public SingleResult<Title> GetTitle([FromODataUri] int key)
        {
            return SingleResult.Create(db.Titles.Where(title => title.Id == key));
        }

        // PUT: odata/Titles(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Title> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Title title = db.Titles.Find(key);
            if (title == null)
            {
                return NotFound();
            }

            patch.Put(title);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TitleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(title);
        }

        // POST: odata/Titles
        public IHttpActionResult Post(Title title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Titles.Add(title);
            db.SaveChanges();

            return Created(title);
        }

        // PATCH: odata/Titles(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Title> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Title title = db.Titles.Find(key);
            if (title == null)
            {
                return NotFound();
            }

            patch.Patch(title);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TitleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(title);
        }

        // DELETE: odata/Titles(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Title title = db.Titles.Find(key);
            if (title == null)
            {
                return NotFound();
            }

            db.Titles.Remove(title);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Titles(5)/Level
        [EnableQuery]
        public SingleResult<Level> GetLevel([FromODataUri] int key)
        {
            return SingleResult.Create(db.Titles.Where(m => m.Id == key).Select(m => m.Level));
        }

        // GET: odata/Titles(5)/TitleCategory
        [EnableQuery]
        public SingleResult<TitleCategory> GetTitleCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.Titles.Where(m => m.Id == key).Select(m => m.TitleCategory));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TitleExists(int key)
        {
            return db.Titles.Count(e => e.Id == key) > 0;
        }
    }
}
