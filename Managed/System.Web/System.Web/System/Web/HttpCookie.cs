using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using Unity;

namespace System.Web
{
	/// <summary>Provides a type-safe way to create and manipulate individual HTTP cookies.</summary>
	// Token: 0x0200008F RID: 143
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpCookie
	{
		// Token: 0x060006E9 RID: 1769 RVA: 0x0000FE54 File Offset: 0x0000E054
		[Obsolete]
		internal HttpCookie(string name, string value, string path, DateTime expires)
		{
			this.name = name;
			this.values = new HttpCookie.CookieNVC();
			this.Value = value;
			this.path = path;
			this.expires = expires;
		}

		/// <summary>Creates and names a new cookie.</summary>
		/// <param name="name">The name of the new cookie. </param>
		// Token: 0x060006EA RID: 1770 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		public HttpCookie(string name)
		{
			this.name = name;
			this.values = new HttpCookie.CookieNVC();
			this.Value = "";
			HttpCookiesSection httpCookiesSection = (HttpCookiesSection)WebConfigurationManager.GetSection("system.web/httpCookies");
			if (!string.IsNullOrWhiteSpace(httpCookiesSection.Domain))
			{
				this.domain = httpCookiesSection.Domain;
			}
			if (httpCookiesSection.HttpOnlyCookies)
			{
				this.flags |= CookieFlags.HttpOnly;
			}
			if (httpCookiesSection.RequireSSL)
			{
				this.flags |= CookieFlags.Secure;
			}
		}

		/// <summary>Creates, names, and assigns a value to a new cookie.</summary>
		/// <param name="name">The name of the new cookie. </param>
		/// <param name="value">The value of the new cookie. </param>
		// Token: 0x060006EB RID: 1771 RVA: 0x0000FF43 File Offset: 0x0000E143
		public HttpCookie(string name, string value)
			: this(name)
		{
			this.Value = value;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000FF54 File Offset: 0x0000E154
		internal string GetCookieHeaderValue()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.name);
			stringBuilder.Append("=");
			stringBuilder.Append(this.Value);
			if (this.domain != null)
			{
				stringBuilder.Append("; domain=");
				stringBuilder.Append(this.domain);
			}
			if (this.path != null)
			{
				stringBuilder.Append("; path=");
				stringBuilder.Append(this.path);
			}
			if (this.expires != DateTime.MinValue)
			{
				stringBuilder.Append("; expires=");
				stringBuilder.Append(this.expires.ToUniversalTime().ToString("r"));
			}
			if ((this.flags & CookieFlags.Secure) != (CookieFlags)0)
			{
				stringBuilder.Append("; secure");
			}
			if ((this.flags & CookieFlags.HttpOnly) != (CookieFlags)0)
			{
				stringBuilder.Append("; HttpOnly");
			}
			return stringBuilder.ToString();
		}

		/// <summary>Gets or sets the domain to associate the cookie with.</summary>
		/// <returns>The name of the domain to associate the cookie with. The default value is the current domain.</returns>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001003E File Offset: 0x0000E23E
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x00010046 File Offset: 0x0000E246
		public string Domain
		{
			get
			{
				return this.domain;
			}
			set
			{
				this.domain = value;
			}
		}

		/// <summary>Gets or sets the expiration date and time for the cookie.</summary>
		/// <returns>The time of day (on the client) at which the cookie expires.</returns>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001004F File Offset: 0x0000E24F
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x00010057 File Offset: 0x0000E257
		public DateTime Expires
		{
			get
			{
				return this.expires;
			}
			set
			{
				this.expires = value;
			}
		}

		/// <summary>Gets a value indicating whether a cookie has subkeys.</summary>
		/// <returns>true if the cookie has subkeys, otherwise, false. The default value is false.</returns>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00010060 File Offset: 0x0000E260
		public bool HasKeys
		{
			get
			{
				return this.values.HasKeys();
			}
		}

		/// <summary>Gets a shortcut to the <see cref="P:System.Web.HttpCookie.Values" /> property. This property is provided for compatibility with previous versions of Active Server Pages (ASP).</summary>
		/// <returns>The cookie value.</returns>
		/// <param name="key">The key (index) of the cookie value. </param>
		// Token: 0x170002B0 RID: 688
		public string this[string key]
		{
			get
			{
				return this.values[key];
			}
			set
			{
				this.values[key] = value;
			}
		}

