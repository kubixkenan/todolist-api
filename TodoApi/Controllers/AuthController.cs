using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        
        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AuthModel model)
        {
            await _userManager.CreateAsync(new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email

            });

            var user = await _userManager.FindByEmailAsync(model.Email);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
            if (result.Succeeded)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest(false);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı!");
            }

            bool signedIn = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!signedIn)
            {
                return BadRequest("Hatalı Şifre");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var tokenResponse = TokenHandler.CreateAccessToken(user.Email, user.Id, roles.ToList());

            return Ok(tokenResponse);
        }
    }
}
