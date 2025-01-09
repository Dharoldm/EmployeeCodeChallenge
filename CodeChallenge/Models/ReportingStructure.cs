using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class ReportingStructure
    {
        public double numberofReports { get; set; } = 0;
        // I am assuming that the employee field also serves as the reporting structure since the json output of this will give you all of the direct reports
        public Employee employee { get; set; }

        // Requiring an employee to initialize the reporting structure because the class doesn't work without one
        public ReportingStructure(Employee employee) 
        {
            this.employee = employee;
        }
    }
}
