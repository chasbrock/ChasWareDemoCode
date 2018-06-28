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
    public class ContactDetailsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public ContactDetailsController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/ContactDetail/5
        [ResponseType(typeof(ContactDetailDTO))]
        public async Task<IHttpActionResult> DeleteContactDetail(int contactId)
        {
            try
            {
                ContactDetail found = await DbContext.ContactDetails.FindAsync(contactId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.ContactDetails.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("ContactDetail.Delete: " + ex);
                throw;
            }
        }

        // GET: api/ContactDetail/5
        [ResponseType(typeof(ContactDetailDTO))]
        public async Task<IHttpActionResult> GetContactDetail(int contactId)
        {
            try
            {
                ContactDetailDTO found = await Task.Run(() => 
                    {
                        return ContactDetailTX.WriteToDTO(DbContext.ContactDetails
                            .AsEnumerable().FirstOrDefault(e => e.ContactId == contactId));
                    });
                if (found == null)
                {
                    return NotFound();
                }
                return Ok(found);
            }
            catch (Exception ex)
            {
                Log.Error("ContactDetail.Get: " + ex);
                throw;
            }
        }

         // POST: api/ContactDetails
        [ResponseType(typeof(ContactDetailDTO))]
        public async Task<IHttpActionResult> PostContactDetail(ContactDetailDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.ContactDetails.Add(ContactDetailTX.ReadFromDTO(new ContactDetail(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { ContactId = value.ContactId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("ContactDetail.Post: " + ex);
                throw;
            }
        }

        // PUT: api/ContactDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContactDetail(int contactId, ContactDetailDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (value.ContactId != contactId)
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
                    if (!ValueExists(contactId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Log.Error("ContactDetail.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int contactId)
        {
            return DbContext.ContactDetails.Count(e => e.ContactId == contactId) > 0;
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

