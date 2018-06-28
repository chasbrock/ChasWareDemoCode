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
    public class EntityContactsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public EntityContactsController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/EntityContact/5
        [ResponseType(typeof(EntityContactDTO))]
        public async Task<IHttpActionResult> DeleteEntityContact(int contactId)
        {
            try
            {
                EntityContact found = await DbContext.EntityContacts.FindAsync(contactId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.EntityContacts.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("EntityContact.Delete: " + ex);
                throw;
            }
        }

        // GET: api/EntityContact/5
        [ResponseType(typeof(EntityContactDTO))]
        public async Task<IHttpActionResult> GetEntityContact(int contactId)
        {
            try
            {
                EntityContactDTO found = await Task.Run(() => 
                    {
                        return EntityContactTX.WriteToDTO(DbContext.EntityContacts
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
                Log.Error("EntityContact.Get: " + ex);
                throw;
            }
        }

         // POST: api/EntityContacts
        [ResponseType(typeof(EntityContactDTO))]
        public async Task<IHttpActionResult> PostEntityContact(EntityContactDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.EntityContacts.Add(EntityContactTX.ReadFromDTO(new EntityContact(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { ContactId = value.ContactId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("EntityContact.Post: " + ex);
                throw;
            }
        }

        // PUT: api/EntityContacts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEntityContact(int contactId, EntityContactDTO value)
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
                Log.Error("EntityContact.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int contactId)
        {
            return DbContext.EntityContacts.Count(e => e.ContactId == contactId) > 0;
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

