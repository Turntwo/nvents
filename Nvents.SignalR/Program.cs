using System;
using System.Diagnostics;
using Topshelf;

namespace Nvents.SignalR
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Debugger.IsAttached)
            {
                HostFactory.Run(x =>
                {
                    x.Service<NventsSignalRWindowsService>(s =>
                    {
                        s.ConstructUsing(hs => new NventsSignalRWindowsService());
                        s.WhenStarted(tm => tm.Start());
                        s.WhenStopped(tm => tm.Stop());
                    });
                    x.SetDescription("Nvents SignalR Service");
                    x.SetDisplayName("Nvents SignalR Service");
                    x.SetServiceName("NventsSignalRService");
                    x.StartAutomaticallyDelayed();
                    x.EnableServiceRecovery(rc =>
                    {
                        rc.RestartService(1); // restart the service after 1 minute
                        rc.RestartService(1); // restart the service after 1 minute
                        rc.RestartService(1); // restart the service after 1 minute
                        rc.SetResetPeriod(1); // set the reset interval to one day
                    });
                });
            }
            else
            {
                // Run service in debug mode
                var service = new NventsSignalRWindowsService();
                service.Start();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                service.Stop();
            }
        }
    }
}
