using System;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Filters;

namespace CatalogAPI.Filters
{
	public class InvalidAddressExceptionFilter:IActionFilter,IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is InvalidAddressException ex)
                context.Result = new ObjectResult(ex.Message)
                {
                    StatusCode = StatusCodes.Status409Conflict
                };
            context.ExceptionHandled = true;
        }
    }
}

