using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Allure.Common;

namespace Allure.Web.Test
{
    public static class Extensions
    {
        public static bool IsOk(this IHttpActionResult result)
        {
            var type = result.GetType();
            return type == typeof(OkResult)
                || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(OkNegotiatedContentResult<>);
        }

        public static bool IsOk<T>(this IHttpActionResult result, out T data)
        {
            var r = result as OkNegotiatedContentResult<T>;

            if (r == null)
            {
                data = default(T);
                return false;
            }
            else
            {
                data = r.Content;
                return true;
            }
        }

        public static bool IsBadRequest(this IHttpActionResult result)
        {
            return result.GetType().In(typeof(BadRequestResult), typeof(BadRequestErrorMessageResult));
        }

        public static bool IsNotFound(this IHttpActionResult result)
        {
            return result.GetType() == typeof(NotFoundResult);
        }
    }
}
