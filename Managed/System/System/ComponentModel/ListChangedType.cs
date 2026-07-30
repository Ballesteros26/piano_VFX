using System;

namespace System.ComponentModel
{
	/// <summary>Specifies how the list changed.</summary>
	// Token: 0x020002A8 RID: 680
	public enum ListChangedType
	{
		/// <summary>Much of the list has changed. Any listening controls should refresh all their data from the list.</summary>
		// Token: 0x04001314 RID: 4884
		Reset,
		/// <summary>An item added to the list. <see cref="P:System.ComponentModel.ListChangedEventArgs.NewIndex" /> contains the index of the item that was added.</summary>
		// Token: 0x04001315 RID: 4885
		ItemAdded,
		/// <summary>An item deleted from the list. <see cref="P:System.ComponentModel.ListChangedEventArgs.NewIndex" /> contains the index of the item that was deleted.</summary>
		// Token: 0x04001316 RID: 4886
		ItemDeleted,
		/// <summary>An item moved within the list. <see cref="P:System.ComponentModel.ListChangedEventArgs.OldIndex" /> contains the previous index for the item, whereas <see cref="P:System.ComponentModel.ListChangedEventArgs.NewIndex" /> contains the new index for the item.</summary>
		// Token: 0x04001317 RID: 4887
		ItemMoved,
		/// <summary>An item changed in the list. <see cref="P:System.ComponentModel.ListChangedEventArgs.NewIndex" /> contains the index of the item that was changed.</summary>
		// Token: 0x04001318 RID: 4888
		ItemChanged,
		/// <summary>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> was added, which changed the schema.</summary>
		// Token: 0x04001319 RID: 4889
		PropertyDescriptorAdded,
		/// <summary>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> was deleted, which changed the schema.</summary>
		// Token: 0x0400131A RID: 4890
		PropertyDescriptorDeleted,
		/// <summary>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> was changed, which changed the schema.</summary>
		// Token: 0x0400131B RID: 4891
		PropertyDescriptorChanged
	}
}
