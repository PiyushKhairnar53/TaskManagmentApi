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
        IEnumerable<TaskManagerDTO> GetTasksByManager(string managerId);
        TaskTable UpdateTaskStatus(TaskStatusUpdateDTO task);
        TaskTable UpdateDeveloperOnTask(TaskDeveloperUpdateDTO task);
        TaskTable UpdateTask(TaskUpdateDTO task);
        IEnumerable<TaskByStatusDTO> GetTasksByStatus(StatusUserDTO statusManager);
        IEnumerable<TaskByStatusDeveloperDTO> GetTasksByStatusDeveloper(StatusUserDTO statusUser);
        TaskTable UpdateTaskDeveloper(TaskUpdateDeveloperDTO task);
        IEnumerable<TaskByStatusDTO> GetTasksByStatusForDeveloper(TaskManagerDeveloperDTO taskManagerDeveloper);
        IEnumerable<TaskByStatusDeveloperDTO> GetTasksByStatusDeveloperForManager(TaskManagerDeveloperDTO taskManagerDeveloper);
    }
    public class TaskService : ITaskService
    {
        private readonly TaskDBContext _taskDBContext;
        public TaskService(TaskDBContext taskDBContext)
        {
            _taskDBContext = taskDBContext;
        }

        public TaskTable AddTask(TaskDTO task)
        {
            if (!string.IsNullOrEmpty(task.Title) && !string.IsNullOrEmpty(task.Description) && !string.IsNullOrEmpty(task.Priority) && !string.IsNullOrEmpty(task.DeveloperId))
            {
                bool isManager = _taskDBContext.Users.Any(m => m.Id == task.ManagerId && m.UserRole == "Manager");

                if (isManager)
                {
                    TaskTable newTask = new TaskTable
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

        public IEnumerable<TaskManagerDTO> GetTasksByManager(string managerId)
        {
            if (!string.IsNullOrEmpty(managerId))
            {
                IEnumerable<TaskTable> mattersByClient = _taskDBContext.Tasks
                        .Include(m => m.Manager.User)
                        .Include(m => m.Developer.User)
                        .Include(m => m.Status)
                        .Where(c => c.ManagerId.Equals(managerId));
                return mattersByClient.Select(c => new TaskManagerMapper().Map(c)).ToList();
            }
            else
            {
                return null;
            }
        }

        public TaskTable UpdateTaskStatus(TaskStatusUpdateDTO task)
        {
            TaskTable findTask = _taskDBContext.Tasks.FirstOrDefault(d => d.Id.Equals(task.TaskId));
            if (findTask != null)
            {
                if (findTask.ManagerId.Equals(task.UserId) || findTask.DeveloperId.Equals(task.UserId))
                {
                    findTask.StatusId = task.StatusId;
                    findTask.UpdatedAt = DateTime.Now;
                    _taskDBContext.SaveChanges();
                    return findTask;
                }
                return null;
            }
            return null;
        }

        public TaskTable UpdateDeveloperOnTask(TaskDeveloperUpdateDTO task)
        {
            TaskTable findTask = _taskDBContext.Tasks.FirstOrDefault(d => d.Id.Equals(task.TaskId));
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

        public TaskTable UpdateTaskDeveloper(TaskUpdateDeveloperDTO task)
        {
            TaskTable findTask = _taskDBContext.Tasks.FirstOrDefault(d => d.Id.Equals(task.TaskId));
            if (findTask != null)
            {
                if (findTask.DeveloperId.Equals(task.UserId))
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
                    if (task.ActualTime <= 0)
                    {
                        task.ActualTime = findTask.ActualTime;
                    }


                    findTask.Title = task.Title;
                    findTask.Description = task.Description;
                    findTask.Priority = task.Priority;
                    findTask.ActualTime = task.ActualTime;
                    findTask.UpdatedAt = DateTime.Now;
                    _taskDBContext.SaveChanges();
                    return findTask;
                }
            }
            return null;
        }

        public TaskTable UpdateTask(TaskUpdateDTO task)
        {
            TaskTable findTask = _taskDBContext.Tasks.FirstOrDefault(d => d.Id.Equals(task.TaskId));
            if (findTask != null)
            {
                if (findTask.ManagerId.Equals(task.UserId))
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
                    if (task.ActualTime <= 0)
                    {
                        task.ActualTime = findTask.ActualTime;
                    }
                    if (string.IsNullOrEmpty(task.DeveloperId))
                    {
                        task.DeveloperId = findTask.DeveloperId;
                    }

                    findTask.Title = task.Title;
                    findTask.Description = task.Description;
                    findTask.Priority = task.Priority;
                    findTask.EstimatedTime = task.EstimatedTime;
                    findTask.DeveloperId = task.DeveloperId;
                    findTask.ActualTime = task.ActualTime;
                    findTask.UpdatedAt = DateTime.Now;
                    _taskDBContext.SaveChanges();
                    return findTask;

                }
                return null;
            }
            return null;
        }

        public IEnumerable<TaskByStatusDTO> GetTasksByStatus(StatusUserDTO statusUser)
        {
            IEnumerable<TaskTable> tasksByStatus = _taskDBContext.Tasks
                   .Include(m => m.Manager)
                   .Include(m => m.Manager.User)
                   .Include(m => m.Developer)
                   .Include(m => m.Developer.User)
                   .Where(c => c.ManagerId.Equals(statusUser.UserId) && c.StatusId.Equals(statusUser.StatusId));

            return tasksByStatus.Select(c => new TasksByStatusMapper().Map(c)).ToList();
        }

        public IEnumerable<TaskByStatusDeveloperDTO> GetTasksByStatusDeveloper(StatusUserDTO statusUser)
        {
            IEnumerable<TaskTable> tasksByStatus = _taskDBContext.Tasks
                   .Include(m => m.Developer)
                   .Include(m => m.Developer.User)
                   .Include(m => m.Manager)
                   .Include(m => m.Manager.User)
                   .Where(c => c.DeveloperId.Equals(statusUser.UserId) && c.StatusId.Equals(statusUser.StatusId));

            return tasksByStatus.Select(c => new TaskByStatusDeveloperMapper().Map(c)).ToList();
        }

        public IEnumerable<TaskByStatusDTO> GetTasksByStatusForDeveloper(TaskManagerDeveloperDTO taskManagerDeveloper)
        {
            IEnumerable<TaskTable> mattersByClient = _taskDBContext.Tasks
                    .Include(m => m.Manager.User)
                    .Include(m => m.Developer.User)
                    .Include(m => m.Status)
                    .Where(c => c.ManagerId.Equals(taskManagerDeveloper.ManagerId)
                                    && c.DeveloperId.Equals(taskManagerDeveloper.DeveloperId)
                                    && c.StatusId.Equals(taskManagerDeveloper.StatusId));

            return mattersByClient.Select(c => new TasksByStatusMapper().Map(c)).ToList();
        }
        public IEnumerable<TaskByStatusDeveloperDTO> GetTasksByStatusDeveloperForManager(TaskManagerDeveloperDTO taskManagerDeveloper)
        {
            IEnumerable<TaskTable> mattersByClient = _taskDBContext.Tasks
                   .Include(m => m.Developer.User)
                   .Include(m => m.Manager)
                   .Include(m => m.Manager.User)
                    .Where(c => c.DeveloperId.Equals(taskManagerDeveloper.DeveloperId)
                                    && c.ManagerId.Equals(taskManagerDeveloper.ManagerId)
                                    && c.StatusId.Equals(taskManagerDeveloper.StatusId));

            return mattersByClient.Select(c => new TaskByStatusDeveloperMapper().Map(c)).ToList();
        }
    }
}
