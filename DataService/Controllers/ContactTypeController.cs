using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ChasWare.Data;
using ChasWare.Data.Classes;
using ChasWare.Data.Classes.DTO;
using ChasWare.Data.Classes.TX;
using Common.Logging;
using System.Collections.Generic;

namespace ChasWare.DataService.Controllers
{
    public class ContactTypesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public ContactTypesController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/ContactType/5
        [ResponseType(typeof(ContactTypeDTO))]
        public async Task<IHttpActionResult> DeleteContactType(int contactTypeId)
        {
            try
            {
                ContactType found = await DbContext.ContactTypes.FindAsync(contactTypeId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.ContactTypes.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("ContactType.Delete: " + ex);
                throw;
            }
        }

        // GET: api/ContactType/5
        [ResponseType(typeof(ContactTypeDTO))]
        public async Task<IHttpActionResult> GetContactType(int contactTypeId)
        {
            try
            {
                ContactTypeDTO found = await Task.Run(() => 
                    {
                        return ContactTypeTX.WriteToDTO(DbContext.ContactTypes
                            .AsEnumerable().FirstOrDefault(e => e.ContactTypeId == contactTypeId));
                    });
                if (found == null)
                {
                    return NotFound();
                }
                return Ok(found);
            }
            catch (Exception ex)
            {
                Log.Error("ContactType.Get: " + ex);
                throw;
            }
        }

         // POST: api/ContactTypes
        [ResponseType(typeof(ContactTypeDTO))]
        public async Task<IHttpActionResult> PostContactType(ContactTypeDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.ContactTypes.Add(ContactTypeTX.ReadFromDTO(new ContactType(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { ContactTypeId = value.ContactTypeId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("ContactType.Post: " + ex);
                throw;
            }
        }

        // PUT: api/ContactTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContactType(int contactTypeId, ContactTypeDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (value.ContactTypeId != contactTypeId)
                {
                    return BadRequest();
                }
                DbContext.Entry(value).State = EntityState.Modified;
                try
                {
                    await DbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValueExists(contactTypeId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Log.Error("ContactType.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int contactTypeId)
        {
            return DbContext.ContactTypes.Count(e => e.ContactTypeId == contactTypeId) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}

