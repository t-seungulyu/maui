using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.DeviceTests;
using Microsoft.Maui.DeviceTests.Stubs;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Media;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;


namespace Microsoft.Maui.Handlers.Memory
{
	/// <summary>
	/// Trying to allocate and then rely on the GC to collect within the same test didn't really work.
	/// My theory here is that it's a threading issue. When a test is running XUnit is holding a thread for it.
	/// For example, if you call Thread.Sleep it will block the entire test run from moving forward whereas locally
	/// it's able to still run things in parallel. So, I broke the allocate and check allocate into two steps
	/// which seems to make Android and WinUI happier. 
	/// </summary>
	[TestCaseOrderer("Microsoft.Maui.Handlers.Memory.MemoryTestOrdering", "Microsoft.Maui.Core.DeviceTests")]
	public class MemoryTests : HandlerTestBase, IClassFixture<MemoryTestFixture>
	{
		MemoryTestFixture _fixture;
		public MemoryTests(MemoryTestFixture fixture)
		{
			_fixture = fixture;
		}

		[Theory]
		[ClassData(typeof(MemoryTestTypes))]
		public async Task Allocate((Type ViewType, Type HandlerType) data)
		{
			var handler = await InvokeOnMainThreadAsync(() => CreateHandler((IElement)Activator.CreateInstance(data.ViewType), data.HandlerType));
			WeakReference weakHandler = new WeakReference(handler);
			_fixture.AddReferences(data.HandlerType, (weakHandler, new WeakReference(handler.VirtualView)));
			handler = null;

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		[Theory]
		[ClassData(typeof(MemoryTestTypes))]
		public async Task CheckAllocation((Type ViewType, Type HandlerType) data)
		{
			await AssertionExtensions.Wait(() =>
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();
				GC.WaitForPendingFinalizers();

				if (_fixture.DoReferencesStillExist(data.HandlerType))
				{
					return false;
				}

				return true;

			}, 1000);

			if (_fixture.DoReferencesStillExist(data.HandlerType))
			{
				Assert.True(false, $"{data.HandlerType} failed to collect.");
			}
		}
	}
}