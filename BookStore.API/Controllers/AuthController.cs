using AutoMapper;
using BookStore.API.Data;
using BookStore.API.DTOModels.User;
using BookStore.API.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<APIUser> userManager;
        private readonly IConfiguration configuration;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<APIUser> userManager, IConfiguration configuration)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            logger.LogInformation($"Registration for {userDTO.Email} ");

            try            {
                var user = mapper.Map<APIUser>(userDTO);
                user.UserName = userDTO.Email;
                var identityResult = await userManager.CreateAsync(user, userDTO.Password);

                if (identityResult.Succeeded == false)
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await userManager.AddToRoleAsync(user, "User");
                return Accepted();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something Went Wrong In {nameof(Register)} ");
                return Problem($"Something Went Wrong In {nameof(Register)} ", statusCode : 500);
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserDTO loginUserDTO)
        {
            logger.LogInformation($"Registration for {loginUserDTO.Email} ");
            //validation during the login process
            try
            {
                var user = await userManager.FindByEmailAsync(loginUserDTO.Email);
                var passwordValid = await userManager.CheckPasswordAsync(user, loginUserDTO.Password);
                if (user == null || passwordValid == false)
                {
                    return Unauthorized(loginUserDTO);
                }

                string tokenString = await GenerateToken(user);

                var response = new AuthResponse
                { 
                    Email = user.Email,
                    TokenValue = tokenString,
                    UserId = user.Id
                };

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something Went Wrong In {nameof(Register)} ");
                return Problem($"Something Went Wrong In {nameof(Register)} ", statusCode: 500);
            }
        }

        private async Task<string> GenerateToken(APIUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(c => new Claim(ClaimTypes.Role, c)).ToList();
            
            var userClaims = await userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id),

            }
            .Union(userClaims)
            .Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(configuration["JwtSetting:Duration"])),
                signingCredentials: credentials
                ) ;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
