using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNet.SignalR.Client;
using Nvents.Services;

namespace Nvents.SignalR
{
    public class SignalRService : ServiceBase
    {
        private IHubProxy nventsHubProxy;
        public HubConnection HubConnection { get; private set; }

        public SignalRService(bool autoStart = true) : base(autoStart)
        {
            if (autoStart)
                Start();
        }

        protected override void OnStart()
        {
            base.OnStart();
            HubConnection = new HubConnection(ConfigurationManager.AppSettings["SignalRServerAddress"]);
            nventsHubProxy = HubConnection.CreateHubProxy("NventsHub");
            nventsHubProxy.On<byte[]>("HandleNvent", ProcessNvent);
            HubConnection.Start().Wait();
        }

        protected override void OnStop()
        {
            base.OnStop();
            HubConnection.Dispose();
        }

        public override void Publish<TEvent>(TEvent e)
        {
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, e);
                nventsHubProxy.Invoke("Publish", stream.ToArray());
            }
        }

        private void ProcessNvent(byte[] nventData)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new MemoryStream(nventData))
            {
                object nvent = formatter.Deserialize(stream);
                foreach (var registration in registrations.Where(x => ShouldEventBeHandled(x, nvent)).ToList())
                    registration.Action.Invoke(nvent);
            }
        }
    }
}
