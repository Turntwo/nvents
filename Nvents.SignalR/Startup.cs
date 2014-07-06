using Owin;

namespace Nvents.SignalR
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
