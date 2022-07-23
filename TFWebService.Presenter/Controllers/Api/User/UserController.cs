using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFWebService.Common.ErrorAndMessage;
using TFWebService.Common.Helper;
using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Dtos.Api.Auth;
using TFWebService.Data.Dtos.Api.User;
using TFWebService.Presenter.Helper.Filter;
using TFWebService.Repo.Infrastructure;
using TFWebService.Services.Common.Encrypt.Interface;

namespace TFWebService.Presenter.Controllers.Api.User
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "WebService")]
    [Route("Api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork<TFDbContext> _dbContext;
        private readonly IMapper _mapper;
        private readonly IEncryptService _encryptService;

        public UserController(IUnitOfWork<TFDbContext> dbContext, IMapper mapper,IEncryptService encryptService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _encryptService = encryptService;
        }

        // GET: Get All Users
        [ServiceFilter(typeof(UserCheckAdminFilter))]
        public async Task<ActionResult> Index()
        {
            var usersFromRepo = await _dbContext.UserRepository.GetAllAsync();
            if (usersFromRepo == null)
                return NoContent();
            var user = usersFromRepo.OrderByDescending(q => q.UpdateTime).ToList();
            return Ok(user);
        }


        // Get : Get User with Id
        [HttpGet("{id}")]
        [ServiceFilter(typeof(UserCheckAdminFilter))]
        public async Task<ActionResult> Index(int id)
        {
            var userFromRepo = await _dbContext.UserRepository.GetByIdAsync(id);
            if (userFromRepo == null)
                return NoContent();
            var mapped = _mapper.Map<UserForDetailDto>(userFromRepo);
            return Ok(mapped);
        }

        //Get : Get UserInfo With That's Id 
        [HttpGet("{userId}")]
        [ServiceFilter(typeof(UserCheckTokenFilter))]
        public async Task<IActionResult> GetUserInfo(int userId)
        {
            var userFromRepo = await _dbContext.UserRepository.GetByIdAsync(userId);
            if (userFromRepo == null)
                return BadRequest("خطایی بوجود آمده است. دوباره تلاش کنید.");

            var userEncrypt = _encryptService.UserEncrypt(userFromRepo);
            var mapped = _mapper.Map<Data.Models.User, UserForDetailDto>(userEncrypt);
            return Ok(mapped);
        }

        [HttpPut("{userId}")]
        [ServiceFilter(typeof(UserCheckTokenFilter))]
        public async Task<IActionResult> UpdateUser(int userId, UserForUpdateDto userForUpdateDto)
        {
            var userFromRepo = await _dbContext.UserRepository.GetByIdAsync(userId);
            if (userFromRepo == null)
                return BadRequest("خطایی بوجود آمده است. دوباره تلاش کنید.");

            if (ModelState.IsValid)
            {
                var userParam = _encryptService.UpdateUserDecrypt(userForUpdateDto);

                userFromRepo.Name = userParam.Name;
                userFromRepo.Email = userParam.Email;
                userFromRepo.PhoneNumber = userParam.PhoneNumber;
                userFromRepo.Address = userParam.Address;
                userFromRepo.City = userParam.City;
                userFromRepo.Gender = userParam.Gender;
                userFromRepo.DateOfBirth = userParam.DateOfBirth;

                _dbContext.UserRepository.Update(userFromRepo);
                if (await _dbContext.SaveAsync())
                {
                    var userEncrypt = _encryptService.UserEncrypt(userFromRepo);
                    var mapped = _mapper.Map<Data.Models.User, UserForDetailDto>(userEncrypt);
                    return Ok(mapped);
                }
                else
                {
                    return BadRequest(new ReturnMessage()
                    {
                        code = "500",
                        message = "Error update User",
                        Status = false,
                        title = "Error update User"
                    });
                }
            }
            else
            {
                return BadRequest("پارامترهای ارسال شده نامعتبر است.");
            }
        }
        

    }
}
