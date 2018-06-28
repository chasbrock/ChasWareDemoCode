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
    public class SalesTerritoriesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public SalesTerritoriesController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/SalesTerritory/5
        [ResponseType(typeof(SalesTerritoryDTO))]
        public async Task<IHttpActionResult> DeleteSalesTerritory(int territoryId)
        {
            try
            {
                SalesTerritory found = await DbContext.SalesTerritories.FindAsync(territoryId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.SalesTerritories.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("SalesTerritory.Delete: " + ex);
                throw;
            }
        }

        // GET: api/SalesTerritory/5
        [ResponseType(typeof(SalesTerritoryDTO))]
        public async Task<IHttpActionResult> GetSalesTerritory(int territoryId)
        {
            try
            {
                SalesTerritoryDTO found = await Task.Run(() => 
                    {
                        return SalesTerritoryTX.WriteToDTO(DbContext.SalesTerritories
                            .AsEnumerable().FirstOrDefault(e => e.TerritoryId == territoryId));
                    });
                if (found == null)
                {
                    return NotFound();
                }
                return Ok(found);
            }
            catch (Exception ex)
            {
                Log.Error("SalesTerritory.Get: " + ex);
                throw;
            }
        }

         // POST: api/SalesTerritories
        [ResponseType(typeof(SalesTerritoryDTO))]
        public async Task<IHttpActionResult> PostSalesTerritory(SalesTerritoryDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.SalesTerritories.Add(SalesTerritoryTX.ReadFromDTO(new SalesTerritory(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { TerritoryId = value.TerritoryId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("SalesTerritory.Post: " + ex);
                throw;
            }
        }

        // PUT: api/SalesTerritories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSalesTerritory(int territoryId, SalesTerritoryDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (value.TerritoryId != territoryId)
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
                    if (!ValueExists(territoryId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Log.Error("SalesTerritory.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int territoryId)
        {
            return DbContext.SalesTerritories.Count(e => e.TerritoryId == territoryId) > 0;
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

