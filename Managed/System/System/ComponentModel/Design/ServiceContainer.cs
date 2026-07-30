using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a simple implementation of the <see cref="T:System.ComponentModel.Design.IServiceContainer" /> interface. This class cannot be inherited.</summary>
	// Token: 0x02000340 RID: 832
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ServiceContainer : IServiceContainer, IServiceProvider, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> class.</summary>
		// Token: 0x06001A23 RID: 6691 RVA: 0x000020EB File Offset: 0x000002EB
		public ServiceContainer()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> class using the specified parent service provider.</summary>
		/// <param name="parentProvider">A parent service provider. </param>
		// Token: 0x06001A24 RID: 6692 RVA: 0x0006A5A1 File Offset: 0x000687A1
		public ServiceContainer(IServiceProvider parentProvider)
		{
			this.parentProvider = parentProvider;
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x0006A5B0 File Offset: 0x000687B0
		private IServiceContainer Container
		{
			get
			{
				IServiceContainer serviceContainer = null;
				if (this.parentProvider != null)
				{
					serviceContainer = (IServiceContainer)this.parentProvider.GetService(typeof(IServiceContainer));
				}
				return serviceContainer;
			}
		}

		/// <summary>Gets the default services implemented directly by <see cref="T:System.ComponentModel.Design.ServiceContainer" />.</summary>
		/// <returns>The default services.</returns>
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x0006A5E3 File Offset: 0x000687E3
		protected virtual Type[] DefaultServices
		{
			get
			{
				return ServiceContainer._defaultServices;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x0006A5EA File Offset: 0x000687EA
		private ServiceContainer.ServiceCollection<object> Services
		{
			get
			{
				if (this.services == null)
				{
					this.services = new ServiceContainer.ServiceCollection<object>();
				}
				return this.services;
			}
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="serviceInstance">An instance of the service to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="serviceInstance" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06001A28 RID: 6696 RVA: 0x0006A605 File Offset: 0x00068805
		public void AddService(Type serviceType, object serviceInstance)
		{
			this.AddService(serviceType, serviceInstance, false);
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType" /> parameter. </param>
		/// <param name="promote">true if this service should be added to any parent service containers; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="serviceInstance" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06001A29 RID: 6697 RVA: 0x0006A610 File Offset: 0x00068810
		public virtual void AddService(Type serviceType, object serviceInstance, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.AddService(serviceType, serviceInstance, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (serviceInstance == null)
			{
				throw new ArgumentNullException("serviceInstance");
			}
			if (!(serviceInstance is ServiceCreatorCallback) && !serviceInstance.GetType().IsCOMObject && !serviceType.IsAssignableFrom(serviceInstance.GetType()))
			{
				throw new ArgumentException(global::SR.GetString("The service instance must derive from or implement {0}.", new object[] { serviceType.FullName }));
			}
			if (this.Services.ContainsKey(serviceType))
			{
				throw new ArgumentException(global::SR.GetString("The service {0} already exists in the service container.", new object[] { serviceType.FullName }), "serviceType");
			}
			this.Services[serviceType] = serviceInstance;
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="callback">A callback object that can create the service. This allows a service to be declared as available, but delays creation of the object until the service is requested. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="callback" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06001A2A RID: 6698 RVA: 0x0006A6D7 File Offset: 0x000688D7
		public void AddService(Type serviceType, ServiceCreatorCallback callback)
		{
			this.AddService(serviceType, callback, false);
		}

		/// <summary>Adds the specified service to the service container.</summary>
		/// <param name="serviceType">The type of service to add. </param>
		/// <param name="callback">A callback object that can create the service. This allows a service to be declared as available, but delays creation of the object until the service is requested. </param>
		/// <param name="promote">true if this service should be added to any parent service containers; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> or <paramref name="callback" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A service of type <paramref name="serviceType" /> already exists in the container.</exception>
		// Token: 0x06001A2B RID: 6699 RVA: 0x0006A6E4 File Offset: 0x000688E4
		public virtual void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.AddService(serviceType, callback, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (this.Services.ContainsKey(serviceType))
			{
				throw new ArgumentException(global::SR.GetString("The service {0} already exists in the service container.", new object[] { serviceType.FullName }), "serviceType");
			}
			this.Services[serviceType] = callback;
		}

		/// <summary>Disposes this service container.</summary>
		// Token: 0x06001A2C RID: 6700 RVA: 0x0006A769 File Offset: 0x00068969
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Disposes this service container.</summary>
		/// <param name="disposing">true if the <see cref="T:System.ComponentModel.Design.ServiceContainer" /> is in the process of being disposed of; otherwise, false.</param>
		// Token: 0x06001A2D RID: 6701 RVA: 0x0006A774 File Offset: 0x00068974
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				ServiceContainer.ServiceCollection<object> serviceCollection = this.services;
				this.services = null;
				if (serviceCollection != null)
				{
					foreach (object obj in serviceCollection.Values)
					{
						if (obj is IDisposable)
						{
							((IDisposable)obj).Dispose();
						}
					}
				}
			}
		}

		/// <summary>Gets the requested service.</summary>
		/// <returns>An instance of the service if it could be found, or null if it could not be found.</returns>
		/// <param name="serviceType">The type of service to retrieve. </param>
		// Token: 0x06001A2E RID: 6702 RVA: 0x0006A7E8 File Offset: 0x000689E8
		public virtual object GetService(Type serviceType)
		{
			object obj = null;
			Type[] defaultServices = this.DefaultServices;
			for (int i = 0; i < defaultServices.Length; i++)
			{
				if (serviceType.IsEquivalentTo(defaultServices[i]))
				{
					obj = this;
					break;
				}
			}
			if (obj == null)
			{
				this.Services.TryGetValue(serviceType, out obj);
			}
			if (obj is ServiceCreatorCallback)
			{
				obj = ((ServiceCreatorCallback)obj)(this, serviceType);
				if (obj != null && !obj.GetType().IsCOMObject && !serviceType.IsAssignableFrom(obj.GetType()))
				{
					obj = null;
				}
				this.Services[serviceType] = obj;
			}
			if (obj == null && this.parentProvider != null)
			{
				obj = this.parentProvider.GetService(serviceType);
			}
			return obj;
		}

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is null.</exception>
		// Token: 0x06001A2F RID: 6703 RVA: 0x0006A889 File Offset: 0x00068A89
		public void RemoveService(Type serviceType)
		{
			this.RemoveService(serviceType, false);
		}

		/// <summary>Removes the specified service type from the service container.</summary>
		/// <param name="serviceType">The type of service to remove. </param>
		/// <param name="promote">true if this service should be removed from any parent service containers; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serviceType" /> is null.</exception>
		// Token: 0x06001A30 RID: 6704 RVA: 0x0006A894 File Offset: 0x00068A94
		public virtual void RemoveService(Type serviceType, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.RemoveService(serviceType, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			this.Services.Remove(serviceType);
		}

		// Token: 0x04001492 RID: 5266
		private ServiceContainer.ServiceCollection<object> services;

		// Token: 0x04001493 RID: 5267
		private IServiceProvider parentProvider;

		// Token: 0x04001494 RID: 5268
		private static Type[] _defaultServices = new Type[]
		{
			typeof(IServiceContainer),
			typeof(ServiceContainer)
		};

		// Token: 0x04001495 RID: 5269
		private static TraceSwitch TRACESERVICE = new TraceSwitch("TRACESERVICE", "ServiceProvider: Trace service provider requests.");

		// Token: 0x02000341 RID: 833
		private sealed class ServiceCollection<T> : Dictionary<Type, T>
		{
			// Token: 0x06001A32 RID: 6706 RVA: 0x0006A913 File Offset: 0x00068B13
			public ServiceCollection()
				: base(ServiceContainer.ServiceCollection<T>.serviceTypeComparer)
			{
			}

			// Token: 0x04001496 RID: 5270
			private static ServiceContainer.ServiceCollection<T>.EmbeddedTypeAwareTypeComparer serviceTypeComparer = new ServiceContainer.ServiceCollection<T>.EmbeddedTypeAwareTypeComparer();

			// Token: 0x02000342 RID: 834
			private sealed class EmbeddedTypeAwareTypeComparer : IEqualityComparer<Type>
			{
				// Token: 0x06001A34 RID: 6708 RVA: 0x0006A92C File Offset: 0x00068B2C
				public bool Equals(Type x, Type y)
				{
					return x.IsEquivalentTo(y);
				}

				// Token: 0x06001A35 RID: 6709 RVA: 0x0006A935 File Offset: 0x00068B35
				public int GetHashCode(Type obj)
				{
					return obj.FullName.GetHashCode();
				}
			}
		}
	}
}
