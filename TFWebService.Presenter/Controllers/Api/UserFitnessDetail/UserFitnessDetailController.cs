using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFWebService.Common.ErrorAndMessage;
using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Models;
using TFWebService.Presenter.Helper.Filter;
using TFWebService.Repo.Infrastructure;

namespace TFWebService.Presenter.Controllers.Api.UserFitnessDetail
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "WebService")]
    [Route("Api/[controller]")]
    [ApiController]
    public class UserFitnessDetailController : Controller
    {
        private readonly IUnitOfWork<TFDbContext> _dbContext;
        private readonly IMapper _mapper;

        public UserFitnessDetailController(IUnitOfWork<TFDbContext> dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return NotFound();
        }

        //get userfitness detailInfo
        [HttpGet("main/{userId}")]
        [ServiceFilter(typeof(UserCheckTokenFilter))]
        public async Task<IActionResult> Index(int userId, DateTime dateTime)
        {
            var userFromRepo = await _dbContext.UserRepository.GetByIdAsync(userId);
            if (userFromRepo == null)
                return BadRequest(new ReturnMessage()
                {
                    Status = false,
                    title = "کاربر یافت نشد",
                    message = "آیدی وارد شده نامعتبر است.",
                    code = "404"
                });

            var mainDetailFromRepo =
                await _dbContext.MainDetailsRepository.GetManyAsync(p => p.UserId == userId,
                    o => o.OrderByDescending(q => q.UpdateTime), null);
            var lastElementOrDefault = mainDetailFromRepo.LastOrDefault();

            if (lastElementOrDefault.UpdateTime.Year == dateTime.Year &&
                lastElementOrDefault.UpdateTime.Month == dateTime.Month &&
                lastElementOrDefault.UpdateTime.Day == dateTime.Day)
            {
                return Ok(lastElementOrDefault);
            }
            else if (lastElementOrDefault.UpdateTime.Year < dateTime.Year ||
                      lastElementOrDefault.UpdateTime.Month < dateTime.Month ||
                      lastElementOrDefault.UpdateTime.Day < dateTime.Day)
            {
                var newMainDetail = new MainDetails()
                {
                    User = userFromRepo,
                    ActivityCalories = "0",
                    FoodCalories = "0",
                    PersianDate = "naN",
                    SelfWeight = "0",
                    TotalCalories = "0",
                    WaterGlasses = "0"
                };

                await _dbContext.MainDetailsRepository.InserAsync(newMainDetail);
                if (await _dbContext.SaveAsync())
                {
                    return Ok(newMainDetail);
                }
                else
                {
                    return BadRequest("در سمت سرور خطایی بوجود آمده است.");
                }
            }

            return NoContent();
        }

    }
}
