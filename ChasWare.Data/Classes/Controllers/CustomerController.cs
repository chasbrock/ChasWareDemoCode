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
    public class CustomersController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public CustomersController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/Customer/5
        [ResponseType(typeof(CustomerDTO))]
        public async Task<IHttpActionResult> DeleteCustomer(int customerId)
        {
            try
            {
                Customer found = await DbContext.Customers.FindAsync(customerId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.Customers.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("Customer.Delete: " + ex);
                throw;
            }
        }

        // GET: api/Customer/5
        [ResponseType(typeof(CustomerDTO))]
        public async Task<IHttpActionResult> GetCustomer(int customerId)
        {
            try
            {
                CustomerDTO found = await Task.Run(() => 
                    {
                        return CustomerTX.WriteToDTO(DbContext.Customers
                            .AsEnumerable().FirstOrDefault(e => e.CustomerId == customerId));
                    });
                if (found == null)
                {
                    return NotFound();
                }
                return Ok(found);
            }
            catch (Exception ex)
            {
                Log.Error("Customer.Get: " + ex);
                throw;
            }
        }

         // POST: api/Customers
        [ResponseType(typeof(CustomerDTO))]
        public async Task<IHttpActionResult> PostCustomer(CustomerDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.Customers.Add(CustomerTX.ReadFromDTO(new Customer(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { CustomerId = value.CustomerId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("Customer.Post: " + ex);
                throw;
            }
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomer(int customerId, CustomerDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (value.CustomerId != customerId)
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
                    if (!ValueExists(customerId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Log.Error("Customer.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int customerId)
        {
            return DbContext.Customers.Count(e => e.CustomerId == customerId) > 0;
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

