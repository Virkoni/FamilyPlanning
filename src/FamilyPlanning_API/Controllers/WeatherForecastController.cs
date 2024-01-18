using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace FamilyPlanning_API.Controllers
{

    [ApiController]
    [Route("api/familytasks")]
    public class FamilyTasksController : ControllerBase
    {
        private readonly ILogger<FamilyTasksController> _logger;

        public FamilyTasksController(ILogger<FamilyTasksController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            List<Task> tasks = GetTasksFromDatabase();
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult AddTask(Task task)
        {
            return Ok();
        }

        [HttpDelete("{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            return Ok();
        }

        [HttpPut("{taskId}")]
        public IActionResult UpdateTask(int taskId, Task task)
        {
            return Ok();
        }

        // Dummy method to simulate retrieving tasks from the database
        private List<Task> GetTasksFromDatabase()
        {
            return new List<Task>
            {
                new Task { Id = 1, Title = "Task 1", Description = "Description for Task 1", Status = "Pending" },
                new Task { Id = 2, Title = "Task 2", Description = "Description for Task 2", Status = "Completed" },
            };
        }
        public class Task
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Status { get; set; }
            public int AssignedTo { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime DueDate { get; set; }
        }
    }

    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        
        // Dummy users (replace with a proper user management system)
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, UserName = "user1", Password = "password1", Role = "Admin" },
            new User { Id = 2, UserName = "user2", Password = "password2", Role = "User" }
        };

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            var user = AuthenticateUser(model.UserName, model.Password);

            if (user != null)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        private User AuthenticateUser(string userName, string password)
        {
            return _users.Find(u => u.UserName == userName && u.Password == password);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class User
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        public class LoginViewModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
