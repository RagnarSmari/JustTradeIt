using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/trades")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        // TODO: Setup routes
    }
}