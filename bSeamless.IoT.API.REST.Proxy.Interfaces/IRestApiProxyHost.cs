using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bSeamless.IoT.API.REST.Proxy.Interfaces
{
    public interface IRestApiProxyHost
    {
        void Start();
        void Stop();
    }
}
