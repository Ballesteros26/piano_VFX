using System;

namespace System.Data
{
	/// <summary>Represents a transaction to be performed at a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CD RID: 205
	public interface IDbTransaction : IDisposable
	{
		/// <summary>Specifies the Connection object to associate with the transaction.</summary>
		/// <returns>The Connection object to associate with the transaction.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000BBC RID: 3004
		IDbConnection Connection { get; }

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction. The default is ReadCommitted.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000BBD RID: 3005
		IsolationLevel IsolationLevel { get; }

		/// <summary>Commits the database transaction.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BBE RID: 3006
		void Commit();

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BBF RID: 3007
		void Rollback();
	}
}
