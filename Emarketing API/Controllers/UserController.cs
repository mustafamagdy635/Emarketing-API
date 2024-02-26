using Emarketing_API.DataAccess.Data;
using Emarketing_API.DataAccess.Repository.IRepository;
using Emarketing_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Emarketing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Context _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserController(IUnitOfWork unitOfWork, Context db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._unitOfWork = unitOfWork;
            this._db = db;
        }

        [HttpGet("Fetch", Name = "Fetch All User")]
        public IActionResult Fetch()
        {
            try
            {
                List<ApplicationUser> applicationUser = _unitOfWork._repositoryApplicationUser.GetAll().ToList();
                if (applicationUser == null)
                {
                    return BadRequest("No data ");
                }
                var UserRoles = _db.UserRoles.ToList();

                var roles = _db.Roles.ToList();

                foreach (var user in applicationUser)
                {
                    var RoleId = UserRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                    user.Role = roles.FirstOrDefault(u => u.Id == RoleId).Name;

                } 
                return Ok(applicationUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Error Occurred While Fetch users Data\n {ex}");
            }
        }

        [HttpGet("{Id}", Name = "FindUser")]
        public IActionResult Find(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    return BadRequest("Invalid User Id");
                }

                ApplicationUser applicationUserObj =
                    _unitOfWork._repositoryApplicationUser.Find(u => u.Id == Id);


                if (applicationUserObj == null)
                {
                    return NotFound();
                }


                return Ok(applicationUserObj);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Error Occurred While Fetch user Data\n {ex}");
            }
        }


        [HttpDelete("{Id}")]
        public IActionResult Delete(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    return BadRequest("Invalid User Id");
                }

                ApplicationUser applicationUserObj = _unitOfWork._repositoryApplicationUser.Find(u => u.Id == Id);

                if (applicationUserObj == null)
                {
                    return NotFound("User not found");
                }

                _unitOfWork._repositoryApplicationUser.Delete(applicationUserObj);
                _unitOfWork.Save();

                return Ok($"User with ID '{Id}' deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Error Occurred While deleting user Data\n {ex}");
            }
        }


    }
}
