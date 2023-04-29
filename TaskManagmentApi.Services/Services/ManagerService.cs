using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentApi.Data.DBContext;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Services.DTOs;

namespace TaskManagmentApi.Services.Services
{
    public interface IManagerService
    {
        public Manager AddManager(String userId);

    }
    public class ManagerService : IManagerService
    {
        private readonly TaskDBContext _taskDBContext;
        public ManagerService(TaskDBContext lexiconDBContext)
        {
            _taskDBContext = lexiconDBContext;
        }

        //public IEnumerable<ManagerDTO> GetAllMatters()
        //{
        //    var allMatters = _taskDBContext.Managers.ToList();
        //    return allMatters.Select(c => new MatterMapper().Map(c)).ToList();
        //}


        public Manager AddManager(String userId)
        {
            try
            {
                var newManager = new Manager
                {
                    Id = 0,
                    UserId = userId
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
    }
}