using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Util;
using Unity;

namespace System.Web
{
	/// <summary>Provides helper methods for processing Web requests.</summary>
	// Token: 0x020000B4 RID: 180
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpServerUtility
	{
		// Token: 0x060009C0 RID: 2496 RVA: 0x00017D64 File Offset: 0x00015F64
		internal HttpServerUtility(HttpContext context)
		{
			this.context = context;
		}

		/// <summary>Clears the previous exception.</summary>
		// Token: 0x060009C1 RID: 2497 RVA: 0x00017D73 File Offset: 0x00015F73
		public void ClearError()
		{
			this.context.ClearError();
		}

		/// <summary>Creates a server instance of a COM object identified by the object's programmatic identifier (ProgID).</summary>
		/// <returns>The new object.</returns>
		/// <param name="progID">The class or type of object to create an instance of.</param>
		/// <exception cref="T:System.Web.HttpException">An instance of the object could not be created.</exception>
		// Token: 0x060009C2 RID: 2498 RVA: 0x00017D80 File Offset: 0x00015F80
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public object CreateObject(string progID)
		{
			throw new HttpException(500, "COM is not supported");
		}

		/// <summary>Creates a server instance of a COM object identified by the object's type.</summary>
		/// <returns>The new object.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> representing the object to create.</param>
		// Token: 0x060009C3 RID: 2499 RVA: 0x00017D80 File Offset: 0x00015F80
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public object CreateObject(Type type)
		{
			throw new HttpException(500, "COM is not supported");
		}

		/// <summary>Creates a server instance of a COM object identified by the object's class identifier (CLSID).</summary>
		/// <returns>The new object.</returns>
		/// <param name="clsid">The class identifier of the object to create an instance of.</param>
		/// <exception cref="T:System.Web.HttpException">An instance of the object could not be created.</exception>
		// Token: 0x060009C4 RID: 2500 RVA: 0x00017D80 File Offset: 0x00015F80
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public object CreateObjectFromClsid(string clsid)
		{
			throw new HttpException(500, "COM is not supported");
		}

		/// <summary>Executes the handler for the specified virtual path in the context of the current request. </summary>
		/// <param name="path">The URL path to execute.</param>
		/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is null.- or -An error occurred while executing the handler specified by <paramref name="path" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. - or -<paramref name="path" /> is not a virtual path.</exception>
		// Token: 0x060009C5 RID: 2501 RVA: 0x00017D91 File Offset: 0x00015F91
		public void Execute(string path)
		{
			this.Execute(path, null, true);
		}

		/// <summary>Executes the handler for the specified virtual path in the context of the current request. A <see cref="T:System.IO.TextWriter" /> captures output from the executed handler.</summary>
		/// <param name="path">The URL path to execute. </param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to capture the output. </param>
		/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is null. - or -An error occurred while executing the handler specified by <paramref name="path" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. - or -<paramref name="path" /> is not a virtual path. </exception>
		// Token: 0x060009C6 RID: 2502 RVA: 0x00017D9C File Offset: 0x00015F9C
		public void Execute(string path, TextWriter writer)
		{
			this.Execute(path, writer, true);
		}

		/// <summary>Executes the handler for the specified virtual path in the context of the current request and specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
		/// <param name="path">The URL path to execute. </param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is null.- or -An error occurred while executing the handler specified by <paramref name="path" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. - or -<paramref name="path" /> is not a virtual path. </exception>
		// Token: 0x060009C7 RID: 2503 RVA: 0x00017DA7 File Offset: 0x00015FA7
		public void Execute(string path, bool preserveForm)
		{
			this.Execute(path, null, preserveForm);
		}

		/// <summary>Executes the handler for the specified virtual path in the context of the current request. A <see cref="T:System.IO.TextWriter" /> captures output from the page and a Boolean parameter specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
		/// <param name="path">The URL path to execute.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to capture the output.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is a null reference (Nothing in Visual Basic). - or -<paramref name="path" /> ends with a period (.).- or -An error occurred while executing the handler specified by <paramref name="path" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is not a virtual path.</exception>
		// Token: 0x060009C8 RID: 2504 RVA: 0x00017DB2 File Offset: 0x00015FB2
		public void Execute(string path, TextWriter writer, bool preserveForm)
		{
			this.Execute(path, writer, preserveForm, false);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00017DC0 File Offset: 0x00015FC0
		private void Execute(string path, TextWriter writer, bool preserveForm, bool isTransfer)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.IndexOf(':') != -1)
			{
				throw new ArgumentException("Invalid path.");
			}
			string text = null;
			int num = path.IndexOf('?');
			if (num != -1)
			{
				text = path.Substring(num + 1);
				path = path.Substring(0, num);
			}
			string text2 = UrlUtils.Combine(this.context.Request.BaseVirtualDir, path);
			SessionStateSection sessionStateSection = WebConfigurationManager.GetWebApplicationSection("system.web/sessionState") as SessionStateSection;
			if (SessionStateModule.IsCookieLess(this.context, sessionStateSection))
			{
				text2 = UrlUtils.RemoveSessionId(VirtualPathUtility.GetDirectory(text2), text2);
			}
			IHttpHandler handler = this.context.ApplicationInstance.GetHandler(this.context, text2, true);
			this.Execute(handler, writer, preserveForm, text2, text, isTransfer, true);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00017E80 File Offset: 0x00016080
		internal void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm, string exePath, string queryString, bool isTransfer, bool isInclude)
		{
			bool flag = handler is StaticFileHandler;
			if (isTransfer && !(handler is Page) && !flag)
			{
				throw new HttpException("Transfer is only allowed to .aspx and static files");
			}
			HttpRequest request = this.context.Request;
			string text = request.QueryStringRaw;
			if (queryString != null)
			{
				request.QueryStringRaw = queryString;
			}
			else if (!preserveForm)
			{
				request.QueryStringRaw = string.Empty;
			}
			HttpResponse response = this.context.Response;
			WebROCollection webROCollection = request.Form as WebROCollection;
			if (!preserveForm)
			{
				request.SetForm(new WebROCollection());
			}
			TextWriter textWriter = writer;
			if (textWriter == null)
			{
				textWriter = response.Output;
			}
			TextWriter textWriter2 = response.SetTextWriter(textWriter);
			string currentExecutionFilePath = request.CurrentExecutionFilePath;
			bool isProcessingInclude = this.context.IsProcessingInclude;
			try
			{
				this.context.PushHandler(handler);
				if (flag)
				{
					request.SetFilePath(exePath);
				}
				request.SetCurrentExePath(exePath);
				this.context.IsProcessingInclude = isInclude;
				if (!(handler is IHttpAsyncHandler))
				{
					handler.ProcessRequest(this.context);
				}
				else
				{
					IHttpAsyncHandler httpAsyncHandler = (IHttpAsyncHandler)handler;
					IAsyncResult asyncResult = httpAsyncHandler.BeginProcessRequest(this.context, null, null);
					WaitHandle waitHandle = ((asyncResult != null) ? asyncResult.AsyncWaitHandle : null);
					if (waitHandle != null)
					{
						waitHandle.WaitOne();
					}
					httpAsyncHandler.EndProcessRequest(asyncResult);
				}
			}
			finally
			{
				if (text != request.QueryStringRaw)
				{
					if (text != null && text.Length > 0)
					{
						text = text.Substring(1);
						request.QueryStringRaw = text;
					}
					else
					{
						request.QueryStringRaw = string.Empty;
					}
				}
				response.SetTextWriter(textWriter2);
				if (!preserveForm)
				{
					request.SetForm(webROCollection);
				}
				this.context.PopHandler();
				request.SetCurrentExePath(currentExecutionFilePath);
				this.context.IsProcessingInclude = isProcessingInclude;
			}
		}

