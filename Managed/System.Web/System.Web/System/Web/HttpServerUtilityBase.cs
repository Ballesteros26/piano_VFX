using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web
{
	/// <summary>Serves as the base class for classes that provide helper methods for processing Web requests.</summary>
	// Token: 0x0200003D RID: 61
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpServerUtilityBase
	{
		/// <summary>When overridden in a derived class, gets the server's computer name.</summary>
		/// <returns>The name of the server computer.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string MachineName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the request time-out value in seconds.</summary>
		/// <returns>The time-out value for requests.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual int ScriptTimeout
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

		/// <summary>When overridden in a derived class, clears the most recent exception.</summary>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000344 RID: 836 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void ClearError()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, creates a server instance of a COM object that is identified by the object's programmatic identifier (ProgID).</summary>
		/// <returns>The new object.</returns>
		/// <param name="progID">The class or type of object to create an instance of.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000345 RID: 837 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual object CreateObject(string progID)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, creates a server instance of a COM object that is identified by the object's type.</summary>
		/// <returns>The new object.</returns>
		/// <param name="type">A type that represents the object to create.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000346 RID: 838 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual object CreateObject(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, creates a server instance of a COM object that is identified by the object's class identifier (CLSID).</summary>
		/// <returns>The new object.</returns>
		/// <param name="clsid">The class identifier of the object to create an instance of.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000347 RID: 839 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual object CreateObjectFromClsid(string clsid)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, executes the handler for the specified virtual path in the context of the current process.</summary>
		/// <param name="path">The URL of the handler to execute.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000348 RID: 840 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Execute(string path)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, executes the handler for the specified virtual path in the context of the current process, using a <see cref="T:System.IO.TextWriter" /> instance to capture output from the executed handler.</summary>
		/// <param name="path">The URL of the handler to execute. </param>
		/// <param name="writer">An object to capture the output. </param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000349 RID: 841 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Execute(string path, TextWriter writer)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, executes the handler for the specified virtual path in the context of the current process and specifies whether to clear the <see cref="P:System.Web.HttpRequestBase.QueryString" /> and <see cref="P:System.Web.HttpRequestBase.Form" /> collections.</summary>
		/// <param name="path">The URL of the handler to execute. </param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600034A RID: 842 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Execute(string path, bool preserveForm)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, executes the handler for the specified virtual path in the context of the current request, using a <see cref="T:System.IO.TextWriter" /> instance to capture output from the page and a value that indicates whether to clear the <see cref="P:System.Web.HttpRequestBase.QueryString" /> and <see cref="P:System.Web.HttpRequestBase.Form" /> collections.</summary>
		/// <param name="path">The URL of the handler to execute.</param>
		/// <param name="writer">The object to capture the output.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600034B RID: 843 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Execute(string path, TextWriter writer, bool preserveForm)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, executes the specified handler in the context of the current process, using a <see cref="T:System.IO.TextWriter" /> instance to capture output from the executed handler and a value that specifies whether to clear the <see cref="P:System.Web.HttpRequestBase.QueryString" /> and <see cref="P:System.Web.HttpRequestBase.Form" /> collections.</summary>
		/// <param name="handler">The HTTP handler that implements the interface to transfer the current request to.</param>
		/// <param name="writer">The object to capture the output.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600034C RID: 844 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns the most recent exception.</summary>
		/// <returns>The most recent exception that was thrown.</returns>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600034D RID: 845 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual Exception GetLastError()
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, decodes an HTML-encoded string and returns the decoded string.</summary>
		/// <returns>The decoded text.</returns>
		/// <param name="s">The HTML string to decode.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600034E RID: 846 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string HtmlDecode(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, decodes an HTML-encoded string and returns the results in a stream.</summary>
		/// <param name="s">The HTML string to decode.</param>
		/// <param name="output">The stream to contain the decoded string.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600034F RID: 847 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void HtmlDecode(string s, TextWriter output)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, HTML-encodes a string and returns the encoded string.</summary>
		/// <returns>The HTML-encoded text.</returns>
		/// <param name="s">The string to encode.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000350 RID: 848 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string HtmlEncode(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, HTML-encodes a string and sends the resulting output to an output stream.</summary>
		/// <param name="s">The string to encode. </param>
		/// <param name="output">The stream to contain the encoded string.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000351 RID: 849 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void HtmlEncode(string s, TextWriter output)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns the physical file path that corresponds to the specified virtual path on the Web server.</summary>
		/// <returns>The physical file path that corresponds to <paramref name="path" />.</returns>
		/// <param name="path">The virtual path to get the physical path for.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000352 RID: 850 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string MapPath(string path)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, terminates execution of the current page and starts execution of a different page or handler by using the specified URL and a value that specifies whether to clear the <see cref="P:System.Web.HttpRequestBase.QueryString" /> and <see cref="P:System.Web.HttpRequestBase.Form" /> collections.</summary>
		/// <param name="path">The URL of the page or handler to execute.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000353 RID: 851 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Transfer(string path, bool preserveForm)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, terminates execution of the current process and starts execution of a page or handler that is specified with a URL.</summary>
		/// <param name="path">The URL of the page or handler to execute.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000354 RID: 852 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Transfer(string path)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, terminates execution of the current process and starts execution of a new request, using a custom HTTP handler and a value that specifies whether to clear the <see cref="P:System.Web.HttpRequestBase.QueryString" /> and <see cref="P:System.Web.HttpRequestBase.Form" /> collections.</summary>
		/// <param name="handler">The HTTP handler to transfer the current request to.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000355 RID: 853 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Transfer(IHttpHandler handler, bool preserveForm)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, asynchronously executes the end point at the specified URL.</summary>
		/// <param name="path">The URL of the page or handler to execute.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000356 RID: 854 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void TransferRequest(string path)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, asynchronously executes the endpoint at the specified URL and specifies whether to clear the <see cref="P:System.Web.HttpRequestBase.QueryString" /> and <see cref="P:System.Web.HttpRequestBase.Form" /> collections.</summary>
		/// <param name="path">The URL of the page or handler to execute.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000357 RID: 855 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void TransferRequest(string path, bool preserveForm)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, asynchronously executes the endpoint at the specified URL by using the specified HTTP method and headers.</summary>
		/// <param name="path">The URL of the page or handler to execute.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <param name="method">The HTTP method (GET, POST, and so on) to use for the new request. If null, the HTTP method of the original request is used.</param>
		/// <param name="headers">A collection of request headers for the new request.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000358 RID: 856 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers)
		{
			throw new NotImplementedException();
		}

		/// <summary>When implemented in a derived class, asynchronously executes the end point at the specified URL, using the specified HTTP method, headers, path, and options to preserve form values and preserve the user identity.</summary>
		/// <param name="path">The path.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <param name="method">The HTTP method to use for the new request.</param>
		/// <param name="headers">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains request headers for the new request.</param>
		/// <param name="preserveUser">true to preserve the user identity; otherwise, false. Other overloads of this method call this method overload with the <paramref name="preserveUser" /> parameter set to true.</param>
		// Token: 0x06000359 RID: 857 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers, bool preserveUser)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, decodes a URL-encoded string and returns the decoded string.</summary>
		/// <returns>The decoded text.</returns>
		/// <param name="s">The string to decode.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600035A RID: 858 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string UrlDecode(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, decodes a URL-encoded string and sends the resulting output to a stream.</summary>
		/// <param name="s">The string to decode.</param>
		/// <param name="output">The stream to contain the decoded string.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600035B RID: 859 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void UrlDecode(string s, TextWriter output)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, URL-encodes a string and returns the encoded string.</summary>
		/// <returns>The URL-encoded text.</returns>
		/// <param name="s">The text to URL-encode.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600035C RID: 860 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string UrlEncode(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, URL-encodes a string and sends the resulting output to a stream.</summary>
		/// <param name="s">The string to encode.</param>
		/// <param name="output">The stream to contain the encoded string.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600035D RID: 861 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void UrlEncode(string s, TextWriter output)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, URL-encodes the path section of a URL string.</summary>
		/// <returns>The URL-encoded text.</returns>
		/// <param name="s">The string to URL-encode.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600035E RID: 862 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string UrlPathEncode(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, decodes a URL string token into an equivalent byte array by using base64-encoded digits.</summary>
		/// <returns>The byte array that contains the decoded URL token.</returns>
		/// <param name="input">The URL string token to decode.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x0600035F RID: 863 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual byte[] UrlTokenDecode(string input)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, encodes a byte array into an equivalent string representation by using base64 digits, which makes it usable for transmission on the URL.</summary>
		/// <returns>The string that contains the encoded array if the length of <paramref name="input" /> is greater than 1; otherwise, an empty string ("")</returns>
		/// <param name="input">The byte array to encode.</param>
		/// <exception cref="T:System.NotImplementedException">Always.</exception>
		// Token: 0x06000360 RID: 864 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string UrlTokenEncode(byte[] input)
		{
			throw new NotImplementedException();
		}
	}
}
