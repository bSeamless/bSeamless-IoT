﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using bSeamless.IoT.API.REST.Proxy.Interfaces;
using bSeamless.IoT.Messaging.WebRequestGateway.Interfaces;

namespace bSeamless.IoT.API.REST.Proxy.OWIN
{
    public class OwinRestApiProxyHost : IRestApiProxyHost
    {
        private const string _address = @"http://*:8080/";
        private Proxy _proxy;

        public OwinRestApiProxyHost(IWebRequestGateway webRequestGateway)
        {
            Proxy.WebRequestGateway = webRequestGateway;
        }

        public void Start()
        {
            ServicePointManager.DefaultConnectionLimit = 20;
            ServicePointManager.MaxServicePointIdleTime = 10000;
            ServicePointManager.UseNagleAlgorithm = false;
            ServicePointManager.Expect100Continue = false;

            Proxy.Start(_address);
        }

        public void Stop()
        {
            Proxy.Stop();
        }
    }
}
