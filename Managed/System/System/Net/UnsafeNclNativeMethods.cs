using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Net
{
	// Token: 0x020004F3 RID: 1267
	internal static class UnsafeNclNativeMethods
	{
		// Token: 0x020004F4 RID: 1268
		internal static class HttpApi
		{
			// Token: 0x040020C6 RID: 8390
			private const int HttpHeaderRequestMaximum = 41;

			// Token: 0x040020C7 RID: 8391
			private const int HttpHeaderResponseMaximum = 30;

			// Token: 0x040020C8 RID: 8392
			private static string[] m_Strings = new string[]
			{
				"Cache-Control", "Connection", "Date", "Keep-Alive", "Pragma", "Trailer", "Transfer-Encoding", "Upgrade", "Via", "Warning",
				"Allow", "Content-Length", "Content-Type", "Content-Encoding", "Content-Language", "Content-Location", "Content-MD5", "Content-Range", "Expires", "Last-Modified",
				"Accept-Ranges", "Age", "ETag", "Location", "Proxy-Authenticate", "Retry-After", "Server", "Set-Cookie", "Vary", "WWW-Authenticate"
			};

			// Token: 0x020004F5 RID: 1269
			internal static class HTTP_REQUEST_HEADER_ID
			{
				// Token: 0x06002619 RID: 9753 RVA: 0x000935A2 File Offset: 0x000917A2
				internal static string ToString(int position)
				{
					return UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.m_Strings[position];
				}

				// Token: 0x040020C9 RID: 8393
				private static string[] m_Strings = new string[]
				{
					"Cache-Control", "Connection", "Date", "Keep-Alive", "Pragma", "Trailer", "Transfer-Encoding", "Upgrade", "Via", "Warning",
					"Allow", "Content-Length", "Content-Type", "Content-Encoding", "Content-Language", "Content-Location", "Content-MD5", "Content-Range", "Expires", "Last-Modified",
					"Accept", "Accept-Charset", "Accept-Encoding", "Accept-Language", "Authorization", "Cookie", "Expect", "From", "Host", "If-Match",
					"If-Modified-Since", "If-None-Match", "If-Range", "If-Unmodified-Since", "Max-Forwards", "Proxy-Authorization", "Referer", "Range", "Te", "Translate",
					"User-Agent"
				};
			}

			// Token: 0x020004F6 RID: 1270
			internal static class HTTP_RESPONSE_HEADER_ID
			{
				// Token: 0x0600261B RID: 9755 RVA: 0x00093730 File Offset: 0x00091930
				static HTTP_RESPONSE_HEADER_ID()
				{
					for (int i = 0; i < 30; i++)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable.Add(UnsafeNclNativeMethods.HttpApi.m_Strings[i], i);
					}
				}

				// Token: 0x0600261C RID: 9756 RVA: 0x00093770 File Offset: 0x00091970
				internal static int IndexOfKnownHeader(string HeaderName)
				{
					object obj = UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable[HeaderName];
					if (obj != null)
					{
						return (int)obj;
					}
					return -1;
				}

				// Token: 0x0600261D RID: 9757 RVA: 0x00093794 File Offset: 0x00091994
				internal static string ToString(int position)
				{
					return UnsafeNclNativeMethods.HttpApi.m_Strings[position];
				}

				// Token: 0x040020CA RID: 8394
				private static Hashtable m_Hashtable = new Hashtable(30);
			}

			// Token: 0x020004F7 RID: 1271
			internal enum Enum
			{
				// Token: 0x040020CC RID: 8396
				HttpHeaderCacheControl,
				// Token: 0x040020CD RID: 8397
				HttpHeaderConnection,
				// Token: 0x040020CE RID: 8398
				HttpHeaderDate,
				// Token: 0x040020CF RID: 8399
				HttpHeaderKeepAlive,
				// Token: 0x040020D0 RID: 8400
				HttpHeaderPragma,
				// Token: 0x040020D1 RID: 8401
				HttpHeaderTrailer,
				// Token: 0x040020D2 RID: 8402
				HttpHeaderTransferEncoding,
				// Token: 0x040020D3 RID: 8403
				HttpHeaderUpgrade,
				// Token: 0x040020D4 RID: 8404
				HttpHeaderVia,
				// Token: 0x040020D5 RID: 8405
				HttpHeaderWarning,
				// Token: 0x040020D6 RID: 8406
				HttpHeaderAllow,
				// Token: 0x040020D7 RID: 8407
				HttpHeaderContentLength,
				// Token: 0x040020D8 RID: 8408
				HttpHeaderContentType,
				// Token: 0x040020D9 RID: 8409
				HttpHeaderContentEncoding,
				// Token: 0x040020DA RID: 8410
				HttpHeaderContentLanguage,
				// Token: 0x040020DB RID: 8411
				HttpHeaderContentLocation,
				// Token: 0x040020DC RID: 8412
				HttpHeaderContentMd5,
				// Token: 0x040020DD RID: 8413
				HttpHeaderContentRange,
				// Token: 0x040020DE RID: 8414
				HttpHeaderExpires,
				// Token: 0x040020DF RID: 8415
				HttpHeaderLastModified,
				// Token: 0x040020E0 RID: 8416
				HttpHeaderAcceptRanges,
				// Token: 0x040020E1 RID: 8417
				HttpHeaderAge,
				// Token: 0x040020E2 RID: 8418
				HttpHeaderEtag,
				// Token: 0x040020E3 RID: 8419
				HttpHeaderLocation,
				// Token: 0x040020E4 RID: 8420
				HttpHeaderProxyAuthenticate,
				// Token: 0x040020E5 RID: 8421
				HttpHeaderRetryAfter,
				// Token: 0x040020E6 RID: 8422
				HttpHeaderServer,
				// Token: 0x040020E7 RID: 8423
				HttpHeaderSetCookie,
				// Token: 0x040020E8 RID: 8424
				HttpHeaderVary,
				// Token: 0x040020E9 RID: 8425
				HttpHeaderWwwAuthenticate,
				// Token: 0x040020EA RID: 8426
				HttpHeaderResponseMaximum,
				// Token: 0x040020EB RID: 8427
				HttpHeaderMaximum = 41
			}
		}

		// Token: 0x020004F8 RID: 1272
		internal static class SecureStringHelper
		{
			// Token: 0x0600261E RID: 9758 RVA: 0x000937A0 File Offset: 0x000919A0
			internal static string CreateString(SecureString secureString)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (secureString == null || secureString.Length == 0)
				{
					return string.Empty;
				}
				string text;
				try
				{
					intPtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
					text = Marshal.PtrToStringUni(intPtr);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
					}
				}
				return text;
			}

			// Token: 0x0600261F RID: 9759 RVA: 0x000937FC File Offset: 0x000919FC
			internal unsafe static SecureString CreateSecureString(string plainString)
			{
				if (plainString == null || plainString.Length == 0)
				{
					return new SecureString();
				}
				SecureString secureString;
				fixed (string text = plainString)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					secureString = new SecureString(ptr, plainString.Length);
				}
				return secureString;
			}
		}
	}
}
