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

        [HttpGet]
        [Route("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            Response response;
            IEnumerable<TaskTable> developers = _taskService.GetAllTask();
            if (developers != null)
            {
                response = new Response(StatusCodes.Status200OK, "Tasks Retreived successfully", developers);
                return Ok(response);
            }
            response = new Response(StatusCodes.Status404NotFound, "Task not Found!", null);
            return BadRequest(response);
        }

        [HttpPost]
        [Route("AddTask")]
        public async Task<IActionResult> AddTask([FromBody] TaskDTO task)
        {
            Response response;
            TaskTable newTask = _taskService.AddTask(task);
            if (newTask != null) 
            {
                response = new Response(StatusCodes.Status200OK, "Task added successfully", newTask);
                return Ok(response);
            }
            response = new Response(StatusCodes.Status500InternalServerError,"Task not added!", null);
            return BadRequest(response);
        }

        [HttpGet("GetTasksByManager/{id}")]
        public async Task<IActionResult> GetTasksByManager(string id)
        {
            Response response;
            IEnumerable<TaskManagerDTO> tasks = _taskService.GetTasksByManager(id);

            if (tasks.Any())
            {
                response = new Response(StatusCodes.Status200OK, "Task Retrieved successfully", tasks.ToList());
                return Ok(response);  
            }
            response = new Response(StatusCodes.Status404NotFound, "Task Not Found",null);
            return BadRequest(response);
        }

        [HttpPut]
        [Route("UpdateTaskStatus")]
        public async Task<IActionResult> UpdateTaskStatus([FromBody] TaskStatusUpdateDTO task)
        {
            Response response;
            TaskTable updatedTask = _taskService.UpdateTaskStatus(task);

            if (updatedTask!=null)
            {
                response = new Response(StatusCodes.Status200OK, "Task Status Updated successfully", updatedTask);
                return Ok(response);
            }
            response = new Response(StatusCodes.Status500InternalServerError, "Task Status Not Updated!", null);
            return BadRequest(response);
        }

        //[HttpPut]
        //[Route("UpdateTaskDeveloper")]
        //public async Task<IActionResult> UpdateTaskDeveloper([FromBody] TaskDeveloperUpdateDTO task)
        //{
        //    Response response;
        //    TaskTable updatedDeveloper = _taskService.UpdateDeveloperOnTask(task);

        //    if (updatedDeveloper != null)
        //    {
        //        response = new Response(StatusCodes.Status200OK, "Task Developer Updated successfully", updatedDeveloper);
        //        return Ok(response);
        //    }
        //    response = new Response(StatusCodes.Status500InternalServerError, "Task Developer Not Updated!", null);
        //    return BadRequest(response);
        //}

        [HttpPut]
        [Route("UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskUpdateDTO task)
        {
            Response response;
            TaskTable updatedTask = _taskService.UpdateTask(task);

            if (updatedTask != null)
            {
                response = new Response(StatusCodes.Status200OK, "Task Updated successfully", updatedTask);
                return Ok(response);
            }
            response = new Response(StatusCodes.Status500InternalServerError, "Task Not Updated!", null);
            return BadRequest(response);
        }

        [HttpPut]
        [Route("UpdateTaskDeveloper")]
        public async Task<IActionResult> UpdateTaskDeveloper([FromBody] TaskUpdateDeveloperDTO task)
        {
            Response response;
            TaskTable updatedTask = _taskService.UpdateTaskDeveloper(task);

            if (updatedTask != null)
            {
                response = new Response(StatusCodes.Status200OK, "Task Updated successfully", updatedTask);
                return Ok(response);
            }
            response = new Response(StatusCodes.Status500InternalServerError, "Task Not Updated!", null);
            return BadRequest(response);
        }

        [HttpPost("GetTasksByStatus")]
        public async Task<IActionResult> GetTasksByStatus(StatusUserDTO statusManager)
        {
            Response response;
            IEnumerable<TaskByStatusDTO> tasks = _taskService.GetTasksByStatus(statusManager);

            if (tasks.Any())
            {
                response = new Response(StatusCodes.Status200OK, "Tasks retreived successfully", tasks.ToList());
                return Ok(response);
            }
            response = new Response(StatusCodes.Status404NotFound, "Tasks Not Found", null);
            return BadRequest(response);
        }


        [HttpPost("GetTasksByStatusDeveloper")]
        public async Task<IActionResult> GetTasksByStatusDeveloper(StatusUserDTO statusUser)
        {
            Response response;
            IEnumerable<TaskByStatusDeveloperDTO> tasks = _taskService.GetTasksByStatusDeveloper(statusUser);

            if (tasks.Any())
            {
                response = new Response(StatusCodes.Status200OK, "Tasks retreived successfully", tasks.ToList());
                return Ok(response);
            }
            response = new Response(StatusCodes.Status404NotFound, "Tasks Not Found", null);
            return BadRequest(response);
        }

    }
}
