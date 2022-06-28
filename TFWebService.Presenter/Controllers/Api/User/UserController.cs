using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TFWebService.Presenter.Controllers.Api.User
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "WebService")]
    [Route("Api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        // GET: UserController
        public ActionResult Index()
        {
            return Ok();
        }

    }
}
