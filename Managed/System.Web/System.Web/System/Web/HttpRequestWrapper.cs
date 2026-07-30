using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Routing;

namespace System.Web
{
	/// <summary>Encapsulates the HTTP intrinsic object that enables ASP.NET to read the HTTP values that are sent by a client during a Web request. </summary>
	// Token: 0x020000A5 RID: 165
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HttpRequestWrapper : HttpRequestBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpRequestWrapper" /> class by using the specified request object.</summary>
		/// <param name="httpRequest">The object that this wrapper class provides access to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="httpRequest" /> is null.</exception>
		// Token: 0x0600082E RID: 2094 RVA: 0x00014500 File Offset: 0x00012700
		public HttpRequestWrapper(HttpRequest httpRequest)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			this.w = httpRequest;
		}

		/// <summary>Gets an array of client-supported MIME accept types.</summary>
		/// <returns>An array of client-supported MIME accept types.</returns>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0001451D File Offset: 0x0001271D
		public override string[] AcceptTypes
		{
			get
			{
				return this.w.AcceptTypes;
			}
		}

		/// <summary>Gets the anonymous identifier for the user, if it is available.</summary>
		/// <returns>The current anonymous user identifier.</returns>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0001452A File Offset: 0x0001272A
		public override string AnonymousID
		{
			get
			{
				return this.w.AnonymousID;
			}
		}

		/// <summary>Gets the virtual path of the root of the ASP.NET application on the server.</summary>
		/// <returns>The virtual root path of the current application.</returns>
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00014537 File Offset: 0x00012737
		public override string ApplicationPath
		{
			get
			{
				return this.w.ApplicationPath;
			}
		}

		/// <summary>Gets the virtual path of the application root and makes it relative by using the tilde (~) notation for the application root (as in "~/page.aspx").</summary>
		/// <returns>The virtual path of the application root for the current request with the tilde operator added.</returns>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x00014544 File Offset: 0x00012744
		public override string AppRelativeCurrentExecutionFilePath
		{
			get
			{
				return this.w.AppRelativeCurrentExecutionFilePath;
			}
		}

		/// <summary>Gets information about the requesting client's browser capabilities.</summary>
		/// <returns>An object that represents the capabilities of the client browser.</returns>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00014551 File Offset: 0x00012751
		public override HttpBrowserCapabilitiesBase Browser
		{
			get
			{
				return new HttpBrowserCapabilitiesWrapper(this.w.Browser);
			}
		}

		/// <summary>Gets the current request's client security certificate.</summary>
		/// <returns>The client's security certificate.</returns>
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00014563 File Offset: 0x00012763
		public override HttpClientCertificate ClientCertificate
		{
			get
			{
				return this.w.ClientCertificate;
			}
		}

		/// <summary>Gets or sets the character set of the data that was provided by the client.</summary>
		/// <returns>The client's character set.</returns>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00014570 File Offset: 0x00012770
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x0001457D File Offset: 0x0001277D
		public override Encoding ContentEncoding
		{
			get
			{
				return this.w.ContentEncoding;
			}
			set
			{
				this.w.ContentEncoding = value;
			}
		}

		/// <summary>Gets the length, in bytes, of content that was sent by the client.</summary>
		/// <returns>The length, in bytes, of content that was sent by the client.</returns>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0001458B File Offset: 0x0001278B
		public override int ContentLength
		{
			get
			{
				return this.w.ContentLength;
			}
		}

		/// <summary>Gets or sets the MIME content type of the request.</summary>
		/// <returns>The MIME content type of the request, such as "text/html".</returns>
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00014598 File Offset: 0x00012798
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x000145A5 File Offset: 0x000127A5
		public override string ContentType
		{
			get
			{
				return this.w.ContentType;
			}
			set
			{
				this.w.ContentType = value;
			}
		}

		/// <summary>Gets the collection of cookies that were sent by the client.</summary>
		/// <returns>The client's cookies.</returns>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x000145B3 File Offset: 0x000127B3
		public override HttpCookieCollection Cookies
		{
			get
			{
				return this.w.Cookies;
			}
		}

		/// <summary>Gets the virtual path of the current request.</summary>
		/// <returns>The virtual path of the page handler that is currently executing.</returns>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x000145C0 File Offset: 0x000127C0
		public override string CurrentExecutionFilePath
		{
			get
			{
				return this.w.CurrentExecutionFilePath;
			}
		}

		/// <summary>Gets the virtual path of the current request.</summary>
		/// <returns>The virtual path of the current request.</returns>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x000145CD File Offset: 0x000127CD
		public override string FilePath
		{
			get
			{
				return this.w.FilePath;
			}
		}

		/// <summary>Gets the collection of files that were uploaded by the client, in multipart MIME format.</summary>
		/// <returns>The files that were uploaded by the client. The items in the <see cref="T:System.Web.HttpFileCollection" /> object are of type <see cref="T:System.Web.HttpPostedFile" />.</returns>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x000145DA File Offset: 0x000127DA
		public override HttpFileCollectionBase Files
		{
			get
			{
				return new HttpFileCollectionWrapper(this.w.Files);
			}
		}

		/// <summary>Gets or sets the filter to use when the current input stream is being read.</summary>
		/// <returns>An object to use as the filter.</returns>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x000145EC File Offset: 0x000127EC
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x000145F9 File Offset: 0x000127F9
		public override Stream Filter
		{
			get
			{
				return this.w.Filter;
			}
			set
			{
				this.w.Filter = value;
			}
		}

		/// <summary>Gets the collection of form variables that were sent by the client.</summary>
		/// <returns>The form variables.</returns>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x00014607 File Offset: 0x00012807
		public override NameValueCollection Form
		{
			get
			{
				return this.w.Form;
			}
		}

		/// <summary>Gets the collection of HTTP headers that were sent by the client.</summary>
		/// <returns>The request headers.</returns>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00014614 File Offset: 0x00012814
		public override NameValueCollection Headers
		{
			get
			{
				return this.w.Headers;
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
		/// <exception cref="T:System.Web.HttpException">The request's entity body has already been loaded and parsed. Examples of properties that cause the entity body to be loaded and parsed include the following: The <see cref="P:System.Web.HttpRequest.Form" /> property.The <see cref="P:System.Web.HttpRequest.Files" /> property.The <see cref="P:System.Web.HttpRequest.InputStream" /> property.The <see cref="M:System.Web.HttpRequest.GetBufferlessInputStream" /> method.To avoid this exception, call the <see cref="P:System.Web.HttpRequest.ReadEntityBodyMode" /> method first. This exception is also thrown if the client disconnects while the entity body is being read.</exception>
		// Token: 0x06000842 RID: 2114 RVA: 0x00014621 File Offset: 0x00012821
		public override Stream GetBufferedInputStream()
		{
			return this.w.GetBufferedInputStream();
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
		/// <exception cref="T:System.Web.HttpException">The request's entity body has already been loaded and parsed. Examples of properties that cause the entity body to be loaded and parsed include the following:The <see cref="P:System.Web.HttpRequest.Form" /> property.The <see cref="P:System.Web.HttpRequest.InputStream" /> property.The <see cref="P:System.Web.HttpRequest.Files" /> property.The <see cref="M:System.Web.HttpRequest.GetBufferedInputStream" /> method.To avoid this exception, call the <see cref="P:System.Web.HttpRequest.ReadEntityBodyMode" /> method first. This exception is also thrown if the client disconnects while the entity body is being read.</exception>
		// Token: 0x06000843 RID: 2115 RVA: 0x0001462E File Offset: 0x0001282E
		public override Stream GetBufferlessInputStream()
		{
			return this.w.GetBufferlessInputStream();
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body, , optionally disabling the request length limit that is set in the <see cref="P:System.Web.Configuration.HttpRuntimeSection.MaxRequestLength" /> property.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
		/// <param name="disableMaxRequestLength">true to disable the request length limit; otherwise, false.</param>
		/// <exception cref="T:System.Web.HttpException">The request's entity body has already been loaded and parsed. Examples of properties that cause the entity body to be loaded and parsed include the following: The <see cref="P:System.Web.HttpRequest.Form" /> property.The <see cref="P:System.Web.HttpRequest.Files" /> property.The <see cref="P:System.Web.HttpRequest.InputStream" /> property.The <see cref="M:System.Web.HttpRequest.GetBufferedInputStream" /> method.To avoid this exception, call the <see cref="P:System.Web.HttpRequest.ReadEntityBodyMode" /> method first. This exception is also thrown if the client disconnects while the entity body is being read.</exception>
		// Token: 0x06000844 RID: 2116 RVA: 0x0001463B File Offset: 0x0001283B
		public override Stream GetBufferlessInputStream(bool disableMaxRequestLength)
		{
			return this.w.GetBufferlessInputStream(disableMaxRequestLength);
		}

		/// <summary>Gets the HTTP data-transfer method (such as GET, POST, or HEAD) that was used by the client.</summary>
		/// <returns>The HTTP data-transfer method that was used by the client.</returns>
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00014649 File Offset: 0x00012849
		public override string HttpMethod
		{
			get
			{
				return this.w.HttpMethod;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> object of the current <see cref="T:System.Web.HttpWorkerRequest" /> instance.</summary>
		/// <returns>The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> object of the current <see cref="T:System.Web.HttpWorkerRequest" /> instance.</returns>
		/// <exception cref="T:System.NotImplementedException">The current <see cref="T:System.Web.HttpWorkerRequest" /> object is not a System.Web.Hosting.IIS7WorkerRequest object or a System.Web.Hosting.ISAPIWorkerRequestInProc object.</exception>
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00014656 File Offset: 0x00012856
		public override ChannelBinding HttpChannelBinding
		{
			get
			{
				return this.w.HttpChannelBinding;
			}
		}

		/// <summary>Gets the contents of the incoming HTTP entity body.</summary>
		/// <returns>The contents of the incoming HTTP content body.</returns>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00014663 File Offset: 0x00012863
		public override Stream InputStream
		{
			get
			{
				return this.w.InputStream;
			}
		}

		/// <summary>Gets a value that indicates whether the request has been authenticated.</summary>
		/// <returns>true if the request has been authenticated; otherwise, false.</returns>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x00014670 File Offset: 0x00012870
		public override bool IsAuthenticated
		{
			get
			{
				return this.w.IsAuthenticated;
			}
		}

		/// <summary>Gets a value that indicates whether the request is from the local computer.</summary>
		/// <returns>true if the request is from the local computer; otherwise, false.</returns>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0001467D File Offset: 0x0001287D
		public override bool IsLocal
		{
			get
			{
				return this.w.IsLocal;
			}
		}

		/// <summary>Gets a value that indicates whether the HTTP connection uses secure sockets (HTTPS protocol).</summary>
		/// <returns>true if the connection is an SSL connection that uses HTTPS protocol; otherwise, false.</returns>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0001468A File Offset: 0x0001288A
		public override bool IsSecureConnection
		{
			get
			{
				return this.w.IsSecureConnection;
			}
		}

		/// <summary>Gets the specified object from the <see cref="P:System.Web.HttpRequest.Cookies" />, <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.QueryString" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collections.</summary>
		/// <returns>The <see cref="P:System.Web.HttpRequest.QueryString" />, <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collection member that is specified by <paramref name="key" />. If the specified <paramref name="key" /> value is not found, null is returned.</returns>
		/// <param name="key">The name of the collection member to get. </param>
		// Token: 0x1700033D RID: 829
		public override string this[string key]
		{
			get
			{
				return this.w[key];
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Principal.WindowsIdentity" /> type for the current user.</summary>
		/// <returns>The identity for the current user.</returns>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x000146A5 File Offset: 0x000128A5
		public override WindowsIdentity LogonUserIdentity
		{
			get
			{
				return this.w.LogonUserIdentity;
			}
		}

		/// <summary>Gets a combined collection of <see cref="P:System.Web.HttpRequest.QueryString" />, <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.ServerVariables" />, and <see cref="P:System.Web.HttpRequest.Cookies" /> items.</summary>
		/// <returns>The collection of combined values.</returns>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x000146B2 File Offset: 0x000128B2
		public override NameValueCollection Params
		{
			get
			{
				return this.w.Params;
			}
		}

		/// <summary>Gets the virtual path of the current request.</summary>
		/// <returns>The virtual path of the current request.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x000146BF File Offset: 0x000128BF
		public override string Path
		{
			get
			{
				return this.w.Path;
			}
		}

		/// <summary>Gets additional path information for a resource that has a URL extension.</summary>
		/// <returns>The additional path information for the resource.</returns>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x000146CC File Offset: 0x000128CC
		public override string PathInfo
		{
			get
			{
				return this.w.PathInfo;
			}
		}

		/// <summary>Gets the physical file-system path of the current application's root directory.</summary>
		/// <returns>The file-system path of the current application's root directory.</returns>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x000146D9 File Offset: 0x000128D9
		public override string PhysicalApplicationPath
		{
			get
			{
				return this.w.PhysicalApplicationPath;
			}
		}

		/// <summary>Gets the physical file-system path of the requested resource.</summary>
		/// <returns>The file-system path of the requested resource.</returns>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x000146E6 File Offset: 0x000128E6
		public override string PhysicalPath
		{
			get
			{
				return this.w.PhysicalPath;
			}
		}

		/// <summary>Gets the collection of HTTP query-string variables.</summary>
		/// <returns>The query-string variables that were sent by the client in the URL of the current request. </returns>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x000146F3 File Offset: 0x000128F3
		public override NameValueCollection QueryString
		{
			get
			{
				return this.w.QueryString;
			}
		}

		/// <summary>Gets the complete URL of the current request.</summary>
		/// <returns>The complete URL of the current request.</returns>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00014700 File Offset: 0x00012900
		public override string RawUrl
		{
			get
			{
				return this.w.RawUrl;
			}
		}

		/// <summary>Gets or sets the HTTP data-transfer method (GET or POST) that was used by the client.</summary>
		/// <returns>The HTTP data-transfer method type that was used by the client.</returns>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0001470D File Offset: 0x0001290D
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0001471A File Offset: 0x0001291A
		public override string RequestType
		{
			get
			{
				return this.w.RequestType;
			}
			set
			{
				this.w.RequestType = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Routing.RequestContext" /> instance of the current request.</summary>
		/// <returns>The <see cref="T:System.Web.Routing.RequestContext" /> instance of the current request. For non-routed requests, the <see cref="T:System.Web.Routing.RequestContext" /> object that is returned is empty.</returns>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00014728 File Offset: 0x00012928
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x00014735 File Offset: 0x00012935
		public override RequestContext RequestContext
		{
			get
			{
				return this.w.RequestContext;
			}
			set
			{
				this.w.RequestContext = value;
			}
		}

		/// <summary>Gets a collection of Web server variables.</summary>
		/// <returns>The server variables.</returns>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00014743 File Offset: 0x00012943
		public override NameValueCollection ServerVariables
		{
			get
			{
				return this.w.ServerVariables;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.CancellationToken" /> object that is tripped when a request times out.</summary>
		/// <returns>The cancellation token.</returns>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00014750 File Offset: 0x00012950
		public override CancellationToken TimedOutToken
		{
			get
			{
				return this.w.TimedOutToken;
			}
		}

		/// <summary>Gets the number of bytes in the current input stream.</summary>
		/// <returns>The number of bytes in the input stream.</returns>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0001475D File Offset: 0x0001295D
		public override int TotalBytes
		{
			get
			{
				return this.w.TotalBytes;
			}
		}

		/// <summary>Provides access to HTTP request values without triggering request validation.</summary>
		/// <returns>Unvalidated request values.</returns>
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0001476A File Offset: 0x0001296A
		public override UnvalidatedRequestValuesBase Unvalidated
		{
			get
			{
				return new UnvalidatedRequestValuesWrapper(this.w.Unvalidated);
			}
		}

		/// <summary>Gets a value that indicates whether the request entity body has been read, and if so, how it was read.</summary>
		/// <returns>The value that indicates how the request entity body was read, or that it has not been read.</returns>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00008B66 File Offset: 0x00006D66
		public override ReadEntityBodyMode ReadEntityBodyMode
		{
			get
			{
				return ReadEntityBodyMode.Classic;
			}
		}

		/// <summary>Gets information about the URL of the current request.</summary>
		/// <returns>An object that contains information about the URL of the current request.</returns>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0001477C File Offset: 0x0001297C
		public override Uri Url
		{
			get
			{
				return this.w.Url;
			}
		}

		/// <summary>Gets information about the URL of the client request that linked to the current URL.</summary>
		/// <returns>The URL of the page that linked to the current request.</returns>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00014789 File Offset: 0x00012989
		public override Uri UrlReferrer
		{
			get
			{
				return this.w.UrlReferrer;
			}
		}

		/// <summary>Gets the complete user-agent string of the client.</summary>
		/// <returns>The complete user-agent string of the client.</returns>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x00014796 File Offset: 0x00012996
		public override string UserAgent
		{
			get
			{
				return this.w.UserAgent;
			}
		}

		/// <summary>Gets the IP host address of the client.</summary>
		/// <returns>The IP address of the client.</returns>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x000147A3 File Offset: 0x000129A3
		public override string UserHostAddress
		{
			get
			{
				return this.w.UserHostAddress;
			}
		}

		/// <summary>Gets the DNS name of the client.</summary>
		/// <returns>The DNS name of the client.</returns>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x000147B0 File Offset: 0x000129B0
		public override string UserHostName
		{
			get
			{
				return this.w.UserHostName;
			}
		}

		/// <summary>Gets a sorted array of client language preferences.</summary>
		/// <returns>A sorted array of client language preferences, or null if the array is empty.</returns>
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x000147BD File Offset: 0x000129BD
		public override string[] UserLanguages
		{
			get
			{
				return this.w.UserLanguages;
			}
		}

		/// <summary>Forcibly terminates the underlying TCP connection, causing any outstanding I/O to fail.</summary>
		// Token: 0x06000863 RID: 2147 RVA: 0x000147CA File Offset: 0x000129CA
		public override void Abort()
		{
			this.w.WorkerRequest.CloseConnection();
		}

		/// <summary>Performs a binary read of a specified number of bytes from the current input stream.</summary>
		/// <returns>An array that contains the binary data.</returns>
		/// <param name="count">The number of bytes to read. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than 0.- or -<paramref name="count" /> is greater than the number of bytes available. </exception>
		// Token: 0x06000864 RID: 2148 RVA: 0x000147DC File Offset: 0x000129DC
		public override byte[] BinaryRead(int count)
		{
			return this.w.BinaryRead(count);
		}

		/// <summary>Maps an incoming image-field form parameter to appropriate x-coordinate and y-coordinate values.</summary>
		/// <returns>A two-dimensional array of integers.</returns>
		/// <param name="imageFieldName">The name of the image map. </param>
		// Token: 0x06000865 RID: 2149 RVA: 0x000147EA File Offset: 0x000129EA
		public override int[] MapImageCoordinates(string imageFieldName)
		{
			return this.w.MapImageCoordinates(imageFieldName);
		}

		/// <summary>Maps the specified virtual path to a physical path on the server.</summary>
		/// <returns>The physical path on the server that is specified by <paramref name="virtualPath" />.</returns>
		/// <param name="virtualPath">The virtual path (absolute or relative) to map to a physical path. </param>
		// Token: 0x06000866 RID: 2150 RVA: 0x000147F8 File Offset: 0x000129F8
		public override string MapPath(string virtualPath)
		{
			return this.w.MapPath(virtualPath);
		}

		/// <summary>Maps the specified virtual path to a physical path on the server.</summary>
		/// <returns>The physical path on the server.</returns>
		/// <param name="virtualPath">The virtual path (absolute or relative) to map to a physical path. </param>
		/// <param name="baseVirtualDir">The virtual base directory path that is used for relative resolution. </param>
		/// <param name="allowCrossAppMapping">true to indicate that <paramref name="virtualPath" /> can belong to another application; otherwise, false. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="allowCrossAppMapping" /> is false and <paramref name="virtualPath" /> belongs to another application. </exception>
		/// <exception cref="T:System.Web.HttpException">No <see cref="T:System.Web.HttpContext" /> object is defined for the request. </exception>
		// Token: 0x06000867 RID: 2151 RVA: 0x00014806 File Offset: 0x00012A06
		public override string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
		{
			return this.w.MapPath(virtualPath, baseVirtualDir, allowCrossAppMapping);
		}

		/// <summary>Maps an incoming image field form parameter into appropriate x and y coordinate values.</summary>
		/// <returns>The x and y coordinate values.</returns>
		/// <param name="imageFieldName">The name of the image field.</param>
		// Token: 0x06000868 RID: 2152 RVA: 0x00014816 File Offset: 0x00012A16
		public override double[] MapRawImageCoordinates(string imageFieldName)
		{
			return this.w.MapRawImageCoordinates(imageFieldName);
		}

		/// <summary>Saves an HTTP request to disk.</summary>
		/// <param name="filename">The physical drive path. </param>
		/// <param name="includeHeaders">A value that specifies whether to save HTTP headers to disk. </param>
		// Token: 0x06000869 RID: 2153 RVA: 0x00014824 File Offset: 0x00012A24
		public override void SaveAs(string filename, bool includeHeaders)
		{
			this.w.SaveAs(filename, includeHeaders);
		}

		/// <summary>Causes validation to occur for the collections that are accessed through the <see cref="P:System.Web.HttpRequest.Cookies" />, <see cref="P:System.Web.HttpRequest.Form" />, and <see cref="P:System.Web.HttpRequest.QueryString" /> properties.</summary>
		// Token: 0x0600086A RID: 2154 RVA: 0x00014833 File Offset: 0x00012A33
		public override void ValidateInput()
		{
			this.w.ValidateInput();
		}

		// Token: 0x04000FC6 RID: 4038
		private HttpRequest w;
	}
}
