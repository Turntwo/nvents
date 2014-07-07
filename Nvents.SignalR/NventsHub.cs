using System;
using Microsoft.AspNet.SignalR;

namespace Nvents.SignalR
{
    public class NventsHub : Hub
    {
        public void Publish(byte[] eventJson)
        {
            Clients.All.HandleNvent(eventJson);
        }
    }
}