		/// <summary>Gets or sets the name of a cookie.</summary>
		/// <returns>The default value is a null reference (Nothing in Visual Basic) unless the constructor specifies otherwise.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001008A File Offset: 0x0000E28A
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x00010092 File Offset: 0x0000E292
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the virtual path to transmit with the current cookie.</summary>
		/// <returns>The virtual path to transmit with the cookie. The default is /, which is the server root.</returns>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001009B File Offset: 0x0000E29B
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x000100A3 File Offset: 0x0000E2A3
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to transmit the cookie using Secure Sockets Layer (SSL)--that is, over HTTPS only.</summary>
		/// <returns>true to transmit the cookie over an SSL connection (HTTPS); otherwise, false. The default value is false.</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x000100AC File Offset: 0x0000E2AC
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x000100B9 File Offset: 0x0000E2B9
		public bool Secure
		{
			get
			{
				return (this.flags & CookieFlags.Secure) == CookieFlags.Secure;
			}
			set
			{
				if (value)
				{
					this.flags |= CookieFlags.Secure;
					return;
				}
				this.flags &= ~CookieFlags.Secure;
			}
		}

		/// <summary>Gets or sets an individual cookie value.</summary>
		/// <returns>The value of the cookie. The default value is a null reference (Nothing in Visual Basic).</returns>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x000100DF File Offset: 0x0000E2DF
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x000100F4 File Offset: 0x0000E2F4
		public string Value
		{
			get
			{
				return HttpUtility.UrlDecode(this.values.ToString());
			}
			set
			{
				this.values.Clear();
				if (value != null && value != "")
				{
					foreach (string text in value.Split(new char[] { '&' }))
					{
						int num = text.IndexOf('=');
						if (num == -1)
						{
							this.values.Add(null, text);
						}
						else
						{
							string text2 = text.Substring(0, num);
							string text3 = text.Substring(num + 1);
							this.values.Add(text2, text3);
						}
					}
				}
			}
		}

		/// <summary>Gets a collection of key/value pairs that are contained within a single cookie object.</summary>
		/// <returns>A collection of cookie values.</returns>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00010180 File Offset: 0x0000E380
		public NameValueCollection Values
		{
			get
			{
				return this.values;
			}
		}

		/// <summary>Gets or sets a value that specifies whether a cookie is accessible by client-side script.</summary>
		/// <returns>true if the cookie has the HttpOnly attribute and cannot be accessed through a client-side script; otherwise, false. The default is false.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00010188 File Offset: 0x0000E388
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x00010195 File Offset: 0x0000E395
		public bool HttpOnly
		{
			get
			{
				return (this.flags & CookieFlags.HttpOnly) == CookieFlags.HttpOnly;
			}
			set
			{
				if (value)
				{
					this.flags |= CookieFlags.HttpOnly;
					return;
				}
				this.flags &= ~CookieFlags.HttpOnly;
			}
		}

		/// <summary>Determines whether the cookie is allowed to participate in output caching.</summary>
		/// <returns>true to specify that output caching will not be suppressed for a give <see cref="T:System.Web.HttpResponse" /> containing one or more outbound cookies; otherwise, false.</returns>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x000101BC File Offset: 0x0000E3BC
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool Shareable
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000101D8 File Offset: 0x0000E3D8
		public static bool TryParse(string input, out HttpCookie result)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x04000F52 RID: 3922
		private string path = "/";

		// Token: 0x04000F53 RID: 3923
		private string domain;

		// Token: 0x04000F54 RID: 3924
		private DateTime expires = DateTime.MinValue;

		// Token: 0x04000F55 RID: 3925
		private string name;

		// Token: 0x04000F56 RID: 3926
		private CookieFlags flags;

		// Token: 0x04000F57 RID: 3927
		private NameValueCollection values;

		// Token: 0x02000090 RID: 144
		[Serializable]
		private sealed class CookieNVC : NameValueCollection
		{
			// Token: 0x06000702 RID: 1794 RVA: 0x000101F3 File Offset: 0x0000E3F3
			public CookieNVC()
				: base(StringComparer.OrdinalIgnoreCase)
			{
			}

			// Token: 0x06000703 RID: 1795 RVA: 0x00010200 File Offset: 0x0000E400
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("");
				bool flag = true;
				foreach (object obj in this.Keys)
				{
					string text = (string)obj;
					if (!flag)
					{
						stringBuilder.Append("&");
					}
					string[] array = this.GetValues(text);
					if (array == null)
					{
						array = new string[] { string.Empty };
					}
					bool flag2 = true;
					foreach (string text2 in array)
					{
						if (!flag2)
						{
							stringBuilder.Append("&");
						}
						if (text != null && text.Length > 0)
						{
							stringBuilder.Append(HttpUtility.UrlEncode(text));
							stringBuilder.Append("=");
						}
						if (text2 != null && text2.Length > 0)
						{
							stringBuilder.Append(HttpUtility.UrlEncode(text2));
						}
						flag2 = false;
					}
					flag = false;
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06000704 RID: 1796 RVA: 0x00010314 File Offset: 0x0000E514
			public override void Set(string name, string value)
			{
				if (base.IsReadOnly)
				{
					throw new NotSupportedException("Collection is read-only");
				}
				if (name == null)
				{
					this.Clear();
					name = string.Empty;
				}
				base.Set(name, value);
			}
		}
	}
}
