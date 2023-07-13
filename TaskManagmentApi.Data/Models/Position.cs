using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentApi.Data.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public string PositionName { get; set; }
        public ICollection<Developer> Developers { get; set; }
    }
}
