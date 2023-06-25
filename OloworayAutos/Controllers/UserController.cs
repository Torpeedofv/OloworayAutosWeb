using Microsoft.AspNetCore.Mvc;
using OloworayAutos.Models.Models;
using OloworayAutos.Models.Repository.Interface;
using OloworayAutos.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OloworayAutos.Controllers
{
    // localhost/api/user/yourMethod
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepo;
        public UserController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public Users user { get; set; }


		[HttpPost("signup")]
		public IActionResult Signup(Users user)
		{
			// Perform validation on the user input
			if (user == null)
			{
				return BadRequest("Invalid user data");
			}

			// Check if the user already exists
			Users existingUser = userRepo.GetUserByEmail(user.Email);
			if (existingUser != null)
			{
				return BadRequest("User already exists");
			}

			// Save the new user to the repository
			userRepo.Add(user);

			// Return a success response
			return Ok("Signup successful");
		}

		
		public class LoginResponse
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

		[HttpPost("login")]
		public IActionResult Login(LoginResponse login)
        {

			// Perform validation on the login input
			if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
			{
				return BadRequest("Invalid login data");
			}

			// Retrieve the user based on the provided email
			Users user = userRepo.GetUserByEmail(login.Email);
			if (user == null)
			{
				return BadRequest("User not found");
			}

			// Compare the retrieved user's password with the provided password
			bool isPasswordValid = HashedPassword.VerifyPassword(login.Password, user.Password);
			if (!isPasswordValid)
			{
				return BadRequest("Invalid password");
			}

			// Authentication successful, return a success response or user information
			return Ok("Login successful");
		}
    }
}
