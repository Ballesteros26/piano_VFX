using System;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	/// <summary>Provides support for programmatic access to application domain protocols.</summary>
	// Token: 0x02000533 RID: 1331
	public abstract class AppDomainProtocolHandler : MarshalByRefObject, IRegisteredObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.AppDomainProtocolHandler" /> class.</summary>
		// Token: 0x06003A57 RID: 14935 RVA: 0x0009D735 File Offset: 0x0009B935
		protected AppDomainProtocolHandler()
		{
			HostingEnvironment.RegisterObject(this);
		}

		/// <summary>Gives the protocol handler an infinite lifetime by preventing a lease from being created.</summary>
		/// <returns>true if the service is initiated; otherwise, false.</returns>
		// Token: 0x06003A58 RID: 14936 RVA: 0x00003BEA File Offset: 0x00001DEA
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		/// <summary>Starts a protocol listener channel.</summary>
		/// <param name="listenerChannelCallback">The callback for the listener channel.</param>
		// Token: 0x06003A59 RID: 14937
		public abstract void StartListenerChannel(IListenerChannelCallback listenerChannelCallback);

		/// <summary>Stops the specified process protocol handler.</summary>
		/// <param name="listenerChannelId">The callback for the listener channel.</param>
		/// <param name="immediate">true to stop the protocol immediately; otherwise, false.</param>
		// Token: 0x06003A5A RID: 14938
		public abstract void StopListenerChannel(int listenerChannelId, bool immediate);

		/// <summary>Stops a protocol.</summary>
		/// <param name="immediate">true to stop the protocol immediately.</param>
		// Token: 0x06003A5B RID: 14939
		public abstract void StopProtocol(bool immediate);

		/// <summary>Stops a queue.</summary>
		/// <param name="immediate">true to stop the queue immediately.</param>
		// Token: 0x06003A5C RID: 14940 RVA: 0x0009D743 File Offset: 0x0009B943
		public virtual void Stop(bool immediate)
		{
			this.StopProtocol(true);
			HostingEnvironment.UnregisterObject(this);
		}
	}
}
