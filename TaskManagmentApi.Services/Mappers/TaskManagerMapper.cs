using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Services.DTOs;

namespace TaskManagmentApi.Services.Mappers
{
    public class TaskManagerMapper
    {
        public TaskManagerDTO Map(TaskTable entity)
        {
            return new TaskManagerDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Priority = entity.Priority,
                DeveloperFirstName = entity.Developer.User.FirstName,
                DeveloperLastName = entity.Developer.User.LastName,
                StatusName = entity.Status.StatusName,
                ActualTime = entity.ActualTime,
                EstimatedTime = entity.EstimatedTime
            };
        }
    }
}
