using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFWebService.Common.ErrorAndMessage;
using TFWebService.Data.DatabaseContext;
using TFWebService.Repo.Infrastructure;

namespace TFWebService.Presenter.Controllers.CAndC.DeviceInfo
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "CANDC")]
    [Route("CANDC/[controller]")]
    [ApiController]
    public class DeviceInfoController : Controller
    {
        private readonly IUnitOfWork<TFDbContext> _dbContext;
        private readonly IMapper _mapper;

        public DeviceInfoController(IUnitOfWork<TFDbContext> dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _dbContext.UserRepository.GetByIdAsync(int.Parse(userId));

            if (user == null)
                return Unauthorized(new ReturnMessage()
                {
                    Status = false,
                    code = "404",
                    message = "کاربر وارد شده اعتبار سنجی نشده است.",
                    title = "خطا"
                });
            else
            {
                if (user.IsAdmin == true)
                {
                    return NoContent();
                }
                else
                {
                    return Unauthorized(new ReturnMessage()
                    {
                        Status = false,
                        code = "403",
                        message = "شما به این سرویس دسترسی ندارید.",
                        title = "خطا"
                    });
                }
            }
        }

        [HttpGet("userId")]
        public async Task<IActionResult> Index(int userId)
        {
            string userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _dbContext.UserRepository.GetByIdAsync(int.Parse(userIdFromToken));

            if (user == null)
                return Unauthorized(new ReturnMessage()
                {
                    Status = false,
                    code = "404",
                    message = "کاربر وارد شده اعتبار سنجی نشده است.",
                    title = "خطا"
                });
            else
            {
                if (user.IsAdmin == true)
                {
                    var deviceFromRepo = await _dbContext.DeviceRepository.GetAsync(q => q.UserId == userId,"User");
                    if(deviceFromRepo == null)
                        return NoContent();
                    return Ok(deviceFromRepo);
                }
                else
                {
                    return Unauthorized(new ReturnMessage()
                    {
                        Status = false,
                        code = "403",
                        message = "شما به این سرویس دسترسی ندارید.",
                        title = "خطا"
                    });
                }
            }
        }
    }
}
