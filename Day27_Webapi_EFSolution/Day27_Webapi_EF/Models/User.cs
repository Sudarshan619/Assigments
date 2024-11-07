﻿using System.ComponentModel.DataAnnotations;

namespace Day27_Webapi_EF.Models
{
   
        public enum Roles
        {
            Admin,
            Supplier,
            Customer,
            Employee
        }
        public class User
        {
            [Key]
            public string Username { get; set; } = string.Empty;
            public byte[] Password { get; set; }
            public byte[] HashKey { get; set; }
            public Roles Role { get; set; }
            public Customer Customer { get; set; }

        }
    
}
