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
        Manager UpdateManager(string id, ManagerUpdateDTO newManager);

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
            List<Manager> allManagers = _taskDBContext.Managers.Include(c => c.User).ToList();
            return allManagers.Select(c => new ManagerMapper().Map(c)).ToList();
        }

        public Manager AddManager(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                Manager newManager = new Manager
                {
                    Id = userId,
                    Bio = "",
                    IsActive = 1
                };
                _taskDBContext.Managers.Add(newManager);
                _taskDBContext.SaveChanges();

                return newManager;
            }
            return null;
        }

        public ManagerDTO GetManagerById(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                Manager manager = _taskDBContext.Managers.Include(c => c.User).FirstOrDefault(d => d.Id.Equals(userId));
                if (manager != null)
                {
                    var mappedManager = new ManagerMapper().Map(manager);
                    return mappedManager;
                }
            }
            return null;
        }

        public Manager UpdateManager(string userId, ManagerUpdateDTO newManager)
        {
            Manager findManager = _taskDBContext.Managers.Include(c => c.User).FirstOrDefault(d => d.Id.Equals(userId));
            if (findManager != null)
            {
                if (string.IsNullOrEmpty(newManager.Bio)){
                    newManager.Bio = findManager.Bio;
                }
                if (string.IsNullOrEmpty(newManager.FirstName)) {
                    newManager.FirstName = findManager.User.FirstName;
                }
                if (string.IsNullOrEmpty(newManager.LastName)) {
                    newManager.LastName = findManager.User.LastName;
                }
                if (string.IsNullOrEmpty(newManager.Email)) {
                    newManager.Email = findManager.User.Email;
                }
                if (string.IsNullOrEmpty(newManager.PhoneNumber)) {
                    newManager.PhoneNumber = findManager.User.PhoneNumber;
                }

                findManager.Bio = newManager.Bio;
                findManager.User.FirstName = newManager.FirstName;
                findManager.User.LastName = newManager.LastName;
                findManager.User.Email = newManager.Email;
                findManager.User.PhoneNumber = newManager.PhoneNumber;
                findManager.User.UpdatedAt = DateTime.Now;
                _taskDBContext.SaveChanges();
                return findManager;
            }
            return null;
        }
    }
}