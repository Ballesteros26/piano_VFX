using System;

namespace System.Data
{
	/// <summary>Determines the action that occurs when a mapping is missing from a source table or a source column.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D7 RID: 215
	public enum MissingMappingAction
	{
		/// <summary>The source column or source table is created and added to the <see cref="T:System.Data.DataSet" /> using its original name.</summary>
		// Token: 0x040007E6 RID: 2022
		Passthrough = 1,
		/// <summary>The column or table not having a mapping is ignored. Returns null.</summary>
		// Token: 0x040007E7 RID: 2023
		Ignore,
		/// <summary>An <see cref="T:System.InvalidOperationException" /> is generated if the specified column mapping is missing.</summary>
		// Token: 0x040007E8 RID: 2024
		Error
	}
}
