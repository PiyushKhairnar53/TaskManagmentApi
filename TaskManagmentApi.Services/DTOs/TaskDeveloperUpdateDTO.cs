using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentApi.Services.DTOs
{
    public class TaskDeveloperUpdateDTO
    {
        public int TaskId { get; set; }
        public string ManagerId { get; set; }
        public string DeveloperId { get; set; }
    }
}
