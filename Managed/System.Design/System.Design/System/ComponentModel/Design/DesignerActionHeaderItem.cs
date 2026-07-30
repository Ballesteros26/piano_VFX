using System;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a static header item on a smart tag panel. This class cannot be inherited.</summary>
	// Token: 0x0200010D RID: 269
	public sealed class DesignerActionHeaderItem : DesignerActionTextItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionHeaderItem" /> class using the provided name string.</summary>
		/// <param name="displayName">The text to be displayed in the header.</param>
		// Token: 0x060007DA RID: 2010 RVA: 0x0000D603 File Offset: 0x0000B803
		public DesignerActionHeaderItem(string displayName)
			: base(displayName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionHeaderItem" /> class using the provided name and category strings.</summary>
		/// <param name="displayName">The text to be displayed in the header.</param>
		/// <param name="category">The case-sensitive <see cref="T:System.String" /> that defines the groupings of panel entries.</param>
		// Token: 0x060007DB RID: 2011 RVA: 0x0000D60D File Offset: 0x0000B80D
		public DesignerActionHeaderItem(string displayName, string category)
			: base(displayName, category)
		{
		}
	}
}
