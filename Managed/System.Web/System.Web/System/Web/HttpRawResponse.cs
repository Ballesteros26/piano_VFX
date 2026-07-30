using System;
using System.Collections;

namespace System.Web
{
	// Token: 0x02000044 RID: 68
	internal class HttpRawResponse
	{
		// Token: 0x060003BC RID: 956 RVA: 0x00007279 File Offset: 0x00005479
		internal HttpRawResponse(int statusCode, string statusDescription, ArrayList headers, ArrayList buffers, bool hasSubstBlocks)
		{
			this._statusCode = statusCode;
			this._statusDescr = statusDescription;
			this._headers = headers;
			this._buffers = buffers;
			this._hasSubstBlocks = hasSubstBlocks;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000072A6 File Offset: 0x000054A6
		internal int StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000072AE File Offset: 0x000054AE
		internal string StatusDescription
		{
			get
			{
				return this._statusDescr;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060003BF RID: 959 RVA: 0x000072B6 File Offset: 0x000054B6
		internal ArrayList Headers
		{
			get
			{
				return this._headers;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x000072BE File Offset: 0x000054BE
		internal ArrayList Buffers
		{
			get
			{
				return this._buffers;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x000072C6 File Offset: 0x000054C6
		internal bool HasSubstBlocks
		{
			get
			{
				return this._hasSubstBlocks;
			}
		}

		// Token: 0x04000DA2 RID: 3490
		private int _statusCode;

		// Token: 0x04000DA3 RID: 3491
		private string _statusDescr;

		// Token: 0x04000DA4 RID: 3492
		private ArrayList _headers;

		// Token: 0x04000DA5 RID: 3493
		private ArrayList _buffers;

		// Token: 0x04000DA6 RID: 3494
		private bool _hasSubstBlocks;
	}
}
