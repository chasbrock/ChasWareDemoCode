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
    public class EntitiesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public EntitiesController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/Entity/5
        [ResponseType(typeof(EntityDTO))]
        public async Task<IHttpActionResult> DeleteEntity(int entityId)
        {
            try
            {
                Entity found = await DbContext.Entities.FindAsync(entityId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.Entities.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("Entity.Delete: " + ex);
                throw;
            }
        }

        // GET: api/Entity/5
        [ResponseType(typeof(EntityDTO))]
        public async Task<IHttpActionResult> GetEntity(int entityId)
        {
            try
            {
                EntityDTO found = await Task.Run(() => 
                    {
                        return EntityTX.WriteToDTO(DbContext.Entities
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
                Log.Error("Entity.Get: " + ex);
                throw;
            }
        }

         // POST: api/Entities
        [ResponseType(typeof(EntityDTO))]
        public async Task<IHttpActionResult> PostEntity(EntityDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.Entities.Add(EntityTX.ReadFromDTO(new Entity(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { EntityId = value.EntityId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("Entity.Post: " + ex);
                throw;
            }
        }

        // PUT: api/Entities/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEntity(int entityId, EntityDTO value)
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
                Log.Error("Entity.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int entityId)
        {
            return DbContext.Entities.Count(e => e.EntityId == entityId) > 0;
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

