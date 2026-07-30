using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Net
{
	/// <summary>Contains protocol headers associated with a request or response.</summary>
	// Token: 0x02000468 RID: 1128
	[ComVisible(true)]
	[Serializable]
	public class WebHeaderCollection : NameValueCollection, ISerializable
	{
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x00080177 File Offset: 0x0007E377
		internal string ContentLength
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[1]);
				}
				return this.m_CommonHeaders[1];
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x00080197 File Offset: 0x0007E397
		internal string CacheControl
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[2]);
				}
				return this.m_CommonHeaders[2];
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x000801B7 File Offset: 0x0007E3B7
		internal string ContentType
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[3]);
				}
				return this.m_CommonHeaders[3];
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x000801D7 File Offset: 0x0007E3D7
		internal string Date
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[4]);
				}
				return this.m_CommonHeaders[4];
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x000801F7 File Offset: 0x0007E3F7
		internal string Expires
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[5]);
				}
				return this.m_CommonHeaders[5];
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x00080217 File Offset: 0x0007E417
		internal string ETag
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[6]);
				}
				return this.m_CommonHeaders[6];
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x00080237 File Offset: 0x0007E437
		internal string LastModified
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[7]);
				}
				return this.m_CommonHeaders[7];
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x00080257 File Offset: 0x0007E457
		internal string Location
		{
			get
			{
				return WebHeaderCollection.HeaderEncoding.DecodeUtf8FromString((this.m_CommonHeaders != null) ? this.m_CommonHeaders[8] : this.Get(WebHeaderCollection.s_CommonHeaderNames[8]));
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x0008027D File Offset: 0x0007E47D
		internal string ProxyAuthenticate
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[9]);
				}
				return this.m_CommonHeaders[9];
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x0008029F File Offset: 0x0007E49F
		internal string SetCookie2
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[11]);
				}
				return this.m_CommonHeaders[11];
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x000802C1 File Offset: 0x0007E4C1
		internal string SetCookie
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[12]);
				}
				return this.m_CommonHeaders[12];
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x000802E3 File Offset: 0x0007E4E3
		internal string Server
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[13]);
				}
				return this.m_CommonHeaders[13];
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x00080305 File Offset: 0x0007E505
		internal string Via
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[14]);
				}
				return this.m_CommonHeaders[14];
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x00080328 File Offset: 0x0007E528
		private void NormalizeCommonHeaders()
		{
			if (this.m_CommonHeaders == null)
			{
				return;
			}
			for (int i = 0; i < this.m_CommonHeaders.Length; i++)
			{
				if (this.m_CommonHeaders[i] != null)
				{
					this.InnerCollection.Add(WebHeaderCollection.s_CommonHeaderNames[i], this.m_CommonHeaders[i]);
				}
			}
			this.m_CommonHeaders = null;
			this.m_NumCommonHeaders = 0;
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06002123 RID: 8483 RVA: 0x00080383 File Offset: 0x0007E583
		private NameValueCollection InnerCollection
		{
			get
			{
				if (this.m_InnerCollection == null)
				{
					this.m_InnerCollection = new NameValueCollection(16, CaseInsensitiveAscii.StaticInstance);
				}
				return this.m_InnerCollection;
			}
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x000803A8 File Offset: 0x0007E5A8
		internal static bool AllowMultiValues(string name)
		{
			HeaderInfo headerInfo = WebHeaderCollection.HInfo[name];
			return headerInfo.AllowMultiValues || headerInfo.HeaderName == "";
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x000803DB File Offset: 0x0007E5DB
		private bool AllowHttpRequestHeader
		{
			get
			{
				if (this.m_Type == WebHeaderCollectionType.Unknown)
				{
					this.m_Type = WebHeaderCollectionType.WebRequest;
				}
				return this.m_Type == WebHeaderCollectionType.WebRequest || this.m_Type == WebHeaderCollectionType.HttpWebRequest || this.m_Type == WebHeaderCollectionType.HttpListenerRequest;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x00080409 File Offset: 0x0007E609
		internal bool AllowHttpResponseHeader
		{
			get
			{
				if (this.m_Type == WebHeaderCollectionType.Unknown)
				{
					this.m_Type = WebHeaderCollectionType.WebResponse;
				}
				return this.m_Type == WebHeaderCollectionType.WebResponse || this.m_Type == WebHeaderCollectionType.HttpWebResponse || this.m_Type == WebHeaderCollectionType.HttpListenerResponse;
			}
		}

		/// <summary>Gets or sets the specified request header.</summary>
		/// <returns>A <see cref="T:System.String" /> instance containing the specified header value.</returns>
		/// <param name="header">The request header value.</param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170006C8 RID: 1736
		public string this[HttpRequestHeader header]
		{
			get
			{
				if (!this.AllowHttpRequestHeader)
				{
					throw new InvalidOperationException(global::SR.GetString("This collection holds response headers and cannot contain the specified request header."));
				}
				return base[UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header)];
			}
			set
			{
				if (!this.AllowHttpRequestHeader)
				{
					throw new InvalidOperationException(global::SR.GetString("This collection holds response headers and cannot contain the specified request header."));
				}
				base[UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header)] = value;
			}
		}

		/// <summary>Gets or sets the specified response header.</summary>
		/// <returns>A <see cref="T:System.String" /> instance containing the specified header.</returns>
		/// <param name="header">The response header value.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170006C9 RID: 1737
		public string this[HttpResponseHeader header]
		{
			get
			{
				if (!this.AllowHttpResponseHeader)
				{
					throw new InvalidOperationException(global::SR.GetString("This collection holds request headers and cannot contain the specified response header."));
				}
				if (this.m_CommonHeaders != null)
				{
					if (header == HttpResponseHeader.ProxyAuthenticate)
					{
						return this.m_CommonHeaders[9];
					}
					if (header == HttpResponseHeader.WwwAuthenticate)
					{
						return this.m_CommonHeaders[15];
					}
				}
				return base[UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header)];
			}
			set
			{
				if (!this.AllowHttpResponseHeader)
				{
					throw new InvalidOperationException(global::SR.GetString("This collection holds request headers and cannot contain the specified response header."));
				}
				if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
				{
					throw new ArgumentOutOfRangeException("value", value, global::SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
				}
				base[UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header)] = value;
			}
		}

		/// <summary>Inserts the specified header with the specified value into the collection.</summary>
		/// <param name="header">The header to add to the collection. </param>
		/// <param name="value">The content of the header. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />. </exception>
		// Token: 0x0600212B RID: 8491 RVA: 0x00080554 File Offset: 0x0007E754
		public void Add(HttpRequestHeader header, string value)
		{
			if (!this.AllowHttpRequestHeader)
			{
				throw new InvalidOperationException(global::SR.GetString("This collection holds response headers and cannot contain the specified request header."));
			}
			this.Add(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Inserts the specified header with the specified value into the collection.</summary>
		/// <param name="header">The header to add to the collection. </param>
		/// <param name="value">The content of the header. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />. </exception>
		// Token: 0x0600212C RID: 8492 RVA: 0x0008057C File Offset: 0x0007E77C
		public void Add(HttpResponseHeader header, string value)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(global::SR.GetString("This collection holds request headers and cannot contain the specified response header."));
			}
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, global::SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.Add(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpRequestHeader" /> value to set. </param>
		/// <param name="value">The content of the header to set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />. </exception>
		// Token: 0x0600212D RID: 8493 RVA: 0x000805F0 File Offset: 0x0007E7F0
		public void Set(HttpRequestHeader header, string value)
		{
			if (!this.AllowHttpRequestHeader)
			{
				throw new InvalidOperationException(global::SR.GetString("This collection holds response headers and cannot contain the specified request header."));
			}
			this.Set(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpResponseHeader" /> value to set. </param>
		/// <param name="value">The content of the header to set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />. </exception>
		// Token: 0x0600212E RID: 8494 RVA: 0x00080618 File Offset: 0x0007E818
		public void Set(HttpResponseHeader header, string value)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(global::SR.GetString("This collection holds request headers and cannot contain the specified response header."));
			}
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, global::SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.Set(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header), value);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x0008068C File Offset: 0x0007E88C
		internal void SetInternal(HttpResponseHeader header, string value)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(global::SR.GetString("This collection holds request headers and cannot contain the specified response header."));
			}
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, global::SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.SetInternal(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpRequestHeader" /> instance to remove from the collection. </param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002130 RID: 8496 RVA: 0x00080700 File Offset: 0x0007E900
		public void Remove(HttpRequestHeader header)
		{
			if (!this.AllowHttpRequestHeader)
			{
				throw new InvalidOperationException(global::SR.GetString("This collection holds response headers and cannot contain the specified request header."));
			}
			this.Remove(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header));
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpResponseHeader" /> instance to remove from the collection. </param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002131 RID: 8497 RVA: 0x00080726 File Offset: 0x0007E926
		public void Remove(HttpResponseHeader header)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(global::SR.GetString("This collection holds request headers and cannot contain the specified response header."));
			}
			this.Remove(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header));
		}

		/// <summary>Inserts a header into the collection without checking whether the header is on the restricted header list.</summary>
		/// <param name="headerName">The header to add to the collection. </param>
		/// <param name="headerValue">The content of the header. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> is null, <see cref="F:System.String.Empty" />, or contains invalid characters.-or- <paramref name="headerValue" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="headerName" /> is not null and the length of <paramref name="headerValue" /> is too long (greater than 65,535 characters).</exception>
		// Token: 0x06002132 RID: 8498 RVA: 0x0008074C File Offset: 0x0007E94C
		protected void AddWithoutValidate(string headerName, string headerValue)
		{
			headerName = WebHeaderCollection.CheckBadChars(headerName, false);
			headerValue = WebHeaderCollection.CheckBadChars(headerValue, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && headerValue != null && headerValue.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("headerValue", headerValue, global::SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(headerName, headerValue);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000807C8 File Offset: 0x0007E9C8
		internal void SetAddVerified(string name, string value)
		{
			if (WebHeaderCollection.HInfo[name].AllowMultiValues)
			{
				this.NormalizeCommonHeaders();
				base.InvalidateCachedArrays();
				this.InnerCollection.Add(name, value);
				return;
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0008081A File Offset: 0x0007EA1A
		internal void AddInternal(string name, string value)
		{
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x00080835 File Offset: 0x0007EA35
		internal void ChangeInternal(string name, string value)
		{
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x00080850 File Offset: 0x0007EA50
		internal void RemoveInternal(string name)
		{
			this.NormalizeCommonHeaders();
			if (this.m_InnerCollection != null)
			{
				base.InvalidateCachedArrays();
				this.m_InnerCollection.Remove(name);
			}
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x00080872 File Offset: 0x0007EA72
		internal void CheckUpdate(string name, string value)
		{
			value = WebHeaderCollection.CheckBadChars(value, true);
			this.ChangeInternal(name, value);
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00080885 File Offset: 0x0007EA85
		private void AddInternalNotCommon(string name, string value)
		{
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0008089C File Offset: 0x0007EA9C
		internal static string CheckBadChars(string name, bool isHeaderValue)
		{
			if (name != null && name.Length != 0)
			{
				if (isHeaderValue)
				{
					name = name.Trim(WebHeaderCollection.HttpTrimCharacters);
					int num = 0;
					for (int i = 0; i < name.Length; i++)
					{
						char c = 'ÿ' & name[i];
						switch (num)
						{
						case 0:
							if (c == '\r')
							{
								num = 1;
							}
							else if (c == '\n')
							{
								num = 2;
							}
							else if (c == '\u007f' || (c < ' ' && c != '\t'))
							{
								throw new ArgumentException(global::SR.GetString("Specified value has invalid Control characters."), "value");
							}
							break;
						case 1:
							if (c != '\n')
							{
								throw new ArgumentException(global::SR.GetString("Specified value has invalid CRLF characters."), "value");
							}
							num = 2;
							break;
						case 2:
							if (c != ' ' && c != '\t')
							{
								throw new ArgumentException(global::SR.GetString("Specified value has invalid CRLF characters."), "value");
							}
							num = 0;
							break;
						}
					}
					if (num != 0)
					{
						throw new ArgumentException(global::SR.GetString("Specified value has invalid CRLF characters."), "value");
					}
				}
				else
				{
					if (name.IndexOfAny(ValidationHelper.InvalidParamChars) != -1)
					{
						throw new ArgumentException(global::SR.GetString("Specified value has invalid HTTP Header characters."), "name");
					}
					if (WebHeaderCollection.ContainsNonAsciiChars(name))
					{
						throw new ArgumentException(global::SR.GetString("Specified value has invalid non-ASCII characters."), "name");
					}
				}
				return name;
			}
			if (!isHeaderValue)
			{
				throw (name == null) ? new ArgumentNullException("name") : new ArgumentException(global::SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "name" }), "name");
			}
			return string.Empty;
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x00080A0E File Offset: 0x0007EC0E
		internal static bool IsValidToken(string token)
		{
			return token.Length > 0 && token.IndexOfAny(ValidationHelper.InvalidParamChars) == -1 && !WebHeaderCollection.ContainsNonAsciiChars(token);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00080A34 File Offset: 0x0007EC34
		internal static bool ContainsNonAsciiChars(string token)
		{
			for (int i = 0; i < token.Length; i++)
			{
				if (token[i] < ' ' || token[i] > '~')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x00080A6C File Offset: 0x0007EC6C
		internal void ThrowOnRestrictedHeader(string headerName)
		{
			if (this.m_Type == WebHeaderCollectionType.HttpWebRequest)
			{
				if (WebHeaderCollection.HInfo[headerName].IsRequestRestricted)
				{
					throw new ArgumentException(global::SR.GetString("The '{0}' header must be modified using the appropriate property or method.", new object[] { headerName }), "name");
				}
			}
			else if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && WebHeaderCollection.HInfo[headerName].IsResponseRestricted)
			{
				throw new ArgumentException(global::SR.GetString("The '{0}' header must be modified using the appropriate property or method.", new object[] { headerName }), "name");
			}
		}

		/// <summary>Inserts a header with the specified name and value into the collection.</summary>
		/// <param name="name">The header to add to the collection. </param>
		/// <param name="value">The content of the header. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null, <see cref="F:System.String.Empty" />, or contains invalid characters.-or- <paramref name="name" /> is a restricted header that must be set with a property setting.-or- <paramref name="value" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		// Token: 0x0600213D RID: 8509 RVA: 0x00080AF0 File Offset: 0x0007ECF0
		public override void Add(string name, string value)
		{
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.ThrowOnRestrictedHeader(name);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, global::SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		/// <summary>Inserts the specified header into the collection.</summary>
		/// <param name="header">The header to add, with the name and value separated by a colon. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="header" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="header" /> does not contain a colon (:) character.The length of <paramref name="value" /> is greater than 65535.-or- The name part of <paramref name="header" /> is <see cref="F:System.String.Empty" /> or contains invalid characters.-or- <paramref name="header" /> is a restricted header that should be set with a property.-or- The value part of <paramref name="header" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length the string after the colon (:) is greater than 65535. </exception>
		// Token: 0x0600213E RID: 8510 RVA: 0x00080B74 File Offset: 0x0007ED74
		public void Add(string header)
		{
			if (ValidationHelper.IsBlankString(header))
			{
				throw new ArgumentNullException("header");
			}
			int num = header.IndexOf(':');
			if (num < 0)
			{
				throw new ArgumentException(global::SR.GetString("Specified value does not have a ':' separator."), "header");
			}
			string text = header.Substring(0, num);
			string text2 = header.Substring(num + 1);
			text = WebHeaderCollection.CheckBadChars(text, false);
			this.ThrowOnRestrictedHeader(text);
			text2 = WebHeaderCollection.CheckBadChars(text2, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && text2 != null && text2.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", text2, global::SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(text, text2);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="name">The header to set. </param>
		/// <param name="value">The content of the header to set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a restricted header.-or- <paramref name="name" /> or <paramref name="value" /> contain invalid characters. </exception>
		// Token: 0x0600213F RID: 8511 RVA: 0x00080C3C File Offset: 0x0007EE3C
		public override void Set(string name, string value)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.ThrowOnRestrictedHeader(name);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, global::SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00080CD0 File Offset: 0x0007EED0
		internal void SetInternal(string name, string value)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			name = WebHeaderCollection.CheckBadChars(name, false);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, global::SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="name">The name of the header to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null<see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a restricted header.-or- <paramref name="name" /> contains invalid characters. </exception>
		// Token: 0x06002141 RID: 8513 RVA: 0x00080D60 File Offset: 0x0007EF60
		public override void Remove(string name)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			this.ThrowOnRestrictedHeader(name);
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.NormalizeCommonHeaders();
			if (this.m_InnerCollection != null)
			{
				base.InvalidateCachedArrays();
				this.m_InnerCollection.Remove(name);
			}
		}

		/// <summary>Gets an array of header values stored in a header.</summary>
		/// <returns>An array of header strings.</returns>
		/// <param name="header">The header to return. </param>
		// Token: 0x06002142 RID: 8514 RVA: 0x00080DB0 File Offset: 0x0007EFB0
		public override string[] GetValues(string header)
		{
			this.NormalizeCommonHeaders();
			HeaderInfo headerInfo = WebHeaderCollection.HInfo[header];
			string[] values = this.InnerCollection.GetValues(header);
			if (headerInfo == null || values == null || !headerInfo.AllowMultiValues)
			{
				return values;
			}
			ArrayList arrayList = null;
			for (int i = 0; i < values.Length; i++)
			{
				string[] array = headerInfo.Parser(values[i]);
				if (arrayList == null)
				{
					if (array.Length > 1)
					{
						arrayList = new ArrayList(values);
						arrayList.RemoveRange(i, values.Length - i);
						arrayList.AddRange(array);
					}
				}
				else
				{
					arrayList.AddRange(array);
				}
			}
			if (arrayList != null)
			{
				string[] array2 = new string[arrayList.Count];
				arrayList.CopyTo(array2);
				return array2;
			}
			return values;
		}

		/// <summary>This method is obsolete.</summary>
		/// <returns>The <see cref="T:System.String" /> representation of the collection.</returns>
		// Token: 0x06002143 RID: 8515 RVA: 0x00080E5A File Offset: 0x0007F05A
		public override string ToString()
		{
			return WebHeaderCollection.GetAsString(this, false, false);
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x00080E64 File Offset: 0x0007F064
		internal string ToString(bool forTrace)
		{
			return WebHeaderCollection.GetAsString(this, false, true);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x00080E70 File Offset: 0x0007F070
		internal static string GetAsString(NameValueCollection cc, bool winInetCompat, bool forTrace)
		{
			if (winInetCompat)
			{
				throw new InvalidOperationException();
			}
			if (cc == null || cc.Count == 0)
			{
				return "\r\n";
			}
			StringBuilder stringBuilder = new StringBuilder(30 * cc.Count);
			string text = cc[string.Empty];
			if (text != null)
			{
				stringBuilder.Append(text).Append("\r\n");
			}
			for (int i = 0; i < cc.Count; i++)
			{
				string key = cc.GetKey(i);
				string text2 = cc.Get(i);
				if (!ValidationHelper.IsBlankString(key))
				{
					stringBuilder.Append(key);
					if (winInetCompat)
					{
						stringBuilder.Append(':');
					}
					else
					{
						stringBuilder.Append(": ");
					}
					stringBuilder.Append(text2).Append("\r\n");
				}
			}
			if (!forTrace)
			{
				stringBuilder.Append("\r\n");
			}
			return stringBuilder.ToString();
		}

		/// <summary>Converts the <see cref="T:System.Net.WebHeaderCollection" /> to a byte array..</summary>
		/// <returns>A <see cref="T:System.Byte" /> array holding the header collection.</returns>
		// Token: 0x06002146 RID: 8518 RVA: 0x00080F3B File Offset: 0x0007F13B
		public byte[] ToByteArray()
		{
			return WebHeaderCollection.HeaderEncoding.GetBytes(this.ToString());
		}

		/// <summary>Tests whether the specified HTTP header can be set for the request.</summary>
		/// <returns>true if the header is restricted; otherwise false.</returns>
		/// <param name="headerName">The header to test. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters. </exception>
		// Token: 0x06002147 RID: 8519 RVA: 0x00080F48 File Offset: 0x0007F148
		public static bool IsRestricted(string headerName)
		{
			return WebHeaderCollection.IsRestricted(headerName, false);
		}

		/// <summary>Tests whether the specified HTTP header can be set for the request or the response.</summary>
		/// <returns>true if the header is restricted; otherwise, false.</returns>
		/// <param name="headerName">The header to test.</param>
		/// <param name="response">Does the Framework test the response or the request?</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters. </exception>
		// Token: 0x06002148 RID: 8520 RVA: 0x00080F51 File Offset: 0x0007F151
		public static bool IsRestricted(string headerName, bool response)
		{
			if (!response)
			{
				return WebHeaderCollection.HInfo[WebHeaderCollection.CheckBadChars(headerName, false)].IsRequestRestricted;
			}
			return WebHeaderCollection.HInfo[WebHeaderCollection.CheckBadChars(headerName, false)].IsResponseRestricted;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebHeaderCollection" /> class.</summary>
		// Token: 0x06002149 RID: 8521 RVA: 0x00080F83 File Offset: 0x0007F183
		public WebHeaderCollection()
			: base(DBNull.Value)
		{
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x00080F90 File Offset: 0x0007F190
		internal WebHeaderCollection(WebHeaderCollectionType type)
			: base(DBNull.Value)
		{
			this.m_Type = type;
			if (type == WebHeaderCollectionType.HttpWebResponse)
			{
				this.m_CommonHeaders = new string[WebHeaderCollection.s_CommonHeaderNames.Length - 1];
			}
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x00080FBC File Offset: 0x0007F1BC
		internal WebHeaderCollection(NameValueCollection cc)
			: base(DBNull.Value)
		{
			this.m_InnerCollection = new NameValueCollection(cc.Count + 2, CaseInsensitiveAscii.StaticInstance);
			int count = cc.Count;
			for (int i = 0; i < count; i++)
			{
				string key = cc.GetKey(i);
				string[] values = cc.GetValues(i);
				if (values != null)
				{
					for (int j = 0; j < values.Length; j++)
					{
						this.InnerCollection.Add(key, values[j]);
					}
				}
				else
				{
					this.InnerCollection.Add(key, null);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebHeaderCollection" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> containing the information required to serialize the <see cref="T:System.Net.WebHeaderCollection" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> containing the source of the serialized stream associated with the new <see cref="T:System.Net.WebHeaderCollection" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is a null reference or <see cref="F:System.String.Empty" />. </exception>
		// Token: 0x0600214C RID: 8524 RVA: 0x00081044 File Offset: 0x0007F244
		protected WebHeaderCollection(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(DBNull.Value)
		{
			int @int = serializationInfo.GetInt32("Count");
			this.m_InnerCollection = new NameValueCollection(@int + 2, CaseInsensitiveAscii.StaticInstance);
			for (int i = 0; i < @int; i++)
			{
				string @string = serializationInfo.GetString(i.ToString(NumberFormatInfo.InvariantInfo));
				string string2 = serializationInfo.GetString((i + @int).ToString(NumberFormatInfo.InvariantInfo));
				this.InnerCollection.Add(@string, string2);
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x0600214D RID: 8525 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void OnDeserialization(object sender)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x0600214E RID: 8526 RVA: 0x000810C0 File Offset: 0x0007F2C0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.NormalizeCommonHeaders();
			serializationInfo.AddValue("Count", this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				serializationInfo.AddValue(i.ToString(NumberFormatInfo.InvariantInfo), this.GetKey(i));
				serializationInfo.AddValue((i + this.Count).ToString(NumberFormatInfo.InvariantInfo), this.Get(i));
			}
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x00081130 File Offset: 0x0007F330
		internal unsafe DataParseStatus ParseHeaders(byte[] buffer, int size, ref int unparsed, ref int totalResponseHeadersLength, int maximumResponseHeadersLength, ref WebParseError parseError)
		{
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			if (buffer.Length < size)
			{
				return DataParseStatus.NeedMoreData;
			}
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int i = unparsed;
			int num4 = totalResponseHeadersLength;
			WebParseErrorCode webParseErrorCode = WebParseErrorCode.Generic;
			for (;;)
			{
				string text = string.Empty;
				string text2 = string.Empty;
				bool flag = false;
				string text3 = null;
				if (this.Count == 0)
				{
					while (i < size)
					{
						char c = (char)ptr[i];
						if (c != ' ' && c != '\t')
						{
							break;
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_6;
						}
					}
					if (i == size)
					{
						goto Block_7;
					}
				}
				int num5 = i;
				while (i < size)
				{
					char c = (char)ptr[i];
					if (c != ':' && c != '\n')
					{
						if (c > ' ')
						{
							num = i;
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_12;
						}
					}
					else
					{
						if (c != ':')
						{
							break;
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_15;
						}
						break;
					}
				}
				if (i == size)
				{
					goto Block_16;
				}
				int num6;
				for (;;)
				{
					num6 = ((this.Count == 0 && num < 0) ? 1 : 0);
					char c;
					while (i < size && num6 < 2)
					{
						c = (char)ptr[i];
						if (c > ' ')
						{
							break;
						}
						if (c == '\n')
						{
							num6++;
							if (num6 == 1)
							{
								if (i + 1 == size)
								{
									goto Block_21;
								}
								flag = ptr[i + 1] == 32 || ptr[i + 1] == 9;
							}
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_24;
						}
					}
					if (num6 != 2 && (num6 != 1 || flag))
					{
						if (i == size)
						{
							goto Block_28;
						}
						num2 = i;
						while (i < size)
						{
							c = (char)ptr[i];
							if (c == '\n')
							{
								break;
							}
							if (c > ' ')
							{
								num3 = i;
							}
							i++;
							if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
							{
								goto Block_32;
							}
						}
						if (i == size)
						{
							goto Block_33;
						}
						num6 = 0;
						while (i < size && num6 < 2)
						{
							c = (char)ptr[i];
							if (c != '\r' && c != '\n')
							{
								break;
							}
							if (c == '\n')
							{
								num6++;
							}
							i++;
							if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
							{
								goto Block_37;
							}
						}
						if (i == size && num6 < 2)
						{
							goto Block_40;
						}
					}
					if (num2 >= 0 && num2 > num && num3 >= num2)
					{
						text2 = WebHeaderCollection.HeaderEncoding.GetString(ptr + num2, num3 - num2 + 1);
					}
					text3 = ((text3 == null) ? text2 : (text3 + " " + text2));
					if (i >= size || num6 != 1)
					{
						break;
					}
					c = (char)ptr[i];
					if (c != ' ' && c != '\t')
					{
						break;
					}
					i++;
					if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
					{
						goto Block_49;
					}
				}
				if (num5 >= 0 && num >= num5)
				{
					text = WebHeaderCollection.HeaderEncoding.GetString(ptr + num5, num - num5 + 1);
				}
				if (text.Length > 0)
				{
					this.AddInternal(text, text3);
				}
				totalResponseHeadersLength = num4;
				unparsed = i;
				if (num6 == 2)
				{
					goto Block_53;
				}
			}
			Block_6:
			DataParseStatus dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_7:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_030A;
			Block_12:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_15:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_16:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_030A;
			Block_21:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_030A;
			Block_24:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_28:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_030A;
			Block_32:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_33:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_030A;
			Block_37:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_40:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_030A;
			Block_49:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_53:
			dataParseStatus = DataParseStatus.Done;
			IL_030A:
			if (dataParseStatus == DataParseStatus.Invalid)
			{
				parseError.Section = WebParseErrorSection.ResponseHeader;
				parseError.Code = webParseErrorCode;
			}
			return dataParseStatus;
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x00081460 File Offset: 0x0007F660
		internal unsafe DataParseStatus ParseHeadersStrict(byte[] buffer, int size, ref int unparsed, ref int totalResponseHeadersLength, int maximumResponseHeadersLength, ref WebParseError parseError)
		{
			WebParseErrorCode webParseErrorCode = WebParseErrorCode.Generic;
			DataParseStatus dataParseStatus = DataParseStatus.Invalid;
			int num = unparsed;
			int num2 = ((maximumResponseHeadersLength <= 0) ? int.MaxValue : (maximumResponseHeadersLength - totalResponseHeadersLength + num));
			DataParseStatus dataParseStatus2 = DataParseStatus.DataTooBig;
			if (size < num2)
			{
				num2 = size;
				dataParseStatus2 = DataParseStatus.NeedMoreData;
			}
			if (num >= num2)
			{
				dataParseStatus = dataParseStatus2;
			}
			else
			{
				try
				{
					fixed (byte[] array = buffer)
					{
						byte* ptr;
						if (buffer == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						while (ptr[num] != 13)
						{
							int num3 = num;
							while (num < num2 && ((ptr[num] > 127) ? WebHeaderCollection.RfcChar.High : WebHeaderCollection.RfcCharMap[(int)ptr[num]]) == WebHeaderCollection.RfcChar.Reg)
							{
								num++;
							}
							if (num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_0416;
							}
							if (num == num3)
							{
								dataParseStatus = DataParseStatus.Invalid;
								webParseErrorCode = WebParseErrorCode.InvalidHeaderName;
								goto IL_0416;
							}
							int num4 = num - 1;
							int num5 = 0;
							WebHeaderCollection.RfcChar rfcChar;
							while (num < num2 && (rfcChar = ((ptr[num] > 127) ? WebHeaderCollection.RfcChar.High : WebHeaderCollection.RfcCharMap[(int)ptr[num]])) != WebHeaderCollection.RfcChar.Colon)
							{
								switch (rfcChar)
								{
								case WebHeaderCollection.RfcChar.CR:
									if (num5 != 0)
									{
										goto IL_011D;
									}
									num5 = 1;
									break;
								case WebHeaderCollection.RfcChar.LF:
									if (num5 != 1)
									{
										goto IL_011D;
									}
									num5 = 2;
									break;
								case WebHeaderCollection.RfcChar.WS:
									if (num5 == 1)
									{
										goto IL_011D;
									}
									num5 = 0;
									break;
								default:
									goto IL_011D;
								}
								num++;
								continue;
								IL_011D:
								dataParseStatus = DataParseStatus.Invalid;
								webParseErrorCode = WebParseErrorCode.CrLfError;
								goto IL_0416;
							}
							if (num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_0416;
							}
							if (num5 != 0)
							{
								dataParseStatus = DataParseStatus.Invalid;
								webParseErrorCode = WebParseErrorCode.IncompleteHeaderLine;
								goto IL_0416;
							}
							if (++num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_0416;
							}
							int num6 = -1;
							int num7 = -1;
							StringBuilder stringBuilder = null;
							while (num < num2 && ((rfcChar = ((ptr[num] > 127) ? WebHeaderCollection.RfcChar.High : WebHeaderCollection.RfcCharMap[(int)ptr[num]])) == WebHeaderCollection.RfcChar.WS || num5 != 2))
							{
								switch (rfcChar)
								{
								case WebHeaderCollection.RfcChar.High:
								case WebHeaderCollection.RfcChar.Reg:
								case WebHeaderCollection.RfcChar.Colon:
								case WebHeaderCollection.RfcChar.Delim:
									if (num5 == 1)
									{
										goto IL_023E;
									}
									if (num5 == 3)
									{
										num5 = 0;
										if (num6 != -1)
										{
											string @string = WebHeaderCollection.HeaderEncoding.GetString(ptr + num6, num7 - num6 + 1);
											if (stringBuilder == null)
											{
												stringBuilder = new StringBuilder(@string, @string.Length * 5);
											}
											else
											{
												stringBuilder.Append(" ");
												stringBuilder.Append(@string);
											}
										}
										num6 = -1;
									}
									if (num6 == -1)
									{
										num6 = num;
									}
									num7 = num;
									break;
								case WebHeaderCollection.RfcChar.Ctl:
									goto IL_023E;
								case WebHeaderCollection.RfcChar.CR:
									if (num5 != 0)
									{
										goto IL_023E;
									}
									num5 = 1;
									break;
								case WebHeaderCollection.RfcChar.LF:
									if (num5 != 1)
									{
										goto IL_023E;
									}
									num5 = 2;
									break;
								case WebHeaderCollection.RfcChar.WS:
									if (num5 == 1)
									{
										goto IL_023E;
									}
									if (num5 == 2)
									{
										num5 = 3;
									}
									break;
								default:
									goto IL_023E;
								}
								num++;
								continue;
								IL_023E:
								dataParseStatus = DataParseStatus.Invalid;
								webParseErrorCode = WebParseErrorCode.CrLfError;
								goto IL_0416;
							}
							if (num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_0416;
							}
							string text = ((num6 == -1) ? "" : WebHeaderCollection.HeaderEncoding.GetString(ptr + num6, num7 - num6 + 1));
							if (stringBuilder != null)
							{
								if (text.Length != 0)
								{
									stringBuilder.Append(" ");
									stringBuilder.Append(text);
								}
								text = stringBuilder.ToString();
							}
							string text2 = null;
							int num8 = num4 - num3 + 1;
							if (this.m_CommonHeaders != null)
							{
								int num9 = (int)WebHeaderCollection.s_CommonHeaderHints[(int)(ptr[num3] & 31)];
								if (num9 >= 0)
								{
									string text3;
									for (;;)
									{
										text3 = WebHeaderCollection.s_CommonHeaderNames[num9++];
										if (text3.Length < num8 || CaseInsensitiveAscii.AsciiToLower[(int)ptr[num3]] != CaseInsensitiveAscii.AsciiToLower[(int)text3[0]])
										{
											goto IL_03E3;
										}
										if (text3.Length <= num8)
										{
											byte* ptr2 = ptr + num3 + 1;
											int num10 = 1;
											while (num10 < text3.Length && ((char)(*(ptr2++)) == text3[num10] || CaseInsensitiveAscii.AsciiToLower[(int)(*(ptr2 - 1))] == CaseInsensitiveAscii.AsciiToLower[(int)text3[num10]]))
											{
												num10++;
											}
											if (num10 == text3.Length)
											{
												break;
											}
										}
									}
									this.m_NumCommonHeaders++;
									num9--;
									if (this.m_CommonHeaders[num9] == null)
									{
										this.m_CommonHeaders[num9] = text;
									}
									else
									{
										this.NormalizeCommonHeaders();
										this.AddInternalNotCommon(text3, text);
									}
									text2 = text3;
								}
							}
							IL_03E3:
							if (text2 == null)
							{
								text2 = WebHeaderCollection.HeaderEncoding.GetString(ptr + num3, num8);
								this.AddInternalNotCommon(text2, text);
							}
							totalResponseHeadersLength += num - unparsed;
							unparsed = num;
						}
						if (++num == num2)
						{
							dataParseStatus = dataParseStatus2;
						}
						else if (ptr[num++] == 10)
						{
							totalResponseHeadersLength += num - unparsed;
							unparsed = num;
							dataParseStatus = DataParseStatus.Done;
						}
						else
						{
							dataParseStatus = DataParseStatus.Invalid;
							webParseErrorCode = WebParseErrorCode.CrLfError;
						}
					}
				}
				finally
				{
					byte[] array = null;
				}
			}
			IL_0416:
			if (dataParseStatus == DataParseStatus.Invalid)
			{
				parseError.Section = WebParseErrorSection.ResponseHeader;
				parseError.Code = webParseErrorCode;
			}
			return dataParseStatus;
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.WebHeaderCollection" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x06002151 RID: 8529 RVA: 0x000818B4 File Offset: 0x0007FAB4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Get the value of a particular header in the collection, specified by the name of the header.</summary>
		/// <returns>A <see cref="T:System.String" /> holding the value of the specified header.</returns>
		/// <param name="name">The name of the Web header.</param>
		// Token: 0x06002152 RID: 8530 RVA: 0x000818C0 File Offset: 0x0007FAC0
		public override string Get(string name)
		{
			if (this.m_CommonHeaders != null && name != null && name.Length > 0 && name[0] < 'Ā')
			{
				int num = (int)WebHeaderCollection.s_CommonHeaderHints[(int)(name[0] & '\u001f')];
				if (num >= 0)
				{
					for (;;)
					{
						string text = WebHeaderCollection.s_CommonHeaderNames[num++];
						if (text.Length < name.Length || CaseInsensitiveAscii.AsciiToLower[(int)name[0]] != CaseInsensitiveAscii.AsciiToLower[(int)text[0]])
						{
							goto IL_00EF;
						}
						if (text.Length <= name.Length)
						{
							int num2 = 1;
							while (num2 < text.Length && (name[num2] == text[num2] || (name[num2] <= 'ÿ' && CaseInsensitiveAscii.AsciiToLower[(int)name[num2]] == CaseInsensitiveAscii.AsciiToLower[(int)text[num2]])))
							{
								num2++;
							}
							if (num2 == text.Length)
							{
								break;
							}
						}
					}
					return this.m_CommonHeaders[num - 1];
				}
			}
			IL_00EF:
			if (this.m_InnerCollection == null)
			{
				return null;
			}
			return this.m_InnerCollection.Get(name);
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Net.WebHeaderCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Net.WebHeaderCollection" />.</returns>
		// Token: 0x06002153 RID: 8531 RVA: 0x000819D2 File Offset: 0x0007FBD2
		public override IEnumerator GetEnumerator()
		{
			this.NormalizeCommonHeaders();
			return new NameObjectCollectionBase.NameObjectKeysEnumerator(this.InnerCollection);
		}

		/// <summary>Gets the number of headers in the collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> indicating the number of headers in a request.</returns>
		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x000819E5 File Offset: 0x0007FBE5
		public override int Count
		{
			get
			{
				return ((this.m_InnerCollection == null) ? 0 : this.m_InnerCollection.Count) + this.m_NumCommonHeaders;
			}
		}

		/// <summary>Gets the collection of header names (keys) in the collection.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> containing all header names in a Web request.</returns>
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x00081A04 File Offset: 0x0007FC04
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				this.NormalizeCommonHeaders();
				return this.InnerCollection.Keys;
			}
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00081A17 File Offset: 0x0007FC17
		internal override bool InternalHasKeys()
		{
			this.NormalizeCommonHeaders();
			return this.m_InnerCollection != null && this.m_InnerCollection.HasKeys();
		}

		/// <summary>Get the value of a particular header in the collection, specified by an index into the collection.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value of the specified header.</returns>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative. -or-<paramref name="index" /> exceeds the size of the collection.</exception>
		// Token: 0x06002157 RID: 8535 RVA: 0x00081A34 File Offset: 0x0007FC34
		public override string Get(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.Get(index);
		}

		/// <summary>Gets an array of header values stored in the <paramref name="index" /> position of the header collection.</summary>
		/// <returns>An array of header strings.</returns>
		/// <param name="index">The header index to return.</param>
		// Token: 0x06002158 RID: 8536 RVA: 0x00081A48 File Offset: 0x0007FC48
		public override string[] GetValues(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.GetValues(index);
		}

		/// <summary>Get the header name at the specified position in the collection.</summary>
		/// <returns>A <see cref="T:System.String" /> holding the header name.</returns>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative. -or-<paramref name="index" /> exceeds the size of the collection.</exception>
		// Token: 0x06002159 RID: 8537 RVA: 0x00081A5C File Offset: 0x0007FC5C
		public override string GetKey(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.GetKey(index);
		}

		/// <summary>Gets all header names (keys) in the collection.</summary>
		/// <returns>An array of type <see cref="T:System.String" /> containing all header names in a Web request.</returns>
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x00081A70 File Offset: 0x0007FC70
		public override string[] AllKeys
		{
			get
			{
				this.NormalizeCommonHeaders();
				return this.InnerCollection.AllKeys;
			}
		}

		/// <summary>Removes all headers from the collection.</summary>
		// Token: 0x0600215B RID: 8539 RVA: 0x00081A83 File Offset: 0x0007FC83
		public override void Clear()
		{
			this.m_CommonHeaders = null;
			this.m_NumCommonHeaders = 0;
			base.InvalidateCachedArrays();
			if (this.m_InnerCollection != null)
			{
				this.m_InnerCollection.Clear();
			}
		}

		// Token: 0x04001E2B RID: 7723
		private const int ApproxAveHeaderLineSize = 30;

		// Token: 0x04001E2C RID: 7724
		private const int ApproxHighAvgNumHeaders = 16;

		// Token: 0x04001E2D RID: 7725
		private static readonly HeaderInfoTable HInfo = new HeaderInfoTable();

		// Token: 0x04001E2E RID: 7726
		private string[] m_CommonHeaders;

		// Token: 0x04001E2F RID: 7727
		private int m_NumCommonHeaders;

		// Token: 0x04001E30 RID: 7728
		private static readonly string[] s_CommonHeaderNames = new string[]
		{
			"Accept-Ranges", "Content-Length", "Cache-Control", "Content-Type", "Date", "Expires", "ETag", "Last-Modified", "Location", "Proxy-Authenticate",
			"P3P", "Set-Cookie2", "Set-Cookie", "Server", "Via", "WWW-Authenticate", "X-AspNet-Version", "X-Powered-By", "["
		};

		// Token: 0x04001E31 RID: 7729
		private static readonly sbyte[] s_CommonHeaderHints = new sbyte[]
		{
			-1, 0, -1, 1, 4, 5, -1, -1, -1, -1,
			-1, -1, 7, -1, -1, -1, 9, -1, -1, 11,
			-1, -1, 14, 15, 16, -1, -1, -1, -1, -1,
			-1, -1
		};

		// Token: 0x04001E32 RID: 7730
		private const int c_AcceptRanges = 0;

		// Token: 0x04001E33 RID: 7731
		private const int c_ContentLength = 1;

		// Token: 0x04001E34 RID: 7732
		private const int c_CacheControl = 2;

		// Token: 0x04001E35 RID: 7733
		private const int c_ContentType = 3;

		// Token: 0x04001E36 RID: 7734
		private const int c_Date = 4;

		// Token: 0x04001E37 RID: 7735
		private const int c_Expires = 5;

		// Token: 0x04001E38 RID: 7736
		private const int c_ETag = 6;

		// Token: 0x04001E39 RID: 7737
		private const int c_LastModified = 7;

		// Token: 0x04001E3A RID: 7738
		private const int c_Location = 8;

		// Token: 0x04001E3B RID: 7739
		private const int c_ProxyAuthenticate = 9;

		// Token: 0x04001E3C RID: 7740
		private const int c_P3P = 10;

		// Token: 0x04001E3D RID: 7741
		private const int c_SetCookie2 = 11;

		// Token: 0x04001E3E RID: 7742
		private const int c_SetCookie = 12;

		// Token: 0x04001E3F RID: 7743
		private const int c_Server = 13;

		// Token: 0x04001E40 RID: 7744
		private const int c_Via = 14;

		// Token: 0x04001E41 RID: 7745
		private const int c_WwwAuthenticate = 15;

		// Token: 0x04001E42 RID: 7746
		private const int c_XAspNetVersion = 16;

		// Token: 0x04001E43 RID: 7747
		private const int c_XPoweredBy = 17;

		// Token: 0x04001E44 RID: 7748
		private NameValueCollection m_InnerCollection;

		// Token: 0x04001E45 RID: 7749
		private WebHeaderCollectionType m_Type;

		// Token: 0x04001E46 RID: 7750
		private static readonly char[] HttpTrimCharacters = new char[] { '\t', '\n', '\v', '\f', '\r', ' ' };

		// Token: 0x04001E47 RID: 7751
		private static WebHeaderCollection.RfcChar[] RfcCharMap = new WebHeaderCollection.RfcChar[]
		{
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.WS,
			WebHeaderCollection.RfcChar.LF,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.CR,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.WS,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Colon,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Ctl
		};

		// Token: 0x02000469 RID: 1129
		internal static class HeaderEncoding
		{
			// Token: 0x0600215D RID: 8541 RVA: 0x00081BB8 File Offset: 0x0007FDB8
			internal unsafe static string GetString(byte[] bytes, int byteIndex, int byteCount)
			{
				byte* ptr;
				if (bytes == null || bytes.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &bytes[0];
				}
				return WebHeaderCollection.HeaderEncoding.GetString(ptr + byteIndex, byteCount);
			}

			// Token: 0x0600215E RID: 8542 RVA: 0x00081BE8 File Offset: 0x0007FDE8
			internal unsafe static string GetString(byte* pBytes, int byteCount)
			{
				if (byteCount < 1)
				{
					return "";
				}
				string text = new string('\0', byteCount);
				fixed (string text2 = text)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr2 = ptr;
					while (byteCount >= 8)
					{
						*ptr2 = (char)(*pBytes);
						ptr2[1] = (char)pBytes[1];
						ptr2[2] = (char)pBytes[2];
						ptr2[3] = (char)pBytes[3];
						ptr2[4] = (char)pBytes[4];
						ptr2[5] = (char)pBytes[5];
						ptr2[6] = (char)pBytes[6];
						ptr2[7] = (char)pBytes[7];
						ptr2 += 8;
						pBytes += 8;
						byteCount -= 8;
					}
					for (int i = 0; i < byteCount; i++)
					{
						ptr2[i] = (char)pBytes[i];
					}
				}
				return text;
			}

			// Token: 0x0600215F RID: 8543 RVA: 0x00081C9E File Offset: 0x0007FE9E
			internal static int GetByteCount(string myString)
			{
				return myString.Length;
			}

			// Token: 0x06002160 RID: 8544 RVA: 0x00081CA8 File Offset: 0x0007FEA8
			internal unsafe static void GetBytes(string myString, int charIndex, int charCount, byte[] bytes, int byteIndex)
			{
				if (myString.Length == 0)
				{
					return;
				}
				fixed (byte[] array = bytes)
				{
					byte* ptr;
					if (bytes == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr + byteIndex;
					int num = charIndex + charCount;
					while (charIndex < num)
					{
						*(ptr2++) = (byte)myString[charIndex++];
					}
				}
			}

			// Token: 0x06002161 RID: 8545 RVA: 0x00081CFC File Offset: 0x0007FEFC
			internal static byte[] GetBytes(string myString)
			{
				byte[] array = new byte[myString.Length];
				if (myString.Length != 0)
				{
					WebHeaderCollection.HeaderEncoding.GetBytes(myString, 0, myString.Length, array, 0);
				}
				return array;
			}

			// Token: 0x06002162 RID: 8546 RVA: 0x00081D30 File Offset: 0x0007FF30
			[global::System.Runtime.CompilerServices.FriendAccessAllowed]
			internal static string DecodeUtf8FromString(string input)
			{
				if (string.IsNullOrWhiteSpace(input))
				{
					return input;
				}
				bool flag = false;
				for (int i = 0; i < input.Length; i++)
				{
					if (input[i] > 'ÿ')
					{
						return input;
					}
					if (input[i] > '\u007f')
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					byte[] array = new byte[input.Length];
					for (int j = 0; j < input.Length; j++)
					{
						if (input[j] > 'ÿ')
						{
							return input;
						}
						array[j] = (byte)input[j];
					}
					try
					{
						return Encoding.GetEncoding("utf-8", EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback).GetString(array);
					}
					catch (ArgumentException)
					{
					}
					return input;
				}
				return input;
			}
		}

		// Token: 0x0200046A RID: 1130
		private enum RfcChar : byte
		{
			// Token: 0x04001E49 RID: 7753
			High,
			// Token: 0x04001E4A RID: 7754
			Reg,
			// Token: 0x04001E4B RID: 7755
			Ctl,
			// Token: 0x04001E4C RID: 7756
			CR,
			// Token: 0x04001E4D RID: 7757
			LF,
			// Token: 0x04001E4E RID: 7758
			WS,
			// Token: 0x04001E4F RID: 7759
			Colon,
			// Token: 0x04001E50 RID: 7760
			Delim
		}
	}
}
