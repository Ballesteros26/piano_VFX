using System;

namespace System.Data.Common
{
	/// <summary>The base class for a transaction. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000352 RID: 850
	public abstract class DbTransaction : MarshalByRefObject, IDbTransaction, IDisposable
	{
		/// <summary>Specifies the <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002852 RID: 10322 RVA: 0x000B13BF File Offset: 0x000AF5BF
		public DbConnection Connection
		{
			get
			{
				return this.DbConnection;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction, or a null reference if the transaction is no longer valid.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</returns>
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x000B13BF File Offset: 0x000AF5BF
		IDbConnection IDbTransaction.Connection
		{
			get
			{
				return this.DbConnection;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</returns>
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002854 RID: 10324
		protected abstract DbConnection DbConnection { get; }

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002855 RID: 10325
		public abstract IsolationLevel IsolationLevel { get; }

		/// <summary>Commits the database transaction.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002856 RID: 10326
		public abstract void Commit();

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbTransaction" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002857 RID: 10327 RVA: 0x000B13C7 File Offset: 0x000AF5C7
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbTransaction" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">If true, this method releases all resources held by any managed objects that this <see cref="T:System.Data.Common.DbTransaction" /> references.</param>
		// Token: 0x06002858 RID: 10328 RVA: 0x00005E03 File Offset: 0x00004003
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002859 RID: 10329
		public abstract void Rollback();
	}
}
