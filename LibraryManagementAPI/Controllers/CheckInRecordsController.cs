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
    builder.EntitySet<CheckInRecord>("CheckInRecords");
    builder.EntitySet<Book>("Books"); 
    builder.EntitySet<Fairy>("Fairies"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CheckInRecordsController : ODataController
    {
        private LibraryManagementDbContext db = new LibraryManagementDbContext();

        // GET: odata/CheckInRecords
        [EnableQuery]
        public IQueryable<CheckInRecord> GetCheckInRecords()
        {
            return db.CheckInRecords;
        }

        // GET: odata/CheckInRecords(5)
        [EnableQuery]
        public SingleResult<CheckInRecord> GetCheckInRecord([FromODataUri] int key)
        {
            return SingleResult.Create(db.CheckInRecords.Where(checkInRecord => checkInRecord.Id == key));
        }

        // PUT: odata/CheckInRecords(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<CheckInRecord> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CheckInRecord checkInRecord = db.CheckInRecords.Find(key);
            if (checkInRecord == null)
            {
                return NotFound();
            }

            patch.Put(checkInRecord);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckInRecordExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(checkInRecord);
        }

        // POST: odata/CheckInRecords
        public IHttpActionResult Post(CheckInRecord checkInRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CheckInRecords.Add(checkInRecord);
            db.SaveChanges();

            return Created(checkInRecord);
        }

        // PATCH: odata/CheckInRecords(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<CheckInRecord> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CheckInRecord checkInRecord = db.CheckInRecords.Find(key);
            if (checkInRecord == null)
            {
                return NotFound();
            }

            patch.Patch(checkInRecord);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckInRecordExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(checkInRecord);
        }

        // DELETE: odata/CheckInRecords(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            CheckInRecord checkInRecord = db.CheckInRecords.Find(key);
            if (checkInRecord == null)
            {
                return NotFound();
            }

            db.CheckInRecords.Remove(checkInRecord);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/CheckInRecords(5)/Book
        [EnableQuery]
        public SingleResult<Book> GetBook([FromODataUri] int key)
        {
            return SingleResult.Create(db.CheckInRecords.Where(m => m.Id == key).Select(m => m.Book));
        }

        // GET: odata/CheckInRecords(5)/Fairy
        [EnableQuery]
        public SingleResult<Fairy> GetFairy([FromODataUri] int key)
        {
            return SingleResult.Create(db.CheckInRecords.Where(m => m.Id == key).Select(m => m.Fairy));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CheckInRecordExists(int key)
        {
            return db.CheckInRecords.Count(e => e.Id == key) > 0;
        }
    }
}
