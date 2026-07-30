using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.WebSockets
{
	/// <summary>Represents an exception that occurred when performing an operation on a WebSocket connection.</summary>
	// Token: 0x020006E2 RID: 1762
	[Serializable]
	public sealed class WebSocketException : Win32Exception
	{
		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		// Token: 0x060036F1 RID: 14065 RVA: 0x000CAE7A File Offset: 0x000C907A
		public WebSocketException()
			: this(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		// Token: 0x060036F2 RID: 14066 RVA: 0x000CAE87 File Offset: 0x000C9087
		public WebSocketException(WebSocketError error)
			: this(error, WebSocketException.GetErrorMessage(error))
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="message">The description of the error.</param>
		// Token: 0x060036F3 RID: 14067 RVA: 0x000CAE96 File Offset: 0x000C9096
		public WebSocketException(WebSocketError error, string message)
			: base(message)
		{
			this._webSocketErrorCode = error;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x060036F4 RID: 14068 RVA: 0x000CAEA6 File Offset: 0x000C90A6
		public WebSocketException(WebSocketError error, Exception innerException)
			: this(error, WebSocketException.GetErrorMessage(error), innerException)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="message">The description of the error.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x060036F5 RID: 14069 RVA: 0x000CAEB6 File Offset: 0x000C90B6
		public WebSocketException(WebSocketError error, string message, Exception innerException)
			: base(message, innerException)
		{
			this._webSocketErrorCode = error;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="nativeError">The native error code for the exception.</param>
		// Token: 0x060036F6 RID: 14070 RVA: 0x000CAEC7 File Offset: 0x000C90C7
		public WebSocketException(int nativeError)
			: base(nativeError)
		{
			this._webSocketErrorCode = ((!WebSocketException.Succeeded(nativeError)) ? WebSocketError.NativeError : WebSocketError.Success);
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="message">The description of the error.</param>
		// Token: 0x060036F7 RID: 14071 RVA: 0x000CAEE9 File Offset: 0x000C90E9
		public WebSocketException(int nativeError, string message)
			: base(nativeError, message)
		{
			this._webSocketErrorCode = ((!WebSocketException.Succeeded(nativeError)) ? WebSocketError.NativeError : WebSocketError.Success);
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x060036F8 RID: 14072 RVA: 0x000CAF0C File Offset: 0x000C910C
		public WebSocketException(int nativeError, Exception innerException)
			: base("An internal WebSocket error occurred. Please see the innerException, if present, for more details.", innerException)
		{
			this._webSocketErrorCode = ((!WebSocketException.Succeeded(nativeError)) ? WebSocketError.NativeError : WebSocketError.Success);
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="nativeError">The native error code for the exception.</param>
		// Token: 0x060036F9 RID: 14073 RVA: 0x000CAF33 File Offset: 0x000C9133
		public WebSocketException(WebSocketError error, int nativeError)
			: this(error, nativeError, WebSocketException.GetErrorMessage(error))
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="message">The description of the error.</param>
		// Token: 0x060036FA RID: 14074 RVA: 0x000CAF43 File Offset: 0x000C9143
		public WebSocketException(WebSocketError error, int nativeError, string message)
			: base(message)
		{
			this._webSocketErrorCode = error;
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x060036FB RID: 14075 RVA: 0x000CAF5A File Offset: 0x000C915A
		public WebSocketException(WebSocketError error, int nativeError, Exception innerException)
			: this(error, nativeError, WebSocketException.GetErrorMessage(error), innerException)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="message">The description of the error.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x060036FC RID: 14076 RVA: 0x000CAF6B File Offset: 0x000C916B
		public WebSocketException(WebSocketError error, int nativeError, string message, Exception innerException)
			: base(message, innerException)
		{
			this._webSocketErrorCode = error;
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="message">The description of the error.</param>
		// Token: 0x060036FD RID: 14077 RVA: 0x000CAF84 File Offset: 0x000C9184
		public WebSocketException(string message)
			: base(message)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="message">The description of the error.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x060036FE RID: 14078 RVA: 0x000CAF8D File Offset: 0x000C918D
		public WebSocketException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Sets the SerializationInfo object with the file name and line number where the exception occurred.</summary>
		/// <param name="info">A SerializationInfo object.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060036FF RID: 14079 RVA: 0x000CAF97 File Offset: 0x000C9197
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>The native error code for the exception that occurred.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.</returns>
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x0007D782 File Offset: 0x0007B982
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}

		/// <summary>Returns a WebSocketError indicating the type of error that occurred.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketError" />.</returns>
		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06003701 RID: 14081 RVA: 0x000CAFA1 File Offset: 0x000C91A1
		public WebSocketError WebSocketErrorCode
		{
			get
			{
				return this._webSocketErrorCode;
			}
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000CAFAC File Offset: 0x000C91AC
		private static string GetErrorMessage(WebSocketError error)
		{
			switch (error)
			{
			case WebSocketError.InvalidMessageType:
				return global::SR.Format("The received  message type is invalid after calling {0}. {0} should only be used if no more data is expected from the remote endpoint. Use '{1}' instead to keep being able to receive data but close the output channel.", string.Format("{0}.{1}", "WebSocket", "CloseAsync"), string.Format("{0}.{1}", "WebSocket", "CloseOutputAsync"));
			case WebSocketError.Faulted:
				return "An exception caused the WebSocket to enter the Aborted state. Please see the InnerException, if present, for more details.";
			case WebSocketError.NotAWebSocket:
				return "A WebSocket operation was called on a request or response that is not a WebSocket.";
			case WebSocketError.UnsupportedVersion:
				return "Unsupported WebSocket version.";
			case WebSocketError.UnsupportedProtocol:
				return "The WebSocket request or response operation was called with unsupported protocol(s).";
			case WebSocketError.HeaderError:
				return "The WebSocket request or response contained unsupported header(s).";
			case WebSocketError.ConnectionClosedPrematurely:
				return "The remote party closed the WebSocket connection without completing the close handshake.";
			case WebSocketError.InvalidState:
				return "The WebSocket instance cannot be used for communication because it has been transitioned into an invalid state.";
			}
			return "An internal WebSocket error occurred. Please see the innerException, if present, for more details.";
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000CB049 File Offset: 0x000C9249
		private void SetErrorCodeOnError(int nativeError)
		{
			if (!WebSocketException.Succeeded(nativeError))
			{
				base.HResult = nativeError;
			}
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x000CB05A File Offset: 0x000C925A
		private static bool Succeeded(int hr)
		{
			return hr >= 0;
		}

		// Token: 0x04002BD7 RID: 11223
		private readonly WebSocketError _webSocketErrorCode;
	}
}
