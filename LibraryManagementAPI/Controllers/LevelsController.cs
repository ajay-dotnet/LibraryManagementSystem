using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using LibraryManagement.ObjectModel;
using LibraryManagement.Data.DataContext;

namespace LibraryManagementAPI.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using LibraryManagement.ObjectModel;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Level>("Levels");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LevelsController : ODataController
    {
        private LibraryManagementDbContext db = new LibraryManagementDbContext();

        // GET: odata/Levels
        [EnableQuery]
        public IQueryable<Level> GetLevels()
        {
            return db.Levels;
        }

        // GET: odata/Levels(5)
        [EnableQuery]
        public SingleResult<Level> GetLevel([FromODataUri] int key)
        {
            return SingleResult.Create(db.Levels.Where(level => level.Id == key));
        }

        // PUT: odata/Levels(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Level> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Level level = db.Levels.Find(key);
            if (level == null)
            {
                return NotFound();
            }

            patch.Put(level);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LevelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(level);
        }

        // POST: odata/Levels
        public IHttpActionResult Post(Level level)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Levels.Add(level);
            db.SaveChanges();

            return Created(level);
        }

        // PATCH: odata/Levels(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Level> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Level level = db.Levels.Find(key);
            if (level == null)
            {
                return NotFound();
            }

            patch.Patch(level);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LevelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(level);
        }

        // DELETE: odata/Levels(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Level level = db.Levels.Find(key);
            if (level == null)
            {
                return NotFound();
            }

            db.Levels.Remove(level);
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

        private bool LevelExists(int key)
        {
            return db.Levels.Count(e => e.Id == key) > 0;
        }
    }
}
