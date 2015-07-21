using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bSeamless.IoT.RestApiNServiceBusProxy
{
    public class NServiceBusDelegator : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var serialized = new HttpMessageContent(request).ReadAsByteArrayAsync().Result;

            var ms = new MemoryStream(serialized);
            var request2 = new HttpRequestMessage();
            request2.Content = new ByteArrayContent(ms.ToArray());
            request2.Content.Headers.Add("Content-Type", "application/http;msgtype=request");
            var r3 = request2.Content.ReadAsHttpRequestMessageAsync(cancellationToken).Result;

            Console.WriteLine(request.ToString());
            Console.WriteLine(r3.ToString());

            Console.WriteLine(r3.Content.ReadAsStringAsync().Result);

            var response = new HttpResponseMessage(HttpStatusCode.Accepted);
            return Task.FromResult(response);
        }
    }
}
