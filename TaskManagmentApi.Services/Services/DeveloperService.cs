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
        IEnumerable<Developer> GetAllDevelopers();
    }
    public class DeveloperService : IDeveloperService
    {
        private readonly TaskDBContext _taskDBContext;
        public DeveloperService(TaskDBContext taskDBContext)
        {
            _taskDBContext = taskDBContext;
        }

        public IEnumerable<Developer> GetAllDevelopers()
        {
            return _taskDBContext.Developers.Include(c => c.User).ToList();
            //return allManagers.Select(c => new ManagerMapper().Map(c)).ToList();
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

    }
}
