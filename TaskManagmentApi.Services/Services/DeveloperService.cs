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
    public interface IDeveloperService
    {
        public Developer AddDeveloper(string userId);
        IEnumerable<DeveloperDTO> GetAllDevelopers();
        DeveloperDTO GetDeveloperById(string userId);
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
            var allDevelopers = _taskDBContext.Developers.Include(c => c.User).ToList();
            return allDevelopers.Select(c => new DeveloperMapper().Map(c)).ToList();
        }

        public Developer AddDeveloper(string userId)
        {
            try
            {
                var newDeveloper = new Developer
                {
                    Id = userId,
                    Bio = ""
                };
                _taskDBContext.Developers.Add(newDeveloper);
                _taskDBContext.SaveChanges();

                return newDeveloper;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DeveloperDTO GetDeveloperById(string userId)
        {
            var developer = _taskDBContext.Developers.Include(c => c.User).FirstOrDefault(d => d.Id.Equals(userId));
            if (developer != null)
            {
                var mappedDeveloper = new DeveloperMapper().Map(developer);
                return mappedDeveloper;
            }
            return null;
        }
    }
}
