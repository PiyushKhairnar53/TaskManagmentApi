using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentApi.Data.Models;

namespace TaskManagmentApi.Services.DTOs
{
    public class TaskDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public int EstimatedTime { get; set; }
        public string ManagerId { get; set; }
        public string DeveloperId { get; set; }
    }
}
