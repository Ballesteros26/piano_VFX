using System;

namespace System.Threading
{
	/// <summary>Specifies whether a lock can be entered multiple times by the same thread.</summary>
	// Token: 0x02000023 RID: 35
	public enum LockRecursionPolicy
	{
		/// <summary>If a thread tries to enter a lock recursively, an exception is thrown. Some classes may allow certain recursions when this setting is in effect. </summary>
		// Token: 0x040001CE RID: 462
		NoRecursion,
		/// <summary>A thread can enter a lock recursively. Some classes may restrict this capability. </summary>
		// Token: 0x040001CF RID: 463
		SupportsRecursion
	}
}
