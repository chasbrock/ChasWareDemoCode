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
    public class PersonsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public PersonsController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/Person/5
        [ResponseType(typeof(PersonDTO))]
        public async Task<IHttpActionResult> DeletePerson(int entityId)
        {
            try
            {
                Person found = await DbContext.Persons.FindAsync(entityId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.Persons.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("Person.Delete: " + ex);
                throw;
            }
        }

        // GET: api/Person/5
        [ResponseType(typeof(PersonDTO))]
        public async Task<IHttpActionResult> GetPerson(int entityId)
        {
            try
            {
                PersonDTO found = await Task.Run(() => 
                    {
                        return PersonTX.WriteToDTO(DbContext.Persons
                            .AsEnumerable().FirstOrDefault(e => e.EntityId == entityId));
                    });
                if (found == null)
                {
                    return NotFound();
                }
                return Ok(found);
            }
            catch (Exception ex)
            {
                Log.Error("Person.Get: " + ex);
                throw;
            }
        }

         // POST: api/Persons
        [ResponseType(typeof(PersonDTO))]
        public async Task<IHttpActionResult> PostPerson(PersonDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.Persons.Add(PersonTX.ReadFromDTO(new Person(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { EntityId = value.EntityId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("Person.Post: " + ex);
                throw;
            }
        }

        // PUT: api/Persons/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPerson(int entityId, PersonDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (value.EntityId != entityId)
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
                    if (!ValueExists(entityId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Log.Error("Person.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int entityId)
        {
            return DbContext.Persons.Count(e => e.EntityId == entityId) > 0;
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

