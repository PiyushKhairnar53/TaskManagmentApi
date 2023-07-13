using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentApi.Services.DTOs
{
    public class TaskManagerDeveloperDTO
    {
        public string ManagerId { get; set; }
        public string DeveloperId { get; set; }
        public int StatusId { get; set; }
    }
}
