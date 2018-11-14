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
    builder.EntitySet<BookCondition>("BookConditions");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BookConditionsController : ODataController
    {
        private LibraryManagementDbContext db = new LibraryManagementDbContext();

        // GET: odata/BookConditions
        [EnableQuery]
        public IQueryable<BookCondition> GetBookConditions()
        {
            return db.BookConditions;
        }

        // GET: odata/BookConditions(5)
        [EnableQuery]
        public SingleResult<BookCondition> GetBookCondition([FromODataUri] int key)
        {
            return SingleResult.Create(db.BookConditions.Where(bookCondition => bookCondition.Id == key));
        }

        // PUT: odata/BookConditions(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<BookCondition> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookCondition bookCondition = db.BookConditions.Find(key);
            if (bookCondition == null)
            {
                return NotFound();
            }

            patch.Put(bookCondition);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookConditionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(bookCondition);
        }

        // POST: odata/BookConditions
        public IHttpActionResult Post(BookCondition bookCondition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BookConditions.Add(bookCondition);
            db.SaveChanges();

            return Created(bookCondition);
        }

        // PATCH: odata/BookConditions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<BookCondition> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookCondition bookCondition = db.BookConditions.Find(key);
            if (bookCondition == null)
            {
                return NotFound();
            }

            patch.Patch(bookCondition);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookConditionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(bookCondition);
        }

        // DELETE: odata/BookConditions(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            BookCondition bookCondition = db.BookConditions.Find(key);
            if (bookCondition == null)
            {
                return NotFound();
            }

            db.BookConditions.Remove(bookCondition);
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

        private bool BookConditionExists(int key)
        {
            return db.BookConditions.Count(e => e.Id == key) > 0;
        }
    }
}
