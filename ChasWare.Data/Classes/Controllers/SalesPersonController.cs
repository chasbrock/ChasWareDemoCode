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
    public class SalesPersonsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public SalesPersonsController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/SalesPerson/5
        [ResponseType(typeof(SalesPersonDTO))]
        public async Task<IHttpActionResult> DeleteSalesPerson(int entityId)
        {
            try
            {
                SalesPerson found = await DbContext.SalesPersons.FindAsync(entityId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.SalesPersons.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("SalesPerson.Delete: " + ex);
                throw;
            }
        }

        // GET: api/SalesPerson/5
        [ResponseType(typeof(SalesPersonDTO))]
        public async Task<IHttpActionResult> GetSalesPerson(int entityId)
        {
            try
            {
                SalesPersonDTO found = await Task.Run(() => 
                    {
                        return SalesPersonTX.WriteToDTO(DbContext.SalesPersons
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
                Log.Error("SalesPerson.Get: " + ex);
                throw;
            }
        }

         // POST: api/SalesPersons
        [ResponseType(typeof(SalesPersonDTO))]
        public async Task<IHttpActionResult> PostSalesPerson(SalesPersonDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.SalesPersons.Add(SalesPersonTX.ReadFromDTO(new SalesPerson(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { EntityId = value.EntityId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("SalesPerson.Post: " + ex);
                throw;
            }
        }

        // PUT: api/SalesPersons/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSalesPerson(int entityId, SalesPersonDTO value)
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
                Log.Error("SalesPerson.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int entityId)
        {
            return DbContext.SalesPersons.Count(e => e.EntityId == entityId) > 0;
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

