using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000303 RID: 771
	internal sealed class DbConnectionOpenBusy : DbConnectionBusy
	{
		// Token: 0x06002240 RID: 8768 RVA: 0x000A017E File Offset: 0x0009E37E
		private DbConnectionOpenBusy()
			: base(ConnectionState.Open)
		{
		}

		// Token: 0x040016CE RID: 5838
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionOpenBusy();
	}
}
