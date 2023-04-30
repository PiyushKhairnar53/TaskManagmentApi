using Microsoft.AspNetCore.Mvc;
using TaskManagmentApi.Services.DTOs;
using TaskManagmentApi.Services.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [HttpGet]
        [Route("GetAllDevelopers")]
        public IEnumerable<DeveloperDTO> GetAllDevelopers()
        {
            IEnumerable<DeveloperDTO> developers = _developerService.GetAllDevelopers();
            return developers;
        }

        [HttpGet("{id}")]
        public IActionResult GetDeveloperById(string id) 
        {
            if (!string.IsNullOrEmpty(id)) 
            {
                DeveloperDTO developer = _developerService.GetDeveloperById(id);
                if (developer != null)
                {
                    return Ok(developer);
                }
                else
                {
                    return NotFound("Developer not found");
                }
            }
            return BadRequest("Enter valid details");
        }
    }
}
