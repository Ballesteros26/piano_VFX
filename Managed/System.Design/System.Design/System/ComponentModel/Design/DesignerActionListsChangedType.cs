using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies the type of change occurring in a collection of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects.</summary>
	// Token: 0x02000114 RID: 276
	[ComVisible(true)]
	public enum DesignerActionListsChangedType
	{
		/// <summary>One or more <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects have been added to the collection.</summary>
		// Token: 0x040001B8 RID: 440
		ActionListsAdded,
		/// <summary>One or more <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects have been removed from the collection.</summary>
		// Token: 0x040001B9 RID: 441
		ActionListsRemoved
	}
}
