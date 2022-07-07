using System;
using System.Collections.Generic;


namespace Microsoft.Maui.Handlers.Memory
{
	public class MemoryTestFixture : IDisposable
	{
		Dictionary<Type, (WeakReference handler, WeakReference view)> _handlers
			= new Dictionary<Type, (WeakReference handler, WeakReference view)>();

		public MemoryTestFixture()
		{
		}

		public void AddReferences(Type handlerType, (WeakReference handler, WeakReference view) value) =>
			_handlers.Add(handlerType, value);

		public bool DoReferencesStillExist(Type handlerType)
		{
			WeakReference weakHandler;
			WeakReference weakView;
			(weakHandler, weakView) = _handlers[handlerType];


			if (weakHandler.Target != null ||
				weakHandler.IsAlive ||
				weakView.Target != null ||
				weakHandler.IsAlive)
			{
				return true;
			}

			return false;
		}

		public void Dispose()
		{
			_handlers.Clear();
		}
	}
}