		/// <summary>Returns the previous exception.</summary>
		/// <returns>The previous exception that was thrown.</returns>
		// Token: 0x060009CB RID: 2507 RVA: 0x00018030 File Offset: 0x00016230
		public Exception GetLastError()
		{
			if (this.context == null)
			{
				return HttpContext.Current.Error;
			}
			return this.context.Error;
		}

		/// <summary>Decodes an HTML-encoded string and returns the decoded string.</summary>
		/// <returns>The decoded text.</returns>
		/// <param name="s">The HTML string to decode.</param>
		// Token: 0x060009CC RID: 2508 RVA: 0x00018050 File Offset: 0x00016250
		public string HtmlDecode(string s)
		{
			return HttpUtility.HtmlDecode(s);
		}

		/// <summary>Decodes an HTML-encoded string and sends the resulting output to a <see cref="T:System.IO.TextWriter" /> output stream.</summary>
		/// <param name="s">The HTML string to decode.</param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> output stream that contains the decoded string.</param>
		// Token: 0x060009CD RID: 2509 RVA: 0x00018058 File Offset: 0x00016258
		public void HtmlDecode(string s, TextWriter output)
		{
			HttpUtility.HtmlDecode(s, output);
		}

		/// <summary>HTML-encodes a string and returns the encoded string.</summary>
		/// <returns>The HTML-encoded text.</returns>
		/// <param name="s">The text string to encode.</param>
		// Token: 0x060009CE RID: 2510 RVA: 0x00018061 File Offset: 0x00016261
		public string HtmlEncode(string s)
		{
			return HttpUtility.HtmlEncode(s);
		}

