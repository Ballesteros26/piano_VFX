using System;

namespace System.Data
{
	/// <summary>Identifies a list of connection string parameters identified by the KeyRestrictions property that are either allowed or not allowed.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D1 RID: 209
	public enum KeyRestrictionBehavior
	{
		/// <summary>Default. Identifies the only additional connection string parameters that are allowed.</summary>
		// Token: 0x040007D2 RID: 2002
		AllowOnly,
		/// <summary>Identifies additional connection string parameters that are not allowed.</summary>
		// Token: 0x040007D3 RID: 2003
		PreventUsage
	}
}
