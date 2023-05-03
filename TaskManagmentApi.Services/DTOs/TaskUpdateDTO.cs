using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentApi.Services.DTOs
{
    public class TaskUpdateDTO
    {
        public int TaskId { get; set; }
        public string UserId { get; set; }
        public int StatusId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public int ActualTime { get; set; }
    }
}
