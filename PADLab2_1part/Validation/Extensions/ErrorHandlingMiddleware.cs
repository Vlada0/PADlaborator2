using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PADLab2_1part.Validation.Extensions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
            }
            catch(ResourseAlreadyExistException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict);
            }
            catch(Exception e)
            {
               await HandleExceptionAsync(context, e, HttpStatusCode.InternalServerError);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext ctx, Exception ex, HttpStatusCode statusCode)
        {
            var response = ctx.Response;
           // response.ContentType = Consts.AppProblemPlusJsonContentType;
           
            response.StatusCode = (int)statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                StatusCode = (int)statusCode,
                Error = ex.Message
            }));
        }
    
    }
}
