using System;
using System.Collections.Generic;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        Compensation Create(Compensation compensation);
        Compensation GetByID(String id);
    }
}
