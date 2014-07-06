using System;
using System.Configuration;
using System.Linq;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Nvents.Services;

namespace Nvents.SignalR
{
    public class SignalRService : ServiceBase
    {
        private IHubProxy nventsHubProxy;
        public HubConnection HubConnection { get; private set; }
        protected override void OnStart()
        {
            base.OnStart();
            HubConnection = new HubConnection(ConfigurationManager.AppSettings["SignalRServerAddress"]);
            nventsHubProxy = HubConnection.CreateHubProxy("NventsHub");
            nventsHubProxy.On<Type, string>("Receive", ProcessNvent);
            HubConnection.Start().Wait();
        }

        protected override void OnStop()
        {
            base.OnStop();
            HubConnection.Dispose();
        }

        public override void Publish<TEvent>(TEvent e)
        {
            nventsHubProxy.Invoke("Publish", e.GetType(), JsonConvert.SerializeObject(e));
        }

        private void ProcessNvent(Type nventType, string nventJson)
        {
            object nvent = JsonConvert.DeserializeObject(nventJson, nventType);

            foreach (var registration in registrations.Where(x => ShouldEventBeHandled(x, nvent)).ToList())
                registration.Action.Invoke(nvent);
        }

    }
}
