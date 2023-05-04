using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Services.DTOs;

namespace TaskManagmentApi.Services.Mappers
{
    public class ManagerMapper
    {
        public ManagerDTO Map(Manager entity)
        {
            return new ManagerDTO
            {
                Id = entity.Id,
                Bio = entity.Bio,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName,
                PhoneNumber = entity.User.PhoneNumber,
                Email = entity.User.Email
            };
        }
    }
}
