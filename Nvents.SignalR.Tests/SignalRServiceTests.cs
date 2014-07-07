using System;
using Nvents.SignalR;
using Xunit;

namespace Nvents.Tests
{
	public class SignalRServiceTests
	{
		[Fact]
		public void SignalRCanPublishEvent()
		{
			var raised = false;
            Events.Subscribe<SerializableFooEvent>(e => raised = true);

            Test.WaitFor(() => raised, TimeSpan.FromSeconds(1), () => Events.Publish(new SerializableFooEvent()));

			Assert.True(raised, "FooEvent was not raised");
		}

        public SignalRServiceTests()
		{
		    var host = new NventsSignalRWindowsService();
            host.Start();
			Events.Service = new SignalRService();
		}
	}
}
