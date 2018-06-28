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
    public class AddressesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public AddressesController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/Address/5
        [ResponseType(typeof(AddressDTO))]
        public async Task<IHttpActionResult> DeleteAddress(int addressId)
        {
            try
            {
                Address found = await DbContext.Addresses.FindAsync(addressId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.Addresses.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("Address.Delete: " + ex);
                throw;
            }
        }

        // GET: api/Address/5
        [ResponseType(typeof(AddressDTO))]
        public async Task<IHttpActionResult> GetAddress(int addressId)
        {
            try
            {
                AddressDTO found = await Task.Run(() => 
                    {
                        return AddressTX.WriteToDTO(DbContext.Addresses
                            .Include(a => a.StateProvince)
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
                Log.Error("Address.Get: " + ex);
                throw;
            }
        }

         // POST: api/Addresses
        [ResponseType(typeof(AddressDTO))]
        public async Task<IHttpActionResult> PostAddress(AddressDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.Addresses.Add(AddressTX.ReadFromDTO(new Address(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { AddressId = value.AddressId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("Address.Post: " + ex);
                throw;
            }
        }

        // PUT: api/Addresses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAddress(int addressId, AddressDTO value)
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
                Log.Error("Address.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int addressId)
        {
            return DbContext.Addresses.Count(e => e.AddressId == addressId) > 0;
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

