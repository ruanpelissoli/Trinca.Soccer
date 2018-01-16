using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Trinca.Soccer.API.Controllers
{
    public class BaseController : ApiController
    {
        protected IHttpActionResult TryCatch(Func<IHttpActionResult> _try)
        {
            try
            {
                return _try();
            }
            catch (Exception ex)
            {
                return Catch(ex);
            }
        }

        protected async Task<IHttpActionResult> TryCatchAsync(Func<Task<IHttpActionResult>> _try)
        {
            try
            {
                return await _try();
            }
            catch (Exception ex)
            {
                return Catch(ex);
            }
        }

        protected IHttpActionResult Catch(Exception exception)
        {
            var properties = exception.Data.Keys.OfType<string>().ToDictionary(key => key, key => exception.Data[key] as string);
            IHttpActionResult actionResult;
            //if (exception is BaseException)
            //{
            //    BaseException baseException = exception as BaseException;
            //    var statusCode = baseException.MessageCode.StatusCode();
            //    properties.Add("httpStatusCode", statusCode.ToString());
            //    actionResult = Content(statusCode, baseException.MessageCode);
            //}
            //else
            //{
            //    actionResult = InternalServerError(exception);
            //}
            actionResult = InternalServerError(exception);
            return actionResult;
        }

    }
}
