using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Allure.Core.Exceptions;
using AutoMapper;

namespace Allure.Web.Filters
{
    public class GenericExceptionFilter : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            IHttpActionResult result;
            var request = actionExecutedContext.Request;
            var exception = actionExecutedContext.Exception;

            if (exception is NotFoundException)
            {
                result = new NotFoundResult(request);
            }
            else if (exception is ConflictException || exception is DbUpdateException)
            {
                result = new ConflictResult(request);
            }
            else if (exception is ForbiddenException)
            {
                result = new StatusCodeResult(HttpStatusCode.Forbidden, request);
            }
            else if (exception is ValidationException || exception is AutoMapperMappingException)
            {
                result = new BadRequestResult(request);
            }
            else
            {
                result = new InternalServerErrorResult(request);
            }

            actionExecutedContext.Response = await result.ExecuteAsync(cancellationToken).ConfigureAwait(false);
            actionExecutedContext.Response.Content = new StringContent(exception.Message);
        }
    }
}