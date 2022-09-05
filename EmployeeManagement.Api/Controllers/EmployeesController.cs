using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Api.Models;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class EmployeesController : ControllerBase 
    {

        private readonly IEmployeeRepository employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {

            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await employeeRepository.GetEmployees());
            }
           catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retreiving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await employeeRepository.GetEmployee(id);

                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retreiving data from the database");
            }

        }

        [HttpGet("{search}/{name}/{gender?}")]
        public async Task<ActionResult<Employee>> Search(string name, Gender? gender)
        {
            try
            {
            var result =   await employeeRepository.Search(name, gender);

                if(result.Any()) 
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)   
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retreiving data from the database");
            }
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<Employee>>> CreateEmploye(Employee employee)
        {

            try
            {
                if (employee==null)

                {
                    return BadRequest();
                }

                var emp = await employeeRepository.GetEmployeeByEmail(employee.Email);

                if (emp != null)
                {
                    ModelState.AddModelError("email", "Email already in use");
                    return BadRequest(ModelState);

                }

                var CreatedEmpoyee = await employeeRepository.AddEmployee(employee);

                return CreatedAtAction(nameof(GetEmployee), new {id = CreatedEmpoyee.EmployeeId},CreatedEmpoyee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   "Error retreiving data from the database");
            }

   
        }

       [HttpPut]
       public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            try
            {

                //if (id !=employee.EmployeeId)
                //{
                //    return BadRequest("EmployeeID mismatch");
                //}

                var EmployeeToUppdate = await employeeRepository.GetEmployee(employee.EmployeeId);

                if (EmployeeToUppdate == null)
                {
                    return NotFound($"Employee with {employee.EmployeeId} not found");
                }

                return await employeeRepository.UpdateEmployee(employee);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
         
            try
            {
             var EmployeeToDelete =  await employeeRepository.GetEmployee(id);

                if (EmployeeToDelete == null)
                {
                    return NotFound($"Employee with ID = {id} not found");
                }

              return await  employeeRepository.DeleteEmployee(id);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                             "Error deleting data");
            }

        }


    }
}
