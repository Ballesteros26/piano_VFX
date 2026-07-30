using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the culture, case, and sort rules to be used by certain overloads of the <see cref="M:System.String.Compare(System.String,System.String)" /> and <see cref="M:System.String.Equals(System.Object)" /> methods.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000230 RID: 560
	[ComVisible(true)]
	[Serializable]
	public enum StringComparison
	{
		/// <summary>Compare strings using culture-sensitive sort rules and the current culture.</summary>
		// Token: 0x04000D21 RID: 3361
		CurrentCulture,
		/// <summary>Compare strings using culture-sensitive sort rules, the current culture, and ignoring the case of the strings being compared.</summary>
		// Token: 0x04000D22 RID: 3362
		CurrentCultureIgnoreCase,
		/// <summary>Compare strings using culture-sensitive sort rules and the invariant culture.</summary>
		// Token: 0x04000D23 RID: 3363
		InvariantCulture,
		/// <summary>Compare strings using culture-sensitive sort rules, the invariant culture, and ignoring the case of the strings being compared.</summary>
		// Token: 0x04000D24 RID: 3364
		InvariantCultureIgnoreCase,
		/// <summary>Compare strings using ordinal sort rules.</summary>
		// Token: 0x04000D25 RID: 3365
		Ordinal,
		/// <summary>Compare strings using ordinal sort rules and ignoring the case of the strings being compared.</summary>
		// Token: 0x04000D26 RID: 3366
		OrdinalIgnoreCase
	}
}
