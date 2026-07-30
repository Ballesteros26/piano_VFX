using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Web.Security
{
	/// <summary>Ensures that an authentication object is present in the context. This class cannot be inherited.</summary>
	// Token: 0x020004BC RID: 1212
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DefaultAuthenticationModule : IHttpModule
	{
		/// <summary>Occurs after the request has been authenticated.</summary>
		// Token: 0x14000103 RID: 259
		// (add) Token: 0x0600367E RID: 13950 RVA: 0x0008E9FD File Offset: 0x0008CBFD
		// (remove) Token: 0x0600367F RID: 13951 RVA: 0x0008EA10 File Offset: 0x0008CC10
		public event DefaultAuthenticationEventHandler Authenticate
		{
			add
			{
				this.events.AddHandler(DefaultAuthenticationModule.authenticateEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(DefaultAuthenticationModule.authenticateEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.DefaultAuthenticationModule" /> class. </summary>
		// Token: 0x06003680 RID: 13952 RVA: 0x0008EA23 File Offset: 0x0008CC23
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public DefaultAuthenticationModule()
		{
		}

		/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.DefaultAuthenticationModule" />.</summary>
		// Token: 0x06003681 RID: 13953 RVA: 0x0000393A File Offset: 0x00001B3A
		public void Dispose()
		{
		}

		/// <summary>Initializes the <see cref="T:System.Web.Security.DefaultAuthenticationModule" /> object.</summary>
		/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
		// Token: 0x06003682 RID: 13954 RVA: 0x0008EA36 File Offset: 0x0008CC36
		public void Init(HttpApplication app)
		{
			app.DefaultAuthentication += this.OnDefaultAuthentication;
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x0008EA4C File Offset: 0x0008CC4C
		private void OnDefaultAuthentication(object sender, EventArgs args)
		{
			HttpContext context = ((HttpApplication)sender).Context;
			DefaultAuthenticationEventHandler defaultAuthenticationEventHandler = this.events[DefaultAuthenticationModule.authenticateEvent] as DefaultAuthenticationEventHandler;
			if (context.User == null && defaultAuthenticationEventHandler != null)
			{
				defaultAuthenticationEventHandler(this, new DefaultAuthenticationEventArgs(context));
			}
			if (context.User == null)
			{
				context.User = DefaultAuthenticationModule.generic_principal;
			}
			Thread.CurrentPrincipal = context.User;
		}

		// Token: 0x04001DB9 RID: 7609
		private static readonly object authenticateEvent = new object();

		// Token: 0x04001DBA RID: 7610
		private static IPrincipal generic_principal = new GenericPrincipal(new GenericIdentity("", ""), new string[0]);

		// Token: 0x04001DBB RID: 7611
		private EventHandlerList events = new EventHandlerList();
	}
}
