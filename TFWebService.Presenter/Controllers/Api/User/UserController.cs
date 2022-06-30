﻿using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFWebService.Common.ErrorAndMessage;
using TFWebService.Data.DatabaseContext;
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
        public async Task<ActionResult> Index()
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
                    var usersFromRepo = await _dbContext.UserRepository.GetAllAsync();
                    if (usersFromRepo == null)
                        return NoContent();
                    return Ok(usersFromRepo);
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


        // Get : Get User with Id
        [HttpGet("{id}")]
        public async Task<ActionResult> Index(int id)
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
                    var userFromRepo = await _dbContext.UserRepository.GetByIdAsync(id);
                    if (userFromRepo == null)
                        return NoContent();
                    return Ok(userFromRepo);
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
