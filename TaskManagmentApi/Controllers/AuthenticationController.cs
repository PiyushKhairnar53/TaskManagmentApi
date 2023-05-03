using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Models;
using TaskManagmentApi.Services.DTOs;
using TaskManagmentApi.Services.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        private readonly IManagerService _managerService;
        private readonly IDeveloperService _developerService;

        public AuthenticationController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IManagerService managerService,
            IDeveloperService developerService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _managerService = managerService;
            _developerService = developerService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    role = userRoles[0],
                    userId = user.Id
                });
            }
            return Unauthorized();
        }


        [HttpPost]
        [Route("RegisterManager")]
        public async Task<IActionResult> RegisterManager([FromBody] RegisterDTO registerDto)
        {
            Response response;
            var userExists = await _userManager.FindByNameAsync(registerDto.Username);
            if (userExists != null)
            {
                response = new Response(StatusCodes.Status500InternalServerError, "User Already Exists", null);
                return BadRequest(response);
            }

            if (!string.IsNullOrEmpty(registerDto.FirstName) && !string.IsNullOrEmpty(registerDto.LastName) && !string.IsNullOrEmpty(registerDto.Email))
            {
                User user = new()
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    UserRole = "Manager",
                    PhoneNumber = registerDto.PhoneNumber,
                    Email = registerDto.Email,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerDto.Username
                };
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                {
                    response = new Response(StatusCodes.Status500InternalServerError, "Failed to register Manager", null);
                    return BadRequest(response);
                }
                else
                {
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
                    }

                    if (await _roleManager.RoleExistsAsync(UserRoles.Manager))
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.Manager);
                    }

                    Manager newManager = _managerService.AddManager(user.Id);
                    response = new Response(StatusCodes.Status200OK, "Manager registerd successfully", newManager);
                    return Ok(response);
                }
            }
            response = new Response(StatusCodes.Status200OK, "Please enter valid details!", null);
            return BadRequest(response);
        }


        [HttpPost]
        [Route("RegisterDeveloper")]
        public async Task<IActionResult> RegisterDeveloper([FromBody] RegisterDTO registerDto)
        {
            Response response;
            var userExists = await _userManager.FindByNameAsync(registerDto.Username);
            if (userExists != null) {
                response = new Response(StatusCodes.Status500InternalServerError, "User Already Exists", null);
                return BadRequest(response);
            }

            if (!string.IsNullOrEmpty(registerDto.FirstName) && !string.IsNullOrEmpty(registerDto.LastName) && !string.IsNullOrEmpty(registerDto.Email))
            {
                User user = new()
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    UserRole = "Developer",
                    PhoneNumber = registerDto.PhoneNumber,
                    Email = registerDto.Email,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerDto.Username
                };
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                {
                    response = new Response(StatusCodes.Status500InternalServerError, "Failed to register Developer", null);
                    return BadRequest(response);
                }
                else
                {
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Developer))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Developer));
                    }

                    if (await _roleManager.RoleExistsAsync(UserRoles.Developer))
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.Developer);
                    }
                    Developer developer = _developerService.AddDeveloper(user.Id);
                    response = new Response(StatusCodes.Status200OK, "Developer registerd successfully", developer);
                    return Ok(response);
                }
            }
            response = new Response(StatusCodes.Status200OK, "Please enter valid details!", null);
            return BadRequest(response);
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
