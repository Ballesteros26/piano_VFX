using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.ProviderBase
{
	// Token: 0x02000304 RID: 772
	internal sealed class DbConnectionClosedConnecting : DbConnectionBusy
	{
		// Token: 0x06002242 RID: 8770 RVA: 0x000A0193 File Offset: 0x0009E393
		private DbConnectionClosedConnecting()
			: base(ConnectionState.Connecting)
		{
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000A019C File Offset: 0x0009E39C
		internal override void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
			connectionFactory.SetInnerConnectionTo(owningObject, DbConnectionClosedPreviouslyOpened.SingletonInstance);
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000A01AA File Offset: 0x0009E3AA
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return this.TryOpenConnection(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000A01B8 File Offset: 0x0009E3B8
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			if (retry == null || !retry.Task.IsCompleted)
			{
				throw ADP.ConnectionAlreadyOpen(base.State);
			}
			DbConnectionInternal result = retry.Task.Result;
			if (result == null)
			{
				connectionFactory.SetInnerConnectionTo(outerConnection, this);
				throw ADP.InternalConnectionError(ADP.ConnectionError.GetConnectionReturnsNull);
			}
			connectionFactory.SetInnerConnectionEvent(outerConnection, result);
			return true;
		}

		// Token: 0x040016CF RID: 5839
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedConnecting();
	}
}
