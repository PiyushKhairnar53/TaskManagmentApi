using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Services.DTOs;

namespace TaskManagmentApi.Services.Mappers
{
    public class DeveloperMapper
    {
        public DeveloperDTO Map(Developer entity)
        {
            return new DeveloperDTO
            {
                Id = entity.Id,
                Bio = entity.Bio,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName,
                Skills = entity.Skills,
                Username = entity.User.UserName,
                PhoneNumber = entity.User.PhoneNumber,
                Email = entity.User.Email
            };
        }
    }
}