		/// <summary>HTML-encodes a string and sends the resulting output to a <see cref="T:System.IO.TextWriter" /> output stream.</summary>
		/// <param name="s">The string to encode. </param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> output stream that contains the encoded string.</param>
		// Token: 0x060009CF RID: 2511 RVA: 0x00018069 File Offset: 0x00016269
		public void HtmlEncode(string s, TextWriter output)
		{
			HttpUtility.HtmlEncode(s, output);
		}

		/// <summary>Returns the physical file path that corresponds to the specified virtual path on the Web server.</summary>
		/// <returns>The physical file path that corresponds to <paramref name="path" />.</returns>
		/// <param name="path">The virtual path of the Web server.</param>
		/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is null.</exception>
		// Token: 0x060009D0 RID: 2512 RVA: 0x00018072 File Offset: 0x00016272
		public string MapPath(string path)
		{
			return this.context.Request.MapPath(path);
		}

		/// <summary>Performs an asynchronous execution of the specified URL.</summary>
		/// <param name="path">The URL path of the new page on the server to execute.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The request requires the integrated pipeline mode of IIS 7.0.</exception>
		/// <exception cref="T:System.Web.HttpException">The server is not available to handle the request.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is invalid.</exception>
		// Token: 0x060009D1 RID: 2513 RVA: 0x00018085 File Offset: 0x00016285
		public void TransferRequest(string path)
		{
			this.TransferRequest(path, false, null, null);
		}

		/// <summary>Performs an asynchronous execution of the specified URL and preserves query string parameters.</summary>
		/// <param name="path">The URL path of the new page on the server to execute.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.Form" /> collection; false to clear the <see cref="P:System.Web.HttpRequest.Form" /> collection.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The request requires the integrated pipeline mode of IIS 7.0.</exception>
		/// <exception cref="T:System.Web.HttpException">The server is not available to handle the request.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is invalid.</exception>
		// Token: 0x060009D2 RID: 2514 RVA: 0x00018091 File Offset: 0x00016291
		public void TransferRequest(string path, bool preserveForm)
		{
			this.TransferRequest(path, preserveForm, null, null);
		}

		/// <summary>Performs an asynchronous execution of the specified URL using the specified HTTP method and headers.</summary>
		/// <param name="path">The URL path of the new page on the server to execute.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.Form" /> collection; false to clear the <see cref="P:System.Web.HttpRequest.Form" /> collection.</param>
		/// <param name="method">The HTTP method to use in the execution of the new request.</param>
		/// <param name="headers">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of request headers for the new request.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The request requires IIS 7.0 running in integrated mode.</exception>
		/// <exception cref="T:System.Web.HttpException">The server is not available to handle the request.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is invalid.</exception>
		// Token: 0x060009D3 RID: 2515 RVA: 0x0001809D File Offset: 0x0001629D
		[global::System.MonoTODO("Always throws PlatformNotSupportedException.")]
		public void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>For the current request, terminates execution of the current page and starts execution of a new page by using the specified URL path of the page.</summary>
		/// <param name="path">The URL path of the new page on the server to execute.</param>
		// Token: 0x060009D4 RID: 2516 RVA: 0x000180A4 File Offset: 0x000162A4
		public void Transfer(string path)
		{
			this.Transfer(path, true);
		}

