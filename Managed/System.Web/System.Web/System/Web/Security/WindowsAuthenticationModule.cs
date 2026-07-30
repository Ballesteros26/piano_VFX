using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.Security
{
	/// <summary>Sets the identity of the user for an ASP.NET application when Windows authentication is enabled. This class cannot be inherited.</summary>
	// Token: 0x020004D5 RID: 1237
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WindowsAuthenticationModule : IHttpModule
	{
		/// <summary>Occurs when the application authenticates the current request.</summary>
		// Token: 0x14000108 RID: 264
		// (add) Token: 0x0600383D RID: 14397 RVA: 0x0009749F File Offset: 0x0009569F
		// (remove) Token: 0x0600383E RID: 14398 RVA: 0x000974B2 File Offset: 0x000956B2
		public event WindowsAuthenticationEventHandler Authenticate
		{
			add
			{
				this.events.AddHandler(WindowsAuthenticationModule.authenticateEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(WindowsAuthenticationModule.authenticateEvent, value);
			}
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.Security.WindowsAuthenticationModule" /> class.</summary>
		// Token: 0x0600383F RID: 14399 RVA: 0x000974C5 File Offset: 0x000956C5
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public WindowsAuthenticationModule()
		{
		}

		/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.WindowsAuthenticationModule" />.</summary>
		// Token: 0x06003840 RID: 14400 RVA: 0x000974D8 File Offset: 0x000956D8
		public void Dispose()
		{
			this.events.Dispose();
		}

		/// <summary>Initializes the <see cref="T:System.Web.Security.WindowsAuthenticationModule" /> object.</summary>
		/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
		// Token: 0x06003841 RID: 14401 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public void Init(HttpApplication app)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001E33 RID: 7731
		private static readonly object authenticateEvent = new object();

		// Token: 0x04001E34 RID: 7732
		private EventHandlerList events = new EventHandlerList();
	}
}
