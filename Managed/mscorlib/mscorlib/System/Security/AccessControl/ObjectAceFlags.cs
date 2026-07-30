using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the presence of object types for Access Control Entries (ACEs).</summary>
	// Token: 0x02000602 RID: 1538
	[Flags]
	public enum ObjectAceFlags
	{
		/// <summary>No object types are present.</summary>
		// Token: 0x040021DC RID: 8668
		None = 0,
		/// <summary>The type of object that is associated with the ACE is present.</summary>
		// Token: 0x040021DD RID: 8669
		ObjectAceTypePresent = 1,
		/// <summary>The type of object that can inherit the ACE.</summary>
		// Token: 0x040021DE RID: 8670
		InheritedObjectAceTypePresent = 2
	}
}
