using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TFWebService.Data.DatabaseContext;
using TFWebService.Repo.Infrastructure;

namespace TFWebService.Presenter.Helper.Filter
{
    public class UserCheckTokenFilter : ActionFilterAttribute
    {
        private readonly IHttpContextAccessor _httpContextAcc;
        private readonly IUnitOfWork<TFDbContext> _dbContext;

        public UserCheckTokenFilter(IHttpContextAccessor httpContextAcc,
            IUnitOfWork<TFDbContext> dbContext)
        {
            _httpContextAcc = httpContextAcc;
            this._dbContext = dbContext;
        }

        public async override void OnActionExecuting(ActionExecutingContext context)
        {
            int userId = int.Parse(context.RouteData.Values["userId"].ToString());
            int userIdFromToken = int.Parse(_httpContextAcc.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (userId == userIdFromToken)
            {
                base.OnActionExecuting(context);
            }
            else
            {
                var result = new ObjectResult(new
                {
                    Error = "شما به این سرویس دسترسی ندارید."
                });
                result.StatusCode = 401;
                context.Result = result;
            }
        }
    }
}
