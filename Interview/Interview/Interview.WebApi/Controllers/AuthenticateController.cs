using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Interview.Entities;
using Interview.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Interview.WebApi.Controllers
{

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Models;


    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return Ok(new ServiceResult<JwtSecurityToken> { Status = "Error", Data = null, Message = "User is not exists" });
            }

            var isPasswordCorrect = await userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordCorrect)
            {
                return Ok(new ServiceResult<JwtSecurityToken> { Status = "Error", Data = null, Message = "Password is not correct" });
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new ServiceResult<string> { Status = "Success", Data = new JwtSecurityTokenHandler().WriteToken(token), Message = "Success" });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);

            if (userExists != null)
            {
                return Ok(new ServiceResult { Status = "Error", Message = "User already exists" });
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return Ok(new ServiceResult { Status = "Error", Message = "User cannot be created. Please check the user details." });
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            var roleResult = await userManager.AddToRoleAsync(user, UserRoles.User);

            if (!roleResult.Succeeded)
            {
                return Ok(new ServiceResult { Status = "Error", Message = "User cannot be added to specified role." });
            }

            return Ok(new ServiceResult { Status = "Success", Message = "User created successfully" });
        }

        [HttpPost]
        [Route("addToRole")]
        public async Task<IActionResult> AddToRole([FromBody] AddToRoleModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return Ok(new ServiceResult { Status = "Error", Message = "User is not exists" });
            }

            var roleExists = await roleManager.RoleExistsAsync(model.RoleName);

            if (!roleExists)
            {
                return Ok(new ServiceResult { Status = "Error", Message = "Role is not exists" });
            }

            var result = await userManager.AddToRoleAsync(user, model.RoleName);

            return Ok(new ServiceResult { Status = "Success", Message = "User is added to specified role successfully" });
        }
    }
}
