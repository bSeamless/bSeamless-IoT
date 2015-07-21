using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bSeamless.IoT.API.REST.Proxy.Interfaces;
using bSeamless.IoT.API.REST.Proxy.OWIN;
using bSeamless.IoT.Messaging.WebRequestGateway.Interfaces;
using bSeamless.IoT.Messaging.WebRequestGateway.MassTransit;
using Ninject;

namespace bSeamless.IoT.API.REST.Proxy.ConsoleHost
{
    class Program
    {
        private static IKernel _kernel;

        static void Main(string[] args)
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IRestApiProxyHost>().To<OwinRestApiProxyHost>().InSingletonScope();
            _kernel.Bind<IWebRequestGateway>().To<MassTransitWithRabbitMQ>();

            var host = _kernel.Get<IRestApiProxyHost>();
            host.Start();

            var rs = new RequestService();
            rs.Start();

            Console.ReadLine();
            host.Stop();
            rs.Stop();
        }
    }
}
