using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.WebCore.FilterExtend
{
    public class CustomActionCacheFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("Cache-Control", "public,max-age=6000");
            Console.WriteLine($"This {nameof(CustomActionCacheFilterAttribute)} OnActionExecuted{Order}");
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This {nameof(CustomActionCacheFilterAttribute)} OnActionExecuting{Order}");
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"This {nameof(CustomActionCacheFilterAttribute)} OnResultExecuting{Order}");
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine($"This {nameof(CustomActionCacheFilterAttribute)} OnResultExecuted{Order}");
        }
    }

}
