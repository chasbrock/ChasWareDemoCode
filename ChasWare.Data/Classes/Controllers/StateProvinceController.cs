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
    public class StateProvincesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public StateProvincesController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/StateProvince/5
        [ResponseType(typeof(StateProvinceDTO))]
        public async Task<IHttpActionResult> DeleteStateProvince(int stateProvinceId)
        {
            try
            {
                StateProvince found = await DbContext.StateProvinces.FindAsync(stateProvinceId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.StateProvinces.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("StateProvince.Delete: " + ex);
                throw;
            }
        }

        // GET: api/StateProvince/5
        [ResponseType(typeof(StateProvinceDTO))]
        public async Task<IHttpActionResult> GetStateProvince(int stateProvinceId)
        {
            try
            {
                StateProvinceDTO found = await Task.Run(() => 
                    {
                        return StateProvinceTX.WriteToDTO(DbContext.StateProvinces
                            .AsEnumerable().FirstOrDefault(e => e.StateProvinceId == stateProvinceId));
                    });
                if (found == null)
                {
                    return NotFound();
                }
                return Ok(found);
            }
            catch (Exception ex)
            {
                Log.Error("StateProvince.Get: " + ex);
                throw;
            }
        }

         // POST: api/StateProvinces
        [ResponseType(typeof(StateProvinceDTO))]
        public async Task<IHttpActionResult> PostStateProvince(StateProvinceDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.StateProvinces.Add(StateProvinceTX.ReadFromDTO(new StateProvince(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { StateProvinceId = value.StateProvinceId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("StateProvince.Post: " + ex);
                throw;
            }
        }

        // PUT: api/StateProvinces/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStateProvince(int stateProvinceId, StateProvinceDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (value.StateProvinceId != stateProvinceId)
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
                    if (!ValueExists(stateProvinceId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Log.Error("StateProvince.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int stateProvinceId)
        {
            return DbContext.StateProvinces.Count(e => e.StateProvinceId == stateProvinceId) > 0;
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

