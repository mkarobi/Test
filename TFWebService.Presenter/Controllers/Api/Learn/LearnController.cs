using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TFWebService.Common.ErrorAndMessage;
using TFWebService.Common.Helper;
using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Dtos.Api.Learn;
using TFWebService.Data.Models;
using TFWebService.Presenter.Helper.Filter;
using TFWebService.Repo.Infrastructure;
using TFWebService.Services.Upload.Interface;

namespace TFWebService.Presenter.Controllers.Api.Learn
{
    public class LearnController : Controller
    {
        private readonly IUnitOfWork<TFDbContext> _dbContext;
        private readonly IUploadService _uploadService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public LearnController(IUnitOfWork<TFDbContext> dbContext,
            IUploadService uploadService,
            IWebHostEnvironment env,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _uploadService = uploadService;
            _env = env;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            return NotFound();
        }

        [HttpGet("Food")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFoodData()
        {
            var foodsFromRepo = await _dbContext.FoodsCaloriesRepository.GetAllAsync();
            var foods = foodsFromRepo.OrderByDescending(q => q.UpdateTime).ToList();
            return Ok(foods);
        }

        [HttpGet("Fitness")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFitnessData()
        {
            var fitnessFromRepo = await _dbContext.FitnessCaloriesRepository.GetAllAsync();
            var fitness = fitnessFromRepo.OrderByDescending(q => q.UpdateTime).ToList();
            return Ok(fitness);
        }

        [HttpPost("Food")]
        [ServiceFilter(typeof(UserCheckAdminFilter))]
        public async Task<IActionResult> AddFoodData([FromForm] FoodForAddDto food)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var uploadRes = await _uploadService.UploadPic(
                food.File,
                userId,
                _env.WebRootPath,
                string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.Value, Request.PathBase),
                EnumCategoryFilesPath.Foods);
            if (uploadRes.Status)
            {
                food.PicUrl = uploadRes.Url;
            }
            else
            {
                return BadRequest("Upload Failed");
            }
            var mapped = _mapper.Map<FoodsCalories>(food);
            await _dbContext.FoodsCaloriesRepository.InserAsync(mapped);
            if (await _dbContext.SaveAsync())
                return Ok(mapped);
            else
                return BadRequest(new ReturnMessage()
                {
                    code = "500",
                    message = "Error Save Food",
                    Status = false,
                    title = "Error Save Food"
                });
        }

        [HttpPost("Fitness")]
        [ServiceFilter(typeof(UserCheckAdminFilter))]
        public async Task<IActionResult> AddFitnessData([FromForm] FitnessForAddDto fitness)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var uploadRes = await _uploadService.UploadPic(
                fitness.File,
                userId,
                _env.WebRootPath,
                string.Format("{0}://{1}{2}", Request.Scheme, Request.Host.Value, Request.PathBase),
                EnumCategoryFilesPath.Fitness);
            if (uploadRes.Status)
            {
                fitness.PicUrl = uploadRes.Url;
            }
            else
            {
                return BadRequest("Upload Failed");
            }
            var mapped = _mapper.Map<FitnessCalories>(fitness);
            await _dbContext.FitnessCaloriesRepository.InserAsync(mapped);
            if (await _dbContext.SaveAsync())
                return Ok(mapped);
            else
                return BadRequest(new ReturnMessage()
                {
                    code = "500",
                    message = "Error Save Fitness",
                    Status = false,
                    title = "Error Save Fitness"
                });
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(UserCheckAdminFilter))]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var foodFromRepo = await _dbContext.FoodsCaloriesRepository.GetByIdAsync(id);
            _dbContext.FoodsCaloriesRepository.Delete(foodFromRepo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(UserCheckAdminFilter))]
        public async Task<IActionResult> DeleteFitness(int id)
        {
            var fitFromRepo = await _dbContext.FitnessCaloriesRepository.GetByIdAsync(id);
            _dbContext.FitnessCaloriesRepository.Delete(fitFromRepo);
            return NoContent();
        }

    }
}
