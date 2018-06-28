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
    public class EmployeesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private DataContext DbContext { get; }

        public EmployeesController()
        {
            DbContext = new DataContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
        }

        // DELETE: api/Employee/5
        [ResponseType(typeof(EmployeeDTO))]
        public async Task<IHttpActionResult> DeleteEmployee(int personId)
        {
            try
            {
                Employee found = await DbContext.Employees.FindAsync(personId);
                if (found == null)
                {
                    return NotFound();
                }
                DbContext.Employees.Remove(found);
                await DbContext.SaveChangesAsync();
                return Ok(found);
            }
            catch(Exception ex)
            {
                Log.Error("Employee.Delete: " + ex);
                throw;
            }
        }

        // GET: api/Employee/5
        [ResponseType(typeof(EmployeeDTO))]
        public async Task<IHttpActionResult> GetEmployee(int personId)
        {
            try
            {
                EmployeeDTO found = await Task.Run(() => 
                    {
                        return EmployeeTX.WriteToDTO(DbContext.Employees
                            .AsEnumerable().FirstOrDefault(e => e.PersonId == personId));
                    });
                if (found == null)
                {
                    return NotFound();
                }
                return Ok(found);
            }
            catch (Exception ex)
            {
                Log.Error("Employee.Get: " + ex);
                throw;
            }
        }

         // POST: api/Employees
        [ResponseType(typeof(EmployeeDTO))]
        public async Task<IHttpActionResult> PostEmployee(EmployeeDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                DbContext.Employees.Add(EmployeeTX.ReadFromDTO(new Employee(), value));
                await DbContext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { PersonId = value.PersonId }, value);
            }
            catch (Exception ex)
            {
                Log.Error("Employee.Post: " + ex);
                throw;
            }
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployee(int personId, EmployeeDTO value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (value.PersonId != personId)
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
                    if (!ValueExists(personId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Log.Error("Employee.Put: " + ex);
                throw;
            }
        }

        private bool ValueExists(int personId)
        {
            return DbContext.Employees.Count(e => e.PersonId == personId) > 0;
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

