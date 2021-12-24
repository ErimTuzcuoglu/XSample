using System;
using System.Collections.Generic;
using Application.Orchestrator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Filter
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ArgumentException:
                    context.Result = new BadRequestObjectResult(new Response
                    {
                        Succeeded = false,
                        Errors = new List<string>{context.Exception.Message}
                    });
                    break;
                case InvalidOperationException:
                    context.Result = new NotFoundObjectResult(new Response
                    {
                        Succeeded = false
                    });
                    break;
                default: break;
            }
        }
    }
}