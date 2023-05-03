using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Services.DTOs;

namespace TaskManagmentApi.Services.Mappers
{
    public class TasksByStatusMapper
    {
        public TaskByStatusDTO Map(TaskTable entity)
        {
            return new TaskByStatusDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Priority = entity.Priority,
                DeveloperFirstName = entity.Developer.User.FirstName,
                DeveloperLastName = entity.Developer.User.LastName,
                EstimatedTime = entity.EstimatedTime,
                ManagerId = entity.ManagerId,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
