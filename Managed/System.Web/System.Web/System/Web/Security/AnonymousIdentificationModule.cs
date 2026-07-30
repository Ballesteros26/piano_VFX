using System;
using System.ComponentModel;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Security
{
	/// <summary>Manages anonymous identifiers for the ASP.NET application.</summary>
	// Token: 0x020004B8 RID: 1208
	public sealed class AnonymousIdentificationModule : IHttpModule
	{
		/// <summary>Occurs when a new anonymous identifier is created.</summary>
		// Token: 0x14000102 RID: 258
		// (add) Token: 0x06003670 RID: 13936 RVA: 0x0008E6A9 File Offset: 0x0008C8A9
		// (remove) Token: 0x06003671 RID: 13937 RVA: 0x0008E6BC File Offset: 0x0008C8BC
		public event AnonymousIdentificationEventHandler Creating
		{
			add
			{
				this.events.AddHandler(AnonymousIdentificationModule.creatingEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(AnonymousIdentificationModule.creatingEvent, value);
			}
		}

		/// <summary>Clears the anonymous cookie or identifier associated with a session.</summary>
		/// <exception cref="T:System.NotSupportedException">Calling <see cref="M:System.Web.Security.AnonymousIdentificationModule.ClearAnonymousIdentifier" /> when the anonymous identification is not enabled.-or-The user for the current request is anonymous.</exception>
		// Token: 0x06003672 RID: 13938 RVA: 0x0008E6CF File Offset: 0x0008C8CF
		public static void ClearAnonymousIdentifier()
		{
			if (AnonymousIdentificationModule.Config == null || !AnonymousIdentificationModule.Config.Enabled)
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.AnonymousIdentificationModule" />.</summary>
		// Token: 0x06003673 RID: 13939 RVA: 0x0008E6EA File Offset: 0x0008C8EA
		public void Dispose()
		{
			this.app.PostAuthenticateRequest -= this.OnEnter;
			this.app = null;
		}

		/// <summary>Initializes the <see cref="T:System.Web.Security.AnonymousIdentificationModule" /> object.</summary>
		/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
		// Token: 0x06003674 RID: 13940 RVA: 0x0008E70A File Offset: 0x0008C90A
		public void Init(HttpApplication app)
		{
			this.app = app;
			app.PostAuthenticateRequest += this.OnEnter;
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x0008E728 File Offset: 0x0008C928
		[global::System.MonoTODO("cookieless userid")]
		private void OnEnter(object source, EventArgs eventArgs)
		{
			if (!AnonymousIdentificationModule.Enabled)
			{
				return;
			}
			string text = null;
			HttpCookie httpCookie = this.app.Request.Cookies[AnonymousIdentificationModule.Config.CookieName];
			if (httpCookie != null && (httpCookie.Expires == DateTime.MinValue || httpCookie.Expires > DateTime.Now))
			{
				try
				{
					text = Encoding.Unicode.GetString(Convert.FromBase64String(httpCookie.Value));
				}
				catch
				{
				}
			}
			if (text == null)
			{
				AnonymousIdentificationEventHandler anonymousIdentificationEventHandler = this.events[AnonymousIdentificationModule.creatingEvent] as AnonymousIdentificationEventHandler;
				if (anonymousIdentificationEventHandler != null)
				{
					AnonymousIdentificationEventArgs anonymousIdentificationEventArgs = new AnonymousIdentificationEventArgs(HttpContext.Current);
					anonymousIdentificationEventHandler(this, anonymousIdentificationEventArgs);
					text = anonymousIdentificationEventArgs.AnonymousID;
				}
				if (text == null)
				{
					text = Guid.NewGuid().ToString();
				}
				HttpCookie httpCookie2 = new HttpCookie(AnonymousIdentificationModule.Config.CookieName);
				httpCookie2.Path = this.app.Request.ApplicationPath;
				httpCookie2.Expires = DateTime.Now + AnonymousIdentificationModule.Config.CookieTimeout;
				httpCookie2.Value = Convert.ToBase64String(Encoding.Unicode.GetBytes(text));
				this.app.Response.AppendCookie(httpCookie2);
			}
			this.app.Request.AnonymousID = text;
		}

		/// <summary>Gets a value indicating whether anonymous identification is enabled for the ASP.NET application.</summary>
		/// <returns>true if anonymous identification is enabled for the ASP.NET application; otherwise, false. The default is false.</returns>
		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06003676 RID: 13942 RVA: 0x0008E880 File Offset: 0x0008CA80
		public static bool Enabled
		{
			get
			{
				return AnonymousIdentificationModule.Config != null && AnonymousIdentificationModule.Config.Enabled;
			}
		}

		// Token: 0x04001DAF RID: 7599
		private static readonly object creatingEvent = new object();

		// Token: 0x04001DB0 RID: 7600
		private HttpApplication app;

		// Token: 0x04001DB1 RID: 7601
		private EventHandlerList events = new EventHandlerList();

		// Token: 0x04001DB2 RID: 7602
		private static AnonymousIdentificationSection Config = (AnonymousIdentificationSection)WebConfigurationManager.GetSection("system.web/anonymousIdentification");
	}
}
