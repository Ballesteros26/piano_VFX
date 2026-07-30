using System;

namespace System.Data
{
	/// <summary>Describes the version of a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000085 RID: 133
	public enum DataRowVersion
	{
		/// <summary>The row contains its original values.</summary>
		// Token: 0x040005A1 RID: 1441
		Original = 256,
		/// <summary>The row contains current values.</summary>
		// Token: 0x040005A2 RID: 1442
		Current = 512,
		/// <summary>The row contains a proposed value.</summary>
		// Token: 0x040005A3 RID: 1443
		Proposed = 1024,
		/// <summary>The default version of <see cref="T:System.Data.DataRowState" />. For a DataRowState value of Added, Modified or Deleted, the default version is Current. For a <see cref="T:System.Data.DataRowState" /> value of Detached, the version is Proposed.</summary>
		// Token: 0x040005A4 RID: 1444
		Default = 1536
	}
}
