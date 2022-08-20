using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TFWebService.Common.ErrorAndMessage;
using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Dtos.Api.UserFitnessDetail.MainDetail;
using TFWebService.Data.Dtos.Api.UserFitnessDetail.TrackDetail;
using TFWebService.Data.Models;
using TFWebService.Presenter.Helper.Filter;
using TFWebService.Repo.Infrastructure;
using TFWebService.Services.Common.Encrypt.Interface;

namespace TFWebService.Presenter.Controllers.Api.UserFitnessDetail
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "WebService")]
    [Route("Api/[controller]")]
    [ApiController]
    public class UserFitnessDetailController : Controller
    {
        private IUnitOfWork<TFDbContext> _dbContext;
        private readonly IMapper _mapper;
        private readonly IEncryptService _encryptService;

        public UserFitnessDetailController(IUnitOfWork<TFDbContext> dbContext,
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

        //get userfitness detailInfo
        [HttpPost("main/{userId}")]
        [ServiceFilter(typeof(UserCheckTokenFilter))]
        public async Task<IActionResult> Index(int userId, [FromForm] string datetime)
        {
            try
            {
                DateTime dateTime = DateTime.Parse(datetime);
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
                        o => o.OrderByDescending(q => q.UpdateTime), "");
                var firstElementOrDefault = mainDetailFromRepo.FirstOrDefault();

                if (firstElementOrDefault == null)
                {
                    var newMainDetail = new MainDetails()
                    {
                        UserId = userFromRepo.Id,
                        ActivityCalories = "0",
                        FoodCalories = "0",
                        PersianDate = "naN",
                        SelfWeight = "0",
                        TotalCalories = "0",
                        WaterGlasses = "0"
                    };

                    await _dbContext.MainDetailsRepository.InserAsync(newMainDetail);
                    if (await _dbContext.SaveAsync(newMainDetail))
                    {
                        var newMainDetailEncrypted = _encryptService.MainDetailsEncrypt(newMainDetail);
                        var mapped = _mapper.Map<MainDetailForUpdateDto>(newMainDetailEncrypted);
                        return Ok(mapped);
                    }
                    else
                    {
                        return BadRequest("در سمت سرور خطایی بوجود آمده است.");
                    }
                }



                if (firstElementOrDefault.UpdateTime.Year == dateTime.Year &&
                    firstElementOrDefault.UpdateTime.Month == dateTime.Month &&
                    firstElementOrDefault.UpdateTime.Day == dateTime.Day)
                {
                    var firstElementEncrypted = _encryptService.MainDetailsEncrypt(firstElementOrDefault);
                    var mapped = _mapper.Map<MainDetailForUpdateDto>(firstElementEncrypted);
                    return Ok(mapped);
                }
                else if (firstElementOrDefault.UpdateTime.Year < dateTime.Year ||
                         firstElementOrDefault.UpdateTime.Month < dateTime.Month ||
                         firstElementOrDefault.UpdateTime.Day < dateTime.Day)
                {
                    var newMainDetail = new MainDetails()
                    {
                        UserId = userFromRepo.Id,
                        ActivityCalories = "0",
                        FoodCalories = "0",
                        PersianDate = "naN",
                        SelfWeight = "0",
                        TotalCalories = "0",
                        WaterGlasses = "0"
                    };

                    _dbContext.Detach(newMainDetail);
                    await _dbContext.MainDetailsRepository.InserAsync(newMainDetail);
                    if (await _dbContext.SaveAsync(newMainDetail))
                    {
                        var newMainDetailEncrypted = _encryptService.MainDetailsEncrypt(newMainDetail);
                        var mapped = _mapper.Map<MainDetailForUpdateDto>(newMainDetailEncrypted);
                        return Ok(mapped);
                    }
                    else
                    {
                        return BadRequest("در سمت سرور خطایی بوجود آمده است.");
                    }
                }
                return NoContent();

            }
            catch (Exception)
            {
                return NoContent();
            }


        }

        [HttpGet("track/{userId}")]
        [ServiceFilter(typeof(UserCheckTokenFilter))]
        public async Task<IActionResult> Index(int userId)
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

            var trackDetailFromRepo = await _dbContext.TrackDetailsRepository.GetManyAsync(p => p.UserId == userId,
                q => q.OrderByDescending(o => o.UpdateTime), "");
            var firstElement = trackDetailFromRepo.FirstOrDefault();

            if (firstElement == null)
            {
                var newTrackDetails = new TrackDetails()
                {
                    UserId = userFromRepo.Id,
                    TrackActivity = "0",
                    TrackFood = "0",
                    PersianDate = "naN",
                    TrackWeight = "0",
                };

                await _dbContext.TrackDetailsRepository.InserAsync(newTrackDetails);
                if (await _dbContext.SaveAsync(newTrackDetails))
                {
                    var newTrackDetailEncrypted = _encryptService.TrackDetailsEncrypt(newTrackDetails);
                    var mapped = _mapper.Map<TrackDetailForUpdateDto>(newTrackDetailEncrypted);
                    return Ok(mapped);
                }
                else
                {
                    return BadRequest("در سمت سرور خطایی بوجود آمده است.");
                }
            }
            else
            {
                var trackDetailEncrypted = _encryptService.TrackDetailsEncrypt(firstElement);
                var mapped = _mapper.Map<TrackDetailForUpdateDto>(trackDetailEncrypted);
                return Ok(mapped);
            }

        }

        [HttpPut("main/{userId}")]
        [ServiceFilter(typeof(UserCheckTokenFilter))]
        public async Task<IActionResult> UpdateMainDetail(int userId, MainDetailForUpdateDto updateDto)
        {
            var mainDetailFromRepo = await _dbContext.MainDetailsRepository.GetByIdAsync(updateDto.Id);
            if (mainDetailFromRepo == null)
                return NoContent();

            var mapped = _mapper.Map<MainDetails>(updateDto);
            var updateDtoDecrypt = _encryptService.MainDetailsDecrypt(mapped);

            mainDetailFromRepo.TotalCalories = updateDtoDecrypt.TotalCalories;
            mainDetailFromRepo.ActivityCalories = updateDtoDecrypt.ActivityCalories;
            mainDetailFromRepo.FoodCalories = updateDtoDecrypt.FoodCalories;
            mainDetailFromRepo.WaterGlasses = updateDtoDecrypt.WaterGlasses;
            mainDetailFromRepo.SelfWeight = updateDtoDecrypt.SelfWeight;
            mainDetailFromRepo.PersianDate = updateDtoDecrypt.PersianDate;

            _dbContext.MainDetailsRepository.Update(mainDetailFromRepo);
            if (await _dbContext.SaveAsync(mainDetailFromRepo))
            {
                var updateMainDetailEncrypted = _encryptService.MainDetailsEncrypt(mainDetailFromRepo);
                var returnMapped = _mapper.Map<MainDetailForUpdateDto>(updateMainDetailEncrypted);
                return Ok(returnMapped);
            }
            else
            {
                return BadRequest("در سمت سرور خطایی بوجود آمده است.");
            }
        }

        [HttpPut("track/{userId}")]
        [ServiceFilter(typeof(UserCheckTokenFilter))]
        public async Task<IActionResult> UpdateTrackDetail(int userId, TrackDetailForUpdateDto updateDto)
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

            var trackDetailFromRepo = await _dbContext.TrackDetailsRepository
                .GetManyAsync(p => p.UserId == userId,
            q => q.OrderByDescending(o => o.UpdateTime), "User");
            var firstElement = trackDetailFromRepo.FirstOrDefault();
            if (firstElement == null)
            {
                return NoContent();
            }

            firstElement.PersianDate = updateDto.PersianDate;
            firstElement.TrackActivity = updateDto.TrackActivity;
            firstElement.TrackFood = updateDto.TrackFood;
            firstElement.TrackWeight = updateDto.TrackWeight;

            var updateDtoDecrypt = _encryptService.TrackDetailsDecrypt(firstElement);
            

            try
            {
                _dbContext.Detach(updateDtoDecrypt);
                _dbContext.TrackDetailsRepository.Update(updateDtoDecrypt);


                if (await _dbContext.SaveAsync(updateDtoDecrypt))
                {
                    var updateTrackDetails = _encryptService.TrackDetailsEncrypt(updateDtoDecrypt);
                    var returnMapped = _mapper.Map<TrackDetailForUpdateDto>(updateTrackDetails);
                    return Ok(returnMapped);
                }
                else
                {
                    return BadRequest("در سمت سرور خطایی بوجود آمده است.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
