using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TFWebService.Data.DatabaseContext;
using TFWebService.Repo.Infrastructure;

namespace TFWebService.Presenter.Helper.Filter
{
    public class UserCheckAdminFilter : ActionFilterAttribute
    {
        private readonly IHttpContextAccessor _httpContextAcc;
        private readonly IUnitOfWork<TFDbContext> _dbContext;

        public UserCheckAdminFilter(IHttpContextAccessor httpContextAcc,
            IUnitOfWork<TFDbContext> dbContext)
        {
            _httpContextAcc = httpContextAcc;
            this._dbContext = dbContext;
        }

        public async override void OnActionExecuting(ActionExecutingContext context)
        {            
            //int id = int.Parse(context.RouteData.Values["userId"].ToString());
            int id = int.Parse(_httpContextAcc.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userFromRepo = await _dbContext.UserRepository.GetByIdAsync(id);
            if (userFromRepo != null)
            {
                if (userFromRepo.IsAdmin)
                {
                    base.OnActionExecuting(context);
                }
                else
                {
                    var result = new ObjectResult(new
                    {
                        Error = "شما دسترسی لازم را ندارید."
                    });
                    result.StatusCode = 401;
                    context.Result = result;
                }
            }
            else
            {
                var result = new ObjectResult(new
                {
                    Error = "توکن شما نا معتبر است."
                });
                result.StatusCode = 401;
                context.Result = result;
            }
        }
    }
}
