using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Entities
{
    public class User
    {
        public Guid Id {get; set;} = Guid.NewGuid();
        public required string FirstName {get; set;} 
        public required string LastName {get; set;}
        public required string Email {get; set;}
        public required string Password {get; set;}
    }
}