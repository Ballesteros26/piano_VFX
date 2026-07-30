using System;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x020001D4 RID: 468
	internal class XmlDataFeed : DataFeed
	{
		// Token: 0x06001587 RID: 5511 RVA: 0x0006C0AF File Offset: 0x0006A2AF
		internal XmlDataFeed(XmlReader source)
		{
			this._source = source;
		}

		// Token: 0x04000E9D RID: 3741
		internal XmlReader _source;
	}
}
