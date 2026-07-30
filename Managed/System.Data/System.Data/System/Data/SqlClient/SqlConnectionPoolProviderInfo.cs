using System;
using System.Data.ProviderBase;

namespace System.Data.SqlClient
{
	// Token: 0x02000191 RID: 401
	internal sealed class SqlConnectionPoolProviderInfo : DbConnectionPoolProviderInfo
	{
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0005DA80 File Offset: 0x0005BC80
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x0005DA88 File Offset: 0x0005BC88
		internal string InstanceName
		{
			get
			{
				return this._instanceName;
			}
			set
			{
				this._instanceName = value;
			}
		}

		// Token: 0x04000C3B RID: 3131
		private string _instanceName;
	}
}
