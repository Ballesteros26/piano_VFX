using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Security
{
	/// <summary>Sets the identity of the user for an ASP.NET application when forms authentication is enabled. This class cannot be inherited.</summary>
	// Token: 0x020004BF RID: 1215
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class FormsAuthenticationModule : IHttpModule
	{
		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x060036B0 RID: 14000 RVA: 0x0008F373 File Offset: 0x0008D573
		internal static bool FormsAuthRequired
		{
			get
			{
				return FormsAuthenticationModule._fAuthRequired;
			}
		}

		/// <summary>Occurs when the application authenticates the current request.</summary>
		// Token: 0x14000104 RID: 260
		// (add) Token: 0x060036B1 RID: 14001 RVA: 0x0008F37A File Offset: 0x0008D57A
		// (remove) Token: 0x060036B2 RID: 14002 RVA: 0x0008F38D File Offset: 0x0008D58D
		public event FormsAuthenticationEventHandler Authenticate
		{
			add
			{
				this.events.AddHandler(FormsAuthenticationModule.authenticateEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(FormsAuthenticationModule.authenticateEvent, value);
			}
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x0008F3A0 File Offset: 0x0008D5A0
		private void InitConfig(HttpContext context)
		{
			if (this.isConfigInitialized)
			{
				return;
			}
			this._config = (AuthenticationSection)WebConfigurationManager.GetSection("system.web/authentication");
			if (!FormsAuthenticationModule._fAuthChecked)
			{
				FormsAuthenticationModule._fAuthRequired = this._config.Mode == AuthenticationMode.Forms;
				FormsAuthenticationModule._fAuthChecked = true;
			}
			this.isConfigInitialized = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationModule" /> class. </summary>
		// Token: 0x060036B4 RID: 14004 RVA: 0x0008F3F2 File Offset: 0x0008D5F2
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public FormsAuthenticationModule()
		{
		}

		/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.FormsAuthenticationModule" />.</summary>
		// Token: 0x060036B5 RID: 14005 RVA: 0x0000393A File Offset: 0x00001B3A
		public void Dispose()
		{
		}

		/// <summary>Initializes the <see cref="T:System.Web.Security.FormsAuthenticationModule" /> object.</summary>
		/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
		// Token: 0x060036B6 RID: 14006 RVA: 0x0008F405 File Offset: 0x0008D605
		public void Init(HttpApplication app)
		{
			app.AuthenticateRequest += this.OnAuthenticateRequest;
			app.EndRequest += this.OnEndRequest;
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x0008F42C File Offset: 0x0008D62C
		private void OnAuthenticateRequest(object sender, EventArgs args)
		{
			HttpContext context = ((HttpApplication)sender).Context;
			this.InitConfig(context);
			if (this._config == null || this._config.Mode != AuthenticationMode.Forms)
			{
				return;
			}
			string name = this._config.Forms.Name;
			string path = this._config.Forms.Path;
			string text = this._config.Forms.LoginUrl;
			bool slidingExpiration = this._config.Forms.SlidingExpiration;
			if (!VirtualPathUtility.IsRooted(text))
			{
				text = "~/" + text;
			}
			string text2 = string.Empty;
			string text3 = null;
			try
			{
				text2 = context.Request.PhysicalPath;
				text3 = context.Request.MapPath(text);
			}
			catch
			{
			}
			context.SkipAuthorization = string.Compare(text2, text3, RuntimeHelpers.CaseInsensitive, Helpers.InvariantCulture) == 0;
			string filePath = context.Request.FilePath;
			if (filePath.Length > 15 && string.CompareOrdinal("WebResource.axd", 0, filePath, filePath.Length - 15, 15) == 0)
			{
				context.SkipAuthorization = true;
			}
			FormsAuthenticationEventArgs formsAuthenticationEventArgs = new FormsAuthenticationEventArgs(context);
			FormsAuthenticationEventHandler formsAuthenticationEventHandler = this.events[FormsAuthenticationModule.authenticateEvent] as FormsAuthenticationEventHandler;
			if (formsAuthenticationEventHandler != null)
			{
				formsAuthenticationEventHandler(this, formsAuthenticationEventArgs);
			}
			bool flag = context.User == null;
			if (formsAuthenticationEventArgs.User != null || !flag)
			{
				if (flag)
				{
					context.User = formsAuthenticationEventArgs.User;
				}
				return;
			}
			HttpCookie httpCookie = context.Request.Cookies[name];
			if (httpCookie == null || (httpCookie.Expires != DateTime.MinValue && httpCookie.Expires < DateTime.Now))
			{
				return;
			}
			FormsAuthenticationTicket formsAuthenticationTicket = null;
			try
			{
				formsAuthenticationTicket = FormsAuthentication.Decrypt(httpCookie.Value);
			}
			catch (ArgumentException)
			{
				return;
			}
			if (formsAuthenticationTicket == null || (!formsAuthenticationTicket.IsPersistent && formsAuthenticationTicket.Expired))
			{
				return;
			}
			FormsAuthenticationTicket formsAuthenticationTicket2 = formsAuthenticationTicket;
			if (slidingExpiration)
			{
				formsAuthenticationTicket = FormsAuthentication.RenewTicketIfOld(formsAuthenticationTicket);
			}
			context.User = new GenericPrincipal(new FormsIdentity(formsAuthenticationTicket), new string[0]);
			if (httpCookie.Expires == DateTime.MinValue && formsAuthenticationTicket2 == formsAuthenticationTicket)
			{
				return;
			}
			httpCookie.Value = FormsAuthentication.Encrypt(formsAuthenticationTicket);
			httpCookie.Path = path;
			if (formsAuthenticationTicket.IsPersistent)
			{
				httpCookie.Expires = formsAuthenticationTicket.Expiration;
			}
			context.Response.Cookies.Add(httpCookie);
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x0008F6A0 File Offset: 0x0008D8A0
		private void OnEndRequest(object sender, EventArgs args)
		{
			HttpContext context = ((HttpApplication)sender).Context;
			if (context.Response.StatusCode != 401 || context.Request.QueryString["ReturnUrl"] != null)
			{
				return;
			}
			if (context.Response.StatusCode == 401 && context.Response.SuppressFormsAuthenticationRedirect)
			{
				return;
			}
			this.InitConfig(context);
			string loginUrl = this._config.Forms.LoginUrl;
			if (this._config == null || this._config.Mode != AuthenticationMode.Forms)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(UrlUtils.Combine(context.Request.ApplicationPath, loginUrl));
			stringBuilder.AppendFormat("?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
			context.Response.Redirect(stringBuilder.ToString(), false);
		}

		// Token: 0x04001DCE RID: 7630
		private static readonly object authenticateEvent = new object();

		// Token: 0x04001DCF RID: 7631
		private static bool _fAuthChecked;

		// Token: 0x04001DD0 RID: 7632
		private static bool _fAuthRequired;

		// Token: 0x04001DD1 RID: 7633
		private AuthenticationSection _config;

		// Token: 0x04001DD2 RID: 7634
		private bool isConfigInitialized;

		// Token: 0x04001DD3 RID: 7635
		private EventHandlerList events = new EventHandlerList();
	}
}
