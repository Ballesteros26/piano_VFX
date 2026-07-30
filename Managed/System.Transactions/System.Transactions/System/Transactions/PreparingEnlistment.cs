using System;
using System.Threading;
using Unity;

namespace System.Transactions
{
	/// <summary>Facilitates communication between an enlisted transaction participant and the transaction manager during the Prepare phase of the transaction.</summary>
	// Token: 0x0200001A RID: 26
	public class PreparingEnlistment : Enlistment
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000021E2 File Offset: 0x000003E2
		internal PreparingEnlistment(Transaction tx, IEnlistmentNotification enlisted)
		{
			this.tx = tx;
			this.enlisted = enlisted;
			this.waitHandle = new ManualResetEvent(false);
		}

		/// <summary>Indicates that the transaction should be rolled back.</summary>
		// Token: 0x0600003A RID: 58 RVA: 0x00002204 File Offset: 0x00000404
		public void ForceRollback()
		{
			this.ForceRollback(null);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000220D File Offset: 0x0000040D
		internal override void InternalOnDone()
		{
			this.Prepared();
		}

		/// <summary>Indicates that the transaction should be rolled back.</summary>
		/// <param name="e">An explanation of why a rollback is triggered.</param>
		// Token: 0x0600003C RID: 60 RVA: 0x00002215 File Offset: 0x00000415
		[MonoTODO]
		public void ForceRollback(Exception e)
		{
			this.tx.Rollback(e, this.enlisted);
			((ManualResetEvent)this.waitHandle).Set();
		}

		/// <summary>Indicates that the transaction can be committed.</summary>
		// Token: 0x0600003D RID: 61 RVA: 0x0000223A File Offset: 0x0000043A
		[MonoTODO]
		public void Prepared()
		{
			this.prepared = true;
			((ManualResetEvent)this.waitHandle).Set();
		}

		/// <summary>Gets the recovery information of an enlistment.</summary>
		/// <returns>The recovery information of an enlistment.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt to get recovery information inside a volatile enlistment, which does not generate any recovery information.</exception>
		// Token: 0x0600003E RID: 62 RVA: 0x00002162 File Offset: 0x00000362
		[MonoTODO]
		public byte[] RecoveryInformation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002254 File Offset: 0x00000454
		internal bool IsPrepared
		{
			get
			{
				return this.prepared;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000225C File Offset: 0x0000045C
		internal WaitHandle WaitHandle
		{
			get
			{
				return this.waitHandle;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002264 File Offset: 0x00000464
		internal IEnlistmentNotification EnlistmentNotification
		{
			get
			{
				return this.enlisted;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000226C File Offset: 0x0000046C
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002274 File Offset: 0x00000474
		internal Exception Exception
		{
			get
			{
				return this.ex;
			}
			set
			{
				this.ex = value;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000021BB File Offset: 0x000003BB
		internal PreparingEnlistment()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000043 RID: 67
		private bool prepared;

		// Token: 0x04000044 RID: 68
		private Transaction tx;

		// Token: 0x04000045 RID: 69
		private IEnlistmentNotification enlisted;

		// Token: 0x04000046 RID: 70
		private WaitHandle waitHandle;

		// Token: 0x04000047 RID: 71
		private Exception ex;
	}
}
