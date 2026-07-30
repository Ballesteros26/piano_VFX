using System;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x020001D2 RID: 466
	internal class StreamDataFeed : DataFeed
	{
		// Token: 0x06001585 RID: 5509 RVA: 0x0006C091 File Offset: 0x0006A291
		internal StreamDataFeed(Stream source)
		{
			this._source = source;
		}

		// Token: 0x04000E9B RID: 3739
		internal Stream _source;
	}
}
