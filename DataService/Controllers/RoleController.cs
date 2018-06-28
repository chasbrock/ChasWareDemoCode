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
    public class RolesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public RolesController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/Role/5
        [ResponseType(typeof(RoleDTO))]
        public async Task<IHttpActionResult> DeleteRole(int roleId)
        {
            try
            {
                Role found = await DbContext.Roles.FindAsync(roleId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.Roles.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("Role.Delete: " + ex);
                throw;
            }
        }

        // GET: api/Role/5
        [ResponseType(typeof(RoleDTO))]
        public async Task<IHttpActionResult> GetRole(int roleId)
        {
            try
            {
                RoleDTO found = await Task.Run(() => 
                    {
                        return RoleTX.WriteToDTO(DbContext.Roles
                            .AsEnumerable().FirstOrDefault(e => e.RoleId == roleId));
                    });
                if (found == null)
                {
                    return NotFound();
                }
                return Ok(found);
            }
            catch (Exception ex)
            {
                Log.Error("Role.Get: " + ex);
                throw;
            }
        }

         // POST: api/Roles
        [ResponseType(typeof(RoleDTO))]
        public async Task<IHttpActionResult> PostRole(RoleDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.Roles.Add(RoleTX.ReadFromDTO(new Role(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { RoleId = value.RoleId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("Role.Post: " + ex);
                throw;
            }
        }

        // PUT: api/Roles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRole(int roleId, RoleDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (value.RoleId != roleId)
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
                    if (!ValueExists(roleId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Log.Error("Role.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int roleId)
        {
            return DbContext.Roles.Count(e => e.RoleId == roleId) > 0;
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

