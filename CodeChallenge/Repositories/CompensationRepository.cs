using CodeChallenge.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;
using System;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly ILogger<ICompensationRepository> _logger;
        private readonly CompensationContext _compensationContext;
        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
            _logger = logger;
        }
        public Compensation Add(Compensation compensation)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();
            _compensationContext.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation GetById(string id)
        {
            //return the compensation for the employee that has an employeeId that is equal to what is queried
            return _compensationContext.Compensations.SingleOrDefault(e => e.employee == id);
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}
