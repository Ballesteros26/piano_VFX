using System;

namespace System
{
	/// <summary>Specifies the behavior for a forced garbage collection.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000164 RID: 356
	[Serializable]
	public enum GCCollectionMode
	{
		/// <summary>The default setting for this enumeration, which is currently <see cref="F:System.GCCollectionMode.Forced" />. </summary>
		// Token: 0x04000918 RID: 2328
		Default,
		/// <summary>Forces the garbage collection to occur immediately.</summary>
		// Token: 0x04000919 RID: 2329
		Forced,
		/// <summary>Allows the garbage collector to determine whether the current time is optimal to reclaim objects. </summary>
		// Token: 0x0400091A RID: 2330
		Optimized
	}
}
