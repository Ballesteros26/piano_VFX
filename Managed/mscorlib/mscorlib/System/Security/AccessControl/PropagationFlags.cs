using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies how Access Control Entries (ACEs) are propagated to child objects.  These flags are significant only if inheritance flags are present. </summary>
	// Token: 0x02000607 RID: 1543
	[Flags]
	public enum PropagationFlags
	{
		/// <summary>Specifies that no inheritance flags are set.</summary>
		// Token: 0x040021E5 RID: 8677
		None = 0,
		/// <summary>Specifies that the ACE is not propagated to child objects.</summary>
		// Token: 0x040021E6 RID: 8678
		NoPropagateInherit = 1,
		/// <summary>Specifies that the ACE is propagated only to child objects. This includes both container and leaf child objects. </summary>
		// Token: 0x040021E7 RID: 8679
		InheritOnly = 2
	}
}
