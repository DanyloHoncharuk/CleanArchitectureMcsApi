using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/Auth/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {

    }
}
