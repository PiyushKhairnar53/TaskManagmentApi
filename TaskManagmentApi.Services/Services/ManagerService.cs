using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentApi.Data.DBContext;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Services.DTOs;
using TaskManagmentApi.Services.Mappers;

namespace TaskManagmentApi.Services.Services
{
    public interface IManagerService
    {
        Manager AddManager(string userId);
        IEnumerable<ManagerDTO> GetAllManagers();
        ManagerDTO GetManagerById(string userId);
    }
    public class ManagerService : IManagerService
    {
        private readonly TaskDBContext _taskDBContext;
        public ManagerService(TaskDBContext taskDBContext)
        {
            _taskDBContext = taskDBContext;
        }

        public IEnumerable<ManagerDTO> GetAllManagers()
        {
            var allManagers = _taskDBContext.Managers.Include(c => c.User).ToList();
            return allManagers.Select(c => new ManagerMapper().Map(c)).ToList();
        }

        public Manager AddManager(string userId)
        {
            try
            {
                var newManager = new Manager
                {
                    Id = userId,
                    Bio = "",
                    IsActive = 1
                };
                _taskDBContext.Managers.Add(newManager);
                _taskDBContext.SaveChanges();

                return newManager;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ManagerDTO GetManagerById(string userId)
        {
            var manager = _taskDBContext.Managers.Include(c => c.User).FirstOrDefault(d => d.Id.Equals(userId));
            if (manager != null)
            {
                var mappedDeveloper = new ManagerMapper().Map(manager);
                return mappedDeveloper;
            }
            return null;
        }
    }
}