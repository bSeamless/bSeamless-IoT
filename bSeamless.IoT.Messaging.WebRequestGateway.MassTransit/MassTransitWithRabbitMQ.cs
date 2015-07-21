using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using bSeamless.IoT.Messaging.WebRequestGateway.Interfaces;
using MassTransit;

namespace bSeamless.IoT.Messaging.WebRequestGateway.MassTransit
{
    public class MassTransitWithRabbitMQ : DelegatingHandler, IWebRequestGateway
    {
        private static IBusControl _bus;
        private static BusHandle _handle;
        private static string _baseurl = "rabbitmq://API.IOT.BSEAMLESS.COM/";

        static MassTransitWithRabbitMQ()
        {
            _bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                x.Host(new Uri(_baseurl), h =>
                {
                    h.Username("iot");
                    h.Password("iot");
                });
            });
            _handle = _bus.Start();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return ForwardAndGetResponseAsync(request, cancellationToken);
        }

        public async Task<HttpResponseMessage> ForwardAndGetResponseAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var serialized = new HttpMessageContent(request).ReadAsByteArrayAsync().Result;

            var ms = new MemoryStream(serialized);
            var request2 = new HttpRequestMessage();
            request2.Content = new ByteArrayContent(ms.ToArray());
            request2.Content.Headers.Add("Content-Type", "application/http;msgtype=request");
            var r3 = request2.Content.ReadAsHttpRequestMessageAsync().Result;

            var serviceAddress = new Uri(_baseurl + "IoT/rest_requests");
            var client = _bus.CreateRequestClient<string, string>(serviceAddress, TimeSpan.FromSeconds(50));
            var reply = await client.Request("HI!", cancellationToken);

            /*
            await endpoint.Send("HI!",

            Console.WriteLine(request.ToString());
            Console.WriteLine(r3.ToString());

            Console.WriteLine(r3.Content.ReadAsStringAsync().Result);
*/
            var response = new HttpResponseMessage(HttpStatusCode.Accepted);
            return response;
        }
    }
}
