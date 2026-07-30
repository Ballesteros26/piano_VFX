using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.ProviderBase
{
	// Token: 0x02000306 RID: 774
	internal sealed class DbConnectionClosedPreviouslyOpened : DbConnectionClosed
	{
		// Token: 0x06002249 RID: 8777 RVA: 0x000A022B File Offset: 0x0009E42B
		private DbConnectionClosedPreviouslyOpened()
			: base(ConnectionState.Closed, true, true)
		{
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x000A01AA File Offset: 0x0009E3AA
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return this.TryOpenConnection(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x040016D1 RID: 5841
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedPreviouslyOpened();
	}
}
