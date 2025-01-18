using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using User.Application.Dto;
using User.Core.Entities;
using User.Infrastructure.Data;
using User.Infrastructure.Repositories;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailService _emailService;
        private readonly UserDbContext _context;
        private readonly IConfiguration _config;
        

        public AccountController(UserRepository userRepository,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            EmailService emailService,
            UserDbContext context,
            IConfiguration config)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _context = context;
            _config = config;           
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) return Unauthorized("Invalid username or password");

            if (user.EmailConfirmed == false) return Unauthorized("Please confirm your email.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) return Unauthorized("Your account has been locked.");
            //if (result.IsLockedOut)
            //{
            //    return Unauthorized(string.Format("Your account has been locked. You should wait until {0} (UTC time) to be able to login", user.LockoutEnd));
            //}

            //if (!result.Succeeded)
            //{
            //    // User has input an invalid password
            //    if (!user.UserName.Equals(SD.AdminUserName))
            //    {
            //        // Increamenting AccessFailedCount of the AspNetUser by 1
            //        await _userManager.AccessFailedAsync(user);
            //    }

            //    if (user.AccessFailedCount >= SD.MaximumLoginAttempts)
            //    {
            //        // Lock the user for one day
            //        await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddDays(1));
            //        return Unauthorized(string.Format("Your account has been locked. You should wait until {0} (UTC time) to be able to login", user.LockoutEnd));
            //    }


            //    return Unauthorized("Invalid username or password");
            //}

            //await _userManager.ResetAccessFailedCountAsync(user);
            //await _userManager.SetLockoutEndDateAsync(user, null);

            return CreateApplicationUserDto(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (await CheckEmailExistsAsync(model.Email))
            {
                return BadRequest($"An existing account is using {model.Email}, email addres. Please try with another email address");
            }

            var userToAdd = new ApplicationUser
            {
                FirstName = model.FirstName.ToLower(),
                LastName = model.LastName.ToLower(),
                UserName = model.Email.ToLower(),
                Email = model.Email.ToLower(),
                //EmailConfirmed=true
                
            };

            // creates a user inside our AspNetUsers table inside our database
            var result = await _userManager.CreateAsync(userToAdd, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            //await _userManager.AddToRoleAsync(userToAdd, SD.PlayerRole);
            //return Ok("You can login");
            try
            {
                if (await SendConfirmEMailAsync(userToAdd))
                {
                    return Ok(new JsonResult(new { title = "Account Created", message = "Your account has been created, please confrim your email address" }));
                }

                return BadRequest("Failed to send email. Please contact admin");
            }
            catch (Exception)
            {
                return BadRequest("Failed to send email. Please contact admin");
            }

        }




        #region Private Helper Methods
        private UserDto CreateApplicationUserDto(ApplicationUser user)
        {
            //await 
                //SaveRefreshTokenAsync(user);
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                JWT = _userRepository.CreateJWT(user),
            };
        }

        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }

        private async Task<bool> SendConfirmEMailAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{_config["JWT:ClientUrl"]}/{_config["Email:ConfirmEmailPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Hello: {user.FirstName} {user.LastName}</p>" +
                "<p>Please confirm your email address by clicking on the following link.</p>" +
                $"<p><a href=\"{url}\">Click here</a></p>" +
                "<p>Thank you,</p>" +
                $"<br>{_config["Email:ApplicationName"]}";

            var emailSend = new EmailSendDto(user.Email, "Confirm your email", body);

            return await _emailService.SendEmailAsync(emailSend);
        }
        #endregion
    }
}