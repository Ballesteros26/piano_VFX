using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000302 RID: 770
	internal sealed class DbConnectionClosedBusy : DbConnectionBusy
	{
		// Token: 0x0600223E RID: 8766 RVA: 0x000A0169 File Offset: 0x0009E369
		private DbConnectionClosedBusy()
			: base(ConnectionState.Closed)
		{
		}

		// Token: 0x040016CD RID: 5837
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedBusy();
	}
}
