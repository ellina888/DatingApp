using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTOs;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _db;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext db, ITokenService tokenService)
        {
            _tokenService = tokenService;

            _db = db;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto re)
        {
            if (await UserExists(re.Username)) return BadRequest("Username is taken!");

            using var hmac = new HMACSHA512();

            var user = new AppUser()
            {
                UserName = re.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(re.Password)),
                PasswordSalt = hmac.Key
            };

            _db.AppUsers.Add(user);
            await _db.SaveChangesAsync();

            return new UserDto()
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(LoginDto lo)
        {
            var user = await _db.AppUsers.SingleOrDefaultAsync(u => u.UserName == lo.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(lo.Password));

            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if (ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password!");
            }

            return new UserDto()
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _db.AppUsers.AnyAsync(u => u.UserName == username.ToLower());
        }
    }
}