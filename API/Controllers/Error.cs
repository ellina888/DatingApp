using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Error : ControllerBase
    {
        private readonly DataContext _db;
        public Error(DataContext db)
        {
            _db = db;
        }

        [HttpGet("not-found")]
        public ActionResult<string> GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This is not a good request.");
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var user=_db.AppUsers.Find(-1);
            return user.UserName.ToString();
        }

        [Authorize]
        [HttpGet("authorize")]
        public ActionResult<string> GetSecret()
        {
            return "Secret Text";
        }
    }
}