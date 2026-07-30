using System;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies methods for the designer host to report on the state of transactions.</summary>
	// Token: 0x0200032B RID: 811
	public interface IDesignerHostTransactionState
	{
		/// <summary>Gets a value indicating whether the designer host is closing a transaction. </summary>
		/// <returns>true if the designer is closing a transaction; otherwise, false. </returns>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060019C4 RID: 6596
		bool IsClosingTransaction { get; }
	}
}
