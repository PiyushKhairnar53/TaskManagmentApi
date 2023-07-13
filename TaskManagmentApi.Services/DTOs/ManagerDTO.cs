using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentApi.Data.Models;

namespace TaskManagmentApi.Services.DTOs
{
    public class ManagerDTO
    {
        public string Id { get; set; }
        public string? Bio { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

    }
}
