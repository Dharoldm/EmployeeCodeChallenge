using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [Route("api/compensation")]
    [ApiController]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;
        // Adding employee service to get access to Employee repository
        private readonly IEmployeeService _employeeService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService, IEmployeeService employeeService)
        {
            _logger = logger;
            _compensationService = compensationService;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Recieved request to create compensation for employee '{compensation.employee}' for amount '{compensation.salary}' on {compensation.effectiveDate}");
            var employee = _employeeService.GetById(compensation.employee);
            // If an employee is not found then employeeService returns null
            if (employee == null)
            {
                return NotFound();
            }
            _compensationService.Create(compensation);
            return CreatedAtRoute("getCompensationById", new { id = compensation.employee }, compensation);
        }

        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensation(String id) 
        {
            _logger.LogDebug($"Received employee get request for '{id}'");
            var compensation = _compensationService.GetByID(id);
            if (compensation == null)
            {
                return NotFound();
            }
            return Ok(compensation);
        }
    }
}
