using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;
namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly ILogger _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger)
        {
            _logger = logger;
        }

        public ReportingStructure GetReportingStructure(Employee employee)
        {

            var reportingStructure = new ReportingStructure(employee);
            reportingStructure.numberofReports = CountReports(employee);
            return reportingStructure;
        }

        private int CountReports(Employee employee)
        {
            //Starting count of 0
            var count = 0;
            // If an employee doesn't have direct reports or the direct reports then this will default to returning zero
            if (employee.DirectReports != null && employee.DirectReports.Count != 0)
            {
                // If the employee does have direct reports, add those to the count and then recursively count the employees under those direct reports
                count += employee.DirectReports.Count;
                foreach (var report in employee.DirectReports) { count += CountReports(report); }
            }
            return count;
        }
    }
}
