using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly family_planningContext _context;

        public AuthenticationController(IConfiguration configuration, family_planningContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            if (_context.WebUsers.Any(u => u.Username == model.UserName))
            {
                return BadRequest("Username is already taken");
            }

            var newUser = new WebUser
            {
                Username = model.UserName,
                Password = model.Password,

                CreatedAt = DateTime.Now,
                Birthdate = model.Birthdate
            };

            // Add the user to the database
            _context.WebUsers.Add(newUser);
            _context.SaveChanges();

            return Ok(new { Message = "Registration successful" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            var user = AuthenticateUser(model.UserName, model.Password);

            if (user != null)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token, UserId = user.Id });
            }

            return Unauthorized("Invalid username or password");
        }

        [Authorize] 
        [HttpPut("update-profile")]
        public IActionResult UpdateProfile([FromBody] UpdateProfileViewModel model)
        {
            // Get the user ID from the token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized("Invalid user token");
            }

            // Retrieve the user from the database
            var user = _context.WebUsers.Find(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            user.Name = model.Name;
            user.Surname = model.Surname;
            _context.SaveChanges();

            return Ok(new { Message = "Profile updated successfully" });
        }

        private WebUser AuthenticateUser(string userName, string password)
        {
            return _context.WebUsers.FirstOrDefault(u => u.Username == userName && u.Password == password);
        }

        private string GenerateJwtToken(WebUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
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

        public class RegisterViewModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public DateTime Birthdate { get; set; }
        }

        public class LoginViewModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class UpdateProfileViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}
