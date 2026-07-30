using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace System.Web.Caching
{
	// Token: 0x0200068A RID: 1674
	internal sealed class CachedRawResponse
	{
		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x0600475E RID: 18270 RVA: 0x000C8AEA File Offset: 0x000C6CEA
		private IList Data
		{
			get
			{
				if (this.data == null)
				{
					this.data = new List<CachedRawResponse.DataItem>();
				}
				return this.data;
			}
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x000C8B05 File Offset: 0x000C6D05
		public CachedRawResponse(HttpCachePolicy policy)
		{
			this.policy = policy;
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06004760 RID: 18272 RVA: 0x000C8B14 File Offset: 0x000C6D14
		// (set) Token: 0x06004761 RID: 18273 RVA: 0x000C8B1C File Offset: 0x000C6D1C
		public HttpCachePolicy Policy
		{
			get
			{
				return this.policy;
			}
			set
			{
				this.policy = value;
			}
		}

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06004762 RID: 18274 RVA: 0x000C8B25 File Offset: 0x000C6D25
		// (set) Token: 0x06004763 RID: 18275 RVA: 0x000C8B2D File Offset: 0x000C6D2D
		public CachedVaryBy VaryBy
		{
			get
			{
				return this.varyby;
			}
			set
			{
				this.varyby = value;
			}
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06004764 RID: 18276 RVA: 0x000C8B36 File Offset: 0x000C6D36
		// (set) Token: 0x06004765 RID: 18277 RVA: 0x000C8B3E File Offset: 0x000C6D3E
		public int StatusCode
		{
			get
			{
				return this.status_code;
			}
			set
			{
				this.status_code = value;
			}
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06004766 RID: 18278 RVA: 0x000C8B47 File Offset: 0x000C6D47
		// (set) Token: 0x06004767 RID: 18279 RVA: 0x000C8B4F File Offset: 0x000C6D4F
		public string StatusDescription
		{
			get
			{
				return this.status_desc;
			}
			set
			{
				this.status_desc = value;
			}
		}

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06004768 RID: 18280 RVA: 0x000C8B58 File Offset: 0x000C6D58
		public NameValueCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x000C8B60 File Offset: 0x000C6D60
		public void SetHeaders(NameValueCollection headers)
		{
			this.headers = headers;
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x000C8B69 File Offset: 0x000C6D69
		public void SetData(MemoryStream ms)
		{
			if (ms == null)
			{
				return;
			}
			this.Data.Add(new CachedRawResponse.DataItem(ms.GetBuffer(), ms.Length));
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x000C8B8C File Offset: 0x000C6D8C
		public void SetData(HttpResponseSubstitutionCallback callback)
		{
			if (callback == null)
			{
				return;
			}
			this.Data.Add(new CachedRawResponse.DataItem(callback));
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x000C8BA4 File Offset: 0x000C6DA4
		public IList GetData()
		{
			if (this.data == null || this.data.Count == 0)
			{
				return null;
			}
			return this.data;
		}

		// Token: 0x040025A2 RID: 9634
		private HttpCachePolicy policy;

		// Token: 0x040025A3 RID: 9635
		private CachedVaryBy varyby;

		// Token: 0x040025A4 RID: 9636
		private int status_code;

		// Token: 0x040025A5 RID: 9637
		private string status_desc;

		// Token: 0x040025A6 RID: 9638
		private NameValueCollection headers;

		// Token: 0x040025A7 RID: 9639
		private List<CachedRawResponse.DataItem> data;

		// Token: 0x0200068B RID: 1675
		public sealed class DataItem
		{
			// Token: 0x0600476D RID: 18285 RVA: 0x000C8BC6 File Offset: 0x000C6DC6
			public DataItem(byte[] buffer, long length)
			{
				this.Buffer = buffer;
				this.Length = length;
			}

			// Token: 0x0600476E RID: 18286 RVA: 0x000C8BDC File Offset: 0x000C6DDC
			public DataItem(HttpResponseSubstitutionCallback callback)
				: this(null, 0L)
			{
				this.Callback = callback;
			}

			// Token: 0x040025A8 RID: 9640
			public readonly byte[] Buffer;

			// Token: 0x040025A9 RID: 9641
			public readonly long Length;

			// Token: 0x040025AA RID: 9642
			public readonly HttpResponseSubstitutionCallback Callback;
		}
	}
}
