using System;
using System.Globalization;
using System.Text;

namespace System.Net.WebSockets
{
	// Token: 0x020006D1 RID: 1745
	internal static class WebSocketValidate
	{
		// Token: 0x0600366E RID: 13934 RVA: 0x000C8E8C File Offset: 0x000C708C
		internal static void ThrowIfInvalidState(WebSocketState currentState, bool isDisposed, WebSocketState[] validStates)
		{
			string text = string.Empty;
			if (validStates != null && validStates.Length != 0)
			{
				int i = 0;
				while (i < validStates.Length)
				{
					WebSocketState webSocketState = validStates[i];
					if (currentState == webSocketState)
					{
						if (isDisposed)
						{
							throw new ObjectDisposedException("WebSocket");
						}
						return;
					}
					else
					{
						i++;
					}
				}
				text = string.Join<WebSocketState>(", ", validStates);
			}
			throw new WebSocketException(WebSocketError.InvalidState, global::SR.Format("The WebSocket is in an invalid state ('{0}') for this operation. Valid states are: '{1}'", currentState, text));
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000C8EF4 File Offset: 0x000C70F4
		internal static void ValidateSubprotocol(string subProtocol)
		{
			if (string.IsNullOrWhiteSpace(subProtocol))
			{
				throw new ArgumentException("Empty string is not a valid subprotocol value. Please use \\\"null\\\" to specify no value.", "subProtocol");
			}
			string text = null;
			foreach (char c in subProtocol)
			{
				if (c < '!' || c > '~')
				{
					text = string.Format(CultureInfo.InvariantCulture, "[{0}]", (int)c);
					break;
				}
				if (!char.IsLetterOrDigit(c) && "()<>@,;:\\\"/[]?={} ".IndexOf(c) >= 0)
				{
					text = c.ToString();
					break;
				}
			}
			if (text != null)
			{
				throw new ArgumentException(global::SR.Format("The WebSocket protocol '{0}' is invalid because it contains the invalid character '{1}'.", subProtocol, text), "subProtocol");
			}
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000C8F90 File Offset: 0x000C7190
		internal static void ValidateCloseStatus(WebSocketCloseStatus closeStatus, string statusDescription)
		{
			if (closeStatus == WebSocketCloseStatus.Empty && !string.IsNullOrEmpty(statusDescription))
			{
				throw new ArgumentException(global::SR.Format("The close status description '{0}' is invalid. When using close status code '{1}' the description must be null.", statusDescription, WebSocketCloseStatus.Empty), "statusDescription");
			}
			if ((closeStatus >= (WebSocketCloseStatus)0 && closeStatus <= (WebSocketCloseStatus)999) || closeStatus == (WebSocketCloseStatus)1006 || closeStatus == (WebSocketCloseStatus)1015)
			{
				throw new ArgumentException(global::SR.Format("The close status code '{0}' is reserved for system use only and cannot be specified when calling this method.", (int)closeStatus), "closeStatus");
			}
			int num = 0;
			if (!string.IsNullOrEmpty(statusDescription))
			{
				num = Encoding.UTF8.GetByteCount(statusDescription);
			}
			if (num > 123)
			{
				throw new ArgumentException(global::SR.Format("The close status description '{0}' is too long. The UTF8-representation of the status description must not be longer than {1} bytes.", statusDescription, 123), "statusDescription");
			}
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000C903E File Offset: 0x000C723E
		internal static void ThrowPlatformNotSupportedException()
		{
			throw new PlatformNotSupportedException("The WebSocket protocol is not supported on this platform.");
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000C904C File Offset: 0x000C724C
		internal static void ValidateArraySegment(ArraySegment<byte> arraySegment, string parameterName)
		{
			if (arraySegment.Array == null)
			{
				throw new ArgumentNullException(parameterName + ".Array");
			}
			if (arraySegment.Offset < 0 || arraySegment.Offset > arraySegment.Array.Length)
			{
				throw new ArgumentOutOfRangeException(parameterName + ".Offset");
			}
			if (arraySegment.Count < 0 || arraySegment.Count > arraySegment.Array.Length - arraySegment.Offset)
			{
				throw new ArgumentOutOfRangeException(parameterName + ".Count");
			}
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000C90D5 File Offset: 0x000C72D5
		internal static void ValidateBuffer(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		// Token: 0x04002B5A RID: 11098
		internal const int MaxControlFramePayloadLength = 123;

		// Token: 0x04002B5B RID: 11099
		private const int CloseStatusCodeAbort = 1006;

		// Token: 0x04002B5C RID: 11100
		private const int CloseStatusCodeFailedTLSHandshake = 1015;

		// Token: 0x04002B5D RID: 11101
		private const int InvalidCloseStatusCodesFrom = 0;

		// Token: 0x04002B5E RID: 11102
		private const int InvalidCloseStatusCodesTo = 999;

		// Token: 0x04002B5F RID: 11103
		private const string Separators = "()<>@,;:\\\"/[]?={} ";
	}
}
