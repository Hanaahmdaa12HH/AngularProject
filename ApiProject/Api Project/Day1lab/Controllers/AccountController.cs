using Day1lab.DTO;
using Day1lab.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Day1lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly Productcontext context;


        public AccountController(UserManager<ApplicationUser> userManager, Productcontext productcontext, RoleManager<IdentityRole> roleManager, IConfiguration Configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            configuration = Configuration;
            this.context = productcontext;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            //save data base
            ApplicationUser userModel = new ApplicationUser();
            userModel.UserName = registerDto.UserName;
            userModel.Email = registerDto.Email;
            IdentityResult result = await userManager.CreateAsync(userModel, registerDto.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            //check identityt  "Create Token" ==Cookie
            ApplicationUser userModel = await userManager.FindByNameAsync(loginDto.Username);
            if (userModel != null)
            {
                if (await userManager.CheckPasswordAsync(userModel, loginDto.Password) == true)
                {
                    //toke base on Claims "Name &Roles &id " +Jti ==>unique Key Token "String"
                    var mytoken = await GenerateToke(userModel);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration = mytoken.ValidTo
                    });
                }
                else
                {
                    //return BadRequest("User NAme and PAssword Not Valid");
                    return Unauthorized();//401
                }
            }
            return Unauthorized();
        }
        [HttpGet("{Name:alpha}")]

        public IActionResult getByID(string Name)
        {
            ApplicationUser User = context.User.FirstOrDefault(user => user.UserName == Name);
            string ID = User.Id;
            if (User == null)
            {
                return BadRequest("User not found");
            }
            return Ok(ID);
        }

        [NonAction]
        public async Task<JwtSecurityToken> GenerateToke(ApplicationUser userModel)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("intakeNo", "42"));//Custom
            claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Id));
            var roles = await userManager.GetRolesAsync(userModel);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //Jti "Identifier Token
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //---------------------------------(: Token :)--------------------------------------
            var key =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecrtKey"]));
            var mytoken = new JwtSecurityToken(
                audience: configuration["JWT:ValidAudience"],
                issuer: configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials:
                       new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return mytoken;
        }
        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterDTO register)
        {
            var userExists = await userManager.FindByNameAsync(register.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Admin already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.UserName
            };
            var result = await userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Admin creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "Admin created successfully!" });
        }
    }
}

