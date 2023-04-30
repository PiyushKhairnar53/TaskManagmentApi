using Microsoft.AspNetCore.Mvc;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Services.DTOs;
using TaskManagmentApi.Services.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagmentApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet]
        [Route("GetAllManagers")]
        public IEnumerable<ManagerDTO> GetAllManagers()
        {
            IEnumerable<ManagerDTO> managers = _managerService.GetAllManagers();
            return managers;
        }

        [HttpGet("{id}")]
        public IActionResult GetManagerById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ManagerDTO manager = _managerService.GetManagerById(id);
                if (manager != null)
                {
                    return Ok(manager);
                }
                else
                {
                    return NotFound("Manager not found");
                }
            }
            return BadRequest("Enter valid details");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateManager(string id, ManagerUpdateDTO newManager)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var manager = _managerService.UpdateManager(id,newManager);
                if (manager != null)
                {
                    return Ok("Manager updated successfully");
                }
                else
                {
                    return NotFound("Manager not found");
                }
            }
            return BadRequest("Enter valid details");
        }
    }
}
