using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies the preferred default binding for a dependent assembly.</summary>
	// Token: 0x02000863 RID: 2147
	[Serializable]
	public enum LoadHint
	{
		/// <summary>No preference specified.</summary>
		// Token: 0x04002BB4 RID: 11188
		Default,
		/// <summary>The dependency is always loaded.</summary>
		// Token: 0x04002BB5 RID: 11189
		Always,
		/// <summary>The dependency is sometimes loaded.</summary>
		// Token: 0x04002BB6 RID: 11190
		Sometimes
	}
}