		/// <summary>Terminates execution of the current page and starts execution of a new page by using the specified URL path of the page. Specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
		/// <param name="path">The URL path of the new page on the server to execute.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.ApplicationException">The current page request is a callback.</exception>
		// Token: 0x060009D5 RID: 2517 RVA: 0x000180AE File Offset: 0x000162AE
		public void Transfer(string path, bool preserveForm)
		{
			this.Execute(path, null, preserveForm, true);
			this.context.Response.End();
		}

		/// <summary>Terminates execution of the current page and starts execution of a new request by using a custom HTTP handler that implements the <see cref="T:System.Web.IHttpHandler" /> interface and specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
		/// <param name="handler">The HTTP handler that implements the <see cref="T:System.Web.IHttpHandler" /> to transfer the current request to.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.ApplicationException">The current page request is a callback.</exception>
		// Token: 0x060009D6 RID: 2518 RVA: 0x000180CA File Offset: 0x000162CA
		public void Transfer(IHttpHandler handler, bool preserveForm)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.Execute(handler, null, preserveForm, this.context.Request.CurrentExecutionFilePath, null, true, true);
			this.context.Response.End();
		}

		/// <summary>Executes the handler for the specified virtual path in the context of the current request. A <see cref="T:System.IO.TextWriter" /> captures output from the executed handler and a Boolean parameter specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
		/// <param name="handler">The HTTP handler that implements the <see cref="T:System.Web.IHttpHandler" /> to transfer the current request to.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to capture the output.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.Web.HttpException">An error occurred while executing the handler specified by <paramref name="handler" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="handler" /> parameter is null.</exception>
		// Token: 0x060009D7 RID: 2519 RVA: 0x00018106 File Offset: 0x00016306
		public void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.Execute(handler, writer, preserveForm, this.context.Request.CurrentExecutionFilePath, null, false, true);
		}

		/// <summary>Decodes a URL string token to its equivalent byte array using base 64 digits.</summary>
		/// <returns>The byte array containing the decoded URL string token.</returns>
		/// <param name="input">The URL string token to decode.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="input" /> parameter is null.</exception>
		// Token: 0x060009D8 RID: 2520 RVA: 0x00018134 File Offset: 0x00016334
		public static byte[] UrlTokenDecode(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (input.Length < 1)
			{
				return new byte[0];
			}
			byte[] bytes = Encoding.ASCII.GetBytes(input);
			int num = input.Length - 1;
			int i = (int)(bytes[num] - 48);
			char[] array = new char[num + i];
			int j;
			for (j = 0; j < num; j++)
			{
				char c = (char)bytes[j];
				if (c != '-')
				{
					if (c != '_')
					{
						array[j] = (char)bytes[j];
					}
					else
					{
						array[j] = '/';
					}
				}
				else
				{
					array[j] = '+';
				}
			}
			while (i > 0)
			{
				array[j++] = '=';
				i--;
			}
			return Convert.FromBase64CharArray(array, 0, array.Length);
		}

		/// <summary>Encodes a byte array into its equivalent string representation using base 64 digits, which is usable for transmission on the URL.</summary>
		/// <returns>The string containing the encoded token if the byte array length is greater than one; otherwise, an empty string ("").</returns>
		/// <param name="input">The byte array to encode.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="input" /> parameter is null.</exception>
		// Token: 0x060009D9 RID: 2521 RVA: 0x000181E0 File Offset: 0x000163E0
		public static string UrlTokenEncode(byte[] input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (input.Length < 1)
			{
				return string.Empty;
			}
			string text = Convert.ToBase64String(input);
			int num;
			if (text == null || (num = text.Length) == 0)
			{
				return string.Empty;
			}
			int num2 = 48;
			while (num > 0 && text[num - 1] == '=')
			{
				num2++;
				num--;
			}
			char[] array = new char[num + 1];
			array[num] = (char)num2;
			for (int i = 0; i < num; i++)
			{
				char c = text[i];
				if (c != '+')
				{
					if (c != '/')
					{
						array[i] = text[i];
					}
					else
					{
						array[i] = '_';
					}
				}
				else
				{
					array[i] = '-';
				}
			}
			return new string(array);
		}

		/// <summary>URL-decodes a string and returns the decoded string.</summary>
		/// <returns>The decoded text.</returns>
		/// <param name="s">The text string to decode.</param>
		// Token: 0x060009DA RID: 2522 RVA: 0x00018298 File Offset: 0x00016498
		public string UrlDecode(string s)
		{
			HttpRequest request = this.context.Request;
			if (request != null)
			{
				return HttpUtility.UrlDecode(s, request.ContentEncoding);
			}
			return HttpUtility.UrlDecode(s);
		}

		/// <summary>Decodes an HTML string received in a URL and sends the resulting output to a <see cref="T:System.IO.TextWriter" /> output stream.</summary>
		/// <param name="s">The HTML string to decode.</param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> output stream that contains the decoded string.</param>
		// Token: 0x060009DB RID: 2523 RVA: 0x000182C7 File Offset: 0x000164C7
		public void UrlDecode(string s, TextWriter output)
		{
			if (s != null)
			{
				output.Write(this.UrlDecode(s));
			}
		}

		/// <summary>URL-encodes a string and returns the encoded string.</summary>
		/// <returns>The URL-encoded text.</returns>
		/// <param name="s">The text to URL-encode.</param>
		// Token: 0x060009DC RID: 2524 RVA: 0x000182DC File Offset: 0x000164DC
		public string UrlEncode(string s)
		{
			HttpResponse response = this.context.Response;
			if (response != null)
			{
				return HttpUtility.UrlEncode(s, response.ContentEncoding);
			}
			return HttpUtility.UrlEncode(s);
		}

		/// <summary>URL-encodes a string and sends the resulting output to a <see cref="T:System.IO.TextWriter" /> output stream.</summary>
		/// <param name="s">The text string to encode.</param>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> output stream that contains the encoded string.</param>
		// Token: 0x060009DD RID: 2525 RVA: 0x0001830B File Offset: 0x0001650B
		public void UrlEncode(string s, TextWriter output)
		{
			if (s != null)
			{
				output.Write(this.UrlEncode(s));
			}
		}

		/// <summary>URL-encodes the path section of a URL string and returns the encoded string.</summary>
		/// <returns>The URL encoded text.</returns>
		/// <param name="s">The text to URL-encode.</param>
		// Token: 0x060009DE RID: 2526 RVA: 0x00018320 File Offset: 0x00016520
		public string UrlPathEncode(string s)
		{
			if (s == null)
			{
				return null;
			}
			int num = s.IndexOf('?');
			string text;
			if (num != -1)
			{
				text = s.Substring(0, num);
				text = HttpUtility.UrlEncode(text) + s.Substring(num);
			}
			else
			{
				text = HttpUtility.UrlEncode(s);
			}
			return text;
		}

		/// <summary>Gets the server's computer name.</summary>
		/// <returns>The name of the local computer.</returns>
		/// <exception cref="T:System.Web.HttpException">The computer name cannot be found.</exception>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00018367 File Offset: 0x00016567
		public string MachineName
		{
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			[EnvironmentPermission(SecurityAction.Assert, Read = "COMPUTERNAME")]
			get
			{
				return Environment.MachineName;
			}
		}

		/// <summary>Gets and sets the request time-out value in seconds.</summary>
		/// <returns>The time-out value setting for requests.</returns>
		/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The time-out period is null or otherwise could not be set.</exception>
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00018370 File Offset: 0x00016570
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00018391 File Offset: 0x00016591
		public int ScriptTimeout
		{
			get
			{
				return (int)this.context.ConfigTimeout.TotalSeconds;
			}
			[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
			set
			{
				this.context.ConfigTimeout = TimeSpan.FromSeconds((double)value);
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal HttpServerUtility()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Performs an asynchronous execution of the specified URL using the specified HTTP method, headers, and path, and optionally preserves form values and the user identity.</summary>
		/// <param name="path">The path.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.Form" /> collection; false to clear the <see cref="P:System.Web.HttpRequest.Form" /> collection.</param>
		/// <param name="method">The HTTP method to use in the new request.</param>
		/// <param name="headers">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains request headers for the new request.</param>
		/// <param name="preserveUser">true to preserve the user identity; otherwise, false. The other method overloads of this method call this overload with this parameter set to true.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The request requires the integrated pipeline mode of IIS 7.0.</exception>
		/// <exception cref="T:System.Web.HttpException">The server is not available to handle the request.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is invalid.</exception>
		// Token: 0x060009E3 RID: 2531 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers, bool preserveUser)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001018 RID: 4120
		private HttpContext context;
	}
}
