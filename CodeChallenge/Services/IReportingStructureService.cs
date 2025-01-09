using System;
using System.Collections.Generic;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public interface IReportingStructureService
    {
        ReportingStructure GetReportingStructure(Employee employee);
    }
}
