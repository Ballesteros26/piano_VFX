using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.Security
{
	/// <summary>Provides a wrapper around Passport Authentication services. This class cannot be inherited. This class is deprecated.</summary>
	// Token: 0x020004C7 RID: 1223
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PassportAuthenticationModule : IHttpModule
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.Security.PassportAuthenticationModule" /> class. This class is deprecated.</summary>
		// Token: 0x06003717 RID: 14103 RVA: 0x00090517 File Offset: 0x0008E717
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public PassportAuthenticationModule()
		{
		}

		/// <summary>Raised during authentication. This is a Global.asax event that must be named PassportAuthentication_OnAuthenticate. This class is deprecated</summary>
		// Token: 0x14000106 RID: 262
		// (add) Token: 0x06003718 RID: 14104 RVA: 0x0009052A File Offset: 0x0008E72A
		// (remove) Token: 0x06003719 RID: 14105 RVA: 0x0009053D File Offset: 0x0008E73D
		public event PassportAuthenticationEventHandler Authenticate
		{
			add
			{
				this.events.AddHandler(PassportAuthenticationModule.authenticateEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(PassportAuthenticationModule.authenticateEvent, value);
			}
		}

		/// <summary>Disposes of the module derived from <see cref="T:System.Web.IHttpModule" /> when called by the <see cref="T:System.Web.HttpRuntime" />. This class is deprecated.</summary>
		// Token: 0x0600371A RID: 14106 RVA: 0x00090550 File Offset: 0x0008E750
		public void Dispose()
		{
			this.events.Dispose();
		}

		/// <summary>Initializes the module derived from <see cref="T:System.Web.IHttpModule" /> when called by the <see cref="T:System.Web.HttpRuntime" />. This class is deprecated</summary>
		/// <param name="app">The <see cref="T:System.Web.HttpApplication" /> module </param>
		// Token: 0x0600371B RID: 14107 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Will we ever implement this? :-)")]
		public void Init(HttpApplication app)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001DEC RID: 7660
		private static readonly object authenticateEvent = new object();

		// Token: 0x04001DED RID: 7661
		private EventHandlerList events = new EventHandlerList();
	}
}
