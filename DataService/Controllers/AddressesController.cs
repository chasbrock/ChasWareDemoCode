// --------------------------------------------------------------------------------------------------------------------
// <copyright file=AddressesController.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ChasWare.Data;
using ChasWare.Data.Classes;
using ChasWare.Data.Classes.DTO;
using ChasWare.Data.Classes.TX;
using Common.Logging;

namespace ChasWare.DataService.Controllers
{
    /// <inheritdoc />
    /// <summary>
    ///     provides web access to Adress objects
    /// </summary>
    public class AddressesController : ApiController
    {
        #region Constants and fields 

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly DataContext _dataContext;

        #endregion

        #region Constructors

        public AddressesController()
        {
            _dataContext = new DataContext();
            _dataContext.Configuration.ProxyCreationEnabled = false;
        }

        #endregion

        #region public methods

        // DELETE: api/Addresses/5
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> DeleteAddress(int id)
        {
            Address address = await Task.Run(() => { return _dataContext.Addresses.AsNoTracking().FirstOrDefault(a => a.EntityAddress.AddressId == id); });

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
            try
            {
                AddressDTO address = await Task.Run(() => { return AddressTX.WriteToDTO( _dataContext.Addresses.Include(a => a.StateProvince).AsEnumerable().FirstOrDefault(a => a.AddressId == id)); });
                if (address == null)
                {
                    return NotFound();
                }

                return Ok(address);
            }
            catch (Exception ex)
            {
                Log.Error($"GetAddress(int {id})", ex);
            }

            return NotFound();
        }

        // GET: api/Addresses
        public IEnumerable<AddressDTO> GetAddresses()
        {
            return _dataContext.Addresses.Include(x => x.StateProvince).AsNoTracking().AsEnumerable().Select(AddressTX.WriteToDTO);
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