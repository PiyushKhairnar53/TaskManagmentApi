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
                Description = entity.Description,
                Priority = entity.Priority,
                DeveloperFirstName = entity.Developer.User.FirstName,
                DeveloperLastName = entity.Developer.User.LastName,
                UserRole = entity.Manager.User.UserRole,
                EstimatedTime = entity.EstimatedTime,
                ManagerId = entity.ManagerId,
                CreatedAt = entity.UpdatedAt,
                DeveloperId = entity.DeveloperId,
                ActualTime = entity.ActualTime
            };
        }
    }
}
