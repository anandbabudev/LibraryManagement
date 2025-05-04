using LibraryManagement.WebAPI.Data;
using LibraryManagement.WebAPI.Entity;
using LibraryManagement.WebAPI.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(dbContext context, PasswordHelper passwordHelper, IToken token) : ControllerBase
    {
        public readonly dbContext context = context;
        public readonly PasswordHelper passwordHelper = passwordHelper;
        public readonly IToken token = token;
        // GET: api/user
        [HttpGet]
        public IActionResult GetUserDetails()
        {
            var userId = HttpContext.User.FindFirst("Id")?.Value;
            if (userId == null)
            {
                return Unauthorized("User not authenticated.");
            }
            // Simulate fetching user details from the database
            var user = context.users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
          
            return Ok(user);
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = AuthenticateUser(model);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            var token = this.token.GenerateJwtToken(user);
            Response.Headers.Append("Authorization", "Bearer " + token); // Append token to response headers
            return Ok(new { Token = token });
        }
        private UserModel? AuthenticateUser(LoginModel model)
        {
            // Simulated database lookup (Replace with actual DB query)
            var user = context.users.FirstOrDefault(u => u.Username == model.Username);

            if (user != null && VerifyPassword(model.Password, user.PasswordHash))
            {
                return user; // Return authenticated user
            }

            return null; // Authentication failed
        }
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var passwordHasher = new PasswordHasher<UserModel>();
            return passwordHasher.VerifyHashedPassword(new UserModel(), storedHash, enteredPassword) == PasswordVerificationResult.Success;
        }
        // POST: api/user   
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null.");
            }
            user.PasswordHash = passwordHelper.HashPassword(user.PasswordHash); // Hash the password

            // Simulate user creation
            context.users.Add(user);
            context.SaveChanges(); // Save changes to the database
            return CreatedAtAction(nameof(GetUserDetails), new { id = user.Id }, user);
        }
    }
}
// Simulated User class

