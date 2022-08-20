using System;
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
using TFWebService.Services.Common.Encrypt.Interface;

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
        private readonly IEncryptService _encryptService;

        public DeviceInfoController(IUnitOfWork<TFDbContext> dbContext, 
            IMapper mapper,
            IEncryptService encryptService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _encryptService = encryptService;
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
        public async Task<IActionResult> AddUserInfo(int userId, DeviceForAddDto device)
        {
            try
            {
                var user = await _dbContext.UserRepository.GetByIdAsync(userId);
                if (user == null)
                    return BadRequest("کاربر یافت نشد");

                if (device == null)
                    return BadRequest("اطلاعاتی یافت نشد");

                Device deviceMap = new Device();
                deviceMap = _mapper.Map<Device>(device);
                deviceMap.UserId = userId;

                var deviseDecrypt = _encryptService.DeviceDecrypt(deviceMap);

                await _dbContext.DeviceRepository.InserAsync(deviseDecrypt);


                if (await _dbContext.SaveAsync(deviseDecrypt))
                {
                    string message = "Ok";
                    return Ok(message);
                }
                else
                    return BadRequest("خطایی رخ داده است.دوباره امتحان کنید.");
            }
            catch(Exception)
            {
                return BadRequest("خطایی رخ داده است.دوباره امتحان کنید.");
            }
        }
    }
}
