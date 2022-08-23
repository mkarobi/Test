using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Dtos.CANDC.LocateInfo;
using TFWebService.Data.Models;
using TFWebService.Presenter.Helper.Filter;
using TFWebService.Repo.Infrastructure;
using TFWebService.Services.Common.Encrypt.Interface;

namespace TFWebService.Presenter.Controllers.CAndC.LocateInfo
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "CANDC")]
    [Route("CANDC/[controller]")]
    [ApiController]
    public class LocateInfoController : Controller
    {
        private readonly IUnitOfWork<TFDbContext> _dbContext;
        private readonly IMapper _mapper;
        private readonly IEncryptService _encryptService;

        public LocateInfoController(IUnitOfWork<TFDbContext> dbContext,
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
            var locationsFromRepo = await _dbContext.LocationRepository.GetManyAsync(q => q.UserId == userId,
                o => o.OrderByDescending(p => p.UpdateTime), "User");
            return Ok(locationsFromRepo);
        }

        [HttpPost("{userId}")]
        [ServiceFilter(typeof(UserCheckTokenFilter))]
        public async Task<IActionResult> AddUserInfo(int userId, locationForAddDto location)
        {
            try
            {
                var user = await _dbContext.UserRepository.GetByIdAsync(userId);
                if (user == null)
                    return BadRequest("کاربر یافت نشد");

                if (location == null)
                    return BadRequest("اطلاعاتی یافت نشد");

                var locationMap = _mapper.Map<Location>(location);
                locationMap.UserId = userId;

                var locationDecrypt = _encryptService.LocationDecrypt(locationMap);

                await _dbContext.LocationRepository.InserAsync(locationDecrypt);


                if (await _dbContext.SaveAsync(locationDecrypt))
                {
                    string message = "Ok";
                    return Ok(message);
                }
                else
                    return BadRequest("خطایی رخ داده است.دوباره امتحان کنید.");
            }
            catch (Exception)
            {
                return BadRequest("خطایی رخ داده است.دوباره امتحان کنید.");
            }
        }
    }
}
