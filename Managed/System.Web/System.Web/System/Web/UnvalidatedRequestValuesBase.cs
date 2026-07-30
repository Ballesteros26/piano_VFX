using System;
using System.Collections.Specialized;

namespace System.Web
{
	/// <summary>Serves as the base class for classes that provide access to HTTP request values without triggering ASP.NET request validation.</summary>
	// Token: 0x0200005A RID: 90
	public abstract class UnvalidatedRequestValuesBase
	{
		/// <summary>When overridden in a derived class, gets the collection of form variables that the client submitted, without triggering ASP.NET request validation.</summary>
		/// <returns>The form variables from the HTTP request.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual NameValueCollection Form
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the collection of HTTP query string variables that the client submitted, without triggering ASP.NET request validation.</summary>
		/// <returns>The collection of query string variables sent by the client.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual NameValueCollection QueryString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the collection of HTTP headers that the client sent, without triggering ASP.NET request validation.</summary>
		/// <returns>The headers from the HTTP request.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual NameValueCollection Headers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the collection of cookies that the client sent, without triggering ASP.NET request validation.</summary>
		/// <returns>The cookies from the HTTP request.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpCookieCollection Cookies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the collection of files that the client uploaded, without triggering ASP.NET request validation.</summary>
		/// <returns>The files from the HTTP request.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpFileCollectionBase Files
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the part of the requested URL that follows the website name, without triggering ASP.NET request validation.</summary>
		/// <returns>The part of the URL that follows the website name.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string RawUrl
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the virtual path of the requested resource without triggering ASP.NET request validation.</summary>
		/// <returns>The virtual path.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string Path
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets additional path information for a resource that has a URL extension, without triggering ASP.NET request validation.</summary>
		/// <returns>A string that contains additional path information for a resource.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string PathInfo
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the specified object from the <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, <see cref="P:System.Web.HttpRequest.QueryString" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collection, without triggering ASP.NET request validation.</summary>
		/// <returns>The object specified by the <paramref name="field" /> parameter.</returns>
		/// <param name="field">The key of the object to retrieve.</param>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x170001C6 RID: 454
		public virtual string this[string field]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the URL data for the request without triggering request validation.</summary>
		/// <returns>An object that contains the URL data for the request.</returns>
		/// <exception cref="T:System.NotImplementedException">The property is not implemented.</exception>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Uri Url
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
