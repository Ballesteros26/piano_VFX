using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Transactions
{
	/// <summary>Represents a transaction.</summary>
	// Token: 0x0200001D RID: 29
	[Serializable]
	public class Transaction : IDisposable, ISerializable
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000022D3 File Offset: 0x000004D3
		internal List<IEnlistmentNotification> Volatiles
		{
			get
			{
				if (this.volatiles == null)
				{
					this.volatiles = new List<IEnlistmentNotification>();
				}
				return this.volatiles;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000022EE File Offset: 0x000004EE
		internal List<ISinglePhaseNotification> Durables
		{
			get
			{
				if (this.durables == null)
				{
					this.durables = new List<ISinglePhaseNotification>();
				}
				return this.durables;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002309 File Offset: 0x00000509
		internal IPromotableSinglePhaseNotification Pspe
		{
			get
			{
				return this.pspe;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002311 File Offset: 0x00000511
		internal Transaction()
		{
			this.info = new TransactionInformation();
			this.level = IsolationLevel.Serializable;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002344 File Offset: 0x00000544
		internal Transaction(Transaction other)
		{
			this.level = other.level;
			this.info = other.info;
			this.dependents = other.dependents;
			this.volatiles = other.Volatiles;
			this.durables = other.Durables;
			this.pspe = other.Pspe;
		}

		/// <summary>Gets a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize this transaction. </summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" /> ) for this serialization. </param>
		// Token: 0x06000052 RID: 82 RVA: 0x00002162 File Offset: 0x00000362
		[MonoTODO]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates that the transaction is completed.</summary>
		/// <exception cref="T:System.ObjectDisposedException">An attempt to subscribe this event on a transaction that has been disposed. </exception>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000053 RID: 83 RVA: 0x000023B8 File Offset: 0x000005B8
		// (remove) Token: 0x06000054 RID: 84 RVA: 0x000023F0 File Offset: 0x000005F0
		public event TransactionCompletedEventHandler TransactionCompleted;

		/// <summary>Gets or sets the ambient transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> that describes the current transaction.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002425 File Offset: 0x00000625
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002431 File Offset: 0x00000631
		public static Transaction Current
		{
			get
			{
				Transaction.EnsureIncompleteCurrentScope();
				return Transaction.CurrentInternal;
			}
			set
			{
				Transaction.EnsureIncompleteCurrentScope();
				Transaction.CurrentInternal = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000243E File Offset: 0x0000063E
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002445 File Offset: 0x00000645
		internal static Transaction CurrentInternal
		{
			get
			{
				return Transaction.ambient;
			}
			set
			{
				Transaction.ambient = value;
			}
		}

		/// <summary>Gets the isolation level of the transaction.</summary>
		/// <returns>One of the <see cref="T:System.Transactions.IsolationLevel" /> values that indicates the isolation level of the transaction.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000244D File Offset: 0x0000064D
		public IsolationLevel IsolationLevel
		{
			get
			{
				Transaction.EnsureIncompleteCurrentScope();
				return this.level;
			}
		}

		/// <summary>Retrieves additional information about a transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.TransactionInformation" /> that contains additional information about the transaction.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000245A File Offset: 0x0000065A
		public TransactionInformation TransactionInformation
		{
			get
			{
				Transaction.EnsureIncompleteCurrentScope();
				return this.info;
			}
		}

		/// <summary>Creates a clone of the transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> that is a copy of the current transaction object.</returns>
		// Token: 0x0600005B RID: 91 RVA: 0x00002467 File Offset: 0x00000667
		public Transaction Clone()
		{
			return new Transaction(this);
		}

		/// <summary>Releases the resources that are held by the object.</summary>
		// Token: 0x0600005C RID: 92 RVA: 0x0000246F File Offset: 0x0000066F
		public void Dispose()
		{
			if (this.TransactionInformation.Status == TransactionStatus.Active)
			{
				this.Rollback();
			}
		}

		/// <summary>Creates a dependent clone of the transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.DependentTransaction" /> that represents the dependent clone.</returns>
		/// <param name="cloneOption">A <see cref="T:System.Transactions.DependentCloneOption" /> that controls what kind of dependent transaction to create.</param>
		// Token: 0x0600005D RID: 93 RVA: 0x00002484 File Offset: 0x00000684
		[MonoTODO]
		public DependentTransaction DependentClone(DependentCloneOption cloneOption)
		{
			DependentTransaction dependentTransaction = new DependentTransaction(this, cloneOption);
			this.dependents.Add(dependentTransaction);
			return dependentTransaction;
		}

		/// <summary>Enlists a durable resource manager that supports two phase commit to participate in a transaction.</summary>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> object that describes the enlistment.</returns>
		/// <param name="resourceManagerIdentifier">A unique identifier for a resource manager, which should persist across resource manager failure or reboot.</param>
		/// <param name="enlistmentNotification">An object that implements the <see cref="T:System.Transactions.IEnlistmentNotification" /> interface to receive two phase commit notifications. </param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		// Token: 0x0600005E RID: 94 RVA: 0x000024A7 File Offset: 0x000006A7
		[MonoTODO("Only SinglePhase commit supported for durable resource managers.")]
		[PermissionSet(SecurityAction.LinkDemand)]
		public Enlistment EnlistDurable(Guid resourceManagerIdentifier, IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions)
		{
			throw new NotImplementedException("DTC unsupported, only SinglePhase commit supported for durable resource managers.");
		}

		/// <summary>Enlists a durable resource manager that supports single phase commit optimization to participate in a transaction.</summary>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> object that describes the enlistment.</returns>
		/// <param name="resourceManagerIdentifier">A unique identifier for a resource manager, which should persist across resource manager failure or reboot.</param>
		/// <param name="singlePhaseNotification">An object that implements the <see cref="T:System.Transactions.ISinglePhaseNotification" /> interface that must be able to receive single phase commit and two phase commit notifications.</param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		// Token: 0x0600005F RID: 95 RVA: 0x000024B4 File Offset: 0x000006B4
		[MonoTODO("Only Local Transaction Manager supported. Cannot have more than 1 durable resource per transaction. Only EnlistmentOptions.None supported yet.")]
		[PermissionSet(SecurityAction.LinkDemand)]
		public Enlistment EnlistDurable(Guid resourceManagerIdentifier, ISinglePhaseNotification singlePhaseNotification, EnlistmentOptions enlistmentOptions)
		{
			Transaction.EnsureIncompleteCurrentScope();
			if (this.pspe != null || this.Durables.Count > 0)
			{
				throw new NotImplementedException("DTC unsupported, multiple durable resource managers aren't supported.");
			}
			if (enlistmentOptions != EnlistmentOptions.None)
			{
				throw new NotImplementedException("EnlistmentOptions other than None aren't supported");
			}
			this.Durables.Add(singlePhaseNotification);
			return new Enlistment();
		}

		/// <summary>Enlists a resource manager that has an internal transaction using a promotable single phase enlistment (PSPE). </summary>
		/// <returns>A <see cref="T:System.Transactions.SinglePhaseEnlistment" /> interface implementation that describes the enlistment.</returns>
		/// <param name="promotableSinglePhaseNotification">A <see cref="T:System.Transactions.IPromotableSinglePhaseNotification" /> interface implemented by the participant.</param>
		// Token: 0x06000060 RID: 96 RVA: 0x00002506 File Offset: 0x00000706
		public bool EnlistPromotableSinglePhase(IPromotableSinglePhaseNotification promotableSinglePhaseNotification)
		{
			Transaction.EnsureIncompleteCurrentScope();
			if (this.pspe != null || this.Durables.Count > 0)
			{
				return false;
			}
			this.pspe = promotableSinglePhaseNotification;
			this.pspe.Initialize();
			return true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002162 File Offset: 0x00000362
		public void SetDistributedTransactionIdentifier(IPromotableSinglePhaseNotification promotableNotification, Guid distributedTransactionIdentifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002162 File Offset: 0x00000362
		public bool EnlistPromotableSinglePhase(IPromotableSinglePhaseNotification promotableSinglePhaseNotification, Guid promoterType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002162 File Offset: 0x00000362
		public byte[] GetPromotedToken()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002162 File Offset: 0x00000362
		public Guid PromoterType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Enlists a volatile resource manager that supports two phase commit to participate in a transaction.</summary>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> object that describes the enlistment.</returns>
		/// <param name="enlistmentNotification">An object that implements the <see cref="T:System.Transactions.IEnlistmentNotification" /> interface to receive two phase commit notifications. </param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		// Token: 0x06000065 RID: 101 RVA: 0x00002538 File Offset: 0x00000738
		[MonoTODO("EnlistmentOptions being ignored")]
		public Enlistment EnlistVolatile(IEnlistmentNotification enlistmentNotification, EnlistmentOptions enlistmentOptions)
		{
			return this.EnlistVolatileInternal(enlistmentNotification, enlistmentOptions);
		}

		/// <summary>Enlists a volatile resource manager that supports single phase commit optimization to participate in a transaction.</summary>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> object that describes the enlistment.</returns>
		/// <param name="singlePhaseNotification">An object that implements the <see cref="T:System.Transactions.ISinglePhaseNotification" /> interface that must be able to receive single phase commit and two phase commit notifications.</param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		// Token: 0x06000066 RID: 102 RVA: 0x00002538 File Offset: 0x00000738
		[MonoTODO("EnlistmentOptions being ignored")]
		public Enlistment EnlistVolatile(ISinglePhaseNotification singlePhaseNotification, EnlistmentOptions enlistmentOptions)
		{
			return this.EnlistVolatileInternal(singlePhaseNotification, enlistmentOptions);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002542 File Offset: 0x00000742
		private Enlistment EnlistVolatileInternal(IEnlistmentNotification notification, EnlistmentOptions options)
		{
			Transaction.EnsureIncompleteCurrentScope();
			this.Volatiles.Add(notification);
			return new Enlistment();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000255A File Offset: 0x0000075A
		[MonoTODO("Only Local Transaction Manager supported. Cannot have more than 1 durable resource per transaction.")]
		[PermissionSet(SecurityAction.LinkDemand)]
		public Enlistment PromoteAndEnlistDurable(Guid manager, IPromotableSinglePhaseNotification promotableNotification, ISinglePhaseNotification notification, EnlistmentOptions options)
		{
			throw new NotImplementedException("DTC unsupported, multiple durable resource managers aren't supported.");
		}

		/// <summary>Determines whether this transaction and the specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> and this transaction are identical; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x06000069 RID: 105 RVA: 0x00002566 File Offset: 0x00000766
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Transaction);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002574 File Offset: 0x00000774
		private bool Equals(Transaction t)
		{
			return t == this || (t != null && this.level == t.level && this.info == t.info);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Transactions.Transaction" /> instances are equivalent.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> are equal; otherwise, false.</returns>
		/// <param name="x">The <see cref="T:System.Transactions.Transaction" /> instance that is to the left of the equality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.Transaction" /> instance that is to the right of the equality operator.</param>
		// Token: 0x0600006B RID: 107 RVA: 0x0000259F File Offset: 0x0000079F
		public static bool operator ==(Transaction x, Transaction y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x.Equals(y);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Transactions.Transaction" /> instances are not equal.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> are not equal; otherwise, false.</returns>
		/// <param name="x">The <see cref="T:System.Transactions.Transaction" /> instance that is to the left of the inequality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.Transaction" /> instance that is to the right of the inequality operator.</param>
		// Token: 0x0600006C RID: 108 RVA: 0x000025B0 File Offset: 0x000007B0
		public static bool operator !=(Transaction x, Transaction y)
		{
			return !(x == y);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600006D RID: 109 RVA: 0x000025BC File Offset: 0x000007BC
		public override int GetHashCode()
		{
			return (int)(this.level ^ (IsolationLevel)this.info.GetHashCode() ^ (IsolationLevel)this.dependents.GetHashCode());
		}

		/// <summary>Rolls back (aborts) the transaction.</summary>
		// Token: 0x0600006E RID: 110 RVA: 0x000025DC File Offset: 0x000007DC
		public void Rollback()
		{
			this.Rollback(null);
		}

		/// <summary>Rolls back (aborts) the transaction.</summary>
		/// <param name="e">An explanation of why a rollback occurred.</param>
		// Token: 0x0600006F RID: 111 RVA: 0x000025E5 File Offset: 0x000007E5
		public void Rollback(Exception e)
		{
			Transaction.EnsureIncompleteCurrentScope();
			this.Rollback(e, null);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000025F4 File Offset: 0x000007F4
		internal void Rollback(Exception ex, object abortingEnlisted)
		{
			if (this.aborted)
			{
				this.FireCompleted();
				return;
			}
			if (this.info.Status == TransactionStatus.Committed)
			{
				throw new TransactionException("Transaction has already been committed. Cannot accept any new work.");
			}
			this.innerException = ex;
			SinglePhaseEnlistment singlePhaseEnlistment = new SinglePhaseEnlistment();
			foreach (IEnlistmentNotification enlistmentNotification in this.Volatiles)
			{
				if (enlistmentNotification != abortingEnlisted)
				{
					enlistmentNotification.Rollback(singlePhaseEnlistment);
				}
			}
			List<ISinglePhaseNotification> list = this.Durables;
			if (list.Count > 0 && list[0] != abortingEnlisted)
			{
				list[0].Rollback(singlePhaseEnlistment);
			}
			if (this.pspe != null && this.pspe != abortingEnlisted)
			{
				this.pspe.Rollback(singlePhaseEnlistment);
			}
			this.Aborted = true;
			this.FireCompleted();
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000026D4 File Offset: 0x000008D4
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000026DC File Offset: 0x000008DC
		private bool Aborted
		{
			get
			{
				return this.aborted;
			}
			set
			{
				this.aborted = value;
				if (this.aborted)
				{
					this.info.Status = TransactionStatus.Aborted;
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000026F9 File Offset: 0x000008F9
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002701 File Offset: 0x00000901
		internal TransactionScope Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000270C File Offset: 0x0000090C
		protected IAsyncResult BeginCommitInternal(AsyncCallback callback)
		{
			if (this.committed || this.committing)
			{
				throw new InvalidOperationException("Commit has already been called for this transaction.");
			}
			this.committing = true;
			this.asyncCommit = new Transaction.AsyncCommit(this.DoCommit);
			return this.asyncCommit.BeginInvoke(callback, null);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000275A File Offset: 0x0000095A
		protected void EndCommitInternal(IAsyncResult ar)
		{
			this.asyncCommit.EndInvoke(ar);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002768 File Offset: 0x00000968
		internal void CommitInternal()
		{
			if (this.committed || this.committing)
			{
				throw new InvalidOperationException("Commit has already been called for this transaction.");
			}
			this.committing = true;
			try
			{
				this.DoCommit();
			}
			catch (TransactionException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new TransactionAbortedException("Transaction failed", ex);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000027CC File Offset: 0x000009CC
		private void DoCommit()
		{
			if (this.Scope != null)
			{
				this.Rollback(null, null);
				this.CheckAborted();
			}
			List<IEnlistmentNotification> list = this.Volatiles;
			List<ISinglePhaseNotification> list2 = this.Durables;
			if (list.Count == 1 && list2.Count == 0)
			{
				ISinglePhaseNotification singlePhaseNotification = list[0] as ISinglePhaseNotification;
				if (singlePhaseNotification != null)
				{
					this.DoSingleCommit(singlePhaseNotification);
					this.Complete();
					return;
				}
			}
			if (list.Count > 0)
			{
				this.DoPreparePhase();
			}
			if (list2.Count > 0)
			{
				this.DoSingleCommit(list2[0]);
			}
			if (this.pspe != null)
			{
				this.DoSingleCommit(this.pspe);
			}
			if (list.Count > 0)
			{
				this.DoCommitPhase();
			}
			this.Complete();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000287A File Offset: 0x00000A7A
		private void Complete()
		{
			this.committing = false;
			this.committed = true;
			if (!this.aborted)
			{
				this.info.Status = TransactionStatus.Committed;
			}
			this.FireCompleted();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000028A4 File Offset: 0x00000AA4
		internal void InitScope(TransactionScope scope)
		{
			this.CheckAborted();
			if (this.committed)
			{
				throw new InvalidOperationException("Commit has already been called on this transaction.");
			}
			this.Scope = scope;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000028C8 File Offset: 0x00000AC8
		private static void PrepareCallbackWrapper(object state)
		{
			PreparingEnlistment preparingEnlistment = state as PreparingEnlistment;
			try
			{
				preparingEnlistment.EnlistmentNotification.Prepare(preparingEnlistment);
			}
			catch (Exception ex)
			{
				preparingEnlistment.Exception = ex;
				if (!preparingEnlistment.IsPrepared)
				{
					((ManualResetEvent)preparingEnlistment.WaitHandle).Set();
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002920 File Offset: 0x00000B20
		private void DoPreparePhase()
		{
			foreach (IEnlistmentNotification enlistmentNotification in this.Volatiles)
			{
				PreparingEnlistment preparingEnlistment = new PreparingEnlistment(this, enlistmentNotification);
				ThreadPool.QueueUserWorkItem(new WaitCallback(Transaction.PrepareCallbackWrapper), preparingEnlistment);
				TimeSpan timeSpan = ((this.Scope != null) ? this.Scope.Timeout : TransactionManager.DefaultTimeout);
				if (!preparingEnlistment.WaitHandle.WaitOne(timeSpan, true))
				{
					this.Aborted = true;
					throw new TimeoutException("Transaction timedout");
				}
				if (preparingEnlistment.Exception != null)
				{
					this.innerException = preparingEnlistment.Exception;
					this.Aborted = true;
					break;
				}
				if (!preparingEnlistment.IsPrepared)
				{
					this.Aborted = true;
					break;
				}
			}
			this.CheckAborted();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002A00 File Offset: 0x00000C00
		private void DoCommitPhase()
		{
			foreach (IEnlistmentNotification enlistmentNotification in this.Volatiles)
			{
				Enlistment enlistment = new Enlistment();
				enlistmentNotification.Commit(enlistment);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002A58 File Offset: 0x00000C58
		private void DoSingleCommit(ISinglePhaseNotification single)
		{
			if (single == null)
			{
				return;
			}
			single.SinglePhaseCommit(new SinglePhaseEnlistment(this, single));
			this.CheckAborted();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002A71 File Offset: 0x00000C71
		private void DoSingleCommit(IPromotableSinglePhaseNotification single)
		{
			if (single == null)
			{
				return;
			}
			single.SinglePhaseCommit(new SinglePhaseEnlistment(this, single));
			this.CheckAborted();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002A8A File Offset: 0x00000C8A
		private void CheckAborted()
		{
			if (this.aborted)
			{
				throw new TransactionAbortedException("Transaction has aborted", this.innerException);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002AA5 File Offset: 0x00000CA5
		private void FireCompleted()
		{
			if (this.TransactionCompleted != null)
			{
				this.TransactionCompleted(this, new TransactionEventArgs(this));
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002AC1 File Offset: 0x00000CC1
		private static void EnsureIncompleteCurrentScope()
		{
			if (Transaction.CurrentInternal == null)
			{
				return;
			}
			if (Transaction.CurrentInternal.Scope != null && Transaction.CurrentInternal.Scope.IsComplete)
			{
				throw new InvalidOperationException("The current TransactionScope is already complete");
			}
		}

		// Token: 0x0400004A RID: 74
		[ThreadStatic]
		private static Transaction ambient;

		// Token: 0x0400004B RID: 75
		private IsolationLevel level;

		// Token: 0x0400004C RID: 76
		private TransactionInformation info;

		// Token: 0x0400004D RID: 77
		private ArrayList dependents = new ArrayList();

		// Token: 0x0400004E RID: 78
		private List<IEnlistmentNotification> volatiles;

		// Token: 0x0400004F RID: 79
		private List<ISinglePhaseNotification> durables;

		// Token: 0x04000050 RID: 80
		private IPromotableSinglePhaseNotification pspe;

		// Token: 0x04000051 RID: 81
		private Transaction.AsyncCommit asyncCommit;

		// Token: 0x04000052 RID: 82
		private bool committing;

		// Token: 0x04000053 RID: 83
		private bool committed;

		// Token: 0x04000054 RID: 84
		private bool aborted;

		// Token: 0x04000055 RID: 85
		private TransactionScope scope;

		// Token: 0x04000056 RID: 86
		private Exception innerException;

		// Token: 0x04000057 RID: 87
		private Guid tag = Guid.NewGuid();

		// Token: 0x0200001E RID: 30
		// (Invoke) Token: 0x06000084 RID: 132
		private delegate void AsyncCommit();
	}
}
