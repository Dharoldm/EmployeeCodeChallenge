using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace CodeChallenge.Models
{
    public class Compensation
    {
        // necessary primary key for adding the compensation to a database for persistence
        public String CompensationId { get; set; } 
        //Using the ID of the employee since those will always be unique. A user can further query the employee endpoints with that ID for more information
        public String employee { get; set; }
        // effectiveeDate written as mm/dd/yyyy for testing. Using String instead of DateTime to remain consistent with the other classes
        public String effectiveDate { get; set; }
        public double salary { get; set; }
    }
}
