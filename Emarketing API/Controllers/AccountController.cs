
using AutoMapper;
using Emarketing_API.DataAccess.Repository.IRepository;
using Emarketing_API.Models.DTO;
using Emarketing_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Emarketing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            this._unitOfWork = unitOfWork;
       
            this._userManager = userManager;
            this._configuration = configuration;
        }



        [HttpPost("Register", Name ="Register")]
        public async Task<IActionResult> Add(AccountDTO accountDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ApplicationUser user = new ApplicationUser();
                user.UserName = accountDTO.UserName;
                user.lastName = accountDTO.LastName;
                user.firstName = accountDTO.FirstName;
                user.Email = accountDTO.Email;
                user.state = accountDTO.State;
                user.street = accountDTO.Street;
                user.Zip_Code = accountDTO.ZipCode;
                user.city = accountDTO.City;

                IdentityResult result =await _userManager.CreateAsync(user, accountDTO.Password);

                if (result.Succeeded)
                {
                    return Ok(result);
                }
               
                    return BadRequest("An Error Occurred while Adding user");
                
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"Internal server error\n {ex}");
            }
        }


        [HttpPost("Login",Name ="Login")]

        public async Task<IActionResult> Login(loginUserDTO loginUserDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if(loginUserDTO==null)
                {
                    return BadRequest("Data missing while Login");
                }
                ApplicationUser user =await _userManager.FindByNameAsync(loginUserDTO.UserName);
                if(user==null)
                {
                    return Unauthorized();
                }
                bool found =await _userManager.CheckPasswordAsync(user, loginUserDTO.Password);
                if (!found)
                {
                    return Unauthorized();
                }

                var claims =new  List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name , user.UserName));

                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                var Roles = await _userManager.GetRolesAsync(user);

                foreach (var Item in Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, Item));
                }

                SecurityKey securityKey = new SymmetricSecurityKey((Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"])));

                SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken MyToken = new JwtSecurityToken(

                    issuer: _configuration["JWT:ValidIssuer"] ,

                    audience: _configuration["JWT:ValidAudiance"],

                    claims: claims ,

                    expires: DateTime.Now.AddHours(1),

                    signingCredentials: signingCredentials

                 );

                return Ok(

                    new
                    {
                       Token = new JwtSecurityTokenHandler().WriteToken(MyToken),

                       Expiration = MyToken.ValidTo
                    });


            }
            catch (Exception ex)
            {
                return StatusCode(500,$"Internal server error\n {ex}");
            }

        }


    }
}
