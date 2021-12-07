using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using serverAPI.Dtos;
using serverAPI.Entities;

namespace serverAPI.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _sigInManager;  
        private readonly IConfiguration _configuration;

        public AuthController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> sigInManager, IConfiguration configuration){
            _userManager = userManager;
            _sigInManager = sigInManager;
            _configuration = configuration;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(UserCreateDto myDto){
            if (ModelState.IsValid){
                var user = new ApplicationUser {UserName = myDto.username, Email = myDto.email};
                var ressul = await _userManager.CreateAsync(user, myDto.password);
                if (ressul.Succeeded)
                    return BuildToken(myDto);
                else    
                    return BadRequest("User name or password invalid");
            } else 
                return BadRequest(ModelState);
        }

        [HttpPost("Login")] //days bY mONTH AND yEAR
        public async Task<IActionResult> Login(UserBaseDto myDto){
            if (ModelState.IsValid){
                var ressul = await _sigInManager.PasswordSignInAsync(myDto.username, myDto.password, isPersistent: false, lockoutOnFailure: false);
                if (ressul.Succeeded)
                    return BuildToken(myDto);
                else{    
                    ModelState.AddModelError(string.Empty, "Invalid logging Attempt");
                    return BadRequest(ModelState);
                }
            } else 
                return BadRequest(ModelState);
        }
        
        private IActionResult BuildToken(UserBaseDto userInfo){
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.username), 
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SuperSecretKey"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "Any",
                claims: claims,
                expires: expiration, 
                signingCredentials: cred
            );

            return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token), expiration = expiration});
        }
    }
}