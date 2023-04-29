using System.ComponentModel.DataAnnotations;

namespace TaskManagmentApi.Data.Models
{
    public class Developer
    {
        [Key]
        public int Id { get; set; }
        //public int DeveloperPositionId { get; set; }
        public string Bio { get; set; }

        public User User { get; set; }

        public ICollection<TaskTable> Tasks { get; set; }

    }
}
