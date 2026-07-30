using System;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a static text item on a smart tag panel.</summary>
	// Token: 0x02000118 RID: 280
	public class DesignerActionTextItem : DesignerActionItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionTextItem" /> class.</summary>
		/// <param name="displayName">The panel text for this item.</param>
		/// <param name="category">The category used to group similar items on the panel.</param>
		// Token: 0x0600082E RID: 2094 RVA: 0x0000D98C File Offset: 0x0000BB8C
		public DesignerActionTextItem(string displayName, string category)
			: base(displayName, category, string.Empty)
		{
		}
	}
}
