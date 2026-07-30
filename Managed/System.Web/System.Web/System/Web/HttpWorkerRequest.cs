using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Unity;

namespace System.Web
{
	/// <summary>This abstract class defines the base worker methods and enumerations used by ASP.NET managed code to process requests. </summary>
	// Token: 0x020000BD RID: 189
	[ComVisible(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HttpWorkerRequest
	{
		// Token: 0x06000A4B RID: 2635 RVA: 0x0001952C File Offset: 0x0001772C
		static HttpWorkerRequest()
		{
			for (int i = 0; i < 40; i++)
			{
				HttpWorkerRequest.RequestHeaderIndexer.Add(HttpWorkerRequest.GetKnownRequestHeaderName(i), i);
			}
			HttpWorkerRequest.ResponseHeaderIndexer = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			for (int j = 0; j < 30; j++)
			{
				HttpWorkerRequest.ResponseHeaderIndexer.Add(HttpWorkerRequest.GetKnownResponseHeaderName(j), j);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00019593 File Offset: 0x00017793
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x0001959B File Offset: 0x0001779B
		internal bool StartedInternally
		{
			get
			{
				return this.started_internally;
			}
			set
			{
				this.started_internally = value;
			}
		}

		/// <summary>Gets the full physical path to the Machine.config file.</summary>
		/// <returns>The physical path to the Machine.config file.</returns>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual string MachineConfigPath
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the physical path to the directory where the ASP.NET binaries are installed.</summary>
		/// <returns>The physical directory to the ASP.NET binary files.</returns>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual string MachineInstallDirectory
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the corresponding Event Tracking for Windows trace ID for the current request.</summary>
		/// <returns>A trace ID for the current ASP.NET request.</returns>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x000195A4 File Offset: 0x000177A4
		public virtual Guid RequestTraceIdentifier
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>Gets the full physical path to the root Web.config file.</summary>
		/// <returns>The physical path to the root Web.config file.</returns>
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual string RootWebConfigPath
		{
			get
			{
				return null;
			}
		}

		/// <summary>Terminates the connection with the client.</summary>
		// Token: 0x06000A52 RID: 2642 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void CloseConnection()
		{
		}

		/// <summary>Returns the virtual path to the currently executing server application.</summary>
		/// <returns>The virtual path of the current application.</returns>
		// Token: 0x06000A53 RID: 2643 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual string GetAppPath()
		{
			return null;
		}

		/// <summary>Returns the physical path to the currently executing server application.</summary>
		/// <returns>The physical path of the current application.</returns>
		// Token: 0x06000A54 RID: 2644 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual string GetAppPathTranslated()
		{
			return null;
		}

		/// <summary>When overridden in a derived class, returns the application pool ID for the current URL.</summary>
		/// <returns>Always returns null.</returns>
		// Token: 0x06000A55 RID: 2645 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual string GetAppPoolID()
		{
			return null;
		}

		/// <summary>Gets the number of bytes read in from the client.</summary>
		/// <returns>A Long containing the number of bytes read.</returns>
		// Token: 0x06000A56 RID: 2646 RVA: 0x000195AB File Offset: 0x000177AB
		public virtual long GetBytesRead()
		{
			return 0L;
		}

		/// <summary>When overridden in a derived class, returns the virtual path to the requested URI.</summary>
		/// <returns>The path to the requested URI.</returns>
		// Token: 0x06000A57 RID: 2647 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual string GetFilePath()
		{
			return null;
		}

		/// <summary>Returns the physical file path to the requested URI (and translates it from virtual path to physical path: for example, "/proj1/page.aspx" to "c:\dir\page.aspx") </summary>
		/// <returns>The translated physical file path to the requested URI.</returns>
		// Token: 0x06000A58 RID: 2648 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual string GetFilePathTranslated()
		{
			return null;
		}

		/// <summary>Returns the standard HTTP request header that corresponds to the specified index.</summary>
		/// <returns>The HTTP request header.</returns>
		/// <param name="index">The index of the header. For example, the <see cref="F:System.Web.HttpWorkerRequest.HeaderAllow" /> field. </param>
		// Token: 0x06000A59 RID: 2649 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual string GetKnownRequestHeader(int index)
		{
			return null;
		}

		/// <summary>Returns additional path information for a resource with a URL extension. That is, for the path /virdir/page.html/tail, the GetPathInfo value is /tail.</summary>
		/// <returns>Additional path information for a resource.</returns>
		// Token: 0x06000A5A RID: 2650 RVA: 0x000195AF File Offset: 0x000177AF
		public virtual string GetPathInfo()
		{
			return "";
		}

		/// <summary>Returns the portion of the HTTP request body that has already been read.</summary>
		/// <returns>The portion of the HTTP request body that has been read.</returns>
		// Token: 0x06000A5B RID: 2651 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual byte[] GetPreloadedEntityBody()
		{
			return null;
		}

		/// <summary>Gets the portion of the HTTP request body that has currently been read by using the specified buffer data and byte offset.</summary>
		/// <returns>The portion of the HTTP request body that has been read.</returns>
		/// <param name="buffer">The data to read.</param>
		/// <param name="offset">The byte offset at which to begin reading.</param>
		// Token: 0x06000A5C RID: 2652 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual int GetPreloadedEntityBody(byte[] buffer, int offset)
		{
			return 0;
		}

		/// <summary>Gets the length of the portion of the HTTP request body that has currently been read.</summary>
		/// <returns>An integer containing the length of the currently read HTTP request body.</returns>
		// Token: 0x06000A5D RID: 2653 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual int GetPreloadedEntityBodyLength()
		{
			return 0;
		}

		/// <summary>When overridden in a derived class, returns the HTTP protocol (HTTP or HTTPS).</summary>
		/// <returns>HTTPS if the <see cref="M:System.Web.HttpWorkerRequest.IsSecure" /> method is true, otherwise HTTP.</returns>
		// Token: 0x06000A5E RID: 2654 RVA: 0x000195B6 File Offset: 0x000177B6
		public virtual string GetProtocol()
		{
			if (this.IsSecure())
			{
				return "https";
			}
			return "http";
		}

		/// <summary>When overridden in a derived class, returns the response query string as an array of bytes.</summary>
		/// <returns>An array of bytes containing the response.</returns>
		// Token: 0x06000A5F RID: 2655 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual byte[] GetQueryStringRawBytes()
		{
			return null;
		}

		/// <summary>When overridden in a derived class, returns the name of the client computer.</summary>
		/// <returns>The name of the client computer.</returns>
		// Token: 0x06000A60 RID: 2656 RVA: 0x000195CB File Offset: 0x000177CB
		public virtual string GetRemoteName()
		{
			return this.GetRemoteAddress();
		}

		/// <summary>When overridden in a derived class, returns the reason for the request.</summary>
		/// <returns>Reason code. The default is ReasonResponseCacheMiss.</returns>
		// Token: 0x06000A61 RID: 2657 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual int GetRequestReason()
		{
			return 0;
		}

		/// <summary>When overridden in a derived class, returns the name of the local server.</summary>
		/// <returns>The name of the local server.</returns>
		// Token: 0x06000A62 RID: 2658 RVA: 0x000195D3 File Offset: 0x000177D3
		public virtual string GetServerName()
		{
			return this.GetLocalAddress();
		}

		/// <summary>Returns a single server variable from a dictionary of server variables associated with the request.</summary>
		/// <returns>The requested server variable.</returns>
		/// <param name="name">The name of the requested server variable. </param>
		// Token: 0x06000A63 RID: 2659 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual string GetServerVariable(string name)
		{
			return null;
		}

		/// <summary>Gets the length of the entire HTTP request body.</summary>
		/// <returns>An integer containing the length of the entire HTTP request body.</returns>
		// Token: 0x06000A64 RID: 2660 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual int GetTotalEntityBodyLength()
		{
			return 0;
		}

		/// <summary>Returns a nonstandard HTTP request header value.</summary>
		/// <returns>The header value.</returns>
		/// <param name="name">The header name. </param>
		// Token: 0x06000A65 RID: 2661 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual string GetUnknownRequestHeader(string name)
		{
			return null;
		}

		/// <summary>Get all nonstandard HTTP header name-value pairs.</summary>
		/// <returns>An array of header name-value pairs.</returns>
		// Token: 0x06000A66 RID: 2662 RVA: 0x00003BEA File Offset: 0x00001DEA
		[CLSCompliant(false)]
		public virtual string[][] GetUnknownRequestHeaders()
		{
			return null;
		}

		/// <summary>When overridden in a derived class, returns the client's impersonation token.</summary>
		/// <returns>A value representing the client's impersonation token. The default is 0.</returns>
		// Token: 0x06000A67 RID: 2663 RVA: 0x000195DB File Offset: 0x000177DB
		public virtual IntPtr GetUserToken()
		{
			return IntPtr.Zero;
		}

		/// <summary>Returns a value indicating whether the request contains body data.</summary>
		/// <returns>true if the request contains body data; otherwise, false.</returns>
		// Token: 0x06000A68 RID: 2664 RVA: 0x00008A69 File Offset: 0x00006C69
		public bool HasEntityBody()
		{
			return false;
		}

		/// <summary>Returns a value indicating whether HTTP response headers have been sent to the client for the current request.</summary>
		/// <returns>true if HTTP response headers have been sent to the client; otherwise, false.</returns>
		// Token: 0x06000A69 RID: 2665 RVA: 0x00008B66 File Offset: 0x00006D66
		public virtual bool HeadersSent()
		{
			return true;
		}

		/// <summary>Returns a value indicating whether the client connection is still active.</summary>
		/// <returns>true if the client connection is still active; otherwise, false.</returns>
		// Token: 0x06000A6A RID: 2666 RVA: 0x00008B66 File Offset: 0x00006D66
		public virtual bool IsClientConnected()
		{
			return true;
		}

		/// <summary>Returns a value indicating whether all request data is available and no further reads from the client are required.</summary>
		/// <returns>true if all request data is available; otherwise, false.</returns>
		// Token: 0x06000A6B RID: 2667 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool IsEntireEntityBodyIsPreloaded()
		{
			return false;
		}

		/// <summary>Returns a value indicating whether the connection uses SSL.</summary>
		/// <returns>true if the connection is an SSL connection; otherwise, false. The default is false.</returns>
		// Token: 0x06000A6C RID: 2668 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool IsSecure()
		{
			return false;
		}

		/// <summary>Returns the physical path corresponding to the specified virtual path.</summary>
		/// <returns>The physical path that corresponds to the virtual path specified in the <paramref name="virtualPath" /> parameter.</returns>
		/// <param name="virtualPath">The virtual path. </param>
		// Token: 0x06000A6D RID: 2669 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual string MapPath(string virtualPath)
		{
			return null;
		}

		/// <summary>Reads request data from the client (when not preloaded).</summary>
		/// <returns>The number of bytes read.</returns>
		/// <param name="buffer">The byte array to read data into. </param>
		/// <param name="size">The maximum number of bytes to read. </param>
		// Token: 0x06000A6E RID: 2670 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual int ReadEntityBody(byte[] buffer, int size)
		{
			return 0;
		}

		/// <summary>Reads request data from the client (when not preloaded) by using the specified buffer to read from, byte offset, and maximum bytes.</summary>
		/// <returns>The number of bytes read.</returns>
		/// <param name="buffer">The byte array to read data into.</param>
		/// <param name="offset">The byte offset at which to begin reading.</param>
		/// <param name="size">The maximum number of bytes to read.</param>
		// Token: 0x06000A6F RID: 2671 RVA: 0x000195E4 File Offset: 0x000177E4
		public virtual int ReadEntityBody(byte[] buffer, int offset, int size)
		{
			byte[] array = new byte[size];
			int num = this.ReadEntityBody(array, size);
			if (num > 0)
			{
				Array.Copy(array, 0, buffer, offset, num);
			}
			return num;
		}

		/// <summary>Adds a Content-Length HTTP header to the response for message bodies that are greater than 2 GB.</summary>
		/// <param name="contentLength">The length of the response, in bytes.</param>
		// Token: 0x06000A70 RID: 2672 RVA: 0x00019610 File Offset: 0x00017810
		public virtual void SendCalculatedContentLength(long contentLength)
		{
			this.SendCalculatedContentLength((int)contentLength);
		}

		/// <summary>Adds a Content-Length HTTP header to the response for message bodies that are less than or equal to 2 GB.</summary>
		/// <param name="contentLength">The length of the response, in bytes.</param>
		// Token: 0x06000A71 RID: 2673 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void SendCalculatedContentLength(int contentLength)
		{
		}

		/// <summary>Adds the specified number of bytes from a block of memory to the response.</summary>
		/// <param name="data">An unmanaged pointer to the block of memory. </param>
		/// <param name="length">The number of bytes to send. </param>
		// Token: 0x06000A72 RID: 2674 RVA: 0x0001961C File Offset: 0x0001781C
		public virtual void SendResponseFromMemory(IntPtr data, int length)
		{
			if (data != IntPtr.Zero)
			{
				byte[] array = new byte[length];
				Marshal.Copy(data, array, 0, length);
				this.SendResponseFromMemory(array, length);
			}
		}

		/// <summary>Registers for an optional notification when all the response data is sent.</summary>
		/// <param name="callback">The notification callback that is called when all data is sent (out-of-band). </param>
		/// <param name="extraData">An additional parameter to the callback. </param>
		// Token: 0x06000A73 RID: 2675 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void SetEndOfSendNotification(HttpWorkerRequest.EndOfSendNotification callback, object extraData)
		{
		}

		/// <summary>Used by the runtime to notify the <see cref="T:System.Web.HttpWorkerRequest" /> that request processing for the current request is complete.</summary>
		// Token: 0x06000A74 RID: 2676
		public abstract void EndOfRequest();

		/// <summary>Sends all pending response data to the client.</summary>
		/// <param name="finalFlush">true if this is the last time response data will be flushed; otherwise, false. </param>
		// Token: 0x06000A75 RID: 2677
		public abstract void FlushResponse(bool finalFlush);

		/// <summary>Returns the specified member of the request header.</summary>
		/// <returns>The HTTP verb returned in the request header.</returns>
		// Token: 0x06000A76 RID: 2678
		public abstract string GetHttpVerbName();

		/// <summary>Provides access to the HTTP version of the request (for example, "HTTP/1.1").</summary>
		/// <returns>The HTTP version returned in the request header.</returns>
		// Token: 0x06000A77 RID: 2679
		public abstract string GetHttpVersion();

		/// <summary>Provides access to the specified member of the request header.</summary>
		/// <returns>The server IP address returned in the request header.</returns>
		// Token: 0x06000A78 RID: 2680
		public abstract string GetLocalAddress();

		/// <summary>Provides access to the specified member of the request header.</summary>
		/// <returns>The server port number returned in the request header.</returns>
		// Token: 0x06000A79 RID: 2681
		public abstract int GetLocalPort();

		/// <summary>Returns the query string specified in the request URL.</summary>
		/// <returns>The request query string.</returns>
		// Token: 0x06000A7A RID: 2682
		public abstract string GetQueryString();

		/// <summary>Returns the URL path contained in the request header with the query string appended.</summary>
		/// <returns>The raw URL path of the request header.</returns>
		// Token: 0x06000A7B RID: 2683
		public abstract string GetRawUrl();

		/// <summary>Provides access to the specified member of the request header.</summary>
		/// <returns>The client's IP address.</returns>
		// Token: 0x06000A7C RID: 2684
		public abstract string GetRemoteAddress();

		/// <summary>Provides access to the specified member of the request header.</summary>
		/// <returns>The client's HTTP port number.</returns>
		// Token: 0x06000A7D RID: 2685
		public abstract int GetRemotePort();

		/// <summary>Returns the virtual path to the requested URI.</summary>
		/// <returns>The path to the requested URI.</returns>
		// Token: 0x06000A7E RID: 2686
		public abstract string GetUriPath();

		/// <summary>Adds a standard HTTP header to the response.</summary>
		/// <param name="index">The header index. For example, <see cref="F:System.Web.HttpWorkerRequest.HeaderContentLength" />. </param>
		/// <param name="value">The value of the header. </param>
		// Token: 0x06000A7F RID: 2687
		public abstract void SendKnownResponseHeader(int index, string value);

		/// <summary>Adds the contents of the specified file to the response and specifies the starting position in the file and the number of bytes to send.</summary>
		/// <param name="handle">The handle of the file to send. </param>
		/// <param name="offset">The starting position in the file. </param>
		/// <param name="length">The number of bytes to send. </param>
		// Token: 0x06000A80 RID: 2688
		public abstract void SendResponseFromFile(IntPtr handle, long offset, long length);

		/// <summary>Adds the contents of the specified file to the response and specifies the starting position in the file and the number of bytes to send.</summary>
		/// <param name="filename">The name of the file to send. </param>
		/// <param name="offset">The starting position in the file. </param>
		/// <param name="length">The number of bytes to send. </param>
		// Token: 0x06000A81 RID: 2689
		public abstract void SendResponseFromFile(string filename, long offset, long length);

		/// <summary>Adds the specified number of bytes from a byte array to the response.</summary>
		/// <param name="data">The byte array to send. </param>
		/// <param name="length">The number of bytes to send, starting at the first byte. </param>
		// Token: 0x06000A82 RID: 2690
		public abstract void SendResponseFromMemory(byte[] data, int length);

		/// <summary>Specifies the HTTP status code and status description of the response, such as SendStatus(200, "Ok").</summary>
		/// <param name="statusCode">The status code to send </param>
		/// <param name="statusDescription">The status description to send. </param>
		// Token: 0x06000A83 RID: 2691
		public abstract void SendStatus(int statusCode, string statusDescription);

		/// <summary>Adds a nonstandard HTTP header to the response.</summary>
		/// <param name="name">The name of the header to send. </param>
		/// <param name="value">The value of the header. </param>
		// Token: 0x06000A84 RID: 2692
		public abstract void SendUnknownResponseHeader(string name, string value);

		/// <summary>Returns the index number of the specified HTTP request header.</summary>
		/// <returns>The index number of the HTTP request header specified in the <paramref name="header" /> parameter.</returns>
		/// <param name="header">The name of the header. </param>
		// Token: 0x06000A85 RID: 2693 RVA: 0x00019650 File Offset: 0x00017850
		public static int GetKnownRequestHeaderIndex(string header)
		{
			int num;
			if (HttpWorkerRequest.RequestHeaderIndexer.TryGetValue(header, out num))
			{
				return num;
			}
			return -1;
		}

		/// <summary>Returns the name of the specified HTTP request header.</summary>
		/// <returns>The name of the HTTP request header specified in the <paramref name="index" /> parameter.</returns>
		/// <param name="index">The index number of the header. </param>
		// Token: 0x06000A86 RID: 2694 RVA: 0x00019670 File Offset: 0x00017870
		public static string GetKnownRequestHeaderName(int index)
		{
			switch (index)
			{
			case 0:
				return "Cache-Control";
			case 1:
				return "Connection";
			case 2:
				return "Date";
			case 3:
				return "Keep-Alive";
			case 4:
				return "Pragma";
			case 5:
				return "Trailer";
			case 6:
				return "Transfer-Encoding";
			case 7:
				return "Upgrade";
			case 8:
				return "Via";
			case 9:
				return "Warning";
			case 10:
				return "Allow";
			case 11:
				return "Content-Length";
			case 12:
				return "Content-Type";
			case 13:
				return "Content-Encoding";
			case 14:
				return "Content-Language";
			case 15:
				return "Content-Location";
			case 16:
				return "Content-MD5";
			case 17:
				return "Content-Range";
			case 18:
				return "Expires";
			case 19:
				return "Last-Modified";
			case 20:
				return "Accept";
			case 21:
				return "Accept-Charset";
			case 22:
				return "Accept-Encoding";
			case 23:
				return "Accept-Language";
			case 24:
				return "Authorization";
			case 25:
				return "Cookie";
			case 26:
				return "Expect";
			case 27:
				return "From";
			case 28:
				return "Host";
			case 29:
				return "If-Match";
			case 30:
				return "If-Modified-Since";
			case 31:
				return "If-None-Match";
			case 32:
				return "If-Range";
			case 33:
				return "If-Unmodified-Since";
			case 34:
				return "Max-Forwards";
			case 35:
				return "Proxy-Authorization";
			case 36:
				return "Referer";
			case 37:
				return "Range";
			case 38:
				return "TE";
			case 39:
				return "User-Agent";
			default:
				throw new IndexOutOfRangeException("index");
			}
		}

		/// <summary>Returns the index number of the specified HTTP response header.</summary>
		/// <returns>The index number of the HTTP response header specified in the <paramref name="header" /> parameter.</returns>
		/// <param name="header">The name of the HTTP header. </param>
		// Token: 0x06000A87 RID: 2695 RVA: 0x00019824 File Offset: 0x00017A24
		public static int GetKnownResponseHeaderIndex(string header)
		{
			int num;
			if (HttpWorkerRequest.ResponseHeaderIndexer.TryGetValue(header, out num))
			{
				return num;
			}
			return -1;
		}

		/// <summary>Returns the name of the specified HTTP response header.</summary>
		/// <returns>The name of the HTTP response header specified in the <paramref name="index" /> parameter.</returns>
		/// <param name="index">The index number of the header. </param>
		// Token: 0x06000A88 RID: 2696 RVA: 0x00019844 File Offset: 0x00017A44
		public static string GetKnownResponseHeaderName(int index)
		{
			switch (index)
			{
			case 0:
				return "Cache-Control";
			case 1:
				return "Connection";
			case 2:
				return "Date";
			case 3:
				return "Keep-Alive";
			case 4:
				return "Pragma";
			case 5:
				return "Trailer";
			case 6:
				return "Transfer-Encoding";
			case 7:
				return "Upgrade";
			case 8:
				return "Via";
			case 9:
				return "Warning";
			case 10:
				return "Allow";
			case 11:
				return "Content-Length";
			case 12:
				return "Content-Type";
			case 13:
				return "Content-Encoding";
			case 14:
				return "Content-Language";
			case 15:
				return "Content-Location";
			case 16:
				return "Content-MD5";
			case 17:
				return "Content-Range";
			case 18:
				return "Expires";
			case 19:
				return "Last-Modified";
			case 20:
				return "Accept-Ranges";
			case 21:
				return "Age";
			case 22:
				return "ETag";
			case 23:
				return "Location";
			case 24:
				return "Proxy-Authenticate";
			case 25:
				return "Retry-After";
			case 26:
				return "Server";
			case 27:
				return "Set-Cookie";
			case 28:
				return "Vary";
			case 29:
				return "WWW-Authenticate";
			default:
				throw new IndexOutOfRangeException("index");
			}
		}

		/// <summary>Returns a string that describes the name of the specified HTTP status code.</summary>
		/// <returns>The status description. For example, <see cref="M:System.Web.HttpWorkerRequest.GetStatusDescription(System.Int32)" /> (404) returns "Not Found".</returns>
		/// <param name="code">The HTTP status code. </param>
		// Token: 0x06000A89 RID: 2697 RVA: 0x00019994 File Offset: 0x00017B94
		public static string GetStatusDescription(int code)
		{
			if (code <= 207)
			{
				switch (code)
				{
				case 100:
					return "Continue";
				case 101:
					return "Switching Protocols";
				case 102:
					return "Processing";
				default:
					switch (code)
					{
					case 200:
						return "OK";
					case 201:
						return "Created";
					case 202:
						return "Accepted";
					case 203:
						return "Non-Authoritative Information";
					case 204:
						return "No Content";
					case 205:
						return "Reset Content";
					case 206:
						return "Partial Content";
					case 207:
						return "Multi-Status";
					}
					break;
				}
			}
			else
			{
				switch (code)
				{
				case 300:
					return "Multiple Choices";
				case 301:
					return "Moved Permanently";
				case 302:
					return "Found";
				case 303:
					return "See Other";
				case 304:
					return "Not Modified";
				case 305:
					return "Use Proxy";
				case 306:
					break;
				case 307:
					return "Temporary Redirect";
				default:
					switch (code)
					{
					case 400:
						return "Bad Request";
					case 401:
						return "Unauthorized";
					case 402:
						return "Payment Required";
					case 403:
						return "Forbidden";
					case 404:
						return "Not Found";
					case 405:
						return "Method Not Allowed";
					case 406:
						return "Not Acceptable";
					case 407:
						return "Proxy Authentication Required";
					case 408:
						return "Request Timeout";
					case 409:
						return "Conflict";
					case 410:
						return "Gone";
					case 411:
						return "Length Required";
					case 412:
						return "Precondition Failed";
					case 413:
						return "Request Entity Too Large";
					case 414:
						return "Request-Uri Too Long";
					case 415:
						return "Unsupported Media Type";
					case 416:
						return "Requested Range Not Satisfiable";
					case 417:
						return "Expectation Failed";
					case 418:
					case 419:
					case 420:
					case 421:
						break;
					case 422:
						return "Unprocessable Entity";
					case 423:
						return "Locked";
					case 424:
						return "Failed Dependency";
					default:
						switch (code)
						{
						case 500:
							return "Internal Server Error";
						case 501:
							return "Not Implemented";
						case 502:
							return "Bad Gateway";
						case 503:
							return "Service Unavailable";
						case 504:
							return "Gateway Timeout";
						case 505:
							return "Http Version Not Supported";
						case 507:
							return "Insufficient Storage";
						}
						break;
					}
					break;
				}
			}
			return "";
		}

		/// <summary>When overridden in a derived class, gets the certification fields (specified in the X.509 standard) from a request issued by the client.</summary>
		/// <returns>A byte array containing the stream of the entire certificate content.</returns>
		// Token: 0x06000A8A RID: 2698 RVA: 0x00019BD5 File Offset: 0x00017DD5
		public virtual byte[] GetClientCertificate()
		{
			return new byte[0];
		}

		/// <summary>Gets the certificate issuer, in binary format.</summary>
		/// <returns>A byte array containing the certificate issuer expressed in binary format.</returns>
		// Token: 0x06000A8B RID: 2699 RVA: 0x00019BD5 File Offset: 0x00017DD5
		public virtual byte[] GetClientCertificateBinaryIssuer()
		{
			return new byte[0];
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Text.Encoding" /> object in which the client certificate was encoded. </summary>
		/// <returns>The certificate encoding, expressed as an integer.</returns>
		// Token: 0x06000A8C RID: 2700 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual int GetClientCertificateEncoding()
		{
			return 0;
		}

		/// <summary>When overridden in a derived class, gets a PublicKey object associated with the client certificate.</summary>
		/// <returns>A PublicKey object.</returns>
		// Token: 0x06000A8D RID: 2701 RVA: 0x00019BD5 File Offset: 0x00017DD5
		public virtual byte[] GetClientCertificatePublicKey()
		{
			return new byte[0];
		}

		/// <summary>When overridden in a derived class, gets the date when the certificate becomes valid. The date varies with international settings. </summary>
		/// <returns>A <see cref="T:System.DateTime" /> object representing when the certificate becomes valid.</returns>
		// Token: 0x06000A8E RID: 2702 RVA: 0x00019BDD File Offset: 0x00017DDD
		public virtual DateTime GetClientCertificateValidFrom()
		{
			return DateTime.Now;
		}

		/// <summary>Gets the certificate expiration date.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object representing the date that the certificate expires.</returns>
		// Token: 0x06000A8F RID: 2703 RVA: 0x00019BDD File Offset: 0x00017DDD
		public virtual DateTime GetClientCertificateValidUntil()
		{
			return DateTime.Now;
		}

		/// <summary>When overridden in a derived class, returns the ID of the current connection.</summary>
		/// <returns>Always returns 0.</returns>
		// Token: 0x06000A90 RID: 2704 RVA: 0x000195AB File Offset: 0x000177AB
		public virtual long GetConnectionID()
		{
			return 0L;
		}

		/// <summary>When overridden in a derived class, returns the context ID of the current connection.</summary>
		/// <returns>Always returns 0.</returns>
		// Token: 0x06000A91 RID: 2705 RVA: 0x000195AB File Offset: 0x000177AB
		public virtual long GetUrlContextID()
		{
			return 0L;
		}

		/// <summary>Gets the impersonation token for the request virtual path.</summary>
		/// <returns>An unmanaged memory pointer for the token for the request virtual path.</returns>
		// Token: 0x06000A92 RID: 2706 RVA: 0x000195DB File Offset: 0x000177DB
		public virtual IntPtr GetVirtualPathToken()
		{
			return IntPtr.Zero;
		}

		/// <summary>Gets a value that indicates whether asynchronous flush operations are supported.</summary>
		/// <returns>true if asynchronous flush operations are supported; otherwise, false.</returns>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00019BE4 File Offset: 0x00017DE4
		public virtual bool SupportsAsyncFlush
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether asynchronous read operations are supported.</summary>
		/// <returns>true if asynchronous read operations are supported; otherwise, false.</returns>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x00019C00 File Offset: 0x00017E00
		public virtual bool SupportsAsyncRead
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Sends the currently buffered response to the client.</summary>
		/// <returns>The worker request buffers the status, headers, and response body until an asynchronous flush operation is initiated. If the underlying <see cref="T:System.Web.HttpWorkerRequest" /> object supports asynchronous flush and this method is called from an asynchronous module event or asynchronous handler, the send operation is performed asynchronously. Otherwise, the implementation performs a synchronous flush operation.</returns>
		/// <param name="callback">The method to call when a corresponding asynchronous operation completes.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous flush operation from other requests.</param>
		// Token: 0x06000A96 RID: 2710 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Begins an asynchronous read operation of the request entity body.</summary>
		/// <param name="buffer">The buffer to read the data into.</param>
		/// <param name="offset">The byte offset in the buffer at which to begin writing data.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="callback">The method to call when a corresponding asynchronous operation completes.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous read from other requests.</param>
		// Token: 0x06000A97 RID: 2711 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Waits for the pending asynchronous flush operation to complete.</summary>
		/// <param name="asyncResult">A reference to the pending asynchronous request.</param>
		// Token: 0x06000A98 RID: 2712 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void EndFlush(IAsyncResult asyncResult)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Waits for the pending asynchronous read operation to complete.</summary>
		/// <returns>The number of bytes that have been read from the stream.</returns>
		/// <param name="asyncResult">A reference to the pending asynchronous request.</param>
		// Token: 0x06000A99 RID: 2713 RVA: 0x00019C1C File Offset: 0x00017E1C
		public virtual int EndRead(IAsyncResult asyncResult)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>The index that represents the HTTP Cache-Control HTTP header.</summary>
		// Token: 0x04001022 RID: 4130
		public const int HeaderCacheControl = 0;

		/// <summary>Specifies the index number for the Connection HTTP header.</summary>
		// Token: 0x04001023 RID: 4131
		public const int HeaderConnection = 1;

		/// <summary>Specifies the index number for the Date HTTP header.</summary>
		// Token: 0x04001024 RID: 4132
		public const int HeaderDate = 2;

		/// <summary>Specifies the index number for the Keep-Alive HTTP header.</summary>
		// Token: 0x04001025 RID: 4133
		public const int HeaderKeepAlive = 3;

		/// <summary>Specifies the index number for the Pragma HTTP header.</summary>
		// Token: 0x04001026 RID: 4134
		public const int HeaderPragma = 4;

		/// <summary>Specifies the index number for the Trailer HTTP header.</summary>
		// Token: 0x04001027 RID: 4135
		public const int HeaderTrailer = 5;

		/// <summary>Specifies the index number for the Transfer-Encoding HTTP header.</summary>
		// Token: 0x04001028 RID: 4136
		public const int HeaderTransferEncoding = 6;

		/// <summary>Specifies the index number for the Upgrade HTTP header.</summary>
		// Token: 0x04001029 RID: 4137
		public const int HeaderUpgrade = 7;

		/// <summary>Specifies the index number for the Via HTTP header.</summary>
		// Token: 0x0400102A RID: 4138
		public const int HeaderVia = 8;

		/// <summary>Specifies the index number for the Warning HTTP header.</summary>
		// Token: 0x0400102B RID: 4139
		public const int HeaderWarning = 9;

		/// <summary>Specifies the index number for the Allow HTTP header.</summary>
		// Token: 0x0400102C RID: 4140
		public const int HeaderAllow = 10;

		/// <summary>Specifies the index number for the Content-Length HTTP header.</summary>
		// Token: 0x0400102D RID: 4141
		public const int HeaderContentLength = 11;

		/// <summary>Specifies the index number for the Content-Type HTTP header.</summary>
		// Token: 0x0400102E RID: 4142
		public const int HeaderContentType = 12;

		/// <summary>Specifies the index number for the Content-Encoding HTTP header.</summary>
		// Token: 0x0400102F RID: 4143
		public const int HeaderContentEncoding = 13;

		/// <summary>Specifies the index number for the Content-Language HTTP header.</summary>
		// Token: 0x04001030 RID: 4144
		public const int HeaderContentLanguage = 14;

		/// <summary>Specifies the index number for the Content-Location HTTP header.</summary>
		// Token: 0x04001031 RID: 4145
		public const int HeaderContentLocation = 15;

		/// <summary>Specifies the index number for the Content-MD5 HTTP header.</summary>
		// Token: 0x04001032 RID: 4146
		public const int HeaderContentMd5 = 16;

		/// <summary>Specifies the index number for the Content-Range HTTP header.</summary>
		// Token: 0x04001033 RID: 4147
		public const int HeaderContentRange = 17;

		/// <summary>Specifies the index number for the Expires HTTP header.</summary>
		// Token: 0x04001034 RID: 4148
		public const int HeaderExpires = 18;

		/// <summary>Specifies the index number for the Last-Modified HTTP header.</summary>
		// Token: 0x04001035 RID: 4149
		public const int HeaderLastModified = 19;

		/// <summary>Specifies the index number for the Accept HTTP header.</summary>
		// Token: 0x04001036 RID: 4150
		public const int HeaderAccept = 20;

		/// <summary>Specifies the index number for the Accept-Charset HTTP header.</summary>
		// Token: 0x04001037 RID: 4151
		public const int HeaderAcceptCharset = 21;

		/// <summary>Specifies the index number for the Accept-Encoding HTTP header.</summary>
		// Token: 0x04001038 RID: 4152
		public const int HeaderAcceptEncoding = 22;

		/// <summary>Specifies the index number for the Accept-Language HTTP header.</summary>
		// Token: 0x04001039 RID: 4153
		public const int HeaderAcceptLanguage = 23;

		/// <summary>Specifies the index number for the Authorization HTTP header.</summary>
		// Token: 0x0400103A RID: 4154
		public const int HeaderAuthorization = 24;

		/// <summary>Specifies the index number for the Cookie HTTP header.</summary>
		// Token: 0x0400103B RID: 4155
		public const int HeaderCookie = 25;

		/// <summary>Specifies the index number for the Except HTTP header.</summary>
		// Token: 0x0400103C RID: 4156
		public const int HeaderExpect = 26;

		/// <summary>Specifies the index number for the From HTTP header.</summary>
		// Token: 0x0400103D RID: 4157
		public const int HeaderFrom = 27;

		/// <summary>Specifies the index number for the Host HTTP header.</summary>
		// Token: 0x0400103E RID: 4158
		public const int HeaderHost = 28;

		/// <summary>Specifies the index number for the If-Match HTTP header.</summary>
		// Token: 0x0400103F RID: 4159
		public const int HeaderIfMatch = 29;

		/// <summary>Specifies the index number for the If-Modified-Since HTTP header.</summary>
		// Token: 0x04001040 RID: 4160
		public const int HeaderIfModifiedSince = 30;

		/// <summary>Specifies the index number for the If-None-Match HTTP header.</summary>
		// Token: 0x04001041 RID: 4161
		public const int HeaderIfNoneMatch = 31;

		/// <summary>Specifies the index number for the If-Range HTTP header.</summary>
		// Token: 0x04001042 RID: 4162
		public const int HeaderIfRange = 32;

		/// <summary>Specifies the index number for the If-Unmodified-Since HTTP header.</summary>
		// Token: 0x04001043 RID: 4163
		public const int HeaderIfUnmodifiedSince = 33;

		/// <summary>Specifies the index number for the Max-Forwards HTTP header.</summary>
		// Token: 0x04001044 RID: 4164
		public const int HeaderMaxForwards = 34;

		/// <summary>Specifies the index number for the Proxy-Authorization HTTP header.</summary>
		// Token: 0x04001045 RID: 4165
		public const int HeaderProxyAuthorization = 35;

		/// <summary>Specifies the index number for the Referer HTTP header.</summary>
		// Token: 0x04001046 RID: 4166
		public const int HeaderReferer = 36;

		/// <summary>Specifies the index number for the Range HTTP header.</summary>
		// Token: 0x04001047 RID: 4167
		public const int HeaderRange = 37;

		/// <summary>Specifies the index number for the TE HTTP header.</summary>
		// Token: 0x04001048 RID: 4168
		public const int HeaderTe = 38;

		/// <summary>Specifies the index number for the User-Agent HTTP header.</summary>
		// Token: 0x04001049 RID: 4169
		public const int HeaderUserAgent = 39;

		/// <summary>Specifies the index number for the Maximum HTTP request header.</summary>
		// Token: 0x0400104A RID: 4170
		public const int RequestHeaderMaximum = 40;

		/// <summary>Specifies the index number for the Accept-Ranges HTTP header.</summary>
		// Token: 0x0400104B RID: 4171
		public const int HeaderAcceptRanges = 20;

		/// <summary>Specifies the index number for the Age HTTP header.</summary>
		// Token: 0x0400104C RID: 4172
		public const int HeaderAge = 21;

		/// <summary>Specifies the index number for the ETag HTTP header.</summary>
		// Token: 0x0400104D RID: 4173
		public const int HeaderEtag = 22;

		/// <summary>Specifies the index number for the Location HTTP header.</summary>
		// Token: 0x0400104E RID: 4174
		public const int HeaderLocation = 23;

		/// <summary>Specifies the index number for the Proxy-Authenticate HTTP header.</summary>
		// Token: 0x0400104F RID: 4175
		public const int HeaderProxyAuthenticate = 24;

		/// <summary>Specifies the index number for the Retry-After HTTP header.</summary>
		// Token: 0x04001050 RID: 4176
		public const int HeaderRetryAfter = 25;

		/// <summary>Specifies the index number for the Server HTTP header.</summary>
		// Token: 0x04001051 RID: 4177
		public const int HeaderServer = 26;

		/// <summary>Specifies the index number for the Set-Cookie HTTP header.</summary>
		// Token: 0x04001052 RID: 4178
		public const int HeaderSetCookie = 27;

		/// <summary>Specifies the index number for the Vary HTTP header.</summary>
		// Token: 0x04001053 RID: 4179
		public const int HeaderVary = 28;

		/// <summary>Specifies the index number for the WWW-Authenticate HTTP header.</summary>
		// Token: 0x04001054 RID: 4180
		public const int HeaderWwwAuthenticate = 29;

		/// <summary>Specifies the index number for the Maximum HTTP response header.</summary>
		// Token: 0x04001055 RID: 4181
		public const int ResponseHeaderMaximum = 30;

		/// <summary>Specifies a reason for the request.</summary>
		// Token: 0x04001056 RID: 4182
		public const int ReasonResponseCacheMiss = 0;

		/// <summary>Specifies a reason for the request.</summary>
		// Token: 0x04001057 RID: 4183
		public const int ReasonFileHandleCacheMiss = 1;

		/// <summary>Specifies a reason for the request.</summary>
		// Token: 0x04001058 RID: 4184
		public const int ReasonCachePolicy = 2;

		/// <summary>Specifies a reason for the request.</summary>
		// Token: 0x04001059 RID: 4185
		public const int ReasonCacheSecurity = 3;

		/// <summary>Specifies a reason for the request.</summary>
		// Token: 0x0400105A RID: 4186
		public const int ReasonClientDisconnect = 4;

		/// <summary>Specifies a reason for the request. The default value is <see cref="F:System.Web.HttpWorkerRequest.ReasonResponseCacheMiss" />.</summary>
		// Token: 0x0400105B RID: 4187
		public const int ReasonDefault = 0;

		// Token: 0x0400105C RID: 4188
		private static readonly Dictionary<string, int> RequestHeaderIndexer = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400105D RID: 4189
		private static readonly Dictionary<string, int> ResponseHeaderIndexer;

		// Token: 0x0400105E RID: 4190
		private bool started_internally;

		/// <summary>Represents the method that Notifies callers when sending of the response is complete.</summary>
		/// <param name="wr">The current <see cref="T:System.Web.HttpWorkerRequest" />. </param>
		/// <param name="extraData">Any additional data needed to process the request. </param>
		// Token: 0x020000BE RID: 190
		// (Invoke) Token: 0x06000A9B RID: 2715
		public delegate void EndOfSendNotification(HttpWorkerRequest wr, object extraData);
	}
}
