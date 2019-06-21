using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAcessLayer.Entities;
using DataAcessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestEmployeeApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public readonly IEmployeeRepository<Employee> _employeeRepo;
        public EmployeesController(IEmployeeRepository<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        // GET: api/Employees
        [HttpGet]
        public IActionResult Get()
        {
            var result = _employeeRepo.GetAll();
            if (result == null)
            {

                return NoContent();
                
            }
            else
            {
                return Ok(result);
            }
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            Employee employee = _employeeRepo.GetById(id);
            if(employee == null)
            {
                return BadRequest("Employee Id does not exist");
            }
            else
            {
                return Ok(employee);
            }
        }

        // POST: api/Employees
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            bool employees= _employeeRepo.Add(employee);
            if(employees)
            {
                return Created("Inserted",  employee);
            }
            else
            {
                return BadRequest("Cannot insert the record");
            }
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            bool employees = _employeeRepo.Update(id, employee);
            if(employees)
            {
                return Created("Updated", employee);
                
            }
            else
            {
                return BadRequest("Cannot Update the record");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool employees = _employeeRepo.Delete(id);
            if(employees)
            {
                return Created("Deleted", id);
            }
            else
            {
                return BadRequest("Cannot delete the record");
            }
        }
    }
}
