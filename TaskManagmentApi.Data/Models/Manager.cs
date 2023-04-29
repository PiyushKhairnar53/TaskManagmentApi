using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TaskManagmentApi.Data.Models
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }
        //public int ManagerPositionId { get; set; }
        public string Bio { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<TaskTable> Tasks { get; set; }

    }
}
