using System;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a panel item that is associated with a property in a class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" />. This class cannot be inherited.</summary>
	// Token: 0x02000116 RID: 278
	public sealed class DesignerActionPropertyItem : DesignerActionItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionPropertyItem" /> class, with the specified property and display names.</summary>
		/// <param name="memberName">The case-sensitive name of the property associated with this panel item.</param>
		/// <param name="displayName">The panel text for this item.</param>
		// Token: 0x06000818 RID: 2072 RVA: 0x0000D8C6 File Offset: 0x0000BAC6
		public DesignerActionPropertyItem(string memberName, string displayName)
			: this(memberName, displayName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionPropertyItem" /> class, with the specified property and category names, and display text.</summary>
		/// <param name="memberName">The case-sensitive name of the property associated with this panel item.</param>
		/// <param name="displayName">The panel text for this item.</param>
		/// <param name="category">The case-sensitive <see cref="T:System.String" /> used to group similar items on the panel.</param>
		// Token: 0x06000819 RID: 2073 RVA: 0x0000D8D1 File Offset: 0x0000BAD1
		public DesignerActionPropertyItem(string memberName, string displayName, string category)
			: this(memberName, displayName, category, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionPropertyItem" /> class, with the specified property and category names, and display and description text.</summary>
		/// <param name="memberName">The case-sensitive name of the property associated with this panel item.</param>
		/// <param name="displayName">The panel text for this item.</param>
		/// <param name="category">The case-sensitive <see cref="T:System.String" /> used to group similar items on the panel.</param>
		/// <param name="description">Supplemental text for this item, used in ToolTips or the status bar.</param>
		// Token: 0x0600081A RID: 2074 RVA: 0x0000D8DD File Offset: 0x0000BADD
		public DesignerActionPropertyItem(string memberName, string displayName, string category, string description)
			: base(displayName, category, description)
		{
			this.member_name = memberName;
		}

		/// <summary>Gets the name of the property that this item is associated with.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the associated property.</returns>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
		public string MemberName
		{
			get
			{
				return this.member_name;
			}
		}

		/// <summary>Gets or sets a component that contributes its items to the current panel.</summary>
		/// <returns>The contributing component, which should have an associated designer that supplies <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects.</returns>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x0000D900 File Offset: 0x0000BB00
		public IComponent RelatedComponent
		{
			get
			{
				return this.related_component;
			}
			set
			{
				this.related_component = value;
			}
		}

		// Token: 0x040001BE RID: 446
		private string member_name;

		// Token: 0x040001BF RID: 447
		private IComponent related_component;
	}
}
