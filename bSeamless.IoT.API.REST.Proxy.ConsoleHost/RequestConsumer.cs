using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace bSeamless.IoT.API.REST.Proxy.ConsoleHost
{
    public class RequestConsumer : IConsumer<string>
    {
        public async Task Consume(ConsumeContext<string> context)
        {
            Console.WriteLine("Yeah, I got a message");
            await context.RespondAsync("Back at ya!");
        }
    }
}
