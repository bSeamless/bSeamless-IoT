using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace bSeamless.IoT.API.REST.Proxy.ConsoleHost
{
    public class RequestService
    {
        private IBusControl _busControl;
        private BusHandle _busHandle;
        private static string _baseurl = "rabbitmq://API.IOT.BSEAMLESS.COM/IoT";
        private IRabbitMqHost _host;

        public void Start()
        {
            _busControl = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                _host = x.Host(new Uri(_baseurl), h =>
                {
                    h.Username("iot");
                    h.Password("iot");
                });
                x.ReceiveEndpoint(_host, "rest_requests", e => { e.Consumer<RequestConsumer>(); });
        });
            _busHandle = _busControl.Start();
        }

        public void Stop()
        {
            if (_busHandle != null) _busHandle.Stop(TimeSpan.FromSeconds(30));
        }
    }
}
