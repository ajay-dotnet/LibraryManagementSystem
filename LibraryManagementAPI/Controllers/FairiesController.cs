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
    builder.EntitySet<Fairy>("Fairies");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class FairiesController : ODataController
    {
        private LibraryManagementDbContext db = new LibraryManagementDbContext();

        // GET: odata/Fairies
        [EnableQuery]
        public IQueryable<Fairy> GetFairies()
        {
            return db.Fairies;
        }

        // GET: odata/Fairies(5)
        [EnableQuery]
        public SingleResult<Fairy> GetFairy([FromODataUri] int key)
        {
            return SingleResult.Create(db.Fairies.Where(fairy => fairy.Id == key));
        }

        // PUT: odata/Fairies(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Fairy> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Fairy fairy = db.Fairies.Find(key);
            if (fairy == null)
            {
                return NotFound();
            }

            patch.Put(fairy);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FairyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(fairy);
        }

        // POST: odata/Fairies
        public IHttpActionResult Post(Fairy fairy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fairies.Add(fairy);
            db.SaveChanges();

            return Created(fairy);
        }

        // PATCH: odata/Fairies(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Fairy> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Fairy fairy = db.Fairies.Find(key);
            if (fairy == null)
            {
                return NotFound();
            }

            patch.Patch(fairy);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FairyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(fairy);
        }

        // DELETE: odata/Fairies(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Fairy fairy = db.Fairies.Find(key);
            if (fairy == null)
            {
                return NotFound();
            }

            db.Fairies.Remove(fairy);
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

        private bool FairyExists(int key)
        {
            return db.Fairies.Count(e => e.Id == key) > 0;
        }
    }
}
