using System;
using System.Data.Common;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x02000300 RID: 768
	internal abstract class DbConnectionClosed : DbConnectionInternal
	{
		// Token: 0x06002232 RID: 8754 RVA: 0x000A013F File Offset: 0x0009E33F
		protected DbConnectionClosed(ConnectionState state, bool hidePassword, bool allowSetConnectionString)
			: base(state, hidePassword, allowSetConnectionString)
		{
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x000A014A File Offset: 0x0009E34A
		public override string ServerVersion
		{
			get
			{
				throw ADP.ClosedConnectionError();
			}
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000A014A File Offset: 0x0009E34A
		protected override void Activate(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x000A014A File Offset: 0x0009E34A
		public override DbTransaction BeginTransaction(IsolationLevel il)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000A014A File Offset: 0x0009E34A
		public override void ChangeDatabase(string database)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x00005E03 File Offset: 0x00004003
		internal override void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000A014A File Offset: 0x0009E34A
		protected override void Deactivate()
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000A014A File Offset: 0x0009E34A
		public override void EnlistTransaction(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000A014A File Offset: 0x0009E34A
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x0006BD01 File Offset: 0x00069F01
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return base.TryOpenConnectionInternal(outerConnection, connectionFactory, retry, userOptions);
		}
	}
}
