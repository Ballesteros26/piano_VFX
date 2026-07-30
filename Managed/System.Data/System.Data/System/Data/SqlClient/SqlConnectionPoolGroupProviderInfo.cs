using System;
using System.Data.ProviderBase;

namespace System.Data.SqlClient
{
	// Token: 0x0200018F RID: 399
	internal sealed class SqlConnectionPoolGroupProviderInfo : DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x060012C5 RID: 4805 RVA: 0x0005D8CF File Offset: 0x0005BACF
		internal SqlConnectionPoolGroupProviderInfo(SqlConnectionString connectionOptions)
		{
			this._failoverPartner = connectionOptions.FailoverPartner;
			if (string.IsNullOrEmpty(this._failoverPartner))
			{
				this._failoverPartner = null;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0005D8F7 File Offset: 0x0005BAF7
		internal string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0005D8FF File Offset: 0x0005BAFF
		internal bool UseFailoverPartner
		{
			get
			{
				return this._useFailoverPartner;
			}
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0005D908 File Offset: 0x0005BB08
		internal void AliasCheck(string server)
		{
			if (this._alias != server)
			{
				lock (this)
				{
					if (this._alias == null)
					{
						this._alias = server;
					}
					else if (this._alias != server)
					{
						base.PoolGroup.Clear();
						this._alias = server;
					}
				}
			}
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0005D980 File Offset: 0x0005BB80
		internal void FailoverCheck(SqlInternalConnection connection, bool actualUseFailoverPartner, SqlConnectionString userConnectionOptions, string actualFailoverPartner)
		{
			if (this.UseFailoverPartner != actualUseFailoverPartner)
			{
				base.PoolGroup.Clear();
				this._useFailoverPartner = actualUseFailoverPartner;
			}
			if (!this._useFailoverPartner && this._failoverPartner != actualFailoverPartner)
			{
				lock (this)
				{
					if (this._failoverPartner != actualFailoverPartner)
					{
						this._failoverPartner = actualFailoverPartner;
					}
				}
			}
		}

		// Token: 0x04000C37 RID: 3127
		private string _alias;

		// Token: 0x04000C38 RID: 3128
		private string _failoverPartner;

		// Token: 0x04000C39 RID: 3129
		private bool _useFailoverPartner;
	}
}
