using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ServiceTemplate.Filters
{
    /// <summary>
    /// Çağırılan methodlara girilen bilgilerde sorun olup olmadığını kontrol eder.
    /// </summary>
    public sealed class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);

                //var doc = actionContext.ModelState.ToBsonDocument();
                //doc.Add("Error", "Hatalı model");

                //actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                //actionContext.Response.Content = new StringContent(doc.ToJson(), Encoding.UTF8, App.MIME_JSON);
            }
        }
    }
}
