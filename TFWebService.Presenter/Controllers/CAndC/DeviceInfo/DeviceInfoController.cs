using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFWebService.Common.ErrorAndMessage;
using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Dtos.CANDC.DeviceInfo;
using TFWebService.Data.Models;
using TFWebService.Presenter.Helper.Filter;
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

        public DeviceInfoController(IUnitOfWork<TFDbContext> dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return NotFound();
        }

        [HttpGet("{userId}")]
        [ServiceFilter(typeof(UserCheckAdminFilter))]
        public async Task<IActionResult> Index(int userId)
        {
            var devicesFromRepo = await _dbContext.DeviceRepository.GetManyAsync(q => q.UserId == userId,
                o => o.OrderByDescending(p => p.UpdateTime), "User");
            return Ok(devicesFromRepo);
        }

        [HttpPost("{userId}")]
        [ServiceFilter(typeof(UserCheckTokenFilter))]
        public async Task<IActionResult> AddUserInfo(string userId, DeviceForAddDto device)
        {
            var user = await _dbContext.UserRepository.GetByIdAsync(userId);
            if (user == null)
                return BadRequest("کاربر یافت نشد");

            if (device == null)
                return BadRequest("اطلاعاتی یافت نشد");

            var deviceMap = _mapper.Map<Device>(device);
            deviceMap.User = user;

            await _dbContext.DeviceRepository.InserAsync(deviceMap);

            if (await _dbContext.SaveAsync())
                return Ok();
            else
                return BadRequest("خطایی رخ داده است.دوباره امتحان کنید.");
        }
    }
}
