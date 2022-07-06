using System;
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


namespace Microsoft.Maui.Handlers
{
	public class AlphabeticalOrderer : ITestCaseOrderer
	{
		public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
				where TTestCase : ITestCase
		{
			var result = testCases.ToList();
			result.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
			return result;
		}
	}

	[TestCaseOrderer("Microsoft.Maui.Handlers.AlphabeticalOrderer", "Microsoft.Maui.Core.DeviceTests")]
	public class MemoryTests : HandlerTestBase
	{
		public MemoryTests()
		{

		}

		static WeakReference weakHandler = null;
		static WeakReference weakView = null;

		[Fact(DisplayName = "Handlers Deallocate When No Longer Referenced")]
		public async Task BHandlersDeallocateWhenNoLongerReferenced()
		{
			await AssertionExtensions.Wait(() =>
			{
				if (weakHandler.Target != null || weakHandler.IsAlive)
					return false;

				if (weakView.Target != null || weakView.IsAlive)
					return false;

				return true;
			});

			if (weakHandler.Target != null || weakHandler.IsAlive)
				Assert.True(false, $"{typeof(DatePickerHandler)} failed to collect");

			if (weakView.Target != null || weakView.IsAlive)
				Assert.True(false, $"{typeof(IDatePicker)} failed to collect");

		}

		[Fact(DisplayName = "Handlers Deallocate When No Longer Referenced")]
		public async Task AHandlersDeallocateWhenNoLongerReferenced()
		{

			var oldHandle = new Java.Interop.JniObjectReference();
			Exception exc = null;
			TaskCompletionSource<bool> finished = new TaskCompletionSource<bool>();
			TaskCompletionSource<bool> innerLoopFinished = new TaskCompletionSource<bool>();

			Java.InteropTests.FinalizerHelpers.PerformNoPinAction(async () =>
			{
				Java.InteropTests.FinalizerHelpers.PerformNoPinAction(async () =>
				{
					try
					{
						var handler = await InvokeOnMainThreadAsync(() => CreateHandler<DatePickerHandler>(new DatePickerStub()) as IPlatformViewHandler);
						oldHandle = handler.PlatformView.PeerReference.NewWeakGlobalRef();
						weakHandler = new WeakReference((DatePickerHandler)handler);
						weakView = new WeakReference((DatePickerStub)handler.VirtualView);
						GC.KeepAlive(handler);
						GC.KeepAlive(handler.PlatformView);

						if (handler is DatePickerHandler dph)
							GC.KeepAlive(dph.DatePickerDialog);
					}
					finally
					{
						innerLoopFinished.SetResult(true);
					}
				});

				await innerLoopFinished.Task.WaitAsync(TimeSpan.FromSeconds(10));
				Java.Interop.JniEnvironment.Runtime.ValueManager.CollectPeers();
				finished.SetResult(true);
			});

			//Java.InteropTests.FinalizerHelpers.PerformNoPinAction(async () =>
			//{
			//	Java.InteropTests.FinalizerHelpers.PerformNoPinAction(async () =>
			//	{
			//		// Because this runs on a thread if it throws an exception
			//		// it will crash the whole process
			//		try
			//		{
			//			var handler = await CreateHandlerAsync(new TStub()) as IPlatformViewHandler;
			//			oldHandle = handler.PlatformView.PeerReference.NewWeakGlobalRef();
			//			weakHandler = new WeakReference((THandler)handler);
			//			weakView = new WeakReference((TStub)handler.VirtualView);
			//			GC.KeepAlive(handler);
			//			GC.KeepAlive(handler.PlatformView);
			//		}
			//		catch (Exception e)
			//		{
			//			exc = e;

			//			//if (!finished.TrySetException(e))
			//			//	throw;
			//		}
			//	});

			//	// Make this better
			//	await Task.Delay(5000);
			//	Java.Interop.JniEnvironment.Runtime.ValueManager.CollectPeers();
			//});

			//Java.InteropTests.FinalizerHelpers.PerformNoPinAction(() =>
			//{
			//	Java.InteropTests.FinalizerHelpers.PerformNoPinAction(() =>
			//	{
			//		Java.Interop.JniEnvironment.Runtime.ValueManager.CollectPeers();
			//	});

			//	Java.Interop.JniEnvironment.Runtime.ValueManager.CollectPeers();
			//	finished.TrySetResult(true);
			//});

			//Java.Interop.JniEnvironment.Runtime.ValueManager.CollectPeers();
			//GC.WaitForPendingFinalizers();



			//Java.InteropTests.FinalizerHelpers.PerformNoPinAction(async () =>
			//{
			//	// Because this runs on a thread if it throws an exception
			//	// it will crash the whole process
			//	try
			//	{
			//		var handler = await CreateHandlerAsync(new TStub()) as IPlatformViewHandler;
			//		oldHandle = handler.PlatformView.PeerReference.NewWeakGlobalRef();
			//		weakHandler = new WeakReference((THandler)handler);
			//		weakView = new WeakReference((TStub)handler.VirtualView);
			//	}
			//	catch (Exception e)
			//	{
			//		exc = e;
			//		finished.SetException(e);
			//	}
			//	finally
			//	{
			//		finished.SetResult(true);
			//	}
			//});

			await finished.Task.WaitAsync(TimeSpan.FromSeconds(30));

			if (weakView == null)
				Assert.True(false, $"Failed to Create handler. Exception: {exc}");

			await AssertionExtensions.Wait(() =>
			{
				Java.Interop.JniEnvironment.Runtime.ValueManager.CollectPeers();
				GC.WaitForPendingFinalizers();
				GC.WaitForPendingFinalizers();

				_ = InvokeOnMainThreadAsync(() =>
				{
					Java.Interop.JniEnvironment.Runtime.ValueManager.CollectPeers();
					GC.WaitForPendingFinalizers();
					GC.WaitForPendingFinalizers();
				});

				if (Java.Interop.JniRuntime.CurrentRuntime.ValueManager.PeekValue(oldHandle) != null)
					return false;

				return true;
			});

			//Assert.Null(Java.Interop.JniRuntime.CurrentRuntime.ValueManager.PeekValue(oldHandle));


			//Java.Interop.JniObjectReference.Dispose(ref oldHandle);
		}
	}
}