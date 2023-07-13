using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentApi.Services.DTOs
{
    public class TaskStatusUpdateDTO
    {
        public int TaskId { get; set; }
        public string UserId { get; set; }
        public int StatusId { get; set; }
    }
}
