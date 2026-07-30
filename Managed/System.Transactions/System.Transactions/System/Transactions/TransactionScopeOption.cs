using System;

namespace System.Transactions
{
	/// <summary>Provides additional options for creating a transaction scope.</summary>
	// Token: 0x0200002B RID: 43
	public enum TransactionScopeOption
	{
		/// <summary>A transaction is required by the scope. It uses an ambient transaction if one already exists. Otherwise, it creates a new transaction before entering the scope. This is the default value.</summary>
		// Token: 0x04000074 RID: 116
		Required,
		/// <summary>A new transaction is always created for the scope.</summary>
		// Token: 0x04000075 RID: 117
		RequiresNew,
		/// <summary>The ambient transaction context is suppressed when creating the scope. All operations within the scope are done without an ambient transaction context.</summary>
		// Token: 0x04000076 RID: 118
		Suppress
	}
}
