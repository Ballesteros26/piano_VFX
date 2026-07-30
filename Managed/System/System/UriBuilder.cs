using System;

namespace System
{
	/// <summary>Provides a custom constructor for uniform resource identifiers (URIs) and modifies URIs for the <see cref="T:System.Uri" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EF RID: 239
	public class UriBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class.</summary>
		// Token: 0x0600066E RID: 1646 RVA: 0x0001A25C File Offset: 0x0001845C
		public UriBuilder()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified URI.</summary>
		/// <param name="uri">A URI string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.<paramref name="uri" /> is a zero length string or contains only spaces.-or- The parsing routine detected a scheme in an invalid form.-or- The parser detected more than two consecutive slashes in a URI that does not use the "file" scheme.-or- <paramref name="uri" /> is not a valid URI. </exception>
		// Token: 0x0600066F RID: 1647 RVA: 0x0001A2D8 File Offset: 0x000184D8
		public UriBuilder(string uri)
		{
			Uri uri2 = new Uri(uri, UriKind.RelativeOrAbsolute);
			if (uri2.IsAbsoluteUri)
			{
				this.Init(uri2);
				return;
			}
			uri = Uri.UriSchemeHttp + Uri.SchemeDelimiter + uri;
			this.Init(new Uri(uri));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified <see cref="T:System.Uri" /> instance.</summary>
		/// <param name="uri">An instance of the <see cref="T:System.Uri" /> class. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is null. </exception>
		// Token: 0x06000670 RID: 1648 RVA: 0x0001A388 File Offset: 0x00018588
		public UriBuilder(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.Init(uri);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001A418 File Offset: 0x00018618
		private void Init(Uri uri)
		{
			this._fragment = uri.Fragment;
			this._query = uri.Query;
			this._host = uri.Host;
			this._path = uri.AbsolutePath;
			this._port = uri.Port;
			this._scheme = uri.Scheme;
			this._schemeDelimiter = (uri.HasAuthority ? Uri.SchemeDelimiter : ":");
			string userInfo = uri.UserInfo;
			if (!string.IsNullOrEmpty(userInfo))
			{
				int num = userInfo.IndexOf(':');
				if (num != -1)
				{
					this._password = userInfo.Substring(num + 1);
					this._username = userInfo.Substring(0, num);
				}
				else
				{
					this._username = userInfo;
				}
			}
			this.SetFieldsFromUri(uri);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme and host.</summary>
		/// <param name="schemeName">An Internet access protocol. </param>
		/// <param name="hostName">A DNS-style domain name or IP address. </param>
		// Token: 0x06000672 RID: 1650 RVA: 0x0001A4D0 File Offset: 0x000186D0
		public UriBuilder(string schemeName, string hostName)
		{
			this.Scheme = schemeName;
			this.Host = hostName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, and port.</summary>
		/// <param name="scheme">An Internet access protocol. </param>
		/// <param name="host">A DNS-style domain name or IP address. </param>
		/// <param name="portNumber">An IP port number for the service. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="portNumber" /> is less than -1 or greater than 65,535. </exception>
		// Token: 0x06000673 RID: 1651 RVA: 0x0001A557 File Offset: 0x00018757
		public UriBuilder(string scheme, string host, int portNumber)
			: this(scheme, host)
		{
			this.Port = portNumber;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, port number, and path.</summary>
		/// <param name="scheme">An Internet access protocol. </param>
		/// <param name="host">A DNS-style domain name or IP address. </param>
		/// <param name="port">An IP port number for the service. </param>
		/// <param name="pathValue">The path to the Internet resource. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than -1 or greater than 65,535. </exception>
		// Token: 0x06000674 RID: 1652 RVA: 0x0001A568 File Offset: 0x00018768
		public UriBuilder(string scheme, string host, int port, string pathValue)
			: this(scheme, host, port)
		{
			this.Path = pathValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, port number, path and query string or fragment identifier.</summary>
		/// <param name="scheme">An Internet access protocol. </param>
		/// <param name="host">A DNS-style domain name or IP address. </param>
		/// <param name="port">An IP port number for the service. </param>
		/// <param name="path">The path to the Internet resource. </param>
		/// <param name="extraValue">A query string or fragment identifier. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="extraValue" /> is neither null nor <see cref="F:System.String.Empty" />, nor does a valid fragment identifier begin with a number sign (#), nor a valid query string begin with a question mark (?). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than -1 or greater than 65,535. </exception>
		// Token: 0x06000675 RID: 1653 RVA: 0x0001A57C File Offset: 0x0001877C
		public UriBuilder(string scheme, string host, int port, string path, string extraValue)
			: this(scheme, host, port, path)
		{
			try
			{
				this.Extra = extraValue;
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException)
				{
					throw;
				}
				throw new ArgumentException("Extra portion of URI not valid.", "extraValue");
			}
		}

		// Token: 0x1700011F RID: 287
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0001A5C8 File Offset: 0x000187C8
		private string Extra
		{
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length <= 0)
				{
					this.Fragment = string.Empty;
					this.Query = string.Empty;
					return;
				}
				if (value[0] == '#')
				{
					this.Fragment = value.Substring(1);
					return;
				}
				if (value[0] == '?')
				{
					int num = value.IndexOf('#');
					if (num == -1)
					{
						num = value.Length;
					}
					else
					{
						this.Fragment = value.Substring(num + 1);
					}
					this.Query = value.Substring(1, num - 1);
					return;
				}
				throw new ArgumentException("Extra portion of URI not valid.", "value");
			}
		}

		/// <summary>Gets or sets the fragment portion of the URI.</summary>
		/// <returns>The fragment portion of the URI. The fragment identifier ("#") is added to the beginning of the fragment.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001A668 File Offset: 0x00018868
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x0001A670 File Offset: 0x00018870
		public string Fragment
		{
			get
			{
				return this._fragment;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length > 0 && value[0] != '#')
				{
					value = "#" + value;
				}
				this._fragment = value;
				this._changed = true;
			}
		}

		/// <summary>Gets or sets the Domain Name System (DNS) host name or IP address of a server.</summary>
		/// <returns>The DNS host name or IP address of the server.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001A6AB File Offset: 0x000188AB
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x0001A6B4 File Offset: 0x000188B4
		public string Host
		{
			get
			{
				return this._host;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this._host = value;
				if (this._host.IndexOf(':') >= 0 && this._host[0] != '[')
				{
					this._host = "[" + this._host + "]";
				}
				this._changed = true;
			}
		}

		/// <summary>Gets or sets the password associated with the user that accesses the URI.</summary>
		/// <returns>The password of the user that accesses the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001A714 File Offset: 0x00018914
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0001A71C File Offset: 0x0001891C
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this._password = value;
				this._changed = true;
			}
		}

