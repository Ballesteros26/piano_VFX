using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers for types of inheritance levels.</summary>
	// Token: 0x020002F8 RID: 760
	public enum InheritanceLevel
	{
		/// <summary>The object is inherited.</summary>
		// Token: 0x0400142C RID: 5164
		Inherited = 1,
		/// <summary>The object is inherited, but has read-only access.</summary>
		// Token: 0x0400142D RID: 5165
		InheritedReadOnly,
		/// <summary>The object is not inherited.</summary>
		// Token: 0x0400142E RID: 5166
		NotInherited
	}
}
