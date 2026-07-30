using System;

namespace System.Data
{
	/// <summary>Indicates the action that occurs when a <see cref="T:System.Data.ForeignKeyConstraint" /> is enforced.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E8 RID: 232
	public enum Rule
	{
		/// <summary>No action taken on related rows.</summary>
		// Token: 0x0400083A RID: 2106
		None,
		/// <summary>Delete or update related rows. This is the default.</summary>
		// Token: 0x0400083B RID: 2107
		Cascade,
		/// <summary>Set values in related rows to DBNull.</summary>
		// Token: 0x0400083C RID: 2108
		SetNull,
		/// <summary>Set values in related rows to the value contained in the <see cref="P:System.Data.DataColumn.DefaultValue" /> property.</summary>
		// Token: 0x0400083D RID: 2109
		SetDefault
	}
}
