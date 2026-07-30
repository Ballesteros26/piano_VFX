using System;

namespace System.Net
{
	// Token: 0x0200043C RID: 1084
	internal static class ExceptionHelper
	{
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x0007F254 File Offset: 0x0007D454
		internal static NotImplementedException MethodNotImplementedException
		{
			get
			{
				return new NotImplementedException(global::SR.GetString("This method is not implemented by this class."));
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x0007F265 File Offset: 0x0007D465
		internal static NotImplementedException PropertyNotImplementedException
		{
			get
			{
				return new NotImplementedException(global::SR.GetString("This property is not implemented by this class."));
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x0007F276 File Offset: 0x0007D476
		internal static NotSupportedException MethodNotSupportedException
		{
			get
			{
				return new NotSupportedException(global::SR.GetString("This method is not supported by this class."));
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060020AF RID: 8367 RVA: 0x0007F287 File Offset: 0x0007D487
		internal static NotSupportedException PropertyNotSupportedException
		{
			get
			{
				return new NotSupportedException(global::SR.GetString("This property is not supported by this class."));
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x0007F298 File Offset: 0x0007D498
		internal static WebException IsolatedException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.KeepAliveFailure), WebExceptionStatus.KeepAliveFailure, WebExceptionInternalStatus.Isolated, null);
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060020B1 RID: 8369 RVA: 0x0007F2AF File Offset: 0x0007D4AF
		internal static WebException RequestAbortedException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x0007F2C2 File Offset: 0x0007D4C2
		internal static WebException CacheEntryNotFoundException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.CacheEntryNotFound), WebExceptionStatus.CacheEntryNotFound);
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x0007F2D7 File Offset: 0x0007D4D7
		internal static WebException RequestProhibitedByCachePolicyException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestProhibitedByCachePolicy), WebExceptionStatus.RequestProhibitedByCachePolicy);
			}
		}
	}
}
