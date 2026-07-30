using System;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x020001D3 RID: 467
	internal class TextDataFeed : DataFeed
	{
		// Token: 0x06001586 RID: 5510 RVA: 0x0006C0A0 File Offset: 0x0006A2A0
		internal TextDataFeed(TextReader source)
		{
			this._source = source;
		}

		// Token: 0x04000E9C RID: 3740
		internal TextReader _source;
	}
}
