using System;

namespace System.Transactions
{
	/// <summary>Makes a code block transactional. This class cannot be inherited.</summary>
	// Token: 0x02000029 RID: 41
	public sealed class TransactionScope : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class. </summary>
		// Token: 0x060000C1 RID: 193 RVA: 0x00002DF6 File Offset: 0x00000FF6
		public TransactionScope()
			: this(TransactionScopeOption.Required, TransactionManager.DefaultTimeout)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002E04 File Offset: 0x00001004
		public TransactionScope(TransactionScopeAsyncFlowOption asyncFlowOption)
			: this(TransactionScopeOption.Required, TransactionManager.DefaultTimeout, asyncFlowOption)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction. </summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		// Token: 0x060000C3 RID: 195 RVA: 0x00002E13 File Offset: 0x00001013
		public TransactionScope(Transaction transactionToUse)
			: this(transactionToUse, TransactionManager.DefaultTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction. </summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		// Token: 0x060000C4 RID: 196 RVA: 0x00002E21 File Offset: 0x00001021
		public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout)
			: this(transactionToUse, scopeTimeout, EnterpriseServicesInteropOption.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value and COM+ interoperability requirements, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction. </summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		/// <param name="interopOption">An instance of the <see cref="T:System.Transactions.EnterpriseServicesInteropOption" /> enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
		// Token: 0x060000C5 RID: 197 RVA: 0x00002E2C File Offset: 0x0000102C
		[MonoTODO("EnterpriseServicesInteropOption not supported.")]
		public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout, EnterpriseServicesInteropOption interopOption)
		{
			this.Initialize(TransactionScopeOption.Required, transactionToUse, TransactionScope.defaultOptions, interopOption, scopeTimeout, TransactionScopeAsyncFlowOption.Suppress);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		// Token: 0x060000C6 RID: 198 RVA: 0x00002E44 File Offset: 0x00001044
		public TransactionScope(TransactionScopeOption scopeOption)
			: this(scopeOption, TransactionManager.DefaultTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value and requirements.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		// Token: 0x060000C7 RID: 199 RVA: 0x00002E52 File Offset: 0x00001052
		public TransactionScope(TransactionScopeOption scopeOption, TimeSpan scopeTimeout)
			: this(scopeOption, scopeTimeout, TransactionScopeAsyncFlowOption.Suppress)
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00002E5D File Offset: 0x0000105D
		public TransactionScope(TransactionScopeOption option, TransactionScopeAsyncFlowOption asyncFlow)
			: this(option, TransactionManager.DefaultTimeout, asyncFlow)
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00002E6C File Offset: 0x0000106C
		public TransactionScope(TransactionScopeOption scopeOption, TimeSpan scopeTimeout, TransactionScopeAsyncFlowOption asyncFlow)
		{
			this.Initialize(scopeOption, null, TransactionScope.defaultOptions, EnterpriseServicesInteropOption.None, scopeTimeout, asyncFlow);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="transactionOptions">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
		// Token: 0x060000CA RID: 202 RVA: 0x00002E84 File Offset: 0x00001084
		public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions)
			: this(scopeOption, transactionOptions, EnterpriseServicesInteropOption.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified scope and COM+ interoperability requirements, and transaction options.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="transactionOptions">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
		/// <param name="interopOption">An instance of the <see cref="T:System.Transactions.EnterpriseServicesInteropOption" /> enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
		// Token: 0x060000CB RID: 203 RVA: 0x00002E8F File Offset: 0x0000108F
		[MonoTODO("EnterpriseServicesInteropOption not supported")]
		public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions, EnterpriseServicesInteropOption interopOption)
		{
			this.Initialize(scopeOption, null, transactionOptions, interopOption, TransactionManager.DefaultTimeout, TransactionScopeAsyncFlowOption.Suppress);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002EA7 File Offset: 0x000010A7
		public TransactionScope(Transaction transactionToUse, TransactionScopeAsyncFlowOption asyncFlowOption)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002EA7 File Offset: 0x000010A7
		public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout, TransactionScopeAsyncFlowOption asyncFlowOption)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002EA7 File Offset: 0x000010A7
		public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions, TransactionScopeAsyncFlowOption asyncFlowOption)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00002EB4 File Offset: 0x000010B4
		private void Initialize(TransactionScopeOption scopeOption, Transaction tx, TransactionOptions options, EnterpriseServicesInteropOption interop, TimeSpan scopeTimeout, TransactionScopeAsyncFlowOption asyncFlow)
		{
			this.completed = false;
			this.isRoot = false;
			this.nested = 0;
			this.asyncFlowEnabled = asyncFlow == TransactionScopeAsyncFlowOption.Enabled;
			if (scopeTimeout < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("scopeTimeout");
			}
			this.timeout = scopeTimeout;
			this.oldTransaction = Transaction.CurrentInternal;
			Transaction.CurrentInternal = (this.transaction = this.InitTransaction(tx, scopeOption));
			if (this.transaction != null)
			{
				this.transaction.InitScope(this);
			}
			if (this.parentScope != null)
			{
				this.parentScope.nested++;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002F58 File Offset: 0x00001158
		private Transaction InitTransaction(Transaction tx, TransactionScopeOption scopeOption)
		{
			if (tx != null)
			{
				return tx;
			}
			if (scopeOption == TransactionScopeOption.Suppress)
			{
				if (Transaction.CurrentInternal != null)
				{
					this.parentScope = Transaction.CurrentInternal.Scope;
				}
				return null;
			}
			if (scopeOption != TransactionScopeOption.Required)
			{
				if (Transaction.CurrentInternal != null)
				{
					this.parentScope = Transaction.CurrentInternal.Scope;
				}
				this.isRoot = true;
				return new Transaction();
			}
			if (Transaction.CurrentInternal == null)
			{
				this.isRoot = true;
				return new Transaction();
			}
			this.parentScope = Transaction.CurrentInternal.Scope;
			return Transaction.CurrentInternal;
		}

		/// <summary>Indicates that all operations within the scope are completed successfully.</summary>
		/// <exception cref="T:System.InvalidOperationException">This method has already been called once.</exception>
		// Token: 0x060000D1 RID: 209 RVA: 0x00002FEF File Offset: 0x000011EF
		public void Complete()
		{
			if (this.completed)
			{
				throw new InvalidOperationException("The current TransactionScope is already complete. You should dispose the TransactionScope.");
			}
			this.completed = true;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000300B File Offset: 0x0000120B
		internal bool IsComplete
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003013 File Offset: 0x00001213
		internal TimeSpan Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		/// <summary>Ends the transaction scope.</summary>
		// Token: 0x060000D4 RID: 212 RVA: 0x0000301C File Offset: 0x0000121C
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (this.parentScope != null)
			{
				this.parentScope.nested--;
			}
			if (this.nested > 0)
			{
				this.transaction.Rollback();
				throw new InvalidOperationException("TransactionScope nested incorrectly");
			}
			if (Transaction.CurrentInternal != this.transaction && !this.asyncFlowEnabled)
			{
				if (this.transaction != null)
				{
					this.transaction.Rollback();
				}
				if (Transaction.CurrentInternal != null)
				{
					Transaction.CurrentInternal.Rollback();
				}
				throw new InvalidOperationException("Transaction.Current has changed inside of the TransactionScope");
			}
			if (this.asyncFlowEnabled)
			{
				if (this.oldTransaction != null)
				{
					this.oldTransaction.Scope = this.parentScope;
				}
				Transaction currentInternal = Transaction.CurrentInternal;
				if (this.transaction == null && currentInternal == null)
				{
					return;
				}
				currentInternal.Scope = this.parentScope;
				Transaction.CurrentInternal = this.oldTransaction;
				this.transaction.Scope = null;
				if (!this.IsComplete)
				{
					this.transaction.Rollback();
					currentInternal.Rollback();
					return;
				}
				if (!this.isRoot)
				{
					return;
				}
				currentInternal.CommitInternal();
				this.transaction.CommitInternal();
				return;
			}
			else
			{
				if (Transaction.CurrentInternal == this.oldTransaction && this.oldTransaction != null)
				{
					this.oldTransaction.Scope = this.parentScope;
				}
				Transaction.CurrentInternal = this.oldTransaction;
				if (this.transaction == null)
				{
					return;
				}
				this.transaction.Scope = null;
				if (!this.IsComplete)
				{
					this.transaction.Rollback();
					return;
				}
				if (!this.isRoot)
				{
					return;
				}
				this.transaction.CommitInternal();
				return;
			}
		}

		// Token: 0x04000066 RID: 102
		private static TransactionOptions defaultOptions = new TransactionOptions(IsolationLevel.Serializable, TransactionManager.DefaultTimeout);

		// Token: 0x04000067 RID: 103
		private Transaction transaction;

		// Token: 0x04000068 RID: 104
		private Transaction oldTransaction;

		// Token: 0x04000069 RID: 105
		private TransactionScope parentScope;

		// Token: 0x0400006A RID: 106
		private TimeSpan timeout;

		// Token: 0x0400006B RID: 107
		private int nested;

		// Token: 0x0400006C RID: 108
		private bool disposed;

		// Token: 0x0400006D RID: 109
		private bool completed;

		// Token: 0x0400006E RID: 110
		private bool isRoot;

		// Token: 0x0400006F RID: 111
		private bool asyncFlowEnabled;
	}
}
