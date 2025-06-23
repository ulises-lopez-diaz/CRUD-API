using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagerController : Controller
    {
        private static List<User> Users = new List<User>()
        {
            new User { Id = 1, Name = "Alice", Email = "alice@example.com"},
            new User { Id = 2, Name = "Bob", Email = "bob@example.com" }
        };

        private readonly ILogger<UserManagerController> _logger;

        public UserManagerController(ILogger<UserManagerController> logger)
        {
            _logger = logger;
        }

        // GET: /UserManager
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return Ok(Users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error at getting user");
                return StatusCode(500, "Internal Server Error");
            }
        }


        // GET: /UserManager/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id) 
        {
            try
            {
                var user = Users.FirstOrDefault(u => u.Id == id);
                if(user == null)
                {
                    return NotFound($"User with ID {id} Not Found");

                }
                return Ok(user);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error at getting user by ID");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // POST: /UserManager
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User newUser)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                newUser.Id = Users.Any() ? Users.Max(u => u.Id) + 1 : 1;
                Users.Add(newUser);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error at creating user");
                return StatusCode(500, "Internal Server Error");
            }
           
        }

        // PUT: /UserManager/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;

                return NoContent();
            } catch(Exception ex)
            {
                _logger.LogError(ex, "Error at updating user");
                return StatusCode(500, "Internal Server Error");
            }
            
        }

        // DELETE: /User/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {

            try
            {
                var user = Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                Users.Remove(user);
                return NoContent();
            } catch(Exception ex)
            {
                _logger.LogError(ex, "Error at deleting user");
                return StatusCode(500, "Internal Server Error");
            }
            
        }



    }
}
