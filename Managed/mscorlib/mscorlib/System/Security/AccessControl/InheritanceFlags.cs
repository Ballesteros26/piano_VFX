using System;

namespace System.Security.AccessControl
{
	/// <summary>Inheritance flags specify the semantics of inheritance for access control entries (ACEs).</summary>
	// Token: 0x020005F2 RID: 1522
	[Flags]
	public enum InheritanceFlags
	{
		/// <summary>The ACE is not inherited by child objects.</summary>
		// Token: 0x040021BF RID: 8639
		None = 0,
		/// <summary>The ACE is inherited by child container objects.</summary>
		// Token: 0x040021C0 RID: 8640
		ContainerInherit = 1,
		/// <summary>The ACE is inherited by child leaf objects.</summary>
		// Token: 0x040021C1 RID: 8641
		ObjectInherit = 2
	}
}
