﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Models;
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
        public IActionResult GetAllManagers()
        {
            Response response;
            try
            {
                IEnumerable<ManagerDTO> managers = _managerService.GetAllManagers();
                if (managers.Any())
                {
                    response = new Response(StatusCodes.Status200OK, "Managers retreived successfully", managers.ToList());
                    return Ok(response);
                }
                if (!managers.Any())
                {
                    response = new Response(StatusCodes.Status404NotFound, "Managers not found", null);
                    return Ok(response);
                }
                response = new Response(StatusCodes.Status400BadRequest, "Something went wrong", null);
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response = new Response(StatusCodes.Status500InternalServerError, "Something went wrong - " + e.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetManagerById(string id)
        {
            Response response;
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    ManagerDTO manager = _managerService.GetManagerById(id);
                    if (manager != null)
                    {
                        response = new Response(StatusCodes.Status200OK, "Developer Retreived successfully", manager);
                        return Ok(response);
                    }
                    else
                    {
                        response = new Response(StatusCodes.Status404NotFound, "Developer not Found!", null);
                        return NotFound("Manager not found");
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

        [HttpPut("{id}")]
        public IActionResult UpdateManager(string id, ManagerUpdateDTO newManager)
        {
            Response response;
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Manager manager = _managerService.UpdateManager(id, newManager);
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
            catch (Exception e)
            {
                response = new Response(StatusCodes.Status500InternalServerError, "Something went wrong - " + e.Message, null);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
