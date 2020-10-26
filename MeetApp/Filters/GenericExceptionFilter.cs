using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace ServiceTemplate.Filters
{
    /// <summary>
    /// Her türlü beklenmeyen istisnayı yakalayan filtre!
    /// </summary>
    public sealed class GenericExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var ex = actionExecutedContext.Exception;
            var doc = new BsonDocument()
            {
                { "Error", ex.ToString() },
                { "ErrorDescription", ex.Message }
            };

            var ms = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            ms.Content = new StringContent(doc.ToJson(), System.Text.Encoding.UTF8, "application/json");
            ms.ReasonPhrase = "Error";

            actionExecutedContext.Response = ms;
        }
    }
}