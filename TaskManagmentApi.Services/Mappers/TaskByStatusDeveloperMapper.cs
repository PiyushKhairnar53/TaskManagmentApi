using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Services.DTOs;

namespace TaskManagmentApi.Services.Mappers
{
    public class TaskByStatusDeveloperMapper
    {
        public TaskByStatusDeveloperDTO Map(TaskTable entity)
        {
            return new TaskByStatusDeveloperDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Priority = entity.Priority,
                ManagerFirstName = entity.Manager.User.FirstName,
                ManagerLastName = entity.Manager.User.LastName,
                EstimatedTime = entity.EstimatedTime,
                ManagerId = entity.ManagerId,
                UpdatedAt = entity.UpdatedAt,
                ActualTime = entity.ActualTime
            };
        }
    }
}
