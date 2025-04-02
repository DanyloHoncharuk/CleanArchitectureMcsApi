using Microsoft.AspNetCore.Mvc;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("api/User/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {

    }
}
