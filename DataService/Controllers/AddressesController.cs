// --------------------------------------------------------------------------------------------------------------------
// <copyright file=AddressesController.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ChasWare.Data;
using ChasWare.Data.Classes;

namespace DataService.Controllers
{
    public class AddressesController : ApiController
    {
        #region Constants and fields 

        private readonly DataContext _dataContext = new DataContext();

        #endregion

        #region public methods

        // DELETE: api/Addresses/5
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> DeleteAddress(int id)
        {
            Address address = await _dataContext.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _dataContext.Addresses.Remove(address);
            await _dataContext.SaveChangesAsync();

            return Ok(address);
        }

        // GET: api/Addresses/5
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> GetAddress(int id)
        {
            Address address = await _dataContext.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // GET: api/Addresses
        public IQueryable<Address> GetAddresses()
        {
            return _dataContext.Addresses;
        }

        // POST: api/Addresses
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dataContext.Addresses.Add(address);
            await _dataContext.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new {id = address.AddressId}, address);
        }

        // PUT: api/Addresses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAddress(int id, Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.AddressId)
            {
                return BadRequest();
            }

            _dataContext.Entry(address).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

        #region other methods

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataContext.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool AddressExists(int id)
        {
            return _dataContext.Addresses.Count(e => e.AddressId == id) > 0;
        }

        #endregion
    }
}