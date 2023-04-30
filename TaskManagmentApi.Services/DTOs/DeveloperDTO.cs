﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentApi.Services.DTOs
{
    public class DeveloperDTO
    {
        public string Id { get; set; }
        public string? Bio { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}