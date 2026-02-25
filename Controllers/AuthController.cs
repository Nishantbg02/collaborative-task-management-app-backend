using Microsoft.AspNetCore.Mvc;
using MiniJiraApi.Data;
using MiniJiraApi.Models;



namespace MiniJiraApi.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Regsiter(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(User login)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);
            if (user == null)
                return Unauthorized("Invalid credentials");

            return Ok (user);
        }
    }
}
