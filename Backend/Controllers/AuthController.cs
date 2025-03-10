using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Backend.Models.Domain;
using Backend.Models.DTO;
using Backend.Services;
using carwash.Data;
using carwash.Models.Domain;
using carwash.Models.DTO;
using carwash.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace carwash.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _rr;
        private readonly IUser _user;
        private readonly IEmailService _emailService;
        private readonly AuthDbContext _authDbContext;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository rr, IUser user, IEmailService emailService, AuthDbContext authDbContext)
        {
            _userManager = userManager;
            _rr = rr;
            _user = user;
            _emailService = emailService;
            _authDbContext = authDbContext;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto obj)
        {
            var identityUser = new IdentityUser
            {
                UserName = obj.Username,
                Email = obj.Username
            };

            var identityResult = await _userManager.CreateAsync(identityUser, obj.Password);
            if (identityResult.Succeeded)
            {
                if (obj.Roles != null && obj.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, obj.Roles);
                    if (identityResult.Succeeded)
                    {
                        var user = new User()
                        {
                            Name = obj.Name,
                            Email = obj.Username,
                            MobileNumber = obj.MobileNumber,
                            Role = obj.Roles[0]
                        };
                        await _user.AddUserAsync(user);

                        // Send registration confirmation email
                        var emailSubject = "Welcome to Car Wash!";
                        var emailBody = $"<h3>Hello {obj.Name},</h3><p>Your account has been successfully registered.</p>";
                        await _emailService.SendEmailAsync(obj.Username, emailSubject, emailBody);

                        return Ok("User was registered! Please login");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }

        // User Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto obj)
        {
            var user = await _userManager.FindByEmailAsync(obj.Username);
            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, obj.Password);
                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = _rr.CreateJwtToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or password incorrect");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new { Errors = "User not found." });
            }

            // Generate OTP
            var otp = GenerateOtp();
            var expiryTime = DateTime.UtcNow.AddMinutes(5);

            // Store OTP in database
            var existingOtp = await _authDbContext.OtpStorage.FirstOrDefaultAsync(o => o.Email == model.Email);
            if (existingOtp != null)
            {
                _authDbContext.OtpStorage.Remove(existingOtp);
            }

            await _authDbContext.OtpStorage.AddAsync(new OtpRecord
            {
                Email = model.Email,
                Otp = otp,
                ExpiryTime = expiryTime
            });

            await _authDbContext.SaveChangesAsync();

            // Send OTP via email
            var emailSubject = "Your OTP for Password Reset";
            var emailBody = $"<h3>Your OTP for password reset is: <b>{otp}</b></h3><p>This OTP is valid for 5 minutes.</p>";
            await _emailService.SendEmailAsync(model.Email, emailSubject, emailBody);

            return Ok(new { Message = "OTP sent to your email." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var otpRecord = await _authDbContext.OtpStorage.FirstOrDefaultAsync(o => o.Email == model.Email && o.Otp == model.Otp);
            if (otpRecord == null || otpRecord.ExpiryTime < DateTime.UtcNow)
            {
                return BadRequest(new { Errors = "Invalid or expired OTP." });
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new { Errors = "User not found." });
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
            }

            // Remove OTP from database after successful reset
            _authDbContext.OtpStorage.Remove(otpRecord);
            await _authDbContext.SaveChangesAsync();

            return Ok(new { Message = "Password reset successfully." });
        }

        private string GenerateOtp()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[4];
                rng.GetBytes(randomBytes);
                return (BitConverter.ToUInt32(randomBytes, 0) % 1000000).ToString("D6");
            }
        }
    }
}

