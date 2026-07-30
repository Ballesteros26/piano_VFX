using System;

namespace System.Net
{
	/// <summary>Contains the values of status codes defined for HTTP.</summary>
	// Token: 0x0200042B RID: 1067
	public enum HttpStatusCode
	{
		/// <summary>Equivalent to HTTP status 100. <see cref="F:System.Net.HttpStatusCode.Continue" /> indicates that the client can continue with its request.</summary>
		// Token: 0x04001C64 RID: 7268
		Continue = 100,
		/// <summary>Equivalent to HTTP status 101. <see cref="F:System.Net.HttpStatusCode.SwitchingProtocols" /> indicates that the protocol version or protocol is being changed.</summary>
		// Token: 0x04001C65 RID: 7269
		SwitchingProtocols,
		/// <summary>Equivalent to HTTP status 200. <see cref="F:System.Net.HttpStatusCode.OK" /> indicates that the request succeeded and that the requested information is in the response. This is the most common status code to receive.</summary>
		// Token: 0x04001C66 RID: 7270
		OK = 200,
		/// <summary>Equivalent to HTTP status 201. <see cref="F:System.Net.HttpStatusCode.Created" /> indicates that the request resulted in a new resource created before the response was sent.</summary>
		// Token: 0x04001C67 RID: 7271
		Created,
		/// <summary>Equivalent to HTTP status 202. <see cref="F:System.Net.HttpStatusCode.Accepted" /> indicates that the request has been accepted for further processing.</summary>
		// Token: 0x04001C68 RID: 7272
		Accepted,
		/// <summary>Equivalent to HTTP status 203. <see cref="F:System.Net.HttpStatusCode.NonAuthoritativeInformation" /> indicates that the returned metainformation is from a cached copy instead of the origin server and therefore may be incorrect.</summary>
		// Token: 0x04001C69 RID: 7273
		NonAuthoritativeInformation,
		/// <summary>Equivalent to HTTP status 204. <see cref="F:System.Net.HttpStatusCode.NoContent" /> indicates that the request has been successfully processed and that the response is intentionally blank.</summary>
		// Token: 0x04001C6A RID: 7274
		NoContent,
		/// <summary>Equivalent to HTTP status 205. <see cref="F:System.Net.HttpStatusCode.ResetContent" /> indicates that the client should reset (not reload) the current resource.</summary>
		// Token: 0x04001C6B RID: 7275
		ResetContent,
		/// <summary>Equivalent to HTTP status 206. <see cref="F:System.Net.HttpStatusCode.PartialContent" /> indicates that the response is a partial response as requested by a GET request that includes a byte range.</summary>
		// Token: 0x04001C6C RID: 7276
		PartialContent,
		/// <summary>Equivalent to HTTP status 300. <see cref="F:System.Net.HttpStatusCode.MultipleChoices" /> indicates that the requested information has multiple representations. The default action is to treat this status as a redirect and follow the contents of the Location header associated with this response.</summary>
		// Token: 0x04001C6D RID: 7277
		MultipleChoices = 300,
		/// <summary>Equivalent to HTTP status 300. <see cref="F:System.Net.HttpStatusCode.Ambiguous" /> indicates that the requested information has multiple representations. The default action is to treat this status as a redirect and follow the contents of the Location header associated with this response.</summary>
		// Token: 0x04001C6E RID: 7278
		Ambiguous = 300,
		/// <summary>Equivalent to HTTP status 301. <see cref="F:System.Net.HttpStatusCode.MovedPermanently" /> indicates that the requested information has been moved to the URI specified in the Location header. The default action when this status is received is to follow the Location header associated with the response.</summary>
		// Token: 0x04001C6F RID: 7279
		MovedPermanently,
		/// <summary>Equivalent to HTTP status 301. <see cref="F:System.Net.HttpStatusCode.Moved" /> indicates that the requested information has been moved to the URI specified in the Location header. The default action when this status is received is to follow the Location header associated with the response. When the original request method was POST, the redirected request will use the GET method.</summary>
		// Token: 0x04001C70 RID: 7280
		Moved = 301,
		/// <summary>Equivalent to HTTP status 302. <see cref="F:System.Net.HttpStatusCode.Found" /> indicates that the requested information is located at the URI specified in the Location header. The default action when this status is received is to follow the Location header associated with the response. When the original request method was POST, the redirected request will use the GET method.</summary>
		// Token: 0x04001C71 RID: 7281
		Found,
		/// <summary>Equivalent to HTTP status 302. <see cref="F:System.Net.HttpStatusCode.Redirect" /> indicates that the requested information is located at the URI specified in the Location header. The default action when this status is received is to follow the Location header associated with the response. When the original request method was POST, the redirected request will use the GET method.</summary>
		// Token: 0x04001C72 RID: 7282
		Redirect = 302,
		/// <summary>Equivalent to HTTP status 303. <see cref="F:System.Net.HttpStatusCode.SeeOther" /> automatically redirects the client to the URI specified in the Location header as the result of a POST. The request to the resource specified by the Location header will be made with a GET.</summary>
		// Token: 0x04001C73 RID: 7283
		SeeOther,
		/// <summary>Equivalent to HTTP status 303. <see cref="F:System.Net.HttpStatusCode.RedirectMethod" /> automatically redirects the client to the URI specified in the Location header as the result of a POST. The request to the resource specified by the Location header will be made with a GET.</summary>
		// Token: 0x04001C74 RID: 7284
		RedirectMethod = 303,
		/// <summary>Equivalent to HTTP status 304. <see cref="F:System.Net.HttpStatusCode.NotModified" /> indicates that the client's cached copy is up to date. The contents of the resource are not transferred.</summary>
		// Token: 0x04001C75 RID: 7285
		NotModified,
		/// <summary>Equivalent to HTTP status 305. <see cref="F:System.Net.HttpStatusCode.UseProxy" /> indicates that the request should use the proxy server at the URI specified in the Location header.</summary>
		// Token: 0x04001C76 RID: 7286
		UseProxy,
		/// <summary>Equivalent to HTTP status 306. <see cref="F:System.Net.HttpStatusCode.Unused" /> is a proposed extension to the HTTP/1.1 specification that is not fully specified.</summary>
		// Token: 0x04001C77 RID: 7287
		Unused,
		/// <summary>Equivalent to HTTP status 307. <see cref="F:System.Net.HttpStatusCode.TemporaryRedirect" /> indicates that the request information is located at the URI specified in the Location header. The default action when this status is received is to follow the Location header associated with the response. When the original request method was POST, the redirected request will also use the POST method.</summary>
		// Token: 0x04001C78 RID: 7288
		TemporaryRedirect,
		/// <summary>Equivalent to HTTP status 307. <see cref="F:System.Net.HttpStatusCode.RedirectKeepVerb" /> indicates that the request information is located at the URI specified in the Location header. The default action when this status is received is to follow the Location header associated with the response. When the original request method was POST, the redirected request will also use the POST method.</summary>
		// Token: 0x04001C79 RID: 7289
		RedirectKeepVerb = 307,
		/// <summary>Equivalent to HTTP status 400. <see cref="F:System.Net.HttpStatusCode.BadRequest" /> indicates that the request could not be understood by the server. <see cref="F:System.Net.HttpStatusCode.BadRequest" /> is sent when no other error is applicable, or if the exact error is unknown or does not have its own error code.</summary>
		// Token: 0x04001C7A RID: 7290
		BadRequest = 400,
		/// <summary>Equivalent to HTTP status 401. <see cref="F:System.Net.HttpStatusCode.Unauthorized" /> indicates that the requested resource requires authentication. The WWW-Authenticate header contains the details of how to perform the authentication.</summary>
		// Token: 0x04001C7B RID: 7291
		Unauthorized,
		/// <summary>Equivalent to HTTP status 402. <see cref="F:System.Net.HttpStatusCode.PaymentRequired" /> is reserved for future use.</summary>
		// Token: 0x04001C7C RID: 7292
		PaymentRequired,
		/// <summary>Equivalent to HTTP status 403. <see cref="F:System.Net.HttpStatusCode.Forbidden" /> indicates that the server refuses to fulfill the request.</summary>
		// Token: 0x04001C7D RID: 7293
		Forbidden,
		/// <summary>Equivalent to HTTP status 404. <see cref="F:System.Net.HttpStatusCode.NotFound" /> indicates that the requested resource does not exist on the server.</summary>
		// Token: 0x04001C7E RID: 7294
		NotFound,
		/// <summary>Equivalent to HTTP status 405. <see cref="F:System.Net.HttpStatusCode.MethodNotAllowed" /> indicates that the request method (POST or GET) is not allowed on the requested resource.</summary>
		// Token: 0x04001C7F RID: 7295
		MethodNotAllowed,
		/// <summary>Equivalent to HTTP status 406. <see cref="F:System.Net.HttpStatusCode.NotAcceptable" /> indicates that the client has indicated with Accept headers that it will not accept any of the available representations of the resource.</summary>
		// Token: 0x04001C80 RID: 7296
		NotAcceptable,
		/// <summary>Equivalent to HTTP status 407. <see cref="F:System.Net.HttpStatusCode.ProxyAuthenticationRequired" /> indicates that the requested proxy requires authentication. The Proxy-authenticate header contains the details of how to perform the authentication.</summary>
		// Token: 0x04001C81 RID: 7297
		ProxyAuthenticationRequired,
		/// <summary>Equivalent to HTTP status 408. <see cref="F:System.Net.HttpStatusCode.RequestTimeout" /> indicates that the client did not send a request within the time the server was expecting the request.</summary>
		// Token: 0x04001C82 RID: 7298
		RequestTimeout,
		/// <summary>Equivalent to HTTP status 409. <see cref="F:System.Net.HttpStatusCode.Conflict" /> indicates that the request could not be carried out because of a conflict on the server.</summary>
		// Token: 0x04001C83 RID: 7299
		Conflict,
		/// <summary>Equivalent to HTTP status 410. <see cref="F:System.Net.HttpStatusCode.Gone" /> indicates that the requested resource is no longer available.</summary>
		// Token: 0x04001C84 RID: 7300
		Gone,
		/// <summary>Equivalent to HTTP status 411. <see cref="F:System.Net.HttpStatusCode.LengthRequired" /> indicates that the required Content-length header is missing.</summary>
		// Token: 0x04001C85 RID: 7301
		LengthRequired,
		/// <summary>Equivalent to HTTP status 412. <see cref="F:System.Net.HttpStatusCode.PreconditionFailed" /> indicates that a condition set for this request failed, and the request cannot be carried out. Conditions are set with conditional request headers like If-Match, If-None-Match, or If-Unmodified-Since.</summary>
		// Token: 0x04001C86 RID: 7302
		PreconditionFailed,
		/// <summary>Equivalent to HTTP status 413. <see cref="F:System.Net.HttpStatusCode.RequestEntityTooLarge" /> indicates that the request is too large for the server to process.</summary>
		// Token: 0x04001C87 RID: 7303
		RequestEntityTooLarge,
		/// <summary>Equivalent to HTTP status 414. <see cref="F:System.Net.HttpStatusCode.RequestUriTooLong" /> indicates that the URI is too long.</summary>
		// Token: 0x04001C88 RID: 7304
		RequestUriTooLong,
		/// <summary>Equivalent to HTTP status 415. <see cref="F:System.Net.HttpStatusCode.UnsupportedMediaType" /> indicates that the request is an unsupported type.</summary>
		// Token: 0x04001C89 RID: 7305
		UnsupportedMediaType,
		/// <summary>Equivalent to HTTP status 416. <see cref="F:System.Net.HttpStatusCode.RequestedRangeNotSatisfiable" /> indicates that the range of data requested from the resource cannot be returned, either because the beginning of the range is before the beginning of the resource, or the end of the range is after the end of the resource.</summary>
		// Token: 0x04001C8A RID: 7306
		RequestedRangeNotSatisfiable,
		/// <summary>Equivalent to HTTP status 417. <see cref="F:System.Net.HttpStatusCode.ExpectationFailed" /> indicates that an expectation given in an Expect header could not be met by the server.</summary>
		// Token: 0x04001C8B RID: 7307
		ExpectationFailed,
		/// <summary>Equivalent to HTTP status 426. <see cref="F:System.Net.HttpStatusCode.UpgradeRequired" /> indicates that the client should switch to a different protocol such as TLS/1.0.</summary>
		// Token: 0x04001C8C RID: 7308
		UpgradeRequired = 426,
		/// <summary>Equivalent to HTTP status 500. <see cref="F:System.Net.HttpStatusCode.InternalServerError" /> indicates that a generic error has occurred on the server.</summary>
		// Token: 0x04001C8D RID: 7309
		InternalServerError = 500,
		/// <summary>Equivalent to HTTP status 501. <see cref="F:System.Net.HttpStatusCode.NotImplemented" /> indicates that the server does not support the requested function.</summary>
		// Token: 0x04001C8E RID: 7310
		NotImplemented,
		/// <summary>Equivalent to HTTP status 502. <see cref="F:System.Net.HttpStatusCode.BadGateway" /> indicates that an intermediate proxy server received a bad response from another proxy or the origin server.</summary>
		// Token: 0x04001C8F RID: 7311
		BadGateway,
		/// <summary>Equivalent to HTTP status 503. <see cref="F:System.Net.HttpStatusCode.ServiceUnavailable" /> indicates that the server is temporarily unavailable, usually due to high load or maintenance.</summary>
		// Token: 0x04001C90 RID: 7312
		ServiceUnavailable,
		/// <summary>Equivalent to HTTP status 504. <see cref="F:System.Net.HttpStatusCode.GatewayTimeout" /> indicates that an intermediate proxy server timed out while waiting for a response from another proxy or the origin server.</summary>
		// Token: 0x04001C91 RID: 7313
		GatewayTimeout,
		/// <summary>Equivalent to HTTP status 505. <see cref="F:System.Net.HttpStatusCode.HttpVersionNotSupported" /> indicates that the requested HTTP version is not supported by the server.</summary>
		// Token: 0x04001C92 RID: 7314
		HttpVersionNotSupported
	}
}
