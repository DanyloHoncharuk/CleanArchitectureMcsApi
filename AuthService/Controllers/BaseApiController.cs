using AuthService.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/Auth/[controller]")]
    public abstract class BaseApiController(AuthServiceDbContext context) : ControllerBase
    {
        protected readonly AuthServiceDbContext _context = context;
    }
}
