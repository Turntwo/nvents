using System;
using System.Configuration;
using Microsoft.Owin.Hosting;

namespace Nvents.SignalR
{
    internal class NventsSignalRWindowsService
    {
        private IDisposable webApp;

        #region Service Methods

        public async void Start()
        {
            string url = ConfigurationManager.AppSettings["SignalRServerAddress"];
            webApp = WebApp.Start(url);
        }

        public void Stop()
        {
            webApp.Dispose();
        }

        #endregion Service Methods
    }
}
