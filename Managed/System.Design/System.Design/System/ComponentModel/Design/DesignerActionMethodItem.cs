using System;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a smart tag panel item that is associated with a method in a class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</summary>
	// Token: 0x02000115 RID: 277
	public class DesignerActionMethodItem : DesignerActionItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> class, with the specified method and display names.</summary>
		/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the method this item is associated with.</param>
		/// <param name="memberName">The case-sensitive name of the method in the class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to invoke through the panel item.</param>
		/// <param name="displayName">The panel text for this item.</param>
		// Token: 0x0600080D RID: 2061 RVA: 0x0000D839 File Offset: 0x0000BA39
		public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName)
			: this(actionList, memberName, displayName, null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> class, with the specified method and display names, and a flag that indicates whether the item should appear in other user interface contexts.</summary>
		/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the method this item is associated with.</param>
		/// <param name="memberName">The case-sensitive name of the method in the class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to invoke through the panel item.</param>
		/// <param name="displayName">The panel text for this item.</param>
		/// <param name="includeAsDesignerVerb">A flag that specifies whether to also treat the associated method as a designer verb.</param>
		// Token: 0x0600080E RID: 2062 RVA: 0x0000D846 File Offset: 0x0000BA46
		public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, bool includeAsDesignerVerb)
			: this(actionList, memberName, displayName, null, includeAsDesignerVerb)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> class, with the specified method, display, and category names.</summary>
		/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the method this item is associated with.</param>
		/// <param name="memberName">The case-sensitive name of the method in the class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to invoke through the panel item.</param>
		/// <param name="displayName">The panel text for this item.</param>
		/// <param name="category">The case-sensitive <see cref="T:System.String" /> used to group similar items on the panel.</param>
		// Token: 0x0600080F RID: 2063 RVA: 0x0000D854 File Offset: 0x0000BA54
		public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, string category)
			: this(actionList, memberName, displayName, category, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> class, with the specified method, display, and category names, and a flag that indicates whether the item should appear in other user interface contexts.</summary>
		/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the method this item is associated with.</param>
		/// <param name="memberName">The case-sensitive name of the method in the class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to invoke through the panel item.</param>
		/// <param name="displayName">The panel text for this item.</param>
		/// <param name="category">The case-sensitive <see cref="T:System.String" /> used to group similar items on the panel.</param>
		/// <param name="includeAsDesignerVerb">A flag that specifies whether to also treat the associated method as a designer verb for the associated component.</param>
		// Token: 0x06000810 RID: 2064 RVA: 0x0000D862 File Offset: 0x0000BA62
		public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, string category, bool includeAsDesignerVerb)
			: this(actionList, memberName, displayName, category, null, includeAsDesignerVerb)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> class, with the specified method and category names, and display and description text.</summary>
		/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the method this item is associated with.</param>
		/// <param name="memberName">The case-sensitive name of the method in the class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to invoke through the panel item.</param>
		/// <param name="displayName">The panel text for this item.</param>
		/// <param name="category">The case-sensitive <see cref="T:System.String" /> used to group similar items on the panel.</param>
		/// <param name="description">Supplemental text for this item, used in ToolTips or the status bar.</param>
		// Token: 0x06000811 RID: 2065 RVA: 0x0000D872 File Offset: 0x0000BA72
		public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, string category, string description)
			: this(actionList, memberName, displayName, category, description, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> class, with the specified method and category names, display and description text, and a flag that indicates whether the item should appear in other user interface contexts.</summary>
		/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the method this item is associated with.</param>
		/// <param name="memberName">The case-sensitive name of the method in the class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to invoke through the panel item.</param>
		/// <param name="displayName">The panel text for this item.</param>
		/// <param name="category">The case-sensitive <see cref="T:System.String" /> used to group similar items on the panel.</param>
		/// <param name="description">Supplemental text for this item, used in ToolTips or the status bar.</param>
		/// <param name="includeAsDesignerVerb">A flag that specifies whether to also treat the associated method as a designer verb for the associated component.</param>
		// Token: 0x06000812 RID: 2066 RVA: 0x0000D882 File Offset: 0x0000BA82
		public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, string category, string description, bool includeAsDesignerVerb)
			: base(displayName, category, description)
		{
			this.action_list = actionList;
			this.member_name = memberName;
			this.designer_verb = includeAsDesignerVerb;
		}

		/// <summary>Gets a value that indicates the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> should appear in other user interface contexts.</summary>
		/// <returns>true if the item is to be used in shortcut menus; otherwise, false. The default is false.</returns>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0000D8A5 File Offset: 0x0000BAA5
		public virtual bool IncludeAsDesignerVerb
		{
			get
			{
				return this.designer_verb;
			}
		}

		/// <summary>Gets the name of the method that this <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> is associated with.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the associated method.</returns>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0000D8AD File Offset: 0x0000BAAD
		public virtual string MemberName
		{
			get
			{
				return this.member_name;
			}
		}

		/// <summary>Gets or sets a component that contributes its <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> objects to the current panel.</summary>
		/// <returns>The contributing component, which should have an associated designer that supplies items.</returns>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0000D8B5 File Offset: 0x0000BAB5
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x0000D8BD File Offset: 0x0000BABD
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

		/// <summary>Programmatically executes the method associated with the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The method, named in <see cref="P:System.ComponentModel.Design.DesignerActionMethodItem.MemberName" /> cannot be found.</exception>
		// Token: 0x06000817 RID: 2071 RVA: 0x0000234B File Offset: 0x0000054B
		public virtual void Invoke()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040001BA RID: 442
		private string member_name;

		// Token: 0x040001BB RID: 443
		private bool designer_verb;

		// Token: 0x040001BC RID: 444
		private IComponent related_component;

		// Token: 0x040001BD RID: 445
		private DesignerActionList action_list;
	}
}
