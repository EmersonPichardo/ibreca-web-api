using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;

namespace ibreca_web_api
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidateUserTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            StringValues token;
            if (!context.HttpContext.Request.Headers.TryGetValue("Token", out token))
            {
                context.Result = new BadRequestResult();
                return;
            }

            Guid tokenGuid;
            if (string.IsNullOrWhiteSpace(token) || !Guid.TryParse(token, out tokenGuid))
            {
                context.Result = new BadRequestResult();
                return;
            }

            ContentResult contentResult = (ContentResult)LoginApi.Instance.IsTokenValid(token).Result.Result;

            if (contentResult.StatusCode == 200)
            {
                bool isValid = JsonConvert.DeserializeObject<bool>(contentResult.Content);

                if (!isValid)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
            else
            {
                context.Result = contentResult;
                return;
            }
        }
    }
}
