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
    [ApiController]
    [Route("api/reportingStructure")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;
        // Adding employee service to get access to Employee repository
        private readonly IEmployeeService _employeeService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService, IEmployeeService employeeService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
            _employeeService = employeeService;
        }

        [HttpGet("{employeeID}")]
        public IActionResult GetReportingStructure(String employeeID)
        {
            _logger.LogDebug($"Received reporting structure request for'{employeeID}'");
            //Calling the employee service in order to get an employee from the ID
            var employee = _employeeService.GetById(employeeID);
            // If an employee is not found then employeeService returns null
            if(employee == null)
            {
                return NotFound();
            }

            //Calling the reporting structure service to create and return the structure
            var reportingStructure = _reportingStructureService.GetReportingStructure(employee);
            return Ok(reportingStructure);
        }

       

    }
}
