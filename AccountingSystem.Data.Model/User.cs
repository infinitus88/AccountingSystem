using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AccountingSystem.Data.Model
{
    public class User
    {
        public User()
        {
            Roles = new List<UserRole>();
            CreationTime = DateTime.Now;
        }

        public int Id { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime DeletionTime { get; set; }

        public bool IsDeleted { get; set; }

        public virtual IList<UserRole> Roles { get; set; }
    }

}