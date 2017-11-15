using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vibbra.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Vibbra.Extensions;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Owin;
using System.Security.Cryptography;


namespace Vibbra.Controllers.api
{
    [Produces("application/json")]
    [Route("api/v[controller]/User")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private UserManager<ApplicationUser> _userManager;


        public UserController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this._dbContext = db;
            this._userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult get()
        {

            var obj = this.GetDbUser(_dbContext);

            return Content(JsonConvert.SerializeObject(new { _data = obj }, Formatting.Indented), "application/json");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Usuario user)
        {
            var _message = string.Empty;

            var obj = _dbContext.Users.Where(x => x.UserName == user.UserName && x.Email == user.Email).FirstOrDefault();

            if (string.IsNullOrEmpty(user.Password))
                _message = "Inform the password";
            else
            {
                var result = _userManager.CheckPasswordAsync(obj, user.Password);

                if (!result.Result)
                    _message = "Invalid email or username or password";
            }

            if (!string.IsNullOrEmpty(_message))
            {
                var response = new
                {
                    message = _message
                };

                return Content(JsonConvert.SerializeObject(new { _data = response }, Formatting.Indented), "application/json");
            }


            return Content(JsonConvert.SerializeObject(new { _data = obj }, Formatting.Indented), "application/json");

        }


        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Usuario user)
        {
            var _message = string.Empty;

            try
            {
                var userToUpdate = await _userManager.FindByIdAsync(user.Id);
                userToUpdate.Email = user.Email;
                userToUpdate.NormalizedEmail = user.Email.ToUpper();
                userToUpdate.UserName = user.UserName;
                userToUpdate.NormalizedUserName = user.NormalizedUserName;
                userToUpdate.PasswordHash = HashPassword(user.Password);
                await _userManager.UpdateAsync(userToUpdate);

                _message = "save with success";

            }
            catch (Exception ex)
            {
                _message = ex.Message;
            }

            var response = new
            {
                message = _message
            };

            return Json(response);

        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

    }
}