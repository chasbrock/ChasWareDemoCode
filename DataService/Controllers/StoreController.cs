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
    public class StoresController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public StoresController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/Store/5
        [ResponseType(typeof(StoreDTO))]
        public async Task<IHttpActionResult> DeleteStore(int entityId)
        {
            try
            {
                Store found = await DbContext.Stores.FindAsync(entityId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.Stores.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("Store.Delete: " + ex);
                throw;
            }
        }

        // GET: api/Store/5
        [ResponseType(typeof(StoreDTO))]
        public async Task<IHttpActionResult> GetStore(int entityId)
        {
            try
            {
                StoreDTO found = await Task.Run(() => 
                    {
                        return StoreTX.WriteToDTO(DbContext.Stores
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
                Log.Error("Store.Get: " + ex);
                throw;
            }
        }

         // POST: api/Stores
        [ResponseType(typeof(StoreDTO))]
        public async Task<IHttpActionResult> PostStore(StoreDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.Stores.Add(StoreTX.ReadFromDTO(new Store(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { EntityId = value.EntityId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("Store.Post: " + ex);
                throw;
            }
        }

        // PUT: api/Stores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStore(int entityId, StoreDTO value)
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
                Log.Error("Store.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int entityId)
        {
            return DbContext.Stores.Count(e => e.EntityId == entityId) > 0;
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

