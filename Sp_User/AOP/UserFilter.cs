
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using UserModel;
using System.Text;
using java.lang;

namespace Sp_User.AOP
{

    public class UserFilter : Attribute, IActionFilter, IAuthorizationFilter, IResourceFilter, IExceptionFilter, IResultFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine(context);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine(context);
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Console.WriteLine(context);
        }

        public void OnException(ExceptionContext context)
        {
            StringBuffer sb = new StringBuffer();
            TData data = new TData();
            data.Tag = 0;
            foreach (var item in context.RouteData.Values.Values) {
                sb.append(item.ToString());
            }
            data.Message = context.Exception.Message+"--"+sb.toString();
            context.Result = new JsonResult(data);

        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
       
   
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine(context);
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine(context);
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            //特性资源异常
            if (context.Result is BadRequestObjectResult && (context.Result as BadRequestObjectResult).Value is ValidationProblemDetails)
            {
                ValidationProblemDetails obj = (context.Result as BadRequestObjectResult).Value as ValidationProblemDetails;
                if (obj != null && obj.Errors.Count > 0)
                {
                    TData data = new TData();
                    data.Tag = 0;
               StringBuffer sb = new StringBuffer();

                    foreach (var item in obj.Errors)
                    {
                        foreach (var itemTwo in item.Value)
                        {
                            sb.append(itemTwo + "  ");
                        }
                    }
                    data.Message = sb.toString();
                    context.Result = new JsonResult(data);
                }
            }
        }

   
    }
}
