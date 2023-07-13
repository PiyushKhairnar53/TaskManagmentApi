using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentApi.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string CommentName { get; set; }

        public DateTime CreatedAt { get; set; }
        public User CreatedByUser { get; set; }

        public int TaskId { get; set; }
        public TaskTable Task { get; set; }

    }
}
