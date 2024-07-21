using Asp.Versioning;
using hotel_river.src.core.application.services;
using hotel_river.src.infrastructure.database.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace hotel_river.src.infrastructure.api.controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmployeesController : Controller
    {        

        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GetEmployee
        /// <summary>
        /// Get the employee by the employee
        /// </summary>        
        /// <returns>The list of employees</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("getall")]
        public async Task<IActionResult> GetEmployee()
        {
            var employees = await _context.Employees
                .Include(c => c.Address)
                .ToListAsync();

            string jsonString = JsonSerializer.Serialize(employees);

            return Ok(jsonString);
        }

        // GET BY ID: GetEmployeeById        
        /// <summary>
        /// Get employee by Id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>The employee entity</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            string jsonString = JsonSerializer.Serialize(employee);

            return Ok(jsonString);
        }

        // POST: SetEmployee              
        /// <summary>
        /// Create employee
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <returns>Created()</returns>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetEmployee(
            [Bind("HotelId,Id,Name,LastName,Birth,Gender,CPF,Email,Phone")]
            Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Id = Guid.NewGuid();
                _context.Add(employee);
                await _context.SaveChangesAsync();

                return Created();
            }

            return NoContent();
        }

        // PATCH: EditEmployee
        /// <summary>
        /// Edit employee
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <param name="employee">Employee object</param>
        /// <returns>The employee entity</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPatch("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(Guid id,
            [Bind("HotelId,Id,Name,LastName,Birth,Gender,CPF,Email,Phone")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok(employee);
            }
            return NoContent();
        }

        // DELETE: Delete Employee        
        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="id">Employee id</param>        
        /// <returns>Ok()</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            Console.WriteLine(employee);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}

