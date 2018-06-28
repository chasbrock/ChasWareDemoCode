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
    public class AddressTypesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public AddressTypesController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/AddressType/5
        [ResponseType(typeof(AddressTypeDTO))]
        public async Task<IHttpActionResult> DeleteAddressType(int addressTypeId)
        {
            try
            {
                AddressType found = await DbContext.AddressTypes.FindAsync(addressTypeId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.AddressTypes.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("AddressType.Delete: " + ex);
                throw;
            }
        }

        // GET: api/AddressType/5
        [ResponseType(typeof(AddressTypeDTO))]
        public async Task<IHttpActionResult> GetAddressType(int addressTypeId)
        {
            try
            {
                AddressTypeDTO found = await Task.Run(() => 
                    {
                        return AddressTypeTX.WriteToDTO(DbContext.AddressTypes
                            .AsEnumerable().FirstOrDefault(e => e.AddressTypeId == addressTypeId));
                    });
                if (found == null)
                {
                    return NotFound();
                }
                return Ok(found);
            }
            catch (Exception ex)
            {
                Log.Error("AddressType.Get: " + ex);
                throw;
            }
        }

         // POST: api/AddressTypes
        [ResponseType(typeof(AddressTypeDTO))]
        public async Task<IHttpActionResult> PostAddressType(AddressTypeDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.AddressTypes.Add(AddressTypeTX.ReadFromDTO(new AddressType(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { AddressTypeId = value.AddressTypeId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("AddressType.Post: " + ex);
                throw;
            }
        }

        // PUT: api/AddressTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAddressType(int addressTypeId, AddressTypeDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (value.AddressTypeId != addressTypeId)
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
                    if (!ValueExists(addressTypeId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Log.Error("AddressType.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int addressTypeId)
        {
            return DbContext.AddressTypes.Count(e => e.AddressTypeId == addressTypeId) > 0;
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

