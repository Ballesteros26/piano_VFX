using System;

namespace System.Runtime.Versioning
{
	/// <summary>Identifies the scope of a sharable resource.</summary>
	// Token: 0x020006C2 RID: 1730
	[Flags]
	public enum ResourceScope
	{
		/// <summary>There is no shared state.</summary>
		// Token: 0x0400268B RID: 9867
		None = 0,
		/// <summary>The state is shared by objects within the machine.</summary>
		// Token: 0x0400268C RID: 9868
		Machine = 1,
		/// <summary>The state is shared within a process.</summary>
		// Token: 0x0400268D RID: 9869
		Process = 2,
		/// <summary>The state is shared by objects within an <see cref="T:System.AppDomain" />.</summary>
		// Token: 0x0400268E RID: 9870
		AppDomain = 4,
		/// <summary>The state is shared by objects within a library.</summary>
		// Token: 0x0400268F RID: 9871
		Library = 8,
		/// <summary>The resource is visible to only the type.</summary>
		// Token: 0x04002690 RID: 9872
		Private = 16,
		/// <summary>The resource is visible at an assembly scope.</summary>
		// Token: 0x04002691 RID: 9873
		Assembly = 32
	}
}
