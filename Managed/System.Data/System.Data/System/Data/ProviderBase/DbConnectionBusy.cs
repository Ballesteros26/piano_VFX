using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.ProviderBase
{
	// Token: 0x02000301 RID: 769
	internal abstract class DbConnectionBusy : DbConnectionClosed
	{
		// Token: 0x0600223C RID: 8764 RVA: 0x000A0151 File Offset: 0x0009E351
		protected DbConnectionBusy(ConnectionState state)
			: base(state, true, false)
		{
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000A015C File Offset: 0x0009E35C
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			throw ADP.ConnectionAlreadyOpen(base.State);
		}
	}
}
