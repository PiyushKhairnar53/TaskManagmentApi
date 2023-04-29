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
        public int Id { get; set; }

        public string Bio { get; set; } = null!;

        [Required]
        public string UserId { get; set; }

    }
}
