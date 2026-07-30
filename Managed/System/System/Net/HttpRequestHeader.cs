using System;

namespace System.Net
{
	/// <summary>The HTTP headers that may be specified in a client request.</summary>
	// Token: 0x02000429 RID: 1065
	public enum HttpRequestHeader
	{
		/// <summary>The Cache-Control header, which specifies directives that must be obeyed by all cache control mechanisms along the request/response chain.</summary>
		// Token: 0x04001C1B RID: 7195
		CacheControl,
		/// <summary>The Connection header, which specifies options that are desired for a particular connection.</summary>
		// Token: 0x04001C1C RID: 7196
		Connection,
		/// <summary>The Date header, which specifies the date and time at which the request originated.</summary>
		// Token: 0x04001C1D RID: 7197
		Date,
		/// <summary>The Keep-Alive header, which specifies a parameter used into order to maintain a persistent connection.</summary>
		// Token: 0x04001C1E RID: 7198
		KeepAlive,
		/// <summary>The Pragma header, which specifies implementation-specific directives that might apply to any agent along the request/response chain.</summary>
		// Token: 0x04001C1F RID: 7199
		Pragma,
		/// <summary>The Trailer header, which specifies the header fields present in the trailer of a message encoded with chunked transfer-coding.</summary>
		// Token: 0x04001C20 RID: 7200
		Trailer,
		/// <summary>The Transfer-Encoding header, which specifies what (if any) type of transformation that has been applied to the message body.</summary>
		// Token: 0x04001C21 RID: 7201
		TransferEncoding,
		/// <summary>The Upgrade header, which specifies additional communications protocols that the client supports.</summary>
		// Token: 0x04001C22 RID: 7202
		Upgrade,
		/// <summary>The Via header, which specifies intermediate protocols to be used by gateway and proxy agents.</summary>
		// Token: 0x04001C23 RID: 7203
		Via,
		/// <summary>The Warning header, which specifies additional information about that status or transformation of a message that might not be reflected in the message.</summary>
		// Token: 0x04001C24 RID: 7204
		Warning,
		/// <summary>The Allow header, which specifies the set of HTTP methods supported.</summary>
		// Token: 0x04001C25 RID: 7205
		Allow,
		/// <summary>The Content-Length header, which specifies the length, in bytes, of the accompanying body data.</summary>
		// Token: 0x04001C26 RID: 7206
		ContentLength,
		/// <summary>The Content-Type header, which specifies the MIME type of the accompanying body data.</summary>
		// Token: 0x04001C27 RID: 7207
		ContentType,
		/// <summary>The Content-Encoding header, which specifies the encodings that have been applied to the accompanying body data.</summary>
		// Token: 0x04001C28 RID: 7208
		ContentEncoding,
		/// <summary>The Content-Langauge header, which specifies the natural language(s) of the accompanying body data.</summary>
		// Token: 0x04001C29 RID: 7209
		ContentLanguage,
		/// <summary>The Content-Location header, which specifies a URI from which the accompanying body may be obtained.</summary>
		// Token: 0x04001C2A RID: 7210
		ContentLocation,
		/// <summary>The Content-MD5 header, which specifies the MD5 digest of the accompanying body data, for the purpose of providing an end-to-end message integrity check.</summary>
		// Token: 0x04001C2B RID: 7211
		ContentMd5,
		/// <summary>The Content-Range header, which specifies where in the full body the accompanying partial body data should be applied.</summary>
		// Token: 0x04001C2C RID: 7212
		ContentRange,
		/// <summary>The Expires header, which specifies the date and time after which the accompanying body data should be considered stale.</summary>
		// Token: 0x04001C2D RID: 7213
		Expires,
		/// <summary>The Last-Modified header, which specifies the date and time at which the accompanying body data was last modified.</summary>
		// Token: 0x04001C2E RID: 7214
		LastModified,
		/// <summary>The Accept header, which specifies the MIME types that are acceptable for the response.</summary>
		// Token: 0x04001C2F RID: 7215
		Accept,
		/// <summary>The Accept-Charset header, which specifies the character sets that are acceptable for the response.</summary>
		// Token: 0x04001C30 RID: 7216
		AcceptCharset,
		/// <summary>The Accept-Encoding header, which specifies the content encodings that are acceptable for the response.</summary>
		// Token: 0x04001C31 RID: 7217
		AcceptEncoding,
		/// <summary>The Accept-Langauge header, which specifies that natural languages that are preferred for the response.</summary>
		// Token: 0x04001C32 RID: 7218
		AcceptLanguage,
		/// <summary>The Authorization header, which specifies the credentials that the client presents in order to authenticate itself to the server.</summary>
		// Token: 0x04001C33 RID: 7219
		Authorization,
		/// <summary>The Cookie header, which specifies cookie data presented to the server.</summary>
		// Token: 0x04001C34 RID: 7220
		Cookie,
		/// <summary>The Expect header, which specifies particular server behaviors that are required by the client.</summary>
		// Token: 0x04001C35 RID: 7221
		Expect,
		/// <summary>The From header, which specifies an Internet E-mail address for the human user who controls the requesting user agent.</summary>
		// Token: 0x04001C36 RID: 7222
		From,
		/// <summary>The Host header, which specifies the host name and port number of the resource being requested.</summary>
		// Token: 0x04001C37 RID: 7223
		Host,
		/// <summary>The If-Match header, which specifies that the requested operation should be performed only if the client's cached copy of the indicated resource is current.</summary>
		// Token: 0x04001C38 RID: 7224
		IfMatch,
		/// <summary>The If-Modified-Since header, which specifies that the requested operation should be performed only if the requested resource has been modified since the indicated data and time.</summary>
		// Token: 0x04001C39 RID: 7225
		IfModifiedSince,
		/// <summary>The If-None-Match header, which specifies that the requested operation should be performed only if none of client's cached copies of the indicated resources are current.</summary>
		// Token: 0x04001C3A RID: 7226
		IfNoneMatch,
		/// <summary>The If-Range header, which specifies that only the specified range of the requested resource should be sent, if the client's cached copy is current.</summary>
		// Token: 0x04001C3B RID: 7227
		IfRange,
		/// <summary>The If-Unmodified-Since header, which specifies that the requested operation should be performed only if the requested resource has not been modified since the indicated date and time.</summary>
		// Token: 0x04001C3C RID: 7228
		IfUnmodifiedSince,
		/// <summary>The Max-Forwards header, which specifies an integer indicating the remaining number of times that this request may be forwarded.</summary>
		// Token: 0x04001C3D RID: 7229
		MaxForwards,
		/// <summary>The Proxy-Authorization header, which specifies the credentials that the client presents in order to authenticate itself to a proxy.</summary>
		// Token: 0x04001C3E RID: 7230
		ProxyAuthorization,
		/// <summary>The Referer header, which specifies the URI of the resource from which the request URI was obtained.</summary>
		// Token: 0x04001C3F RID: 7231
		Referer,
		/// <summary>The Range header, which specifies the the sub-range(s) of the response that the client requests be returned in lieu of the entire response.</summary>
		// Token: 0x04001C40 RID: 7232
		Range,
		/// <summary>The TE header, which specifies the transfer encodings that are acceptable for the response.</summary>
		// Token: 0x04001C41 RID: 7233
		Te,
		/// <summary>The Translate header, a Microsoft extension to the HTTP specification used in conjunction with WebDAV functionality.</summary>
		// Token: 0x04001C42 RID: 7234
		Translate,
		/// <summary>The User-Agent header, which specifies information about the client agent.</summary>
		// Token: 0x04001C43 RID: 7235
		UserAgent
	}
}
