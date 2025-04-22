using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/User/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {

    }
}
