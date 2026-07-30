using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000312 RID: 786
	internal class DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x000A2B4C File Offset: 0x000A0D4C
		// (set) Token: 0x060022FC RID: 8956 RVA: 0x000A2B54 File Offset: 0x000A0D54
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._poolGroup;
			}
			set
			{
				this._poolGroup = value;
			}
		}

		// Token: 0x0400172C RID: 5932
		private DbConnectionPoolGroup _poolGroup;
	}
}
