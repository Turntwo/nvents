using System;
using Nvents.Services;
using Nvents.SignalR;
using Xunit;

namespace Nvents.Tests
{
	public class InMemoryServiceTests
	{
		[Fact]
		public void SignalRCanPublishEvent()
		{
			var raised = false;
			Events.Subscribe<FooEvent>(e => raised = true);

			Test.WaitFor(() => raised, TimeSpan.FromSeconds(1), () => Events.Publish(new FooEvent()));

			Assert.True(raised, "FooEvent was not raised");
		}

		public InMemoryServiceTests()
		{
		    var host = new NventsSignalRWindowsService();
            host.Start();
			Events.Service = new SignalRService();
		}
	}
}
