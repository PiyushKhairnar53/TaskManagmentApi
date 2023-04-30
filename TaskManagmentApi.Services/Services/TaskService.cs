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
    }
    public class TaskService:ITaskService
    {
        private readonly TaskDBContext _taskDBContext;
        public TaskService(TaskDBContext taskDBContext)
        {
            _taskDBContext = taskDBContext;
        }

        public IEnumerable<TaskTable> GetAllTask()
        {
            var allTasks = _taskDBContext.Tasks.Include(c => c.Manager).Include(c=>c.Developer).Include(c=>c.Status).ToList();
            return allTasks;
        }

        public TaskTable AddTask(TaskDTO task)
        {
            try
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
                    EstimatedTime = 0,
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
                        _taskDBContext.SaveChanges();
                        return findTask;
                    }
                }
                return null;
            }
            return null;
        }
    }
}
