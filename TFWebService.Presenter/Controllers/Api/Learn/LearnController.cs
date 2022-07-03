using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Dtos.Api.Learn;
using TFWebService.Presenter.Helper.Filter;
using TFWebService.Repo.Infrastructure;
using TFWebService.Services.Upload.Interface;

namespace TFWebService.Presenter.Controllers.Api.Learn
{
    public class LearnController : Controller
    {
        private readonly IUnitOfWork<TFDbContext> _dbContext;
        private readonly IUploadService _uploadService;

        public LearnController(IUnitOfWork<TFDbContext> dbContext,
            IUploadService uploadService)
        {
            _dbContext = dbContext;
            _uploadService = uploadService;
        }


        public IActionResult Index()
        {
            return NoContent();
        }

        [HttpGet("Food")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFoodData()
        {
            var foodsFromRepo = await _dbContext.FoodsCaloriesRepository.GetAllAsync();
            return Ok(foodsFromRepo);
        }

        [HttpGet("Fitness")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFitnessData()
        {
            var fitnessFromRepo = await _dbContext.FitnessCaloriesRepository.GetAllAsync();
            return Ok(fitnessFromRepo);
        }

        [HttpPost("Food")]
        [ServiceFilter(typeof(UserCheckAdminFilter))]
        public async Task<IActionResult> AddFoodData([FromForm]FoodForAddDto food)
        {
            var uploadRes = await _uploadService.
        }
    }
}
