﻿using Microsoft.EntityFrameworkCore;
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
    public interface IDeveloperService
    {
        public Developer AddDeveloper(string userId);
        IEnumerable<DeveloperDTO> GetAllDevelopers();
        DeveloperDTO GetDeveloperById(string userId);
        DeveloperDTO UpdateDeveloper(string userId, DeveloperUpdateDTO newDeveloper);
        IEnumerable<TaskManagerDTO> GetTasksForDeveloper(string developerId);
    }
    public class DeveloperService : IDeveloperService
    {
        private readonly TaskDBContext _taskDBContext;
        public DeveloperService(TaskDBContext taskDBContext)
        {
            _taskDBContext = taskDBContext;
        }

        public IEnumerable<DeveloperDTO> GetAllDevelopers()
        {
            List<Developer> allDevelopers = _taskDBContext.Developers.Include(c => c.User).ToList();
            return allDevelopers.Select(c => new DeveloperMapper().Map(c)).ToList();
        }

        public Developer AddDeveloper(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                Developer newDeveloper = new Developer
                {
                    Id = userId,
                    Bio = "",
                    IsActive = 1
                };
                _taskDBContext.Developers.Add(newDeveloper);
                _taskDBContext.SaveChanges();

                return newDeveloper;
            }
            return null;
        }

        public DeveloperDTO GetDeveloperById(string userId)
        {
                Developer developer = _taskDBContext.Developers.Include(c => c.User).FirstOrDefault(d => d.Id.Equals(userId));
                if (developer != null)
                {
                    var mappedDeveloper = new DeveloperMapper().Map(developer);
                    return mappedDeveloper;
                }
                return null;  
        }

        public DeveloperDTO UpdateDeveloper(string userId, DeveloperUpdateDTO newDeveloper)
        {
            Developer findDeveloper = _taskDBContext.Developers.Include(c => c.User).FirstOrDefault(d => d.Id.Equals(userId));
            if (findDeveloper != null)
            {
                if (string.IsNullOrEmpty(newDeveloper.Bio))
                {
                    newDeveloper.Bio = findDeveloper.Bio;
                }
                if (string.IsNullOrEmpty(newDeveloper.FirstName))
                {
                    newDeveloper.FirstName = findDeveloper.User.FirstName;
                }
                if (string.IsNullOrEmpty(newDeveloper.LastName))
                {
                    newDeveloper.LastName = findDeveloper.User.LastName;
                }
                if (string.IsNullOrEmpty(newDeveloper.Email))
                {
                    newDeveloper.Email = findDeveloper.User.Email;
                }
                if (string.IsNullOrEmpty(newDeveloper.PhoneNumber))
                {
                    newDeveloper.PhoneNumber = findDeveloper.User.PhoneNumber;
                }

                findDeveloper.Bio = newDeveloper.Bio;
                findDeveloper.User.FirstName = newDeveloper.FirstName;
                findDeveloper.User.LastName = newDeveloper.LastName;
                findDeveloper.User.Email = newDeveloper.Email;
                findDeveloper.User.PhoneNumber = newDeveloper.PhoneNumber;
                findDeveloper.Skills = newDeveloper.Skills;
                findDeveloper.User.Gender = newDeveloper.Gender;
                findDeveloper.User.Address = newDeveloper.Address;
                findDeveloper.User.UpdatedAt = DateTime.Now;
                _taskDBContext.SaveChanges();

                var mappedDeveloper = new DeveloperMapper().Map(findDeveloper);

                return mappedDeveloper;
            }
            return null;
        }

        public IEnumerable<TaskManagerDTO> GetTasksForDeveloper(string developerId)
        {                IEnumerable<TaskTable> mattersByClient = _taskDBContext.Tasks
                        .Include(m => m.Manager)
                        .Include(m => m.Developer)
                        .Include(m => m.Manager.User)
                        .Include(m => m.Developer.User)
                        .Include(m => m.Status)
                        .Where(c => c.DeveloperId.Equals(developerId));
                return mattersByClient.Select(c => new TaskManagerMapper().Map(c)).ToList();
        }
    }
}
