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
    public class DepartmentsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public DepartmentsController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/Department/5
        [ResponseType(typeof(DepartmentDTO))]
        public async Task<IHttpActionResult> DeleteDepartment(short departmentId)
        {
            try
            {
                Department found = await DbContext.Departments.FindAsync(departmentId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.Departments.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("Department.Delete: " + ex);
                throw;
            }
        }

        // GET: api/Department/5
        [ResponseType(typeof(DepartmentDTO))]
        public async Task<IHttpActionResult> GetDepartment(short departmentId)
        {
            try
            {
                DepartmentDTO found = await Task.Run(() => 
                    {
                        return DepartmentTX.WriteToDTO(DbContext.Departments
                            .AsEnumerable().FirstOrDefault(e => e.DepartmentId == departmentId));
                    });
                if (found == null)
                {
                    return NotFound();
                }
                return Ok(found);
            }
            catch (Exception ex)
            {
                Log.Error("Department.Get: " + ex);
                throw;
            }
        }

         // POST: api/Departments
        [ResponseType(typeof(DepartmentDTO))]
        public async Task<IHttpActionResult> PostDepartment(DepartmentDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.Departments.Add(DepartmentTX.ReadFromDTO(new Department(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { DepartmentId = value.DepartmentId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("Department.Post: " + ex);
                throw;
            }
        }

        // PUT: api/Departments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDepartment(short departmentId, DepartmentDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (value.DepartmentId != departmentId)
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
                    if (!ValueExists(departmentId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Log.Error("Department.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(short departmentId)
        {
            return DbContext.Departments.Count(e => e.DepartmentId == departmentId) > 0;
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

