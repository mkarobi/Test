using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFWebService.Common.ErrorAndMessage;
using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Dtos.Api.Auth;
using TFWebService.Presenter.Helper.Filter;
using TFWebService.Repo.Infrastructure;

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

        public UserController(IUnitOfWork<TFDbContext> dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // GET: Get All Users
        [ServiceFilter(typeof(UserCheckAdminFilter))]
        public async Task<ActionResult> Index()
        {
            var usersFromRepo = await _dbContext.UserRepository.GetAllAsync();
            if (usersFromRepo == null)
                return NoContent();
            return Ok(usersFromRepo);
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
        
    }
}
