using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http.Headers;

namespace MeetApp.MessageHandlers
{
    /// <summary>
    /// Controller'a parametre olarak gönderilen verilerin JSON formatýnda olmasýný garanti eder.
    /// </summary>
    public sealed class ContentTypeHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            MediaTypeHeaderValue ct = null;
            string contentType = null;

            if ((ct = request.Content.Headers.ContentType) != null)
            {
                contentType = ct.MediaType;
            }

            if ((request.Method == HttpMethod.Post) &&
                (contentType == null || string.Compare(contentType, "application/json", true) != 0))
            {

                if (!request.Content.IsMimeMultipartContent())
                {
                    HttpResponseMessage forbiddenResponse = request.CreateResponse(HttpStatusCode.Forbidden);
                    forbiddenResponse.ReasonPhrase = "Forbidden (Content type must be application/json)";
                    forbiddenResponse.Content = new StringContent("Forbidden (Content type must be application/json)");

                    return Task.FromResult<HttpResponseMessage>(forbiddenResponse);
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
