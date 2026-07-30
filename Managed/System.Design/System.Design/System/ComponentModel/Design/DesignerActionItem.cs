using System;
using System.Collections;
using Unity;

namespace System.ComponentModel.Design
{
	/// <summary>Provides the base class for types that represent a panel item on a smart tag panel.</summary>
	// Token: 0x0200010E RID: 270
	public abstract class DesignerActionItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> class.</summary>
		/// <param name="displayName">The panel text for this item.</param>
		/// <param name="category">The case-sensitive <see cref="T:System.String" /> that defines the groupings of panel entries.</param>
		/// <param name="description">Supplemental text for this item, potentially used in ToolTips or the status bar.</param>
		// Token: 0x060007DC RID: 2012 RVA: 0x0000D617 File Offset: 0x0000B817
		public DesignerActionItem(string displayName, string category, string description)
		{
			this.display_name = displayName;
			this.description = description;
			this.category = category;
		}

		/// <summary>Gets or sets a value indicating whether to allow this item to be placed into a group of items that have the same <see cref="P:System.ComponentModel.Design.DesignerActionItem.Category" /> property value.</summary>
		/// <returns>true if the item can be grouped; otherwise, false. The default is false.</returns>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0000D634 File Offset: 0x0000B834
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x0000D63C File Offset: 0x0000B83C
		public bool AllowAssociate
		{
			get
			{
				return this.allow_associate;
			}
			set
			{
				this.allow_associate = value;
			}
		}

		/// <summary>Gets the group name for an item.</summary>
		/// <returns>A string that represents the group that the item is a member of. </returns>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0000D645 File Offset: 0x0000B845
		public virtual string Category
		{
			get
			{
				return this.category;
			}
		}

		/// <summary>Gets the supplemental text for the item.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the descriptive text for the item.</returns>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0000D64D File Offset: 0x0000B84D
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		/// <summary>Gets the text for this item.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the display text for the item.</returns>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0000D655 File Offset: 0x0000B855
		public virtual string DisplayName
		{
			get
			{
				return this.display_name;
			}
		}

		/// <summary>Gets a reference to a collection that can be used to store programmer-defined key/value pairs.</summary>
		/// <returns>A collection that implements <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0000D65D File Offset: 0x0000B85D
		public IDictionary Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new Hashtable();
				}
				return this.properties;
			}
		}

		/// <summary>Gets or sets a value that indicates whether this item appears in source code view.</summary>
		/// <returns>true if this item appears in source code view; otherwise, false. The default is true.</returns>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0000D678 File Offset: 0x0000B878
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00009519 File Offset: 0x00007719
		public bool ShowInSourceView
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040001AC RID: 428
		private bool allow_associate;

		// Token: 0x040001AD RID: 429
		private string category;

		// Token: 0x040001AE RID: 430
		private string description;

		// Token: 0x040001AF RID: 431
		private string display_name;

		// Token: 0x040001B0 RID: 432
		private IDictionary properties;
	}
}
