using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Sag.Framework.ExceptionHandlers;
using System.Diagnostics.CodeAnalysis;

namespace Sag.SampleBFF.Api.Api.Controllers
{
    [ApiController]
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status500InternalServerError)]
    public class BaseController : ControllerBase
    {
        [NonAction]
        [SuppressMessage("Critical Code Smell", "S4019:Base class methods should not be hidden", Justification = "As intended")]
        protected ObjectResult Forbid([ActionResultObjectValue] object value)
        {
            return StatusCode(403, value);
        }
    }
}
