using System;
using Nvents.Services;
using Xunit;

namespace Nvents.Tests
{
	public class InMemoryServiceTests
	{
		[Fact]
		public void CanPublishSignalREvent()
		{
			var raised = false;
			Events.Subscribe<FooEvent>(
				e => raised = true);

			Test.WaitFor(() => raised, TimeSpan.FromSeconds(1), () =>
				Events.Publish(new FooEvent()));

			Assert.True(raised, "FooEvent was not raised");
		}

		public InMemoryServiceTests()
		{
			Events.Service = new InMemoryService();
		}
	}
}
