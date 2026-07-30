using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Threading;
using System.Web.Configuration;

namespace System.Web.Security
{
	/// <summary>Manages a <see cref="T:System.Web.Security.RolePrincipal" /> instance for the current user. This class cannot be inherited.</summary>
	// Token: 0x020004C9 RID: 1225
	public sealed class RoleManagerModule : IHttpModule
	{
		/// <summary>A global application event that is raised when the <see cref="T:System.Web.Security.RoleManagerModule" /> is ready to create a <see cref="T:System.Web.Security.RolePrincipal" /> that represents the current user.</summary>
		// Token: 0x14000107 RID: 263
		// (add) Token: 0x06003758 RID: 14168 RVA: 0x000907F9 File Offset: 0x0008E9F9
		// (remove) Token: 0x06003759 RID: 14169 RVA: 0x0009080C File Offset: 0x0008EA0C
		public event RoleManagerEventHandler GetRoles
		{
			add
			{
				this.events.AddHandler(RoleManagerModule.getRolesEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(RoleManagerModule.getRolesEvent, value);
			}
		}

		/// <summary>Called by the HTTP runtime to dispose of the role-manager module.</summary>
		// Token: 0x0600375A RID: 14170 RVA: 0x0000393A File Offset: 0x00001B3A
		public void Dispose()
		{
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x00090820 File Offset: 0x0008EA20
		private void ClearCookie(HttpApplication app, string cookieName)
		{
			HttpCookie httpCookie = new HttpCookie(this._config.CookieName, "");
			httpCookie.Path = this._config.CookiePath;
			httpCookie.Expires = DateTime.MinValue;
			httpCookie.Domain = this._config.Domain;
			httpCookie.Secure = this._config.CookieRequireSSL;
			app.Response.SetCookie(httpCookie);
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x00090890 File Offset: 0x0008EA90
		private void OnPostAuthenticateRequest(object sender, EventArgs args)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			if (this._config == null || !this._config.Enabled)
			{
				return;
			}
			RoleManagerEventHandler roleManagerEventHandler = this.events[RoleManagerModule.getRolesEvent] as RoleManagerEventHandler;
			if (roleManagerEventHandler != null)
			{
				RoleManagerEventArgs roleManagerEventArgs = new RoleManagerEventArgs(httpApplication.Context);
				roleManagerEventHandler(this, roleManagerEventArgs);
				if (roleManagerEventArgs.RolesPopulated)
				{
					return;
				}
			}
			HttpCookie httpCookie = httpApplication.Request.Cookies[this._config.CookieName];
			IIdentity identity = httpApplication.Context.User.Identity;
			RolePrincipal rolePrincipal;
			if (httpApplication.Request.IsAuthenticated)
			{
				if (httpCookie != null)
				{
					if (!this._config.CacheRolesInCookie)
					{
						httpCookie = null;
					}
					else if (this._config.CookieRequireSSL && !httpApplication.Request.IsSecureConnection)
					{
						httpCookie = null;
						this.ClearCookie(httpApplication, this._config.CookieName);
					}
				}
				if (httpCookie == null || string.IsNullOrEmpty(httpCookie.Value))
				{
					rolePrincipal = new RolePrincipal(identity);
				}
				else
				{
					rolePrincipal = new RolePrincipal(identity, httpCookie.Value);
				}
			}
			else
			{
				if (httpCookie != null)
				{
					this.ClearCookie(httpApplication, this._config.CookieName);
				}
				rolePrincipal = new RolePrincipal(identity);
			}
			httpApplication.Context.User = rolePrincipal;
			Thread.CurrentPrincipal = rolePrincipal;
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000909CC File Offset: 0x0008EBCC
		private void OnEndRequest(object sender, EventArgs args)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			if (this._config == null || !this._config.Enabled || !this._config.CacheRolesInCookie)
			{
				return;
			}
			if (!httpApplication.Request.IsAuthenticated)
			{
				return;
			}
			if (this._config.CookieRequireSSL && !httpApplication.Request.IsSecureConnection)
			{
				return;
			}
			RolePrincipal rolePrincipal = httpApplication.Context.User as RolePrincipal;
			if (rolePrincipal == null)
			{
				return;
			}
			if (!rolePrincipal.CachedListChanged)
			{
				return;
			}
			string text = rolePrincipal.ToEncryptedTicket();
			if (text == null || text.Length > 4096)
			{
				this.ClearCookie(httpApplication, this._config.CookieName);
				return;
			}
			HttpCookie httpCookie = new HttpCookie(this._config.CookieName, text);
			httpCookie.HttpOnly = true;
			if (!string.IsNullOrEmpty(this._config.Domain))
			{
				httpCookie.Domain = this._config.Domain;
			}
			if (this._config.CookieRequireSSL)
			{
				httpCookie.Secure = true;
			}
			if (this._config.CookiePath.Length > 1)
			{
				httpCookie.Path = this._config.CookiePath;
			}
			httpApplication.Response.SetCookie(httpCookie);
		}

		/// <summary>Associates the role manager with the specified application.</summary>
		/// <param name="app">The <see cref="T:System.Web.HttpApplication" /> to associate the <see cref="T:System.Web.Security.RoleManagerModule" /> with.</param>
		// Token: 0x0600375E RID: 14174 RVA: 0x00090AF4 File Offset: 0x0008ECF4
		public void Init(HttpApplication app)
		{
			this._config = (RoleManagerSection)WebConfigurationManager.GetSection("system.web/roleManager");
			app.PostAuthenticateRequest += this.OnPostAuthenticateRequest;
			app.EndRequest += this.OnEndRequest;
		}

		// Token: 0x04001DEE RID: 7662
		private static readonly object getRolesEvent = new object();

		// Token: 0x04001DEF RID: 7663
		private RoleManagerSection _config;

		// Token: 0x04001DF0 RID: 7664
		private EventHandlerList events = new EventHandlerList();
	}
}
