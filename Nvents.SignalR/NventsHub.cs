using System;
using Microsoft.AspNet.SignalR;

namespace Nvents.SignalR
{
    public class NventsHub : Hub
    {
        public void Publish(Type eventType, string eventJson)
        {
            Clients.All.Receive(eventType, eventJson);
        }
    }
}
