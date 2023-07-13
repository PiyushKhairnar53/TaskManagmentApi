using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentApi.Services.DTOs
{
    public class TaskByStatusDeveloperDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public int EstimatedTime { get; set; }
        public string ManagerId { get; set; }
        public int ActualTime { get; set; }
        public string DeveloperId { get; set; }
        public string UserRole { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
