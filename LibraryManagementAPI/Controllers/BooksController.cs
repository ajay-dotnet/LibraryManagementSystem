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
    builder.EntitySet<Book>("Books");
    builder.EntitySet<BookCondition>("BookConditions"); 
    builder.EntitySet<Title>("Titles"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BooksController : ODataController
    {
        private LibraryManagementDbContext db = new LibraryManagementDbContext();

        // GET: odata/Books
        [EnableQuery]
        public IQueryable<Book> GetBooks()
        {
            return db.Books;
        }

        // GET: odata/Books(5)
        [EnableQuery]
        public SingleResult<Book> GetBook([FromODataUri] int key)
        {
            return SingleResult.Create(db.Books.Where(book => book.Id == key));
        }

        // PUT: odata/Books(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Book> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Book book = db.Books.Find(key);
            if (book == null)
            {
                return NotFound();
            }

            patch.Put(book);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(book);
        }

        // POST: odata/Books
        public IHttpActionResult Post(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return Created(book);
        }

        // PATCH: odata/Books(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Book> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Book book = db.Books.Find(key);
            if (book == null)
            {
                return NotFound();
            }

            patch.Patch(book);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(book);
        }

        // DELETE: odata/Books(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Book book = db.Books.Find(key);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Books(5)/BookCondition
        [EnableQuery]
        public SingleResult<BookCondition> GetBookCondition([FromODataUri] int key)
        {
            return SingleResult.Create(db.Books.Where(m => m.Id == key).Select(m => m.BookCondition));
        }

        // GET: odata/Books(5)/Title
        [EnableQuery]
        public SingleResult<Title> GetTitle([FromODataUri] int key)
        {
            return SingleResult.Create(db.Books.Where(m => m.Id == key).Select(m => m.Title));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int key)
        {
            return db.Books.Count(e => e.Id == key) > 0;
        }
    }
}
