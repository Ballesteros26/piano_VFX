using System;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.SessionState
{
	/// <summary>Manages unique identifiers for ASP.NET session state.</summary>
	// Token: 0x0200049B RID: 1179
	public class SessionIDManager : ISessionIDManager
	{
		/// <summary>Gets the maximum length of a valid session identifier.</summary>
		/// <returns>The maximum length of a valid session identifier.</returns>
		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06003588 RID: 13704 RVA: 0x0008BCA8 File Offset: 0x00089EA8
		public static int SessionIDMaxLength
		{
			get
			{
				return 80;
			}
		}

		/// <summary>Creates a unique session identifier for the session.</summary>
		/// <returns>A unique session identifier.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
		// Token: 0x06003589 RID: 13705 RVA: 0x0008BCAC File Offset: 0x00089EAC
		public virtual string CreateSessionID(HttpContext context)
		{
			return SessionId.Create();
		}

		/// <summary>Decodes a URL-encoded session identifier obtained from a cookie or the URL.</summary>
		/// <returns>The decoded session identifier.</returns>
		/// <param name="id">The session identifier to decode. </param>
		// Token: 0x0600358A RID: 13706 RVA: 0x0008BCB3 File Offset: 0x00089EB3
		public virtual string Decode(string id)
		{
			return HttpUtility.UrlDecode(id);
		}

		/// <summary>Encodes the session identifier for saving to either a cookie or the URL.</summary>
		/// <returns>The encoded session identifier.</returns>
		/// <param name="id">The session identifier to encode. </param>
		// Token: 0x0600358B RID: 13707 RVA: 0x0008BCBB File Offset: 0x00089EBB
		public virtual string Encode(string id)
		{
			return HttpUtility.UrlEncode(id);
		}

		/// <summary>Gets the session-identifier value from the current Web request.</summary>
		/// <returns>The current <see cref="P:System.Web.SessionState.HttpSessionState.SessionID" />.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
		/// <exception cref="T:System.Web.HttpException">The length of the session-identifier value retrieved from the HTTP request exceeds the <see cref="P:System.Web.SessionState.SessionIDManager.SessionIDMaxLength" /> value.</exception>
		// Token: 0x0600358C RID: 13708 RVA: 0x0008BCC4 File Offset: 0x00089EC4
		public string GetSessionID(HttpContext context)
		{
			string text = null;
			if (SessionStateModule.IsCookieLess(context, this.config))
			{
				string text2 = context.Request.Headers["AspFilterSessionId"];
				if (text2 != null)
				{
					text = this.Decode(text2);
				}
			}
			else
			{
				HttpCookie httpCookie = context.Request.Cookies[this.config.CookieName];
				if (httpCookie != null)
				{
					text = this.Decode(httpCookie.Value);
				}
			}
			if (text != null && text.Length > SessionIDManager.SessionIDMaxLength)
			{
				throw new HttpException("The length of the session-identifier value retrieved from the HTTP request exceeds the SessionIDMaxLength value.");
			}
			if (!this.Validate(text))
			{
				throw new HttpException("Invalid session ID");
			}
			return text;
		}

		/// <summary>Initializes the <see cref="T:System.Web.SessionState.SessionIDManager" /> object with information from configuration files.</summary>
		// Token: 0x0600358D RID: 13709 RVA: 0x0008BD60 File Offset: 0x00089F60
		public void Initialize()
		{
			this.config = WebConfigurationManager.GetSection("system.web/sessionState") as SessionStateSection;
		}

		/// <summary>Performs per-request initialization of the <see cref="T:System.Web.SessionState.SessionIDManager" /> object.</summary>
		/// <returns>true to indicate the <see cref="T:System.Web.SessionState.SessionIDManager" /> object has done a redirect to determine cookie support; otherwise, false.</returns>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> object that contains information about the current request.</param>
		/// <param name="suppressAutoDetectRedirect">true to redirect to determine cookie support; otherwise, false to suppress automatic redirection to determine cookie support.</param>
		/// <param name="supportSessionIDReissue">When this method returns, contains a Boolean that indicates whether the <see cref="T:System.Web.SessionState.SessionIDManager" /> object supports issuing new session IDs when the original ID is out of date. This parameter is passed uninitialized.</param>
		// Token: 0x0600358E RID: 13710 RVA: 0x0008BD77 File Offset: 0x00089F77
		public bool InitializeRequest(HttpContext context, bool suppressAutoDetectRedirect, out bool supportSessionIDReissue)
		{
			if (this.config.CookieLess)
			{
				supportSessionIDReissue = true;
				return false;
			}
			supportSessionIDReissue = false;
			return false;
		}

		/// <summary>Deletes the session-identifier cookie from the HTTP response.</summary>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
		// Token: 0x0600358F RID: 13711 RVA: 0x0008BD8F File Offset: 0x00089F8F
		public void RemoveSessionID(HttpContext context)
		{
			context.Response.Cookies.Remove(this.config.CookieName);
		}

		/// <summary>Saves a newly created session identifier to the HTTP response.</summary>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
		/// <param name="id">The session identifier. </param>
		/// <param name="redirected">When this method returns, contains a Boolean value that is true if the response is redirected to the current URL with the session identifier added to the URL; otherwise, false. </param>
		/// <param name="cookieAdded">When this method returns, contains a Boolean value that is true if a cookie has been added to the HTTP response; otherwise, false. </param>
		/// <exception cref="T:System.Web.HttpException">The response has already been sent.-or-The session ID passed to this method failed validation. </exception>
		// Token: 0x06003590 RID: 13712 RVA: 0x0008BDAC File Offset: 0x00089FAC
		public void SaveSessionID(HttpContext context, string id, out bool redirected, out bool cookieAdded)
		{
			if (!this.Validate(id))
			{
				throw new HttpException("Invalid session ID");
			}
			HttpRequest request = context.Request;
			if (!SessionStateModule.IsCookieLess(context, this.config))
			{
				HttpCookie httpCookie = new HttpCookie(this.config.CookieName, id);
				httpCookie.Path = request.ApplicationPath;
				context.Response.AppendCookie(httpCookie);
				cookieAdded = true;
				redirected = false;
				return;
			}
			request.SetHeader("AspFilterSessionId", id);
			cookieAdded = false;
			redirected = true;
			UriBuilder uriBuilder = new UriBuilder(request.Url);
			uriBuilder.Path = UrlUtils.InsertSessionId(id, request.FilePath);
			context.Response.Redirect(uriBuilder.Uri.PathAndQuery, false);
		}

		/// <summary>Gets a value indicating whether a session identifier is valid.</summary>
		/// <returns>true if the session identifier is valid; otherwise, false.</returns>
		/// <param name="id">The session identifier to validate. </param>
		// Token: 0x06003591 RID: 13713 RVA: 0x00008B66 File Offset: 0x00006D66
		public virtual bool Validate(string id)
		{
			return true;
		}

		// Token: 0x04001D5A RID: 7514
		private SessionStateSection config;
	}
}
