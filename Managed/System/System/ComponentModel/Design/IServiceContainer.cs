using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a container for services.</summary>
	// Token: 0x02000338 RID: 824
	[ComVisible(true)]
	public interface IServiceContainer : IServiceProvider
	{
		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter. </param>
		// Token: 0x060019F9 RID: 6649
		void AddService(Type serviceType, object serviceInstance);

		/// <summary>Adds the specified service to the service container, and optionally promotes the service to any parent service containers.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter. </param>
		/// <param name="promote">true to promote this request to any parent service containers; otherwise, false. </param>
		// Token: 0x060019FA RID: 6650
		void AddService(Type serviceType, object serviceInstance, bool promote);

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="callback">A callback object that is used to create the service. This allows a service to be declared as available, but delays the creation of the object until the service is requested. </param>
		// Token: 0x060019FB RID: 6651
		void AddService(Type serviceType, ServiceCreatorCallback callback);

		/// <summary>Adds the specified service to the service container, and optionally promotes the service to parent service containers.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="callback">A callback object that is used to create the service. This allows a service to be declared as available, but delays the creation of the object until the service is requested. </param>
		/// <param name="promote">true to promote this request to any parent service containers; otherwise, false. </param>
		// Token: 0x060019FC RID: 6652
		void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote);

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove. </param>
		// Token: 0x060019FD RID: 6653
		void RemoveService(Type serviceType);

		/// <summary>Removes the specified service type from the service container, and optionally promotes the service to parent service containers.</summary>
		/// <param name="serviceType">The type of service to remove. </param>
		/// <param name="promote">true to promote this request to any parent service containers; otherwise, false. </param>
		// Token: 0x060019FE RID: 6654
		void RemoveService(Type serviceType, bool promote);
	}
}
