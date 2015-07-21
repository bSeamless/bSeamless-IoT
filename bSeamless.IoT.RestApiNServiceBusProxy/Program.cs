using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using bSeamless.IoT.RestApiNServiceBusProxy;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Proxy))]

namespace bSeamless.IoT.RestApiNServiceBusProxy
{

    class Program
    {
        static string _proxyAddress = @"http://*:8080/"; 

        static void Main(string[] args)
        {
            // Need lots of outgoing connections and hang on to them
            ServicePointManager.DefaultConnectionLimit = 20;
            ServicePointManager.MaxServicePointIdleTime = 10000;
            //send packets as soon as you get them
            ServicePointManager.UseNagleAlgorithm = false;
            //send both header and body together
            ServicePointManager.Expect100Continue = false;

            Proxy.Start(_proxyAddress);

            Console.ReadLine();

            Proxy.Stop();
        }
    }
}
