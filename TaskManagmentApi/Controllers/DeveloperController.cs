using Microsoft.AspNetCore.Mvc;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Models;
using TaskManagmentApi.Services.DTOs;
using TaskManagmentApi.Services.Services;


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
        public IActionResult GetAllDevelopers()
        {
            Response response;
            try
            {
                IEnumerable<DeveloperDTO> developers = _developerService.GetAllDevelopers();
                if (developers != null)
                {
                    response = new Response(StatusCodes.Status200OK, "Developers Retreived successfully", developers);
                    return Ok(response);
                }
                response = new Response(StatusCodes.Status404NotFound, "Task not Found!", null);
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response = new Response(StatusCodes.Status500InternalServerError, "Something went wrong - " + e.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetDeveloperById(string id)
        {
            Response response;

            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    DeveloperDTO developer = _developerService.GetDeveloperById(id);
                    if (developer != null)
                    {
                        response = new Response(StatusCodes.Status200OK, "Developer Retreived successfully", developer);
                        return Ok(response);
                    }
                    response = new Response(StatusCodes.Status404NotFound, "Developer not Found!", null);
                    return NotFound(response);
                }
                return BadRequest("Enter valid details");
            }
            catch (Exception e)
            {
                response = new Response(StatusCodes.Status500InternalServerError, "Something went wrong - " + e.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDeveloper(string id, DeveloperUpdateDTO newDeveloper)
        {
            Response response;

            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    DeveloperDTO developer = _developerService.UpdateDeveloper(id, newDeveloper);
                    if (developer != null)
                    {
                        return Ok("Developer updated successfully" + developer);
                    }
                    else
                    {
                        return NotFound("Developer not found");
                    }
                }
                return BadRequest("Enter valid details");
            }
            catch (Exception e)
            {
                response = new Response(StatusCodes.Status500InternalServerError, "Something went wrong - " + e.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("GetTasksByDeveloper/{id}")]
        public IActionResult GetTasksByDeveloper(string id)
        {
            Response response;
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    IEnumerable<TaskManagerDTO> tasks = _developerService.GetTasksForDeveloper(id);

                    if (tasks.Any())
                    {
                        response = new Response(StatusCodes.Status200OK, "Tasks retreived successfully", tasks.ToList());
                        return Ok(response);
                    }
                }
                response = new Response(StatusCodes.Status404NotFound, "Tasks Not Found", null);
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response = new Response(StatusCodes.Status500InternalServerError, "Something went wrong - " + e.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
