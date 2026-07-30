using System;
using System.Collections.Specialized;

namespace System.Web
{
	/// <summary>Provides a wrapper class for the <see cref="T:System.Web.UnvalidatedRequestValuesBase" /> class, and provides access to HTTP request values without triggering ASP.NET request validation.</summary>
	// Token: 0x0200005B RID: 91
	public class UnvalidatedRequestValuesWrapper : UnvalidatedRequestValuesBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UnvalidatedRequestValuesWrapper" /> class.</summary>
		/// <param name="requestValues">The object that is passed to the constructor to initialize the class.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestValues" /> parameter is null.</exception>
		// Token: 0x060003F4 RID: 1012 RVA: 0x000073B2 File Offset: 0x000055B2
		public UnvalidatedRequestValuesWrapper(UnvalidatedRequestValues requestValues)
		{
			if (requestValues == null)
			{
				throw new ArgumentNullException("requestValues");
			}
			this._requestValues = requestValues;
		}

		/// <summary>Gets the collection of form variables that the client submitted, without triggering ASP.NET request validation.</summary>
		/// <returns>The form variables from the HTTP request.</returns>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x000073CF File Offset: 0x000055CF
		public override NameValueCollection Form
		{
			get
			{
				return this._requestValues.Form;
			}
		}

		/// <summary>Gets the collection of HTTP query string variables that the client submitted, without triggering ASP.NET request validation.</summary>
		/// <returns>The collection of query string variables sent by the client.</returns>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000073DC File Offset: 0x000055DC
		public override NameValueCollection QueryString
		{
			get
			{
				return this._requestValues.QueryString;
			}
		}

		/// <summary>Gets the collection of HTTP headers that the client sent, without triggering ASP.NET request validation.</summary>
		/// <returns>The headers from the HTTP request.</returns>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x000073E9 File Offset: 0x000055E9
		public override NameValueCollection Headers
		{
			get
			{
				return this._requestValues.Headers;
			}
		}

		/// <summary>Gets the collection of cookies that the client sent, without triggering ASP.NET request validation.</summary>
		/// <returns>The cookies from the HTTP request.</returns>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000073F6 File Offset: 0x000055F6
		public override HttpCookieCollection Cookies
		{
			get
			{
				return this._requestValues.Cookies;
			}
		}

		/// <summary>Gets the collection of files that the client uploaded, without triggering ASP.NET request validation.</summary>
		/// <returns>The files from the HTTP request.</returns>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00007403 File Offset: 0x00005603
		public override HttpFileCollectionBase Files
		{
			get
			{
				return new HttpFileCollectionWrapper(this._requestValues.Files);
			}
		}

		/// <summary>Gets the part of the requested URL that follows the website name, without triggering ASP.NET request validation.</summary>
		/// <returns>The part of the URL that follows the website name.</returns>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00007415 File Offset: 0x00005615
		public override string RawUrl
		{
			get
			{
				return this._requestValues.RawUrl;
			}
		}

		/// <summary>Gets the virtual path of the requested resource without triggering ASP.NET request validation.</summary>
		/// <returns>The virtual path.</returns>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00007422 File Offset: 0x00005622
		public override string Path
		{
			get
			{
				return this._requestValues.Path;
			}
		}

		/// <summary>Gets additional path information for a resource that has a URL extension, without triggering ASP.NET request validation.</summary>
		/// <returns>A string that contains additional path information for a resource.</returns>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000742F File Offset: 0x0000562F
		public override string PathInfo
		{
			get
			{
				return this._requestValues.PathInfo;
			}
		}

		/// <summary>Gets the specified object from the <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, <see cref="P:System.Web.HttpRequest.QueryString" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collection, without triggering ASP.NET request validation.</summary>
		/// <returns>The requested object, or null if the object is not found.</returns>
		/// <param name="field">The key of the object to retrieve.</param>
		// Token: 0x170001D0 RID: 464
		public override string this[string field]
		{
			get
			{
				return this._requestValues[field];
			}
		}

		/// <summary>Gets the URL data for the request without triggering ASP.NET request validation.</summary>
		/// <returns>An object that contains the URL data for the request.</returns>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000744A File Offset: 0x0000564A
		public override Uri Url
		{
			get
			{
				return this._requestValues.Url;
			}
		}

		// Token: 0x04000E2F RID: 3631
		private readonly UnvalidatedRequestValues _requestValues;
	}
}
