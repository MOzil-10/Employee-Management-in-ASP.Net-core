using EmployeeManagement.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeController(EmployeeDbContext employeeDbContext) { 
        
            _employeeDbContext = employeeDbContext;

        }

        private bool EmployeeExists(int id) { 
        
            return _employeeDbContext.Employees.Any(e => e.EmployeeId == id);
        
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            return await _employeeDbContext.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            
            if( employee == null )
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            _employeeDbContext.Employees.Add(employee);
            await _employeeDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateEmployee), employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee (int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest(id);
            }

            _employeeDbContext.Entry(employee).State = EntityState.Modified;

            try
            {
                await _employeeDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound(id);
            }

            _employeeDbContext.Employees.Remove(employee);
            await _employeeDbContext.SaveChangesAsync();
            return NoContent();
        }


    }
}
