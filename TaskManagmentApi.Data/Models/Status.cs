using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TaskManagmentApi.Data.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string StatusName { get; set; }
        public ICollection<TaskTable> Tasks { get; set; }

    }
}
