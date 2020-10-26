using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ServiceTemplate.Filters
{
    public class AuthorizationFailureResult: IHttpActionResult
    {
        public AuthorizationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public string ReasonPhrase { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        { 
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            response.RequestMessage = Request;
            response.ReasonPhrase = ReasonPhrase;
            response.Content = new StringContent("HTTP " + (int)(response.StatusCode) + " : " + ReasonPhrase, System.Text.Encoding.UTF8);
            return Task.FromResult(response);
        }
    }
}