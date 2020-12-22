using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly DataContext _db;
        public UsersController(DataContext db)
        {
            _db=db;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
//            var users=_db.AppUsers.ToList();
            //return Ok(users);
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users=await _db.AppUsers.ToListAsync();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUsers(int id)
        {
            var user=await _db.AppUsers.FindAsync(id);
            return user;
        }
    }
}