using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentApi.Data.DBContext;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Services.DTOs;
using TaskManagmentApi.Services.Mappers;

namespace TaskManagmentApi.Services.Services
{
    public interface ITaskService
    {
        TaskTable AddTask(TaskDTO task);
        IEnumerable<TaskTable> GetAllTask();
        IEnumerable<TaskManagerDTO> GetTasksByManager(string managerId);
        TaskTable UpdateTaskStatus(TaskStatusUpdateDTO task);
        TaskTable UpdateDeveloperOnTask(TaskDeveloperUpdateDTO task);
        TaskTable UpdateTask(TaskUpdateDTO task);
        IEnumerable<TaskByStatusDTO> GetTasksByStatus(StatusManagerDTO statusManager);
    }
    public class TaskService : ITaskService
    {
        private readonly TaskDBContext _taskDBContext;
        public TaskService(TaskDBContext taskDBContext)
        {
            _taskDBContext = taskDBContext;
        }

        public IEnumerable<TaskTable> GetAllTask()
        {
            var allTasks = _taskDBContext.Tasks.Include(c => c.Manager).Include(c => c.Developer).Include(c => c.Status).ToList();
            return allTasks;
        }

        public TaskTable AddTask(TaskDTO task)
        {
            try
            {
                if (!string.IsNullOrEmpty(task.Title) && !string.IsNullOrEmpty(task.Description) && !string.IsNullOrEmpty(task.Priority) && !string.IsNullOrEmpty(task.DeveloperId))
                {
                    bool isManager = _taskDBContext.Tasks.Include(u => u.Manager).Include(u => u.Manager.User).Any(m => m.Manager.Id == task.ManagerId && m.Manager.User.UserRole == "Manager");

                    if (isManager)
                    {
                        var newTask = new TaskTable
                        {
                            Id = 0,
                            Title = task.Title,
                            Description = task.Description,
                            Priority = task.Priority,
                            ManagerId = task.ManagerId,
                            DeveloperId = task.DeveloperId,
                            StatusId = 1,
                            EstimatedTime = task.EstimatedTime,
                            ActualTime = 0,
                            CreatedAt = DateTime.Now,
                            CreatedByManagerId = task.ManagerId,
                            UpdatedAt = DateTime.Now,
                            IsActive = 1
                        };
                        _taskDBContext.Tasks.Add(newTask);
                        _taskDBContext.SaveChanges();

                        return newTask;
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<TaskManagerDTO> GetTasksByManager(string managerId)
        {

            var mattersByClient = _taskDBContext.Tasks
                    .Include(m => m.Manager)
                    .Include(m => m.Developer)
                    .Include(m => m.Manager.User)
                    .Include(m => m.Developer.User)
                    .Include(m => m.Status)
                    .Where(c => c.ManagerId.Equals(managerId));
            return mattersByClient.Select(c => new TaskManagerMapper().Map(c)).ToList();

        }

        public TaskTable UpdateTaskStatus(TaskStatusUpdateDTO task)
        {
            var findTask = _taskDBContext.Tasks.FirstOrDefault(d => d.Id.Equals(task.TaskId));
            if (findTask != null)
            {
                if (findTask.ManagerId.Equals(task.UserId) || findTask.DeveloperId.Equals(task.UserId))
                {
                    if (task.StatusId >= 1 && task.StatusId <= 5)
                    {
                        findTask.StatusId = task.StatusId;
                        findTask.UpdatedAt = DateTime.Now;
                        _taskDBContext.SaveChanges();
                        return findTask;
                    }
                }
                return null;
            }
            return null;
        }

        public TaskTable UpdateDeveloperOnTask(TaskDeveloperUpdateDTO task)
        {
            var findTask = _taskDBContext.Tasks.FirstOrDefault(d => d.Id.Equals(task.TaskId));
            if (findTask != null)
            {
                if (findTask.ManagerId.Equals(task.ManagerId))
                {
                    if (!string.IsNullOrEmpty(task.DeveloperId))
                    {
                        findTask.DeveloperId = task.DeveloperId;
                        findTask.UpdatedAt = DateTime.Now;
                        _taskDBContext.SaveChanges();
                        return findTask;
                    }
                }
                return null;
            }
            return null;
        }

        public TaskTable UpdateTask(TaskUpdateDTO task)
        {
            var findTask = _taskDBContext.Tasks.FirstOrDefault(d => d.Id.Equals(task.TaskId));
            if (findTask != null)
            {
                if (findTask.ManagerId.Equals(task.UserId) || findTask.DeveloperId.Equals(task.UserId))
                {
                    if (string.IsNullOrEmpty(task.Title))
                    {
                        task.Title = findTask.Title;
                    }
                    if (string.IsNullOrEmpty(task.Description))
                    {
                        task.Description = findTask.Description;
                    }
                    if (string.IsNullOrEmpty(task.Priority))
                    {
                        task.Priority = findTask.Priority;
                    }
                    if (task.StatusId==0 || task.StatusId == null)
                    {
                        task.StatusId = findTask.StatusId;
                    }
                    if (task.ActualTime>=1 && task.ActualTime<=1000)
                    {
                        task.ActualTime = findTask.ActualTime;
                    }

                    findTask.Title = task.Title;
                    findTask.Description = task.Description;
                    findTask.Priority = task.Priority;
                    findTask.StatusId = task.StatusId;
                    findTask.ActualTime = task.ActualTime;
                    findTask.UpdatedAt = DateTime.Now;
                    _taskDBContext.SaveChanges();
                    return findTask;

                }
                return null;
            }
            return null;
        }

        public IEnumerable<TaskByStatusDTO> GetTasksByStatus(StatusManagerDTO statusManager) 
        {
            var tasksByStatus = _taskDBContext.Tasks
                   .Include(m => m.Manager)
                   .Include(m => m.Developer)
                   .Include(m => m.Developer.User)
                   .Where(c => c.ManagerId.Equals(statusManager.ManagerId) && c.StatusId.Equals(statusManager.StatusId));

            return tasksByStatus.Select(c => new TasksByStatusMapper().Map(c)).ToList();
        }
    }
}
