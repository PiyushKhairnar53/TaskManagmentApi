using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentApi.Services.DTOs
{
    public class TaskManagerDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public string StatusName { get; set; }
        public string DeveloperFirstName { get; set; }
        public string DeveloperLastName { get; set; }
        public int EstimatedTime { get; set; }
        public int ActualTime { get; set; }
    }
}
