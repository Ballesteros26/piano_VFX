using System;

namespace System.ComponentModel
{
	/// <summary>Specifies how the collection is changed.</summary>
	// Token: 0x0200023F RID: 575
	public enum CollectionChangeAction
	{
		/// <summary>Specifies that an element was added to the collection.</summary>
		// Token: 0x04001279 RID: 4729
		Add = 1,
		/// <summary>Specifies that an element was removed from the collection.</summary>
		// Token: 0x0400127A RID: 4730
		Remove,
		/// <summary>Specifies that the entire collection has changed. This is caused by using methods that manipulate the entire collection, such as <see cref="M:System.Collections.CollectionBase.Clear" />.</summary>
		// Token: 0x0400127B RID: 4731
		Refresh
	}
}
