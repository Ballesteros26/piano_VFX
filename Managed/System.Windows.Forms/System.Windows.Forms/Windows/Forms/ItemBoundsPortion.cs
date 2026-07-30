using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies a portion of the list view item from which to retrieve the bounding rectangle.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001EE RID: 494
	public enum ItemBoundsPortion
	{
		/// <summary>The bounding rectangle of the entire item, including the icon, the item text, and the subitem text (if displayed), should be retrieved.</summary>
		// Token: 0x04001041 RID: 4161
		Entire,
		/// <summary>The bounding rectangle of the icon or small icon should be retrieved.</summary>
		// Token: 0x04001042 RID: 4162
		Icon,
		/// <summary>The bounding rectangle of the item text should be retrieved.</summary>
		// Token: 0x04001043 RID: 4163
		Label,
		/// <summary>The bounding rectangle of the icon or small icon and the item text should be retrieved. In all views except the details view of the <see cref="T:System.Windows.Forms.ListView" />, this value specifies the same bounding rectangle as the Entire value. In details view, this value specifies the bounding rectangle specified by the Entire value without the subitems. If the <see cref="P:System.Windows.Forms.ListView.CheckBoxes" /> property is set to true, this property does not include the area of the check boxes in its bounding rectangle. To include the entire item, including the check boxes, use the Entire value when calling the <see cref="M:System.Windows.Forms.ListView.GetItemRect(System.Int32)" /> method.</summary>
		// Token: 0x04001044 RID: 4164
		ItemOnly
	}
}
