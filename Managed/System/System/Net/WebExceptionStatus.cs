using System;

namespace System.Net
{
	/// <summary>Defines status codes for the <see cref="T:System.Net.WebException" /> class.</summary>
	// Token: 0x02000465 RID: 1125
	public enum WebExceptionStatus
	{
		/// <summary>No error was encountered.</summary>
		// Token: 0x04001E09 RID: 7689
		Success,
		/// <summary>The name resolver service could not resolve the host name.</summary>
		// Token: 0x04001E0A RID: 7690
		NameResolutionFailure,
		/// <summary>The remote service point could not be contacted at the transport level.</summary>
		// Token: 0x04001E0B RID: 7691
		ConnectFailure,
		/// <summary>A complete response was not received from the remote server.</summary>
		// Token: 0x04001E0C RID: 7692
		ReceiveFailure,
		/// <summary>A complete request could not be sent to the remote server.</summary>
		// Token: 0x04001E0D RID: 7693
		SendFailure,
		/// <summary>The request was a piplined request and the connection was closed before the response was received.</summary>
		// Token: 0x04001E0E RID: 7694
		PipelineFailure,
		/// <summary>The request was canceled, the <see cref="M:System.Net.WebRequest.Abort" /> method was called, or an unclassifiable error occurred. This is the default value for <see cref="P:System.Net.WebException.Status" />.</summary>
		// Token: 0x04001E0F RID: 7695
		RequestCanceled,
		/// <summary>The response received from the server was complete but indicated a protocol-level error. For example, an HTTP protocol error such as 401 Access Denied would use this status.</summary>
		// Token: 0x04001E10 RID: 7696
		ProtocolError,
		/// <summary>The connection was prematurely closed.</summary>
		// Token: 0x04001E11 RID: 7697
		ConnectionClosed,
		/// <summary>A server certificate could not be validated.</summary>
		// Token: 0x04001E12 RID: 7698
		TrustFailure,
		/// <summary>An error occurred while establishing a connection using SSL.</summary>
		// Token: 0x04001E13 RID: 7699
		SecureChannelFailure,
		/// <summary>The server response was not a valid HTTP response.</summary>
		// Token: 0x04001E14 RID: 7700
		ServerProtocolViolation,
		/// <summary>The connection for a request that specifies the Keep-alive header was closed unexpectedly.</summary>
		// Token: 0x04001E15 RID: 7701
		KeepAliveFailure,
		/// <summary>An internal asynchronous request is pending.</summary>
		// Token: 0x04001E16 RID: 7702
		Pending,
		/// <summary>No response was received during the time-out period for a request.</summary>
		// Token: 0x04001E17 RID: 7703
		Timeout,
		/// <summary>The name resolver service could not resolve the proxy host name.</summary>
		// Token: 0x04001E18 RID: 7704
		ProxyNameResolutionFailure,
		/// <summary>An exception of unknown type has occurred.</summary>
		// Token: 0x04001E19 RID: 7705
		UnknownError,
		/// <summary>A message was received that exceeded the specified limit when sending a request or receiving a response from the server.</summary>
		// Token: 0x04001E1A RID: 7706
		MessageLengthLimitExceeded,
		/// <summary>The specified cache entry was not found.</summary>
		// Token: 0x04001E1B RID: 7707
		CacheEntryNotFound,
		/// <summary>The request was not permitted by the cache policy. In general, this occurs when a request is not cacheable and the effective policy prohibits sending the request to the server. You might receive this status if a request method implies the presence of a request body, a request method requires direct interaction with the server, or a request contains a conditional header.</summary>
		// Token: 0x04001E1C RID: 7708
		RequestProhibitedByCachePolicy,
		/// <summary>This request was not permitted by the proxy.</summary>
		// Token: 0x04001E1D RID: 7709
		RequestProhibitedByProxy
	}
}
