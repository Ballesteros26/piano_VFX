using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Transactions;

namespace System.Data.Odbc
{
	// Token: 0x020002B6 RID: 694
	internal sealed class OdbcConnectionOpen : DbConnectionInternal
	{
		// Token: 0x06001D9A RID: 7578 RVA: 0x000921F8 File Offset: 0x000903F8
		internal OdbcConnectionOpen(OdbcConnection outerConnection, OdbcConnectionString connectionOptions)
		{
			OdbcEnvironmentHandle globalEnvironmentHandle = OdbcEnvironment.GetGlobalEnvironmentHandle();
			outerConnection.ConnectionHandle = new OdbcConnectionHandle(outerConnection, connectionOptions, globalEnvironmentHandle);
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x00092220 File Offset: 0x00090420
		internal OdbcConnection OuterConnection
		{
			get
			{
				OdbcConnection odbcConnection = (OdbcConnection)base.Owner;
				if (odbcConnection == null)
				{
					throw ODBC.OpenConnectionNoOwner();
				}
				return odbcConnection;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x00092243 File Offset: 0x00090443
		public override string ServerVersion
		{
			get
			{
				return this.OuterConnection.Open_GetServerVersion();
			}
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00005E03 File Offset: 0x00004003
		protected override void Activate(Transaction transaction)
		{
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00092250 File Offset: 0x00090450
		public override DbTransaction BeginTransaction(IsolationLevel isolevel)
		{
			return this.BeginOdbcTransaction(isolevel);
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00092259 File Offset: 0x00090459
		internal OdbcTransaction BeginOdbcTransaction(IsolationLevel isolevel)
		{
			return this.OuterConnection.Open_BeginTransaction(isolevel);
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x00092267 File Offset: 0x00090467
		public override void ChangeDatabase(string value)
		{
			this.OuterConnection.Open_ChangeDatabase(value);
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x00092275 File Offset: 0x00090475
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			return new OdbcReferenceCollection();
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0009227C File Offset: 0x0009047C
		protected override void Deactivate()
		{
			base.NotifyWeakReference(0);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00005E03 File Offset: 0x00004003
		public override void EnlistTransaction(Transaction transaction)
		{
		}
	}
}
