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
    builder.EntitySet<TitleList>("TitleLists");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TitleListsController : ODataController
    {
        private LibraryManagementDbContext db = new LibraryManagementDbContext();

        // GET: odata/TitleLists
        [EnableQuery]
        public IQueryable<TitleList> GetTitleLists()
        {
            return db.TitleLists;
        }

        // GET: odata/TitleLists(5)
        [EnableQuery]
        public SingleResult<TitleList> GetTitleList([FromODataUri] int key)
        {
            return SingleResult.Create(db.TitleLists.Where(titleList => titleList.Id == key));
        }

        // PUT: odata/TitleLists(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<TitleList> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TitleList titleList = db.TitleLists.Find(key);
            if (titleList == null)
            {
                return NotFound();
            }

            patch.Put(titleList);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TitleListExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(titleList);
        }

        // POST: odata/TitleLists
        public IHttpActionResult Post(TitleList titleList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TitleLists.Add(titleList);
            db.SaveChanges();

            return Created(titleList);
        }

        // PATCH: odata/TitleLists(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<TitleList> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TitleList titleList = db.TitleLists.Find(key);
            if (titleList == null)
            {
                return NotFound();
            }

            patch.Patch(titleList);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TitleListExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(titleList);
        }

        // DELETE: odata/TitleLists(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            TitleList titleList = db.TitleLists.Find(key);
            if (titleList == null)
            {
                return NotFound();
            }

            db.TitleLists.Remove(titleList);
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

        private bool TitleListExists(int key)
        {
            return db.TitleLists.Count(e => e.Id == key) > 0;
        }
    }
}
