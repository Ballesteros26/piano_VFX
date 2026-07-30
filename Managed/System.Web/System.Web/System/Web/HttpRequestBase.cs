using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Routing;

namespace System.Web
{
	/// <summary>Serves as the base class for classes that enable ASP.NET to read the HTTP values sent by a client during a Web request.</summary>
	// Token: 0x0200003B RID: 59
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpRequestBase
	{
		/// <summary>When overridden in a derived class, gets an array of client-supported MIME accept types.</summary>
		/// <returns>An array of client-supported MIME accept types.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string[] AcceptTypes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the virtual root path of the ASP.NET application on the server.</summary>
		/// <returns>The virtual path of the current application.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string ApplicationPath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the anonymous identifier for the user, if it is available.</summary>
		/// <returns>The current anonymous user identifier.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string AnonymousID
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the virtual path of the application root and makes it relative by using the tilde (~) notation for the application root (as in "~/page.aspx").</summary>
		/// <returns>The virtual path of the application root for the current request with the tilde operator (~) added.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string AppRelativeCurrentExecutionFilePath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets information about the requesting client's browser capabilities.</summary>
		/// <returns>An object that represents the capabilities of the client browser.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpBrowserCapabilitiesBase Browser
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> object of the current <see cref="T:System.Web.HttpWorkerRequest" /> instance.</summary>
		/// <returns>The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> object of the current <see cref="T:System.Web.HttpWorkerRequest" /> instance.</returns>
		/// <exception cref="T:System.NotImplementedException">The current <see cref="T:System.Web.HttpWorkerRequest" /> object is not a System.Web.Hosting.IIS7WorkerRequest object or a System.Web.Hosting.ISAPIWorkerRequestInProc object.</exception>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual ChannelBinding HttpChannelBinding
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the current request's client security certificate.</summary>
		/// <returns>The client's security certificate.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpClientCertificate ClientCertificate
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the character set of the data that is provided by the client.</summary>
		/// <returns>The client's character set.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Encoding ContentEncoding
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the length, in bytes, of content that was sent by the client.</summary>
		/// <returns>The length, in bytes, of content that was sent by the client.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int ContentLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the MIME content type of the request.</summary>
		/// <returns>The MIME content type of the request, such as "text/html".</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string ContentType
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the collection of cookies that were sent by the client.</summary>
		/// <returns>The client's cookies.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpCookieCollection Cookies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the virtual path of the current request.</summary>
		/// <returns>The virtual path of the page handler that is currently executing.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string CurrentExecutionFilePath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When implemented in a derived class, gets the extension of the file name that is specified in the <see cref="P:System.Web.HttpRequest.CurrentExecutionFilePath" /> property.</summary>
		/// <returns>The extension of the file name that is specified in the <see cref="P:System.Web.HttpRequest.CurrentExecutionFilePath" /> property.</returns>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string CurrentExecutionFilePathExtension
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the virtual path of the current request.</summary>
		/// <returns>The virtual path of the current request.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string FilePath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the collection of files that were uploaded by the client, in multipart MIME format.</summary>
		/// <returns>The files that were uploaded by the client. The items in the <see cref="T:System.Web.HttpFileCollection" /> object are of type <see cref="T:System.Web.HttpPostedFile" />.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual HttpFileCollectionBase Files
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the filter to use when the current input stream is being read.</summary>
		/// <returns>An object to use as the filter.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Stream Filter
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the collection of form variables that were sent by the client.</summary>
		/// <returns>The form variables.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual NameValueCollection Form
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the HTTP data-transfer method (such as GET, POST, or HEAD) that was used by the client.</summary>
		/// <returns>The HTTP data-transfer method that was used by the client.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string HttpMethod
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the contents of the incoming HTTP entity body.</summary>
		/// <returns>The contents of the incoming HTTP entity body.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Stream InputStream
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the request has been authenticated.</summary>
		/// <returns>true if the request has been authenticated; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsAuthenticated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the request is from the local computer.</summary>
		/// <returns>true if the request is from the local computer; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsLocal
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a value that indicates whether the HTTP connection uses secure sockets (HTTPS protocol).</summary>
		/// <returns>true if the connection is an SSL connection that uses HTTPS protocol; otherwise, false.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual bool IsSecureConnection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Security.Principal.WindowsIdentity" /> type for the current user.</summary>
		/// <returns>The identity for the current user.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual WindowsIdentity LogonUserIdentity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a combined collection of <see cref="P:System.Web.HttpRequest.QueryString" />, <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.ServerVariables" />, and <see cref="P:System.Web.HttpRequest.Cookies" /> items.</summary>
		/// <returns>The collection of combined values.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual NameValueCollection Params
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the virtual path of the current request.</summary>
		/// <returns>The virtual path of the current request.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string Path
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets additional path information for a resource that has a URL extension.</summary>
		/// <returns>The additional path information for the resource.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string PathInfo
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the physical file-system path of the current application's root directory.</summary>
		/// <returns>The file-system path of the current application's root directory.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string PhysicalApplicationPath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the physical file-system path of the requested resource.</summary>
		/// <returns>The file-system path of the requested resource.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string PhysicalPath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the complete URL of the current request.</summary>
		/// <returns>The complete URL of the current request.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string RawUrl
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When implemented in a derived class, gets a value that indicates whether the request entity body has been read, and if so, how it was read.</summary>
		/// <returns>The value that indicates how the request entity body was read, or that it has not yet been read.</returns>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual ReadEntityBodyMode ReadEntityBodyMode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.Routing.RequestContext" /> instance of the current request.</summary>
		/// <returns>The <see cref="T:System.Web.Routing.RequestContext" /> instance of the current request. For non-routed requests, the <see cref="T:System.Web.Routing.RequestContext" /> object that is returned is empty.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual RequestContext RequestContext
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the HTTP data-transfer method (GET or POST) that was used by the client.</summary>
		/// <returns>The HTTP data-transfer method type that was used by the client.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string RequestType
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a collection of Web server variables.</summary>
		/// <returns>The server variables.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual NameValueCollection ServerVariables
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When implemented in a derived class, gets a <see cref="T:System.Threading.CancellationToken" /> object that is tripped when a request times out.</summary>
		/// <returns>The cancellation token.</returns>
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual CancellationToken TimedOutToken
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual ITlsTokenBindingInfo TlsTokenBindingInfo
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the number of bytes in the current input stream.</summary>
		/// <returns>The number of bytes in the input stream.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int TotalBytes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When implemented in a derived class, provides access to HTTP request values without triggering request validation.</summary>
		/// <returns>Unvalidated request values.</returns>
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual UnvalidatedRequestValuesBase Unvalidated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets information about the URL of the current request.</summary>
		/// <returns>An object that contains information about the URL of the current request.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Uri Url
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets information about the URL of the client request that linked to the current URL.</summary>
		/// <returns>The URL of the page that linked to the current request.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Uri UrlReferrer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the complete user-agent string of the client.</summary>
		/// <returns>The complete user-agent string of the client.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string UserAgent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets a sorted array of client language preferences.</summary>
		/// <returns>A sorted array of client language preferences, or null if the array is empty.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string[] UserLanguages
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the IP host address of the client.</summary>
		/// <returns>The IP address of the client.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string UserHostAddress
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the DNS name of the client.</summary>
		/// <returns>The DNS name of the client.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string UserHostName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the collection of HTTP headers that were sent by the client.</summary>
		/// <returns>The request headers.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual NameValueCollection Headers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the collection of HTTP query-string variables.</summary>
		/// <returns>The query-string variables that were sent by the client in the URL of the current request. </returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual NameValueCollection QueryString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the specified object from the <see cref="P:System.Web.HttpRequestBase.Cookies" />, <see cref="P:System.Web.HttpRequestBase.Form" />, <see cref="P:System.Web.HttpRequestBase.QueryString" />, or <see cref="P:System.Web.HttpRequestBase.ServerVariables" /> collections.</summary>
		/// <returns>The <see cref="P:System.Web.HttpRequestBase.QueryString" />, <see cref="P:System.Web.HttpRequestBase.Form" />, <see cref="P:System.Web.HttpRequestBase.Cookies" />, or <see cref="P:System.Web.HttpRequestBase.ServerVariables" /> collection member that is specified by <paramref name="key" />. If the specified <paramref name="key" /> value is not found, null is returned.</returns>
		/// <param name="key">The name of the collection member to get.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700015B RID: 347
		public virtual string this[string key]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Forcibly terminates the underlying TCP connection, causing any outstanding I/O to fail.</summary>
		// Token: 0x060002C9 RID: 713 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Abort()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, performs a binary read of a specified number of bytes from the current input stream.</summary>
		/// <returns>An array that contains the binary data.</returns>
		/// <param name="count">The number of bytes to read. </param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060002CA RID: 714 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual byte[] BinaryRead(int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>When implemented in a derived class, gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body. Because this information is preserved, downstream code such as ASP.NET Web Forms pages (.aspx files) will run successfully. This is not the case with the <see cref="M:System.Web.HttpRequest.GetBufferlessInputStream" /> method.</returns>
		// Token: 0x060002CB RID: 715 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Stream GetBufferedInputStream()
		{
			throw new NotImplementedException();
		}

		/// <summary>When implemented in a derived class, gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body, optionally disabling the request length limit that is set in the <see cref="P:System.Web.Configuration.HttpRuntimeSection.MaxRequestLength" /> property.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
		// Token: 0x060002CC RID: 716 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Stream GetBufferlessInputStream()
		{
			throw new NotImplementedException();
		}

		/// <summary>When implemented in a derived class, gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body, optionally disabling the request length limit that is set in the <see cref="P:System.Web.Configuration.HttpRuntimeSection.MaxRequestLength" /> property.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
		/// <param name="disableMaxRequestLength">true to disable the request length limit; otherwise, false.</param>
		// Token: 0x060002CD RID: 717 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Stream GetBufferlessInputStream(bool disableMaxRequestLength)
		{
			throw new NotImplementedException();
		}

		/// <summary>When implemented in a derived class, provides a copy of the HTTP request entity body to IIS.</summary>
		// Token: 0x060002CE RID: 718 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void InsertEntityBody()
		{
			throw new NotImplementedException();
		}

		/// <summary>When implemented in a derived class, provides IIS with a copy of the HTTP request entity body and with information about the request entity object.</summary>
		/// <param name="buffer">An array that contains the request entity data.</param>
		/// <param name="offset">The zero-based position in <paramref name="buffer" /> at which to begin storing the request entity data.</param>
		/// <param name="count">The number of bytes to read into the <paramref name="buffer" /> array.</param>
		// Token: 0x060002CF RID: 719 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void InsertEntityBody(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, maps an incoming image-field form parameter to appropriate x-coordinate and y-coordinate values.</summary>
		/// <returns>A two-dimensional array of integers.</returns>
		/// <param name="imageFieldName">The name of the image map. </param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060002D0 RID: 720 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int[] MapImageCoordinates(string imageFieldName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps an incoming image field form parameter into appropriate x and y coordinate values.</summary>
		/// <returns>The x and y coordinate values.</returns>
		/// <param name="imageFieldName">The name of the image field.</param>
		// Token: 0x060002D1 RID: 721 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual double[] MapRawImageCoordinates(string imageFieldName)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, maps the specified virtual path to a physical path on the server.</summary>
		/// <returns>The physical path on the server that is specified by <paramref name="virtualPath" />.</returns>
		/// <param name="virtualPath">The virtual path (absolute or relative) to map to a physical path. </param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060002D2 RID: 722 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string MapPath(string virtualPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, maps the specified virtual path to a physical path on the server.</summary>
		/// <returns>The physical path on the server.</returns>
		/// <param name="virtualPath">The virtual path (absolute or relative) to map to a physical path. </param>
		/// <param name="baseVirtualDir">The virtual base directory path that is used for relative resolution. </param>
		/// <param name="allowCrossAppMapping">true to indicate that <paramref name="virtualPath" /> can belong to another application; otherwise, false. </param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060002D3 RID: 723 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, causes validation to occur for the collections that are accessed through the <see cref="P:System.Web.HttpRequestBase.Cookies" />, <see cref="P:System.Web.HttpRequestBase.Form" />, and <see cref="P:System.Web.HttpRequestBase.QueryString" /> properties.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060002D4 RID: 724 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void ValidateInput()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, saves an HTTP request to disk.</summary>
		/// <param name="filename">The physical drive path. </param>
		/// <param name="includeHeaders">A value that specifies whether to save HTTP headers to disk. </param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x060002D5 RID: 725 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void SaveAs(string filename, bool includeHeaders)
		{
			throw new NotImplementedException();
		}
	}
}
