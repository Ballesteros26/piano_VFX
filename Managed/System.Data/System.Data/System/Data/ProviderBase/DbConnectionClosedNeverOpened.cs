using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000305 RID: 773
	internal sealed class DbConnectionClosedNeverOpened : DbConnectionClosed
	{
		// Token: 0x06002247 RID: 8775 RVA: 0x000A0214 File Offset: 0x0009E414
		private DbConnectionClosedNeverOpened()
			: base(ConnectionState.Closed, false, true)
		{
		}

		// Token: 0x040016D0 RID: 5840
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedNeverOpened();
	}
}