		/// <summary>Gets or sets the path to the resource referenced by the URI.</summary>
		/// <returns>The path to the resource referenced by the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001A736 File Offset: 0x00018936
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x0001A73E File Offset: 0x0001893E
		public string Path
		{
			get
			{
				return this._path;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					value = "/";
				}
				this._path = Uri.InternalEscapeString(value.Replace('\\', '/'));
				this._changed = true;
			}
		}

		/// <summary>Gets or sets the port number of the URI.</summary>
		/// <returns>The port number of the URI.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port cannot be set to a value less than -1 or greater than 65,535. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0001A76E File Offset: 0x0001896E
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x0001A776 File Offset: 0x00018976
		public int Port
		{
			get
			{
				return this._port;
			}
			set
			{
				if (value < -1 || value > 65535)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._port = value;
				this._changed = true;
			}
		}

		/// <summary>Gets or sets any query information included in the URI.</summary>
		/// <returns>The query information included in the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001A79D File Offset: 0x0001899D
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0001A7A5 File Offset: 0x000189A5
		public string Query
		{
			get
			{
				return this._query;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length > 0 && value[0] != '?')
				{
					value = "?" + value;
				}
				this._query = value;
				this._changed = true;
			}
		}

		/// <summary>Gets or sets the scheme name of the URI.</summary>
		/// <returns>The scheme of the URI.</returns>
		/// <exception cref="T:System.ArgumentException">The scheme cannot be set to an invalid scheme name. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0001A7E0 File Offset: 0x000189E0
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x0001A7E8 File Offset: 0x000189E8
		public string Scheme
		{
			get
			{
				return this._scheme;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				int num = value.IndexOf(':');
				if (num != -1)
				{
					value = value.Substring(0, num);
				}
				if (value.Length != 0)
				{
					if (!Uri.CheckSchemeName(value))
					{
						throw new ArgumentException("Invalid URI: The URI scheme is not valid.", "value");
					}
					value = value.ToLowerInvariant();
				}
				this._scheme = value;
				this._changed = true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Uri" /> instance constructed by the specified <see cref="T:System.UriBuilder" /> instance.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI constructed by the <see cref="T:System.UriBuilder" />.</returns>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.The URI constructed by the <see cref="T:System.UriBuilder" /> properties is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001A84C File Offset: 0x00018A4C
		public Uri Uri
		{
			get
			{
				if (this._changed)
				{
					this._uri = new Uri(this.ToString());
					this.SetFieldsFromUri(this._uri);
					this._changed = false;
				}
				return this._uri;
			}
		}

		/// <summary>The user name associated with the user that accesses the URI.</summary>
		/// <returns>The user name of the user that accesses the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001A880 File Offset: 0x00018A80
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x0001A888 File Offset: 0x00018A88
		public string UserName
		{
			get
			{
				return this._username;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this._username = value;
				this._changed = true;
			}
		}

		/// <summary>Compares an existing <see cref="T:System.Uri" /> instance with the contents of the <see cref="T:System.UriBuilder" /> for equality.</summary>
		/// <returns>true if <paramref name="rparam" /> represents the same <see cref="T:System.Uri" /> as the <see cref="T:System.Uri" /> constructed by this <see cref="T:System.UriBuilder" /> instance; otherwise, false.</returns>
		/// <param name="rparam">The object to compare with the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000688 RID: 1672 RVA: 0x0001A8A2 File Offset: 0x00018AA2
		public override bool Equals(object rparam)
		{
			return rparam != null && this.Uri.Equals(rparam.ToString());
		}

		/// <summary>Returns the hash code for the URI.</summary>
		/// <returns>The hash code generated for the URI.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000689 RID: 1673 RVA: 0x0001A8BA File Offset: 0x00018ABA
		public override int GetHashCode()
		{
			return this.Uri.GetHashCode();
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001A8C8 File Offset: 0x00018AC8
		private void SetFieldsFromUri(Uri uri)
		{
			this._fragment = uri.Fragment;
			this._query = uri.Query;
			this._host = uri.Host;
			this._path = uri.AbsolutePath;
			this._port = uri.Port;
			this._scheme = uri.Scheme;
			this._schemeDelimiter = (uri.HasAuthority ? Uri.SchemeDelimiter : ":");
			string userInfo = uri.UserInfo;
			if (userInfo.Length > 0)
			{
				int num = userInfo.IndexOf(':');
				if (num != -1)
				{
					this._password = userInfo.Substring(num + 1);
					this._username = userInfo.Substring(0, num);
					return;
				}
				this._username = userInfo;
			}
		}

		/// <summary>Returns the display string for the specified <see cref="T:System.UriBuilder" /> instance.</summary>
		/// <returns>The string that contains the unescaped display string of the <see cref="T:System.UriBuilder" />.</returns>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.The <see cref="T:System.UriBuilder" /> instance has a bad password. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600068B RID: 1675 RVA: 0x0001A97C File Offset: 0x00018B7C
		public override string ToString()
		{
			if (this._username.Length == 0 && this._password.Length > 0)
			{
				throw new UriFormatException("Invalid URI: The username:password construct is badly formed.");
			}
			if (this._scheme.Length != 0)
			{
				UriParser syntax = UriParser.GetSyntax(this._scheme);
				if (syntax != null)
				{
					this._schemeDelimiter = ((syntax.InFact(UriSyntaxFlags.MustHaveAuthority) || (this._host.Length != 0 && syntax.NotAny(UriSyntaxFlags.MailToLikeUri) && syntax.InFact(UriSyntaxFlags.OptionalAuthority))) ? Uri.SchemeDelimiter : ":");
				}
				else
				{
					this._schemeDelimiter = ((this._host.Length != 0) ? Uri.SchemeDelimiter : ":");
				}
			}
			string text = ((this._scheme.Length != 0) ? (this._scheme + this._schemeDelimiter) : string.Empty);
			return string.Concat(new string[]
			{
				text,
				this._username,
				(this._password.Length > 0) ? (":" + this._password) : string.Empty,
				(this._username.Length > 0) ? "@" : string.Empty,
				this._host,
				(this._port != -1 && this._host.Length > 0) ? (":" + this._port.ToString()) : string.Empty,
				(this._host.Length > 0 && this._path.Length != 0 && this._path[0] != '/') ? "/" : string.Empty,
				this._path,
				this._query,
				this._fragment
			});
		}

		// Token: 0x04000BF6 RID: 3062
		private bool _changed = true;

		// Token: 0x04000BF7 RID: 3063
		private string _fragment = string.Empty;

		// Token: 0x04000BF8 RID: 3064
		private string _host = "localhost";

		// Token: 0x04000BF9 RID: 3065
		private string _password = string.Empty;

		// Token: 0x04000BFA RID: 3066
		private string _path = "/";

		// Token: 0x04000BFB RID: 3067
		private int _port = -1;

		// Token: 0x04000BFC RID: 3068
		private string _query = string.Empty;

		// Token: 0x04000BFD RID: 3069
		private string _scheme = "http";

		// Token: 0x04000BFE RID: 3070
		private string _schemeDelimiter = Uri.SchemeDelimiter;

		// Token: 0x04000BFF RID: 3071
		private Uri _uri;

		// Token: 0x04000C00 RID: 3072
		private string _username = string.Empty;
	}
}
