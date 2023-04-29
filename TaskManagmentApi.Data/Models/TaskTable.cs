using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TaskManagmentApi.Data.Models
{
    public class TaskTable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Priority { get; set; }


        [Required]
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

        [Required]
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }

        [Required]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        public int EstimatedTime { get; set; }
        public int ActualTime { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public int CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }


    }
}
