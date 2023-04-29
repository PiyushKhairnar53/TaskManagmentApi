using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentApi.Data.Models
{
    public class Developer
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }
        //public int ManagerPositionId { get; set; }
        public string? Bio { get; set; }
        public User User { get; set; }
        public ICollection<TaskTable> Tasks { get; set; }
        public int IsActive { get; set; }

    }
}
