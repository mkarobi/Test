using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TFWebService.Common.ErrorAndMessage;
using TFWebService.Data.DatabaseContext;
using TFWebService.Data.Dtos.Api.Auth;
using TFWebService.Data.Models;
using TFWebService.Repo.Infrastructure;
using TFWebService.Services.Site.Admin.Auth.Interface;

namespace TFWebService.Presentation.Controllers.Site.Admin
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "WebService")]
    [Route("Api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork<TFDbContext> _db;
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IUnitOfWork<TFDbContext> dbContext, IAuthService authService, IMapper mapper, IConfiguration config)
        {
            _db = dbContext;
            _authService = authService;
            _config = config; 
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _db.UserRepository.UserExists(userForRegisterDto.UserName))
                return BadRequest(new ReturnMessage()
                {
                    Status = false,
                    title = "خطا",
                    message = "نام کاربری وجود دارد"
                });

            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName,
                Email = userForRegisterDto.UserName,
                Name = userForRegisterDto.Name,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                Address = "",
                City = "",
                Gender = true,
                DateOfBirth = DateTime.Now.ToString(),
                IsAcive = true,
                Status = true,
                IsAdmin = false
            };

            var createdUser = await _authService.Register(userToCreate, userForRegisterDto.Password);

            if (createdUser != null)
            {
                string token = CreateToken(createdUser.Id.ToString(), createdUser.UserName);
                return Ok(new
                {
                    userToCreate.Id,
                    token
                });
            }
            else
            {
                return StatusCode(500);
            }

        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> Login (UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _authService.Login(userForLoginDto.UserName, userForLoginDto.Password);

            if(userFromRepo == null)
                return Unauthorized(new ReturnMessage()
                {
                    Status = false,
                    title = "خطا",
                    message = "کاربری با این یوزر و پس وجود ندارد"
                });


            string token = CreateToken(userFromRepo.Id.ToString(), userFromRepo.UserName);

            //var user = _mapper.Map<UserForDetailDto>(userFromRepo);

            return Ok(new
            {
                userFromRepo.Id,
                token
            });
        }

        private string CreateToken(string id,string username)
        {
            var Claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,id),
                new Claim(ClaimTypes.Name,username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDes);

            return "Bearer "+ tokenHandler.WriteToken(token); 

        }

    }
}
