using System;

namespace System.Transactions
{
	/// <summary>Facilitates communication between an enlisted transaction participant and the transaction manager during the final phase of the transaction.</summary>
	// Token: 0x02000010 RID: 16
	public class Enlistment
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000021C2 File Offset: 0x000003C2
		internal Enlistment()
		{
			this.done = false;
		}

		/// <summary>Indicates that the transaction participant has completed its work.</summary>
		// Token: 0x0600002A RID: 42 RVA: 0x000021D1 File Offset: 0x000003D1
		public void Done()
		{
			this.done = true;
			this.InternalOnDone();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000021E0 File Offset: 0x000003E0
		internal virtual void InternalOnDone()
		{
		}

		// Token: 0x04000033 RID: 51
		internal bool done;
	}
}
