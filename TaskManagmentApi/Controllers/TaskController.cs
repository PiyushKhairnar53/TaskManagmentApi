using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Models;
using TaskManagmentApi.Services.DTOs;
using TaskManagmentApi.Services.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [Route("AddTask")]
        public IActionResult AddTask([FromBody] TaskDTO task)
        {
            Response response;
            try
            {
                TaskTable newTask = _taskService.AddTask(task);
                if (newTask != null)
                {
                    response = new Response(StatusCodes.Status200OK, "Task added successfully", newTask);
                    return Ok(response);
                }
                response = new Response(StatusCodes.Status500InternalServerError, "Task not added!", null);
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response = new Response(StatusCodes.Status500InternalServerError, "Something went wrong - " + e.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("GetTasksByManager/{id}")]
        public IActionResult GetTasksByManager(string id)
        {
            Response response;
            try
            {
                IEnumerable<TaskManagerDTO> tasks = _taskService.GetTasksByManager(id);
                if (tasks.Any())
                {
                    response = new Response(StatusCodes.Status200OK, "Task Retrieved successfully", tasks.ToList());
                    return Ok(response);
                }
                response = new Response(StatusCodes.Status404NotFound, "Task Not Found", null);
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response = new Response(StatusCodes.Status500InternalServerError, "Something went wrong - " + e.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("UpdateTaskStatus")]
        public IActionResult UpdateTaskStatus([FromBody] TaskStatusUpdateDTO task)
        {
            Response response;
            try
            {
                TaskTable updatedTask = _taskService.UpdateTaskStatus(task);
                if (updatedTask != null)
                {
                    response = new Response(StatusCodes.Status200OK, "Task Status Updated successfully", updatedTask);
                    return Ok(response);
                }
                response = new Response(StatusCodes.Status500InternalServerError, "Task Status Not Updated!", null);
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response = new Response(StatusCodes.Status500InternalServerError, "Something went wrong - " + e.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("UpdateTask")]
        public IActionResult UpdateTask([FromBody] TaskUpdateDTO task)
        {
            Response response;
            try
            {
                TaskTable updatedTask = _taskService.UpdateTask(task);

                if (updatedTask != null)
                {
                    response = new Response(StatusCodes.Status200OK, "Task Updated successfully", updatedTask);
                    return Ok(response);
                }
                response = new Response(StatusCodes.Status500InternalServerError, "Task Not Updated!", null);
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response = new Response(StatusCodes.Status500InternalServerError, "Something went wrong - " + e.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        [Route("UpdateTaskDeveloper")]
        public async Task<IActionResult> UpdateTaskDeveloper([FromBody] TaskUpdateDeveloperDTO task)
        {
            Response response;
            try
            {
                TaskTable updatedTask = _taskService.UpdateTaskDeveloper(task);
                if (updatedTask != null)
                {
                    response = new Response(StatusCodes.Status200OK, "Task Updated successfully", updatedTask);
                    return Ok(response);
                }
                response = new Response(StatusCodes.Status500InternalServerError, "Task Not Updated!", null);
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response = new Response(StatusCodes.Status500InternalServerError, "Something went wrong - " + e.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("GetTasksByStatus")]
        public IActionResult GetTasksByStatus(StatusUserDTO statusManager)
        {
            Response response;
            try
            {
                IEnumerable<TaskByStatusDTO> tasks = _taskService.GetTasksByStatus(statusManager);
                if (tasks.Any())
                {
                    response = new Response(StatusCodes.Status200OK, "Tasks retreived successfully", tasks.ToList());
                    return Ok(response);
                }
                if (!tasks.Any())
                {
                    response = new Response(StatusCodes.Status200OK, "No tasks avaialaible", tasks.ToList());
                    return Ok(response);
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


        [HttpPost("GetTasksByStatusDeveloper")]
        public IActionResult GetTasksByStatusDeveloper(StatusUserDTO statusUser)
        {
            Response response;
            try
            {
                IEnumerable<TaskByStatusDeveloperDTO> tasks = _taskService.GetTasksByStatusDeveloper(statusUser);
                if (tasks.Any())
                {
                    response = new Response(StatusCodes.Status200OK, "Tasks retreived successfully", tasks.ToList());
                    return Ok(response);
                }
                if (!tasks.Any())
                {
                    response = new Response(StatusCodes.Status200OK, "No tasks avaialaible", tasks.ToList());
                    return Ok(response);
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

        [HttpPost("GetTasksByStatusForDeveloper")]
        public IActionResult GetTasksByStatusForDeveloper(TaskManagerDeveloperDTO managerDeveloper)
        {
            Response response;
            try
            {
                IEnumerable<TaskByStatusDTO> tasks = _taskService.GetTasksByStatusForDeveloper(managerDeveloper);
                if (tasks.Any())
                {
                    response = new Response(StatusCodes.Status200OK, "Tasks retreived successfully", tasks.ToList());
                    return Ok(response);
                }
                if (!tasks.Any())
                {
                    response = new Response(StatusCodes.Status200OK, "No tasks avaialaible", tasks.ToList());
                    return Ok(response);
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

        [HttpPost("GetTasksByStatusDeveloperForManager")]
        public IActionResult GetTasksByStatusDeveloperForManager(TaskManagerDeveloperDTO managerDeveloper)
        {
            Response response;
            try
            {
                IEnumerable<TaskByStatusDeveloperDTO> tasks = _taskService.GetTasksByStatusDeveloperForManager(managerDeveloper);
                if (tasks.Any())
                {
                    response = new Response(StatusCodes.Status200OK, "Tasks retreived successfully", tasks.ToList());
                    return Ok(response);
                }
                if (!tasks.Any())
                {
                    response = new Response(StatusCodes.Status200OK, "No tasks avaialaible", tasks.ToList());
                    return Ok(response);
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
