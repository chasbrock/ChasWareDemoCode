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
    public class EntityAddressesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public EntityAddressesController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/EntityAddress/5
        [ResponseType(typeof(EntityAddressDTO))]
        public async Task<IHttpActionResult> DeleteEntityAddress(int addressId)
        {
            try
            {
                EntityAddress found = await DbContext.EntityAddresses.FindAsync(addressId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.EntityAddresses.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("EntityAddress.Delete: " + ex);
                throw;
            }
        }

        // GET: api/EntityAddress/5
        [ResponseType(typeof(EntityAddressDTO))]
        public async Task<IHttpActionResult> GetEntityAddress(int addressId)
        {
            try
            {
                EntityAddressDTO found = await Task.Run(() => 
                    {
                        return EntityAddressTX.WriteToDTO(DbContext.EntityAddresses
                            .AsEnumerable().FirstOrDefault(e => e.AddressId == addressId));
                    });
                if (found == null)
                {
                    return NotFound();
                }
                return Ok(found);
            }
            catch (Exception ex)
            {
                Log.Error("EntityAddress.Get: " + ex);
                throw;
            }
        }

         // POST: api/EntityAddresses
        [ResponseType(typeof(EntityAddressDTO))]
        public async Task<IHttpActionResult> PostEntityAddress(EntityAddressDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.EntityAddresses.Add(EntityAddressTX.ReadFromDTO(new EntityAddress(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { AddressId = value.AddressId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("EntityAddress.Post: " + ex);
                throw;
            }
        }

        // PUT: api/EntityAddresses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEntityAddress(int addressId, EntityAddressDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (value.AddressId != addressId)
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
                    if (!ValueExists(addressId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Log.Error("EntityAddress.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int addressId)
        {
            return DbContext.EntityAddresses.Count(e => e.AddressId == addressId) > 0;
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

