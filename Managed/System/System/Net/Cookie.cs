using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>Provides a set of properties and methods that are used to manage cookies. This class cannot be inherited.</summary>
	// Token: 0x020004B0 RID: 1200
	[Serializable]
	public sealed class Cookie
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cookie" /> class.</summary>
		// Token: 0x06002348 RID: 9032 RVA: 0x00088A44 File Offset: 0x00086C44
		public Cookie()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cookie" /> class with a specified <see cref="P:System.Net.Cookie.Name" /> and <see cref="P:System.Net.Cookie.Value" />.</summary>
		/// <param name="name">The name of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="name" />: equal sign, semicolon, comma, newline (\n), return (\r), tab (\t), and space character. The dollar sign character ("$") cannot be the first character. </param>
		/// <param name="value">The value of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="value" />: semicolon, comma. </param>
		/// <exception cref="T:System.Net.CookieException">The <paramref name="name" /> parameter is null. -or- The <paramref name="name" /> parameter is of zero length. -or- The <paramref name="name" /> parameter contains an invalid character.-or- The <paramref name="value" /> parameter is null .-or - The <paramref name="value" /> parameter contains a string not enclosed in quotes that contains an invalid character. </exception>
		// Token: 0x06002349 RID: 9033 RVA: 0x00088AD8 File Offset: 0x00086CD8
		public Cookie(string name, string value)
		{
			this.Name = name;
			this.m_value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cookie" /> class with a specified <see cref="P:System.Net.Cookie.Name" />, <see cref="P:System.Net.Cookie.Value" />, and <see cref="P:System.Net.Cookie.Path" />.</summary>
		/// <param name="name">The name of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="name" />: equal sign, semicolon, comma, newline (\n), return (\r), tab (\t), and space character. The dollar sign character ("$") cannot be the first character. </param>
		/// <param name="value">The value of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="value" />: semicolon, comma. </param>
		/// <param name="path">The subset of URIs on the origin server to which this <see cref="T:System.Net.Cookie" /> applies. The default value is "/". </param>
		/// <exception cref="T:System.Net.CookieException">The <paramref name="name" /> parameter is null. -or- The <paramref name="name" /> parameter is of zero length. -or- The <paramref name="name" /> parameter contains an invalid character.-or- The <paramref name="value" /> parameter is null .-or - The <paramref name="value" /> parameter contains a string not enclosed in quotes that contains an invalid character.</exception>
		// Token: 0x0600234A RID: 9034 RVA: 0x00088B78 File Offset: 0x00086D78
		public Cookie(string name, string value, string path)
			: this(name, value)
		{
			this.Path = path;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cookie" /> class with a specified <see cref="P:System.Net.Cookie.Name" />, <see cref="P:System.Net.Cookie.Value" />, <see cref="P:System.Net.Cookie.Path" />, and <see cref="P:System.Net.Cookie.Domain" />.</summary>
		/// <param name="name">The name of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="name" />: equal sign, semicolon, comma, newline (\n), return (\r), tab (\t), and space character. The dollar sign character ("$") cannot be the first character. </param>
		/// <param name="value">The value of a <see cref="T:System.Net.Cookie" /> object. The following characters must not be used inside <paramref name="value" />: semicolon, comma. </param>
		/// <param name="path">The subset of URIs on the origin server to which this <see cref="T:System.Net.Cookie" /> applies. The default value is "/". </param>
		/// <param name="domain">The optional internet domain for which this <see cref="T:System.Net.Cookie" /> is valid. The default value is the host this <see cref="T:System.Net.Cookie" /> has been received from. </param>
		/// <exception cref="T:System.Net.CookieException">The <paramref name="name" /> parameter is null. -or- The <paramref name="name" /> parameter is of zero length. -or- The <paramref name="name" /> parameter contains an invalid character.-or- The <paramref name="value" /> parameter is null .-or - The <paramref name="value" /> parameter contains a string not enclosed in quotes that contains an invalid character.</exception>
		// Token: 0x0600234B RID: 9035 RVA: 0x00088B89 File Offset: 0x00086D89
		public Cookie(string name, string value, string path, string domain)
			: this(name, value, path)
		{
			this.Domain = domain;
		}

		/// <summary>Gets or sets a comment that the server can add to a <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>An optional comment to document intended usage for this <see cref="T:System.Net.Cookie" />.</returns>
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x00088B9C File Offset: 0x00086D9C
		// (set) Token: 0x0600234D RID: 9037 RVA: 0x00088BA4 File Offset: 0x00086DA4
		public string Comment
		{
			get
			{
				return this.m_comment;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.m_comment = value;
			}
		}

		/// <summary>Gets or sets a URI comment that the server can provide with a <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>An optional comment that represents the intended usage of the URI reference for this <see cref="T:System.Net.Cookie" />. The value must conform to URI format.</returns>
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x00088BB7 File Offset: 0x00086DB7
		// (set) Token: 0x0600234F RID: 9039 RVA: 0x00088BBF File Offset: 0x00086DBF
		public Uri CommentUri
		{
			get
			{
				return this.m_commentUri;
			}
			set
			{
				this.m_commentUri = value;
			}
		}

		/// <summary>Determines whether a page script or other active content can access this cookie.</summary>
		/// <returns>Boolean value that determines whether a page script or other active content can access this cookie.</returns>
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x00088BC8 File Offset: 0x00086DC8
		// (set) Token: 0x06002351 RID: 9041 RVA: 0x00088BD0 File Offset: 0x00086DD0
		public bool HttpOnly
		{
			get
			{
				return this.m_httpOnly;
			}
			set
			{
				this.m_httpOnly = value;
			}
		}

		/// <summary>Gets or sets the discard flag set by the server.</summary>
		/// <returns>true if the client is to discard the <see cref="T:System.Net.Cookie" /> at the end of the current session; otherwise, false. The default is false.</returns>
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x00088BD9 File Offset: 0x00086DD9
		// (set) Token: 0x06002353 RID: 9043 RVA: 0x00088BE1 File Offset: 0x00086DE1
		public bool Discard
		{
			get
			{
				return this.m_discard;
			}
			set
			{
				this.m_discard = value;
			}
		}

		/// <summary>Gets or sets the URI for which the <see cref="T:System.Net.Cookie" /> is valid.</summary>
		/// <returns>The URI for which the <see cref="T:System.Net.Cookie" /> is valid.</returns>
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x00088BEA File Offset: 0x00086DEA
		// (set) Token: 0x06002355 RID: 9045 RVA: 0x00088BF2 File Offset: 0x00086DF2
		public string Domain
		{
			get
			{
				return this.m_domain;
			}
			set
			{
				this.m_domain = ((value == null) ? string.Empty : value);
				this.m_domain_implicit = false;
				this.m_domainKey = string.Empty;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002356 RID: 9046 RVA: 0x00088C18 File Offset: 0x00086E18
		private string _Domain
		{
			get
			{
				if (!this.Plain && !this.m_domain_implicit && this.m_domain.Length != 0)
				{
					return "$Domain=" + (this.IsQuotedDomain ? "\"" : string.Empty) + this.m_domain + (this.IsQuotedDomain ? "\"" : string.Empty);
				}
				return string.Empty;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x00088C80 File Offset: 0x00086E80
		// (set) Token: 0x06002358 RID: 9048 RVA: 0x00088C88 File Offset: 0x00086E88
		internal bool DomainImplicit
		{
			get
			{
				return this.m_domain_implicit;
			}
			set
			{
				this.m_domain_implicit = value;
			}
		}

		/// <summary>Gets or sets the current state of the <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>true if the <see cref="T:System.Net.Cookie" /> has expired; otherwise, false. The default is false.</returns>
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x00088C91 File Offset: 0x00086E91
		// (set) Token: 0x0600235A RID: 9050 RVA: 0x00088CBC File Offset: 0x00086EBC
		public bool Expired
		{
			get
			{
				return this.m_expires != DateTime.MinValue && this.m_expires.ToLocalTime() <= DateTime.Now;
			}
			set
			{
				if (value)
				{
					this.m_expires = DateTime.Now;
				}
			}
		}

		/// <summary>Gets or sets the expiration date and time for the <see cref="T:System.Net.Cookie" /> as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The expiration date and time for the <see cref="T:System.Net.Cookie" /> as a <see cref="T:System.DateTime" /> instance.</returns>
		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x00088CCC File Offset: 0x00086ECC
		// (set) Token: 0x0600235C RID: 9052 RVA: 0x00088CD4 File Offset: 0x00086ED4
		public DateTime Expires
		{
			get
			{
				return this.m_expires;
			}
			set
			{
				this.m_expires = value;
			}
		}

		/// <summary>Gets or sets the name for the <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>The name for the <see cref="T:System.Net.Cookie" />.</returns>
		/// <exception cref="T:System.Net.CookieException">The value specified for a set operation is null or the empty string- or -The value specified for a set operation contained an illegal character. The following characters must not be used inside the <see cref="P:System.Net.Cookie.Name" /> property: equal sign, semicolon, comma, newline (\n), return (\r), tab (\t), and space character. The dollar sign character ("$") cannot be the first character.</exception>
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x00088CDD File Offset: 0x00086EDD
		// (set) Token: 0x0600235E RID: 9054 RVA: 0x00088CE5 File Offset: 0x00086EE5
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				if (ValidationHelper.IsBlankString(value) || !this.InternalSetName(value))
				{
					throw new CookieException(global::SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[]
					{
						"Name",
						(value == null) ? "<null>" : value
					}));
				}
			}
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x00088D24 File Offset: 0x00086F24
		internal bool InternalSetName(string value)
		{
			if (ValidationHelper.IsBlankString(value) || value[0] == '$' || value.IndexOfAny(Cookie.Reserved2Name) != -1)
			{
				this.m_name = string.Empty;
				return false;
			}
			this.m_name = value;
			return true;
		}

		/// <summary>Gets or sets the URIs to which the <see cref="T:System.Net.Cookie" /> applies.</summary>
		/// <returns>The URIs to which the <see cref="T:System.Net.Cookie" /> applies.</returns>
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x00088D5C File Offset: 0x00086F5C
		// (set) Token: 0x06002361 RID: 9057 RVA: 0x00088D64 File Offset: 0x00086F64
		public string Path
		{
			get
			{
				return this.m_path;
			}
			set
			{
				this.m_path = ((value == null) ? string.Empty : value);
				this.m_path_implicit = false;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x00088D7E File Offset: 0x00086F7E
		private string _Path
		{
			get
			{
				if (!this.Plain && !this.m_path_implicit && this.m_path.Length != 0)
				{
					return "$Path=" + this.m_path;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x00088DB3 File Offset: 0x00086FB3
		internal bool Plain
		{
			get
			{
				return this.Variant == CookieVariant.Plain;
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x00088DC0 File Offset: 0x00086FC0
		internal Cookie Clone()
		{
			Cookie cookie = new Cookie(this.m_name, this.m_value);
			if (!this.m_port_implicit)
			{
				cookie.Port = this.m_port;
			}
			if (!this.m_path_implicit)
			{
				cookie.Path = this.m_path;
			}
			cookie.Domain = this.m_domain;
			cookie.DomainImplicit = this.m_domain_implicit;
			cookie.m_timeStamp = this.m_timeStamp;
			cookie.Comment = this.m_comment;
			cookie.CommentUri = this.m_commentUri;
			cookie.HttpOnly = this.m_httpOnly;
			cookie.Discard = this.m_discard;
			cookie.Expires = this.m_expires;
			cookie.Version = this.m_version;
			cookie.Secure = this.m_secure;
			cookie.m_cookieVariant = this.m_cookieVariant;
			return cookie;
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x00088E8C File Offset: 0x0008708C
		private static bool IsDomainEqualToHost(string domain, string host)
		{
			return host.Length + 1 == domain.Length && string.Compare(host, 0, domain, 1, host.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x00088EB4 File Offset: 0x000870B4
		internal bool VerifySetDefaults(CookieVariant variant, Uri uri, bool isLocalDomain, string localDomain, bool set_default, bool isThrow)
		{
			string host = uri.Host;
			int port = uri.Port;
			string absolutePath = uri.AbsolutePath;
			bool flag = true;
			if (set_default)
			{
				if (this.Version == 0)
				{
					variant = CookieVariant.Plain;
				}
				else if (this.Version == 1 && variant == CookieVariant.Unknown)
				{
					variant = CookieVariant.Rfc2109;
				}
				this.m_cookieVariant = variant;
			}
			if (this.m_name == null || this.m_name.Length == 0 || this.m_name[0] == '$' || this.m_name.IndexOfAny(Cookie.Reserved2Name) != -1)
			{
				if (isThrow)
				{
					throw new CookieException(global::SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[]
					{
						"Name",
						(this.m_name == null) ? "<null>" : this.m_name
					}));
				}
				return false;
			}
			else if (this.m_value == null || ((this.m_value.Length <= 2 || this.m_value[0] != '"' || this.m_value[this.m_value.Length - 1] != '"') && this.m_value.IndexOfAny(Cookie.Reserved2Value) != -1))
			{
				if (isThrow)
				{
					throw new CookieException(global::SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[]
					{
						"Value",
						(this.m_value == null) ? "<null>" : this.m_value
					}));
				}
				return false;
			}
			else if (this.Comment != null && (this.Comment.Length <= 2 || this.Comment[0] != '"' || this.Comment[this.Comment.Length - 1] != '"') && this.Comment.IndexOfAny(Cookie.Reserved2Value) != -1)
			{
				if (isThrow)
				{
					throw new CookieException(global::SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Comment", this.Comment }));
				}
				return false;
			}
			else
			{
				if (this.Path == null || (this.Path.Length > 2 && this.Path[0] == '"' && this.Path[this.Path.Length - 1] == '"') || this.Path.IndexOfAny(Cookie.Reserved2Value) == -1)
				{
					if (set_default && this.m_domain_implicit)
					{
						this.m_domain = host;
					}
					else
					{
						if (!this.m_domain_implicit)
						{
							string text = this.m_domain;
							if (!Cookie.DomainCharsTest(text))
							{
								if (isThrow)
								{
									throw new CookieException(global::SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[]
									{
										"Domain",
										(text == null) ? "<null>" : text
									}));
								}
								return false;
							}
							else
							{
								if (text[0] != '.')
								{
									if (variant != CookieVariant.Rfc2965 && variant != CookieVariant.Plain)
									{
										if (isThrow)
										{
											throw new CookieException(global::SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Domain", this.m_domain }));
										}
										return false;
									}
									else
									{
										text = "." + text;
									}
								}
								int num = host.IndexOf('.');
								if (isLocalDomain && string.Compare(localDomain, text, StringComparison.OrdinalIgnoreCase) == 0)
								{
									flag = true;
								}
								else if (text.IndexOf('.', 1, text.Length - 2) == -1)
								{
									if (!Cookie.IsDomainEqualToHost(text, host))
									{
										flag = false;
									}
								}
								else if (variant == CookieVariant.Plain)
								{
									if (!Cookie.IsDomainEqualToHost(text, host) && (host.Length <= text.Length || string.Compare(host, host.Length - text.Length, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) != 0))
									{
										flag = false;
									}
								}
								else if ((num == -1 || text.Length != host.Length - num || string.Compare(host, num, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) != 0) && !Cookie.IsDomainEqualToHost(text, host))
								{
									flag = false;
								}
								if (flag)
								{
									this.m_domainKey = text.ToLower(CultureInfo.InvariantCulture);
								}
							}
						}
						else if (string.Compare(host, this.m_domain, StringComparison.OrdinalIgnoreCase) != 0)
						{
							flag = false;
						}
						if (!flag)
						{
							if (isThrow)
							{
								throw new CookieException(global::SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Domain", this.m_domain }));
							}
							return false;
						}
					}
					if (set_default && this.m_path_implicit)
					{
						switch (this.m_cookieVariant)
						{
						case CookieVariant.Plain:
							this.m_path = absolutePath;
							goto IL_04B8;
						case CookieVariant.Rfc2109:
							this.m_path = absolutePath.Substring(0, absolutePath.LastIndexOf('/'));
							goto IL_04B8;
						}
						this.m_path = absolutePath.Substring(0, absolutePath.LastIndexOf('/') + 1);
					}
					else if (!absolutePath.StartsWith(CookieParser.CheckQuoted(this.m_path)))
					{
						if (isThrow)
						{
							throw new CookieException(global::SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Path", this.m_path }));
						}
						return false;
					}
					IL_04B8:
					if (set_default && !this.m_port_implicit && this.m_port.Length == 0)
					{
						this.m_port_list = new int[] { port };
					}
					if (!this.m_port_implicit)
					{
						flag = false;
						int[] port_list = this.m_port_list;
						for (int i = 0; i < port_list.Length; i++)
						{
							if (port_list[i] == port)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							if (isThrow)
							{
								throw new CookieException(global::SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Port", this.m_port }));
							}
							return false;
						}
					}
					return true;
				}
				if (isThrow)
				{
					throw new CookieException(global::SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Path", this.Path }));
				}
				return false;
			}
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x00089404 File Offset: 0x00087604
		private static bool DomainCharsTest(string name)
		{
			if (name == null || name.Length == 0)
			{
				return false;
			}
			foreach (char c in name)
			{
				if ((c < '0' || c > '9') && c != '.' && c != '-' && (c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && c != '_')
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets or sets a list of TCP ports that the <see cref="T:System.Net.Cookie" /> applies to.</summary>
		/// <returns>The list of TCP ports that the <see cref="T:System.Net.Cookie" /> applies to.</returns>
		/// <exception cref="T:System.Net.CookieException">The value specified for a set operation could not be parsed or is not enclosed in double quotes. </exception>
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x00089467 File Offset: 0x00087667
		// (set) Token: 0x06002369 RID: 9065 RVA: 0x00089470 File Offset: 0x00087670
		public string Port
		{
			get
			{
				return this.m_port;
			}
			set
			{
				this.m_port_implicit = false;
				if (value == null || value.Length == 0)
				{
					this.m_port = string.Empty;
					return;
				}
				if (value[0] != '"' || value[value.Length - 1] != '"')
				{
					throw new CookieException(global::SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Port", value }));
				}
				string[] array = value.Split(Cookie.PortSplitDelimiters);
				List<int> list = new List<int>();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != string.Empty)
					{
						int num;
						if (!int.TryParse(array[i], out num))
						{
							throw new CookieException(global::SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Port", value }));
						}
						if (num < 0 || num > 65535)
						{
							throw new CookieException(global::SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Port", value }));
						}
						list.Add(num);
					}
				}
				this.m_port_list = list.ToArray();
				this.m_port = value;
				this.m_version = 1;
				this.m_cookieVariant = CookieVariant.Rfc2965;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x0008958D File Offset: 0x0008778D
		internal int[] PortList
		{
			get
			{
				return this.m_port_list;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x00089595 File Offset: 0x00087795
		private string _Port
		{
			get
			{
				if (!this.m_port_implicit)
				{
					return "$Port" + ((this.m_port.Length == 0) ? string.Empty : ("=" + this.m_port));
				}
				return string.Empty;
			}
		}

		/// <summary>Gets or sets the security level of a <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>true if the client is only to return the cookie in subsequent requests if those requests use Secure Hypertext Transfer Protocol (HTTPS); otherwise, false. The default is false.</returns>
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x000895D3 File Offset: 0x000877D3
		// (set) Token: 0x0600236D RID: 9069 RVA: 0x000895DB File Offset: 0x000877DB
		public bool Secure
		{
			get
			{
				return this.m_secure;
			}
			set
			{
				this.m_secure = value;
			}
		}

		/// <summary>Gets the time when the cookie was issued as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The time when the cookie was issued as a <see cref="T:System.DateTime" />.</returns>
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x000895E4 File Offset: 0x000877E4
		public DateTime TimeStamp
		{
			get
			{
				return this.m_timeStamp;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Net.Cookie.Value" /> for the <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>The <see cref="P:System.Net.Cookie.Value" /> for the <see cref="T:System.Net.Cookie" />.</returns>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x000895EC File Offset: 0x000877EC
		// (set) Token: 0x06002370 RID: 9072 RVA: 0x000895F4 File Offset: 0x000877F4
		public string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x00089607 File Offset: 0x00087807
		// (set) Token: 0x06002372 RID: 9074 RVA: 0x0008960F File Offset: 0x0008780F
		internal CookieVariant Variant
		{
			get
			{
				return this.m_cookieVariant;
			}
			set
			{
				this.m_cookieVariant = value;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x00089618 File Offset: 0x00087818
		internal string DomainKey
		{
			get
			{
				if (!this.m_domain_implicit)
				{
					return this.m_domainKey;
				}
				return this.Domain;
			}
		}

		/// <summary>Gets or sets the version of HTTP state maintenance to which the cookie conforms.</summary>
		/// <returns>The version of HTTP state maintenance to which the cookie conforms.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a version is not allowed. </exception>
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x0008962F File Offset: 0x0008782F
		// (set) Token: 0x06002375 RID: 9077 RVA: 0x00089637 File Offset: 0x00087837
		public int Version
		{
			get
			{
				return this.m_version;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_version = value;
				if (value > 0 && this.m_cookieVariant < CookieVariant.Rfc2109)
				{
					this.m_cookieVariant = CookieVariant.Rfc2109;
				}
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x00089664 File Offset: 0x00087864
		private string _Version
		{
			get
			{
				if (this.Version != 0)
				{
					return "$Version=" + (this.IsQuotedVersion ? "\"" : string.Empty) + this.m_version.ToString(NumberFormatInfo.InvariantInfo) + (this.IsQuotedVersion ? "\"" : string.Empty);
				}
				return string.Empty;
			}
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000896C1 File Offset: 0x000878C1
		internal static IComparer GetComparer()
		{
			return Cookie.staticComparer;
		}

		/// <summary>Overrides the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <returns>Returns true if the <see cref="T:System.Net.Cookie" /> is equal to <paramref name="comparand" />. Two <see cref="T:System.Net.Cookie" /> instances are equal if their <see cref="P:System.Net.Cookie.Name" />, <see cref="P:System.Net.Cookie.Value" />, <see cref="P:System.Net.Cookie.Path" />, <see cref="P:System.Net.Cookie.Domain" />, and <see cref="P:System.Net.Cookie.Version" /> properties are equal. <see cref="P:System.Net.Cookie.Name" /> and <see cref="P:System.Net.Cookie.Domain" /> string comparisons are case-insensitive.</returns>
		/// <param name="comparand">A reference to a <see cref="T:System.Net.Cookie" />. </param>
		// Token: 0x06002378 RID: 9080 RVA: 0x000896C8 File Offset: 0x000878C8
		public override bool Equals(object comparand)
		{
			if (!(comparand is Cookie))
			{
				return false;
			}
			Cookie cookie = (Cookie)comparand;
			return string.Compare(this.Name, cookie.Name, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Value, cookie.Value, StringComparison.Ordinal) == 0 && string.Compare(this.Path, cookie.Path, StringComparison.Ordinal) == 0 && string.Compare(this.Domain, cookie.Domain, StringComparison.OrdinalIgnoreCase) == 0 && this.Version == cookie.Version;
		}

		/// <summary>Overrides the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>The 32-bit signed integer hash code for this instance.</returns>
		// Token: 0x06002379 RID: 9081 RVA: 0x00089748 File Offset: 0x00087948
		public override int GetHashCode()
		{
			return string.Concat(new object[] { this.Name, "=", this.Value, ";", this.Path, "; ", this.Domain, "; ", this.Version }).GetHashCode();
		}

		/// <summary>Overrides the <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>Returns a string representation of this <see cref="T:System.Net.Cookie" /> object that is suitable for including in a HTTP Cookie: request header.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600237A RID: 9082 RVA: 0x000897B8 File Offset: 0x000879B8
		public override string ToString()
		{
			string domain = this._Domain;
			string path = this._Path;
			string port = this._Port;
			string version = this._Version;
			string text = string.Concat(new string[]
			{
				(version.Length == 0) ? string.Empty : (version + "; "),
				this.Name,
				"=",
				this.Value,
				(path.Length == 0) ? string.Empty : ("; " + path),
				(domain.Length == 0) ? string.Empty : ("; " + domain),
				(port.Length == 0) ? string.Empty : ("; " + port)
			});
			if (text == "=")
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x00089894 File Offset: 0x00087A94
		internal string ToServerString()
		{
			string text = this.Name + "=" + this.Value;
			if (this.m_comment != null && this.m_comment.Length > 0)
			{
				text = text + "; Comment=" + this.m_comment;
			}
			if (this.m_commentUri != null)
			{
				text = text + "; CommentURL=\"" + this.m_commentUri.ToString() + "\"";
			}
			if (this.m_discard)
			{
				text += "; Discard";
			}
			if (!this.m_domain_implicit && this.m_domain != null && this.m_domain.Length > 0)
			{
				text = text + "; Domain=" + this.m_domain;
			}
			if (this.Expires != DateTime.MinValue)
			{
				int num = (int)(this.Expires.ToLocalTime() - DateTime.Now).TotalSeconds;
				if (num < 0)
				{
					num = 0;
				}
				text = text + "; Max-Age=" + num.ToString(NumberFormatInfo.InvariantInfo);
			}
			if (!this.m_path_implicit && this.m_path != null && this.m_path.Length > 0)
			{
				text = text + "; Path=" + this.m_path;
			}
			if (!this.Plain && !this.m_port_implicit && this.m_port != null && this.m_port.Length > 0)
			{
				text = text + "; Port=" + this.m_port;
			}
			if (this.m_version > 0)
			{
				text = text + "; Version=" + this.m_version.ToString(NumberFormatInfo.InvariantInfo);
			}
			if (!(text == "="))
			{
				return text;
			}
			return null;
		}

		// Token: 0x04001F83 RID: 8067
		internal const int MaxSupportedVersion = 1;

		// Token: 0x04001F84 RID: 8068
		internal const string CommentAttributeName = "Comment";

		// Token: 0x04001F85 RID: 8069
		internal const string CommentUrlAttributeName = "CommentURL";

		// Token: 0x04001F86 RID: 8070
		internal const string DiscardAttributeName = "Discard";

		// Token: 0x04001F87 RID: 8071
		internal const string DomainAttributeName = "Domain";

		// Token: 0x04001F88 RID: 8072
		internal const string ExpiresAttributeName = "Expires";

		// Token: 0x04001F89 RID: 8073
		internal const string MaxAgeAttributeName = "Max-Age";

		// Token: 0x04001F8A RID: 8074
		internal const string PathAttributeName = "Path";

		// Token: 0x04001F8B RID: 8075
		internal const string PortAttributeName = "Port";

		// Token: 0x04001F8C RID: 8076
		internal const string SecureAttributeName = "Secure";

		// Token: 0x04001F8D RID: 8077
		internal const string VersionAttributeName = "Version";

		// Token: 0x04001F8E RID: 8078
		internal const string HttpOnlyAttributeName = "HttpOnly";

		// Token: 0x04001F8F RID: 8079
		internal const string SeparatorLiteral = "; ";

		// Token: 0x04001F90 RID: 8080
		internal const string EqualsLiteral = "=";

		// Token: 0x04001F91 RID: 8081
		internal const string QuotesLiteral = "\"";

		// Token: 0x04001F92 RID: 8082
		internal const string SpecialAttributeLiteral = "$";

		// Token: 0x04001F93 RID: 8083
		internal static readonly char[] PortSplitDelimiters = new char[] { ' ', ',', '"' };

		// Token: 0x04001F94 RID: 8084
		internal static readonly char[] Reserved2Name = new char[] { ' ', '\t', '\r', '\n', '=', ';', ',' };

		// Token: 0x04001F95 RID: 8085
		internal static readonly char[] Reserved2Value = new char[] { ';', ',' };

		// Token: 0x04001F96 RID: 8086
		private static Comparer staticComparer = new Comparer();

		// Token: 0x04001F97 RID: 8087
		private string m_comment = string.Empty;

		// Token: 0x04001F98 RID: 8088
		private Uri m_commentUri;

		// Token: 0x04001F99 RID: 8089
		private CookieVariant m_cookieVariant = CookieVariant.Plain;

		// Token: 0x04001F9A RID: 8090
		private bool m_discard;

		// Token: 0x04001F9B RID: 8091
		private string m_domain = string.Empty;

		// Token: 0x04001F9C RID: 8092
		private bool m_domain_implicit = true;

		// Token: 0x04001F9D RID: 8093
		private DateTime m_expires = DateTime.MinValue;

		// Token: 0x04001F9E RID: 8094
		private string m_name = string.Empty;

		// Token: 0x04001F9F RID: 8095
		private string m_path = string.Empty;

		// Token: 0x04001FA0 RID: 8096
		private bool m_path_implicit = true;

		// Token: 0x04001FA1 RID: 8097
		private string m_port = string.Empty;

		// Token: 0x04001FA2 RID: 8098
		private bool m_port_implicit = true;

		// Token: 0x04001FA3 RID: 8099
		private int[] m_port_list;

		// Token: 0x04001FA4 RID: 8100
		private bool m_secure;

		// Token: 0x04001FA5 RID: 8101
		[OptionalField]
		private bool m_httpOnly;

		// Token: 0x04001FA6 RID: 8102
		private DateTime m_timeStamp = DateTime.Now;

		// Token: 0x04001FA7 RID: 8103
		private string m_value = string.Empty;

		// Token: 0x04001FA8 RID: 8104
		private int m_version;

		// Token: 0x04001FA9 RID: 8105
		private string m_domainKey = string.Empty;

		// Token: 0x04001FAA RID: 8106
		internal bool IsQuotedVersion;

		// Token: 0x04001FAB RID: 8107
		internal bool IsQuotedDomain;
	}
}
