using System;
using System.Collections.Specialized;

namespace System.Web
{
	/// <summary>Provides access to HTTP request values without triggering ASP.NET request validation.</summary>
	// Token: 0x020000E7 RID: 231
	public sealed class UnvalidatedRequestValues
	{
		/// <summary>Gets the collection of cookies that the client sent, without triggering ASP.NET request validation.</summary>
		/// <returns>The cookies from the HTTP request.</returns>
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x000212CC File Offset: 0x0001F4CC
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x000212D4 File Offset: 0x0001F4D4
		public HttpCookieCollection Cookies { get; internal set; }

		/// <summary>Gets the collection of files that the client uploaded, without triggering ASP.NET request validation.</summary>
		/// <returns>The files from the HTTP request.</returns>
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x000212DD File Offset: 0x0001F4DD
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x000212E5 File Offset: 0x0001F4E5
		public HttpFileCollection Files { get; internal set; }

		/// <summary>Gets the collection of form variables that the client submitted, without triggering ASP.NET request validation.</summary>
		/// <returns>The form variables from the HTTP request.</returns>
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x000212EE File Offset: 0x0001F4EE
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x000212F6 File Offset: 0x0001F4F6
		public NameValueCollection Form { get; internal set; }

		/// <summary>Gets the collection of HTTP headers that the client sent, without triggering request validation.</summary>
		/// <returns>The headers from the HTTP request.</returns>
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x000212FF File Offset: 0x0001F4FF
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x00021307 File Offset: 0x0001F507
		public NameValueCollection Headers { get; internal set; }

		/// <summary>Gets the virtual path of the requested resource without triggering ASP.NET request validation.</summary>
		/// <returns>The virtual path.</returns>
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x00021310 File Offset: 0x0001F510
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x00021318 File Offset: 0x0001F518
		public string Path { get; internal set; }

		/// <summary>Gets additional path information for a resource that has a URL extension, without triggering ASP.NET request validation.</summary>
		/// <returns>A string that contains additional path information for a resource.</returns>
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x00021321 File Offset: 0x0001F521
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x00021329 File Offset: 0x0001F529
		public string PathInfo { get; internal set; }

		/// <summary>Gets the collection of HTTP query string variables that the client submitted, without triggering ASP.NET request validation.</summary>
		/// <returns>The collection of query string variables sent by the client.</returns>
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00021332 File Offset: 0x0001F532
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x0002133A File Offset: 0x0001F53A
		public NameValueCollection QueryString { get; internal set; }

		/// <summary>Gets the part of the requested URL that follows the website name, without triggering ASP.NET request validation.</summary>
		/// <returns>The part of the URL that follows the website name. </returns>
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00021343 File Offset: 0x0001F543
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x0002134B File Offset: 0x0001F54B
		public string RawUrl { get; internal set; }

		/// <summary>Gets the URL data for the request without triggering ASP.NET request validation.</summary>
		/// <returns>An object that contains the URL data for the request. </returns>
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x00021354 File Offset: 0x0001F554
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x0002135C File Offset: 0x0001F55C
		public Uri Url { get; internal set; }

		/// <summary>Gets the specified object from the <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, <see cref="P:System.Web.HttpRequest.QueryString" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collection, without triggering ASP.NET request validation.</summary>
		/// <returns>The requested object, or null if the object is not found.</returns>
		/// <param name="field">The key of the object to retrieve.</param>
		// Token: 0x1700044A RID: 1098
		public string this[string field]
		{
			get
			{
				if (this.Form != null && this.Form[field] != null)
				{
					return this.Form[field];
				}
				if (this.Cookies != null && this.Cookies[field] != null)
				{
					return this.Cookies[field].Value;
				}
				if (this.QueryString != null && this.QueryString[field] != null)
				{
					return this.QueryString[field];
				}
				return null;
			}
		}
	}
}
