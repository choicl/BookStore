using AutoMapper;
using BookStore.API.Data;
using BookStore.API.DTOModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<APIUser> userManager;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<APIUser> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
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
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            logger.LogInformation($"Registration for {loginUserDTO.Email} ");
            //validation during the login process
            try
            {
                var user = await userManager.FindByEmailAsync(loginUserDTO.Email);
                var passwordValid = await userManager.CheckPasswordAsync(user, loginUserDTO.Password);
                if (user == null || passwordValid == false)
                {
                    return NotFound();
                }
                return Accepted();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something Went Wrong In {nameof(Register)} ");
                return Problem($"Something Went Wrong In {nameof(Register)} ", statusCode: 500);
            }
        }
    }
}
